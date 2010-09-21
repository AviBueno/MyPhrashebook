using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MyFinnishPhrasebookNamespace
{
	public class BaseForm : Form
	{
		static Font m_Font;

		static BaseForm()
		{
			m_Font = new Font( FontFamily.GenericSansSerif, 12.0f );
		}

		public BaseForm()
		{
			this.Font = m_Font;
			this.Icon = Properties.Resources.AppIcon;
		}
	}
}
