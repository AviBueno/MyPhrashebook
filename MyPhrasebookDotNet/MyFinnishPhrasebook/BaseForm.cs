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
		static Font formFont;

		static BaseForm()
		{
			formFont = new Font( FontFamily.GenericSansSerif, 12.0f );
		}

		public BaseForm()
		{
			this.Font = formFont;
		}
	}
}
