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
			this.HideSelection = false;
		}

		#region Event Handlers
		bool m_FirstClickAfterEnter;
		void TextBoxSelectAll_Enter( object sender, EventArgs e )
		{
			m_FirstClickAfterEnter = true;
			this.SelectAll();
		}

		void TextBoxSelectAll_Click( object sender, EventArgs e )
		{
			if ( m_FirstClickAfterEnter )
			{
				m_FirstClickAfterEnter = false;
				this.SelectAll();
			}
		}

		void TextBoxSelectAll_Leave( object sender, EventArgs e )
		{
			this.DeselectAll();
		}
		#endregion
	}
}
