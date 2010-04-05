using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MyFinnishPhrasebookNamespace
{
	public partial class MainForm : BaseForm
	{
		int nColIdxEnglish = -1;
		int nColIdxFinnish = -1;
		string m_sMDBFilePath;
		Keys m_RefreshKey = Keys.F5;

		#region Form Init
		public MainForm()
		{
			InitializeComponent();

			string sTxt = Properties.Settings.Default.MyPhrasebookDB;

			string sDS = "Data Source=";
			int nIdx = sTxt.IndexOf( sDS );
			nIdx += sDS.Length;

			m_sMDBFilePath = sTxt.Substring( nIdx );
		}
		
		private void CombinedForm_Load( object sender, EventArgs e )
		{
			HookGlobalKeyboardKeys();

			ReadDatabase();
			OnFilterData();

			dataGridView.OnEditRow += new EventHandler( this.OnEditRow );
			dataGridView.OnDeleteRow += new EventHandler( this.OnDeleteRow );
		}
		#endregion

		#region Keyboard Events Processing
		Utilities.globalKeyboardHook gkh = new Utilities.globalKeyboardHook();
		void HookGlobalKeyboardKeys()
		{
			gkh.HookedKeys.Add( m_RefreshKey );
			gkh.HookedKeys.Add( Keys.Enter );
			gkh.KeyDown += new KeyEventHandler( gkh_KeyDown );
			//gkh.KeyUp += new KeyEventHandler( gkh_KeyUp );
		}

		/// <summary>
		/// NOTE: The operating system doesn't allow lengthy operations on hooked procedures.
		/// Therefore we only record the pressed key here and start a 1 msec timer that will
		/// process the key when it expires (i.e. now)
		/// (*) We use a timer (intead of a different thread) in order to remain on the main UI thread.
		/// </summary>
		Queue<Keys> KeyDownQueue = new Queue<Keys>();
		void gkh_KeyDown( object sender, KeyEventArgs e )
		{
			if ( Form.ActiveForm == this && gkh.HookedKeys.Contains( e.KeyCode ) )
			{
				lock ( KeyDownQueue )
				{
					KeyDownQueue.Enqueue( e.KeyCode );
					e.Handled = true;
					timerMessageQueue.Enabled = true;
				}
			}
		}

		private void timerMessageQueue_Tick( object sender, EventArgs e )
		{
			while ( true )
			{
				Keys key;

				lock ( KeyDownQueue )
				{
					int nMessages = KeyDownQueue.Count;
					if ( nMessages > 0 )
					{
						key = KeyDownQueue.Dequeue();
					}
					else
					{
						timerMessageQueue.Enabled = false;	// Disable the timer
						break;	// No more messages to dequeue --> End Processing
					}
				}

				if ( key == Keys.Enter )
				{
					if ( dataGridView.SelectedRow != null )
					{
						OnEditRow( this, null );
					}
					else
					{
						OnAddWord();
					}
				}
				else if ( key == m_RefreshKey )
				{
					bttRandom_Click( null, null );
				}
			}
		}
		#endregion

		private void ReadDatabase()
		{
			DBWrapper.Instance.PopulateDatabase();
			UpdateDataGridView();
		}

		void UpdateDataGridView()
		{
			OnFilterData();
			txtSearch.Focus();
			txtSearch.SelectAll();
		}

		#region Event Handlers
		private void bttSearch_Click( object sender, EventArgs e )
		{
			OnFilterData();
		}

		private void OnFilterData()
		{
			string filterQuery = "";

			string sSearchText = txtSearch.Text.Trim();
			if ( !string.IsNullOrEmpty( sSearchText ) )
			{
				sSearchText = sSearchText.Replace( "'", "''" );	// Overcome ending apostrophe issue
				filterQuery = string.Format( "(English like '%{0}%') OR (Finnish like '%{0}%')", sSearchText );
			}

			dataGridView.DataSource = DBWrapper.Instance.FilterRows( filterQuery );

			labelNumEntries.Text = string.Format( "{0} | {1} / {2}",
															m_sMDBFilePath,
															dataGridView.Rows.Count,
															DBWrapper.Instance.MyDataTable.Rows.Count );
		}

		private void txtSearch_TextChanged( object sender, EventArgs e )
		{
			timerFilterText.Stop();
			timerFilterText.Start();
		}

		private void timer1_Tick( object sender, EventArgs e )
		{
			timerFilterText.Stop();
			OnFilterData();
		}

		private void bttRandom_Click( object sender, EventArgs e )
		{
			txtSearch.Text = "";
			OnFilterData();

			int nRows = dataGridView.Rows.Count;
			if ( nRows > 0 )
			{
				dataGridView.ClearSelection();
				int nRowIdx = new Random().Next( nRows );
				dataGridView.CurrentCell = dataGridView[ nColIdxFinnish, nRowIdx ];
			}
		}

		private void BindToRow( TextBox tb, int nColIdx, int nRowIdx )
		{
			if ( tb.DataBindings.Count > 0 )
				tb.DataBindings.RemoveAt( 0 );

			tb.DataBindings.Add( new Binding( "Text", dataGridView[ nColIdx, nRowIdx ], "Value", false ) );
		}

		private void dataGridView_SelectionChanged( object sender, EventArgs e )
		{
			if ( dataGridView.SelectedRows.Count <= 0 )
			{
				return;
			}

			int nRowIdx = dataGridView.SelectedRows[ 0 ].Index;

			if ( nColIdxEnglish == -1 || nColIdxFinnish == -1 )
			{
				foreach ( DataGridViewColumn col in dataGridView.Columns )
				{
					if ( col.DataPropertyName == "English" )
					{
						nColIdxEnglish = col.Index;
					}
					else if ( col.DataPropertyName == "Finnish" )
					{
						nColIdxFinnish = col.Index;
					}
				}
			}

			BindToRow( txtEnglish, nColIdxEnglish, nRowIdx );
			BindToRow( txtFinnish, nColIdxFinnish, nRowIdx );
		}

		private void AddSearchText( string text )
		{
			int i = txtSearch.SelectionStart;
			txtSearch.SelectedText = text;
			txtSearch.Focus();
			txtSearch.SelectionStart = i + 1;
			txtSearch.SelectionLength = 0;
		}

		private void bttO_Click( object sender, EventArgs e )
		{
			AddSearchText( bttO.Text );
		}

		private void bttA_Click( object sender, EventArgs e )
		{
			AddSearchText( bttA.Text );
		}

		private void bttAddWord_Click( object sender, EventArgs e )
		{
			OnAddWord();
		}

		private void dataGridView_CellDoubleClick( object sender, DataGridViewCellEventArgs e )
		{
			OnEditRow( sender, e );
		}

		private void OnAddWord()
		{
			EditForm form = new EditForm( txtSearch.Text, txtSearch.Text );
			if ( form.ShowDialog() == DialogResult.OK
					&& form.FinnishText.Length > 0 && form.EnglishText.Length > 0 )
			{
				bool bAdded = DBWrapper.Instance.InsertRow( form.EnglishText, form.FinnishText );
				if ( bAdded )
				{
					dataGridView.DataSource = null;
					ReadDatabase();
				}
			}
		}

		private void OnEditRow( object sender, EventArgs paramArgs )
		{
			if ( dataGridView.SelectedRows.Count > 0 )
			{
				DataGridViewRow selectedRow = dataGridView.SelectedRows[ 0 ];
				string sEnglishText = selectedRow.Cells[ dataGridView.ColIdxEnglish ].Value.ToString();
				string sFinnishText = selectedRow.Cells[ dataGridView.ColIdxFinnish ].Value.ToString();
				EditForm form = new EditForm( sEnglishText, sFinnishText );
				if ( form.ShowDialog() == DialogResult.OK )
				{
					selectedRow.Cells[ dataGridView.ColIdxEnglish ].Value = form.EnglishText;
					selectedRow.Cells[ dataGridView.ColIdxFinnish ].Value = form.FinnishText;
					DBWrapper.Instance.CommitChanges();
					UpdateDataGridView();
				}
			}
		}

		private void OnDeleteRow( object sender, EventArgs paramArgs )
		{
			if ( dataGridView.SelectedRows.Count > 0 )
			{
				DataGridViewRow row = dataGridView.SelectedRows[ 0 ];
				DialogResult res = MessageBox.Show( string.Format( 
														"Are you sure you want to delete\n{0} | {1}"
															, row.Cells[ dataGridView.ColIdxEnglish ].Value.ToString()
															, row.Cells[ dataGridView.ColIdxFinnish ].Value.ToString()
															),
															"Delete Values?",
															MessageBoxButtons.YesNo															
												);
				if ( res == DialogResult.Yes )
				{
					dataGridView.DeleteRow( row );
					OnFilterData();
				}
			}
		}
		#endregion
	}
}
