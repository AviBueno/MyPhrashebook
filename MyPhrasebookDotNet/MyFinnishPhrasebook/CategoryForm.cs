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
	public partial class CategoryForm : Form
	{
		public CategoryForm()
		{
			InitializeComponent();
		}

		private void bttAdd_Click( object sender, EventArgs e )
		{
			if ( string.IsNullOrEmpty( txtCategory.Text ) )
			{
				MessageBox.Show( "Please enter a category name" );
				return;
			}

			if ( string.IsNullOrEmpty( txtTitle.Text ) )
			{
				MessageBox.Show( "Please enter title text" );
				return;
			}

			bool bAdded = DBWrapper.Instance.AddCategory( txtCategory.Text, txtTitle.Text );
			if ( !bAdded )
			{
				MessageBox.Show( "Failed to add category" );
				return;
			}

			this.DialogResult = DialogResult.OK;
		}
	}
}
