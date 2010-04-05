using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyFinnishPhrasebookNamespace
{
	public class TextBoxSelectAll : TextBox
	{
		public TextBoxSelectAll()
		{
			this.Enter += new EventHandler( TextBoxSelectAll_Enter );
			this.Click += new EventHandler( TextBoxSelectAll_Click );
			this.Leave += new EventHandler( TextBoxSelectAll_Leave );
		}

		void TextBoxSelectAll_Enter( object sender, EventArgs e )
		{
			TextBox tb = sender as TextBox;
			tb.SelectAll();
			tb.HideSelection = false;
		}

		void TextBoxSelectAll_Click( object sender, EventArgs e )
		{
			TextBox tb = sender as TextBox;
			if ( tb.SelectionLength > 0 )
			{
				tb.SelectAll();
			}
		}

		void TextBoxSelectAll_Leave( object sender, EventArgs e )
		{
 			TextBox tb = sender as TextBox;
			if ( !tb.HideSelection )
			{
				tb.HideSelection = true;
			}
		}
	}
}
