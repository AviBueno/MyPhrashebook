using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyFinnishPhrasebookNamespace
{
	public class DialogForm : BaseForm
	{
		public DialogForm()
		{
			InitializeComponent();
		}

		public void OnOK()
		{
			DialogResult = DialogResult.OK;
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// DialogForm
			// 
			this.ClientSize = new System.Drawing.Size( 284, 262 );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DialogForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout( false );

		}
	}
}
