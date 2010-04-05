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
		public int ColIdxEnglish { get; private set; }
		public int ColIdxFinnish { get; private set; }

		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
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
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();

			this.Columns.AddRange(
				new System.Windows.Forms.DataGridViewColumn[] {
					this.dataGridViewTextBoxColumn1,
					this.dataGridViewTextBoxColumn2,
					this.dataGridViewTextBoxColumn3
				}
			);

			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "ID";
			this.dataGridViewTextBoxColumn1.HeaderText = "ID";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Visible = false;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "English";
			this.dataGridViewTextBoxColumn2.HeaderText = "English";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			ColIdxEnglish = this.dataGridViewTextBoxColumn2.Index;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "Finnish";
			this.dataGridViewTextBoxColumn3.HeaderText = "Finnish";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			ColIdxFinnish = this.dataGridViewTextBoxColumn3.Index;

			//this.DataSource = DBWrapper.Instance.MyDataTable;
		}

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
			MyPhrasebookDataSet.DBTablePhrasebookRow dataRow = row.DataBoundItem as MyPhrasebookDataSet.DBTablePhrasebookRow;

			// NOTE: If we don't set the data source to null right now, a reference to the soon-to-be-removed row
			// will remain inside the view, and after the row is deleted, this reference to the already-removed
			// row will cause an exception to be thrown.
			this.DataSource = null;

			DBWrapper.Instance.DeleteRowAt( dataRow );			
		}
	}
}
