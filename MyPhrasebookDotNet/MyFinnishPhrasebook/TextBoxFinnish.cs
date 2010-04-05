using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyFinnishPhrasebookNamespace
{
	public partial class TextBoxFinnish : UserControl
	{
		public TextBoxFinnish()
		{
			InitializeComponent();
			bttA.Click += new EventHandler( langBttClick );
			bttO.Click += new EventHandler( langBttClick );
		}

		public TextBoxSelectAll TxtBox { get { return txtBox; } }

		private void langBttClick( object sender, EventArgs e )
		{
			Button btt = sender as Button;
			if ( btt != null )
			{
				AddText( btt.Text );
			}
		}

		private void AddText( string text )
		{
			int i = txtBox.SelectionStart;
			txtBox.SelectedText = text;
			txtBox.Focus();
			txtBox.SelectionStart = i + 1;
			txtBox.SelectionLength = 0;
		}
	}
}
