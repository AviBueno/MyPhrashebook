using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MyFinnishPhrasebookNamespace
{
	class MyDataGridView : DataGridView
	{
		public int ColIdxEnglish { get { return this.dgvCol_English.Index; } }
		public int ColIdxFinnish { get { return this.dgvCol_Finnish.Index; } }

		private System.Windows.Forms.DataGridViewTextBoxColumn dgvCol_ID;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvCol_English;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvCol_Finnish;
		private ContextMenu contextMenu = new ContextMenu();

		public event EventHandler OnEditRow;
		public event EventHandler OnDeleteRow;

		public MyDataGridView()
		{
			InitializeComponent();
			InitDataBinding();

			contextMenu.MenuItems.Add( "Edit", InternalOnEditRow );
			contextMenu.MenuItems.Add( "Delete", InternalOnDeleteRow );
		}

		public DataGridViewRow SelectedRow
		{
			get
			{
				if ( SelectedRows.Count > 0 )
				{
					return SelectedRows[ 0 ];
				}

				return null;
			}
		}

		private void InternalOnEditRow( object sender, EventArgs e )
		{
			if ( OnEditRow != null )
			{
				OnEditRow( sender, e );
			}
		}

		private void InternalOnDeleteRow( object sender, EventArgs e )
		{
			if ( OnDeleteRow != null )
			{
				OnDeleteRow( sender, e );
			}
		}

		void InitDataBinding()
		{
			this.dgvCol_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvCol_English = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvCol_Finnish = new System.Windows.Forms.DataGridViewTextBoxColumn();

			this.Columns.AddRange(
				new System.Windows.Forms.DataGridViewColumn[] {
					this.dgvCol_ID,
					this.dgvCol_Finnish,
					this.dgvCol_English
				}
			);

			// 
			// dgvCol_ID
			// 
			this.dgvCol_ID.DataPropertyName = "_id";
			this.dgvCol_ID.HeaderText = "ID";
			this.dgvCol_ID.Name = "dgvCol_ID";
			this.dgvCol_ID.ReadOnly = true;
			this.dgvCol_ID.Visible = false;
			// 
			// dgvCol_Finnish
			// 
			this.dgvCol_Finnish.DataPropertyName = "_language";
			this.dgvCol_Finnish.HeaderText = "Finnish";
			this.dgvCol_Finnish.Name = "dgvCol_Finnish";
			this.dgvCol_Finnish.ReadOnly = true;
			// 
			// dgvCol_English
			// 
			this.dgvCol_English.DataPropertyName = "_english";
			this.dgvCol_English.HeaderText = "English";
			this.dgvCol_English.Name = "dgvCol_English";
			this.dgvCol_English.ReadOnly = true;

			LoadColumnsOrderPersistence();
		}

		#region ColumnsOrderPersistence
		const char persistDelimiter = ';';
		private void LoadColumnsOrderPersistence()
		{
			string sColsOrder = Properties.Settings.Default.ColumnsOrderPersistence;
			if ( sColsOrder.Length > 0 )
			{
				string[] sColIdxPairs = sColsOrder.Split( new char[] { persistDelimiter } );
				foreach ( string sColIdxPair in sColIdxPairs )
				{
					string[] sValues = sColIdxPair.Split( new char[] { '=' } );
					this.Columns[ sValues[ 0 ] ].DisplayIndex = int.Parse( sValues[ 1 ] );
				}
			}
		}
		
		public void SaveColumnsOrderPersistence()
		{
			string sColsOrder = string.Empty;
			bool bFirstCol = true;
			foreach ( DataGridViewColumn col in this.Columns )
			{
				sColsOrder += string.Format( "{0}{1}={2}", bFirstCol ? "" : persistDelimiter.ToString(), col.Name, col.DisplayIndex );
				bFirstCol = false;
			}

			Properties.Settings.Default.ColumnsOrderPersistence = sColsOrder;
			Properties.Settings.Default.Save();
		}
		#endregion

		private void FilterData( string sFilterText )
		{
			string filterQuery = "";

			if ( !string.IsNullOrEmpty( sFilterText ) )
			{
				filterQuery = string.Format( "(English like '%{0}%') OR (Finnish like '%{0}%')", sFilterText );
			}

			this.DataSource = DBWrapper.Instance.FilterRows( filterQuery );
		}

		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// MyDataGridView
			// 
			this.AllowUserToOrderColumns = true;
			this.MouseUp += new System.Windows.Forms.MouseEventHandler( this.MyDataGridView_MouseUp );
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout( false );

		}

		// Context menus
		// http://www.c-sharpcorner.com/UploadFile/nipuntomar/contextmenuforgridview01162007124516PM/contextmenuforgridview.aspx
		private void MyDataGridView_MouseUp( object sender, MouseEventArgs e )
		{
			// Load context menu on right mouse click
			if ( e.Button == MouseButtons.Right )
			{
				DataGridView.HitTestInfo hitTestInfo = this.HitTest( e.X, e.Y );
				DataGridViewRow row = this.Rows[ hitTestInfo.RowIndex ];
				if ( row != null )
				{
					row.Selected = true;
					contextMenu.Show( this, new Point( e.X, e.Y ) );
				}
			}
		}

		public void DeleteRow( DataGridViewRow row )
		{
			// Get the data table row that is associated with this view's row
			MPBDataSet.PhrasebookRow dataRow = row.DataBoundItem as MPBDataSet.PhrasebookRow;

			// NOTE: If we don't set the data source to null right now, a reference to the soon-to-be-removed row
			// will remain inside the view, and after the row is deleted, this reference to the already-removed
			// row will cause an exception to be thrown.
			this.DataSource = null;

			DBWrapper.Instance.DeleteRowAt( dataRow );			
		}
	}
}
