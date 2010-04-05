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
		public EditForm( string sEnglishText, string sFinnishText )
		{
			InitializeComponent();

			txtEnglish.Text = sEnglishText;
			textBoxFinnish1.TxtBox.Text = sFinnishText;			
		}

		public string EnglishText { get { return txtEnglish.Text; } }
		public string FinnishText { get { return textBoxFinnish1.TxtBox.Text; } }

		private void bttOK_Click( object sender, EventArgs e )
		{
			System.Diagnostics.Trace.WriteLine( "bttOK_Click" );
			DialogResult = DialogResult.OK;
		}
	}
}
