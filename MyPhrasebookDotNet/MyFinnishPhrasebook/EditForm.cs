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
		DBWrapper.RowWithCategoryInfo m_originalRWCI = null;
		DBWrapper.RowWithCategoryInfo m_editedRWCI = null;

		public EditForm( DBWrapper.RowWithCategoryInfo rwci )
		{
			InitializeComponent();

			// Save the original row
			m_originalRWCI = rwci;

			// Clone it to a new row
			MPBDataSet.PhrasebookRow newRow = DBWrapper.Instance.CreateNewDataRow();
			m_editedRWCI = new DBWrapper.RowWithCategoryInfo( newRow );
			m_editedRWCI.Row.ItemArray = m_originalRWCI.Row.ItemArray;


			// Set data source and binding 
			phrasebookBindingSource.DataSource = m_editedRWCI.Row;
			textBoxFinnish1.TxtBox.DataBindings.Add( new Binding( "Text", phrasebookBindingSource, "_language" ) );
			txtEnglish.DataBindings.Add( new Binding( "Text", phrasebookBindingSource, "_english" ) );

			foreach ( KeyValuePair<string, long> category in DBWrapper.Instance.CategoriesMap )
			{
				if ( category.Key == "All" )
				{
					continue;
				}

				CheckBox cb = new CheckBox();
				cb.Text = category.Key;

				if ( rwci.CatID2CheckboxMap.ContainsKey( category.Value ) )
				{
					cb.Checked = true;
				}

				cb.AutoSize = true;
				flowLayoutPanel1.Controls.Add( cb );
			}
		}

		private void bttOK_Click( object sender, EventArgs e )
		{
			m_originalRWCI.CatID2CheckboxMap.Clear();
			foreach ( Control ctrl in flowLayoutPanel1.Controls )
			{
				CheckBox cb = ctrl as CheckBox;
				if ( cb != null && cb.Checked )
				{
					long catId = DBWrapper.Instance.CategoriesMap[ cb.Text ];
					m_originalRWCI.CatID2CheckboxMap[ catId ] = true;
				}
			}

			// Copy data back from edited row to the original one
			// (*) In case the edit was cancelled, the original row will remain unmodified.
			m_originalRWCI.Row.ItemArray = m_editedRWCI.Row.ItemArray;

			OnOK();
		}
	}
}
