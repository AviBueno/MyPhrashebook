namespace MyFinnishPhrasebookNamespace
{
	partial class EditForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Label englishLabel;
			System.Windows.Forms.Label finnishLabel;
			this.txtFinnish = new System.Windows.Forms.TextBox();
			this.txtEnglish = new System.Windows.Forms.TextBox();
			this.bttOK = new System.Windows.Forms.Button();
			this.bttCancel = new System.Windows.Forms.Button();
			this.textBoxFinnish1 = new MyFinnishPhrasebookNamespace.TextBoxFinnish();
			englishLabel = new System.Windows.Forms.Label();
			finnishLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// englishLabel
			// 
			englishLabel.AutoSize = true;
			englishLabel.Location = new System.Drawing.Point( 15, 23 );
			englishLabel.Margin = new System.Windows.Forms.Padding( 6, 0, 6, 0 );
			englishLabel.Name = "englishLabel";
			englishLabel.Size = new System.Drawing.Size( 65, 20 );
			englishLabel.TabIndex = 21;
			englishLabel.Text = "English:";
			// 
			// finnishLabel
			// 
			finnishLabel.AutoSize = true;
			finnishLabel.Location = new System.Drawing.Point( 16, 58 );
			finnishLabel.Margin = new System.Windows.Forms.Padding( 6, 0, 6, 0 );
			finnishLabel.Name = "finnishLabel";
			finnishLabel.Size = new System.Drawing.Size( 64, 20 );
			finnishLabel.TabIndex = 22;
			finnishLabel.Text = "Finnish:";
			// 
			// txtFinnish
			// 
			this.txtFinnish.Location = new System.Drawing.Point( 411, 56 );
			this.txtFinnish.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.txtFinnish.Name = "txtFinnish";
			this.txtFinnish.Size = new System.Drawing.Size( 51, 26 );
			this.txtFinnish.TabIndex = 2;
			// 
			// txtEnglish
			// 
			this.txtEnglish.Location = new System.Drawing.Point( 90, 20 );
			this.txtEnglish.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.txtEnglish.Name = "txtEnglish";
			this.txtEnglish.Size = new System.Drawing.Size( 372, 26 );
			this.txtEnglish.TabIndex = 1;
			// 
			// bttOK
			// 
			this.bttOK.Location = new System.Drawing.Point( 160, 90 );
			this.bttOK.Name = "bttOK";
			this.bttOK.Size = new System.Drawing.Size( 75, 27 );
			this.bttOK.TabIndex = 3;
			this.bttOK.Text = "OK";
			this.bttOK.UseVisualStyleBackColor = true;
			this.bttOK.Click += new System.EventHandler( this.bttOK_Click );
			// 
			// bttCancel
			// 
			this.bttCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bttCancel.Location = new System.Drawing.Point( 241, 90 );
			this.bttCancel.Name = "bttCancel";
			this.bttCancel.Size = new System.Drawing.Size( 75, 27 );
			this.bttCancel.TabIndex = 4;
			this.bttCancel.Text = "Cancel";
			this.bttCancel.UseVisualStyleBackColor = true;
			// 
			// textBoxFinnish1
			// 
			this.textBoxFinnish1.Location = new System.Drawing.Point( 88, 54 );
			this.textBoxFinnish1.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.textBoxFinnish1.Name = "textBoxFinnish1";
			this.textBoxFinnish1.Size = new System.Drawing.Size( 313, 29 );
			this.textBoxFinnish1.TabIndex = 2;
			// 
			// EditForm
			// 
			this.AcceptButton = this.bttOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 9F, 20F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bttCancel;
			this.ClientSize = new System.Drawing.Size( 477, 131 );
			this.Controls.Add( this.textBoxFinnish1 );
			this.Controls.Add( this.bttCancel );
			this.Controls.Add( this.bttOK );
			this.Controls.Add( this.txtFinnish );
			this.Controls.Add( this.txtEnglish );
			this.Controls.Add( englishLabel );
			this.Controls.Add( finnishLabel );
			this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.Name = "EditForm";
			this.Text = "!!FORM TITLE!!";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtFinnish;
		private System.Windows.Forms.TextBox txtEnglish;
		private System.Windows.Forms.Button bttOK;
		private System.Windows.Forms.Button bttCancel;
		private TextBoxFinnish textBoxFinnish1;
	}
}