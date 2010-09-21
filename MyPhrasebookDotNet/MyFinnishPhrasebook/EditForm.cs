using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyFinnishPhrasebookNamespace
{
	public partial class EditForm : DialogForm
	{
		MPBDataSet.PhrasebookRow m_originalRow = null;
		MPBDataSet.PhrasebookRow m_editedRow = null;

		public EditForm( MPBDataSet.PhrasebookRow row )
		{
			InitializeComponent();

			// Save the original row
			m_originalRow = row;

			// Clone it to a new row
			m_editedRow = DBWrapper.Instance.CreateNewDataRow();
			m_editedRow.ItemArray = m_originalRow.ItemArray;

			// Set data source and binding 
			dBTablePhrasebookBindingSource.DataSource = m_editedRow;
			textBoxFinnish1.TxtBox.DataBindings.Add( new Binding( "Text", dBTablePhrasebookBindingSource, "_language" ) );
			txtEnglish.DataBindings.Add( new Binding( "Text", dBTablePhrasebookBindingSource, "_english" ) );

			foreach ( string categoryName in DBWrapper.Instance.CategoryNamesList )
			{
				DataColumn col = m_editedRow.Table.Columns[ categoryName ];
				if ( col != null )
				{
					CheckBox cb = new CheckBox();
					cb.Text = categoryName;
					cb.DataBindings.Add( new Binding("Checked", dBTablePhrasebookBindingSource, categoryName) );
					cb.AutoSize = true;
					flowLayoutPanel1.Controls.Add( cb );
				}
			}
		}

		private void bttOK_Click( object sender, EventArgs e )
		{
			// Copy data back from edited row to the original one
			// (*) In case the edit was cancelled, the original row will remain unmodified.
			m_originalRow.ItemArray = m_editedRow.ItemArray;
			OnOK();
		}
	}
}
