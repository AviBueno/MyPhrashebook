using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace MyFinnishPhrasebookNamespace
{
	public partial class MainForm : BaseForm
	{
		int nColIdxEnglish = -1;
		int nColIdxFinnish = -1;
		Keys m_RefreshKey = Keys.F5;

		private TextBox SearchTextBox { get { return txtBoxSearch.TxtBox; } }

		#region Form Init
		public MainForm()
		{
			InitializeComponent();
			InitDatabaseDirectory();
			SearchTextBox.TextChanged += new System.EventHandler( this.SearchTextBox_TextChanged );
			txtBoxSearch.OnSearchOptionChanged += new EventHandler( OnFilterData );

			this.Text += string.Format( " ({0})", Application.ProductVersion );

			InitCategoriesDropDown();
		}

		private void InitCategoriesDropDown()
		{
			foreach ( string categoryId in DBWrapper.Instance.FilterFieldNameToQueryString.Keys )
			{
				string sFilterText = DBWrapper.Instance.FilterFieldNameToQueryString[ categoryId ];
				AddCategoryDropMenu( tsDDCategory, categoryId, sFilterText );
			}
		}

		private void AddCategoryDropMenu( ToolStripDropDownButton ddButton, string categoryText, string sFilterText )
		{
			ToolStripDropDownItem tsddi = new ToolStripMenuItem();
			tsddi.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsddi.Text = categoryText;
			tsddi.Tag = sFilterText;
			tsddi.Click += new EventHandler( OnCategoryItemClick );
			ddButton.DropDownItems.Add( tsddi );
		}

		string m_CategoryFilter = "_catID = 0";
		void OnCategoryItemClick( object sender, EventArgs e )
		{
			string sCategoryText = "All";
			string sCategoryFilter = "";

			ToolStripDropDownItem tsddi = sender as ToolStripDropDownItem;
			if ( tsddi != null )
			{
				sCategoryFilter = tsddi.Tag.ToString();
				if ( sCategoryFilter != m_CategoryFilter )
				{
					m_CategoryFilter = sCategoryFilter;
					sCategoryText = tsddi.Text;
					OnFilterData( this, null );
				}
			}

			// Update the drop down button's text
			tsDDCategory.Text = sCategoryText;
		}

		/// <summary>
		/// Determine where to read the database from and init the DataDirectory variable accordingly.
		/// </summary>
		private void InitDatabaseDirectory()
		{
			// The DB connectionstring contains |DataDirectory| which is retrieved at runtime from AppDomain.
			// The following method will load the database from the current directory if one exists there,
			// and if not - will fallback to the DefaultDBDir value from the application's settings.
			// This is done in order to work on an external directory's database during development time,
			// and reading one from current dir on an end-user's machine.
			string dataDir = string.Empty;

			if ( System.IO.File.Exists( Path.Combine( Environment.CurrentDirectory, DBWrapper.DatabaseFileName ) ) )
			{
				// Exists in current dir (may be different than .exe dir if pointed by a symbolic-link for example)
				dataDir = Environment.CurrentDirectory;
			}			
			else if ( System.IO.File.Exists( Path.Combine( Application.StartupPath, DBWrapper.DatabaseFileName ) ) )
			{
				// Exists in .exe dir
				dataDir = Application.StartupPath;
			}
			else
			{
				// This fallback is mainly for debugging purposes
				dataDir = System.IO.Path.GetFullPath( Properties.Settings.Default.DefaultDBDir );
				if ( ! System.IO.File.Exists( Path.Combine( dataDir, DBWrapper.DatabaseFileName ) ) )
				{
				}
			}

			AppDomain.CurrentDomain.SetData( "DataDirectory", dataDir );

			// Update database path name text box
			string sTxt = Properties.Settings.Default.MPBConnectionString;

			string sDS = "Data Source=";
			int nIdx = sTxt.IndexOf( sDS );
			nIdx += sDS.Length;

			textBoxDBFilePath.Text = sTxt.Substring( nIdx ).Replace( "|DataDirectory|", AppDomain.CurrentDomain.GetData( "DataDirectory" ) as string );
		}

		private void MainForm_Load( object sender, EventArgs e )
		{
			HookGlobalKeyboardKeys();

			ReadDatabase();
			SelectRandomWord(); //OnFilterData( this, EventArgs.Empty );

			dataGridView.OnEditRow += new EventHandler( this.OnEditRow );
			dataGridView.OnDeleteRow += new EventHandler( this.OnDeleteRow );

			Application.Idle += new EventHandler( MainForm_Loaded );
		}

		private void MainForm_Loaded( object sender, EventArgs args )
		{
			Application.Idle -= new EventHandler( MainForm_Loaded );

			// TODO: add relevant code here
			if ( Program.StartInQuizMode )
			{
				LaunchQuizDialog();
			}
		}

		private void LaunchQuizDialog()
		{
			new QuizForm().ShowDialog();
		}
		#endregion

		#region Keyboard Events Processing
		Utilities.globalKeyboardHook gkh = new Utilities.globalKeyboardHook();
		void HookGlobalKeyboardKeys()
		{
			gkh.HookedKeys.Add( m_RefreshKey );
			gkh.HookedKeys.Add( Keys.Enter );
			gkh.KeyDown += new KeyEventHandler( gkh_KeyDown );
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
					SelectRandomWord();
				}
			}
		}
		#endregion

		private void ReadDatabase()
		{
			DBWrapper.Instance.PopulateDatabase();
			UpdateDataGridView();
		}

		void PutFocusOnSearchText()
		{
			SearchTextBox.Focus();
			txtBoxSearch.Focus();
		}

		void UpdateDataGridView()
		{
			OnFilterData( this, EventArgs.Empty );
			PutFocusOnSearchText();
			SearchTextBox.SelectAll();
		}

		#region Event Handlers
		private void bttSearch_Click( object sender, EventArgs e )
		{
			OnFilterData( this, EventArgs.Empty );
		}

		private void OnFilterData( object sender, EventArgs e )
		{
			string filterQuery = "";

			string sSearchText = SearchTextBox.Text.Trim();
			if ( !string.IsNullOrEmpty( sSearchText ) )
			{
				sSearchText = sSearchText.Replace( "'", "''" );	// Overcome ending apostrophe issue
				filterQuery = GetSearchQueryFilter( sSearchText );
			}

/*
			if ( string.IsNullOrEmpty( filterQuery ) )
			{
				filterQuery = m_CategoryFilter;
			}
			else
			{
				// Append category filter to the query
				filterQuery = string.Format( "({0}) AND {1}", filterQuery, m_CategoryFilter );
			}
*/

			dataGridView.DataSource = DBWrapper.Instance.FilterRows( filterQuery );

			labelNumEntries.Text = string.Format( "{0} / {1}",
															dataGridView.Rows.Count,
															DBWrapper.Instance.PhrasebookDataTable.Rows.Count );

			PutFocusOnSearchText();
		}

		private string GetSearchQueryFilter( string sSearchText )
		{
			string sFilterQuery = string.Empty;
			switch ( txtBoxSearch.CurrentSearchOption )
			{
				case TextBoxFinnish.SearchOption.Exact:
					sFilterQuery = string.Format( Properties.Resources.QFExactMatch, sSearchText );
					break;

				case TextBoxFinnish.SearchOption.Contains:
					sFilterQuery = string.Format( Properties.Resources.QFContains, sSearchText );
					break;

				case TextBoxFinnish.SearchOption.StartsWith:
					sFilterQuery = string.Format( Properties.Resources.QFStartsWith, sSearchText );
					break;

				case TextBoxFinnish.SearchOption.EndsWith:
					sFilterQuery = string.Format( Properties.Resources.QFEndsWith, sSearchText );
					break;
			}

			return sFilterQuery;
		}

		private void SearchTextBox_TextChanged( object sender, EventArgs e )
		{
			timerFilterText.Stop();
			timerFilterText.Start();
		}

		private void timerFilterText_Tick( object sender, EventArgs e )
		{
			timerFilterText.Stop();
			//if ( !string.IsNullOrEmpty( txtBoxSearch.TxtBox.Text.Trim() ) )
			{
				OnFilterData( this, EventArgs.Empty );
			}
		}

		private void SelectRandomWord()
		{
			SearchTextBox.Text = "";
			OnFilterData( this, EventArgs.Empty );

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
					if ( col.DataPropertyName == "_english" )
					{
						nColIdxEnglish = col.Index;
					}
					else if ( col.DataPropertyName == "_language" )
					{
						nColIdxFinnish = col.Index;
					}
				}
			}

			BindToRow( txtEnglish, nColIdxEnglish, nRowIdx );
			BindToRow( txtFinnish, nColIdxFinnish, nRowIdx );
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
			MPBDataSet.PhrasebookRow row = DBWrapper.Instance.CreateNewDataRow();
			row._language = row._english = SearchTextBox.Text;
			DBWrapper.RowWithCategoryInfo rwci = new DBWrapper.RowWithCategoryInfo( row );
			EditForm form = new EditForm( rwci );
			if ( form.ShowDialog() == DialogResult.OK )
			{
				bool bAdded = DBWrapper.Instance.InsertRow( rwci );
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
				DataGridViewRow selectedDataRow = dataGridView.SelectedRows[ 0 ];
				MPBDataSet.PhrasebookRow selectedRow = selectedDataRow.DataBoundItem as MPBDataSet.PhrasebookRow;
				MPBDataSet.PhrasebookRow editedRow = selectedRow;
				DBWrapper.RowWithCategoryInfo rwci = new DBWrapper.RowWithCategoryInfo( editedRow );
				EditForm form = new EditForm( rwci );
				if ( form.ShowDialog() == DialogResult.OK )
				{
					selectedRow = rwci.Row;
					DBWrapper.Instance.UpdateRow( rwci );
					if ( SearchTextBox.Text != string.Empty )
					{
						UpdateDataGridView();
					}
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
					OnFilterData( this, EventArgs.Empty );
				}
			}
		}

		private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			dataGridView.SaveColumnsOrderPersistence();
		}
		#endregion

		private void tsBttRandom_Click( object sender, EventArgs e )
		{
			SelectRandomWord();
		}

		private void tsBttQuiz_Click( object sender, EventArgs e )
		{
			LaunchQuizDialog();
		}

		private void addCategoryToolStripMenuItem_Click( object sender, EventArgs e )
		{
			new CategoryForm().ShowDialog(this);
		}
	}
}
