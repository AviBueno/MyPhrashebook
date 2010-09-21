namespace MyFinnishPhrasebookNamespace
{
	partial class CategoryForm
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
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bttAdd = new System.Windows.Forms.Button();
			this.bttCancel = new System.Windows.Forms.Button();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtCategory
			// 
			this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCategory.Location = new System.Drawing.Point( 70, 12 );
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size( 203, 20 );
			this.txtCategory.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 12, 15 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 52, 13 );
			this.label1.TabIndex = 1;
			this.label1.Text = "Category:";
			// 
			// bttAdd
			// 
			this.bttAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bttAdd.Location = new System.Drawing.Point( 117, 68 );
			this.bttAdd.Name = "bttAdd";
			this.bttAdd.Size = new System.Drawing.Size( 75, 23 );
			this.bttAdd.TabIndex = 2;
			this.bttAdd.Text = "Add";
			this.bttAdd.UseVisualStyleBackColor = true;
			this.bttAdd.Click += new System.EventHandler( this.bttAdd_Click );
			// 
			// bttCancel
			// 
			this.bttCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bttCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bttCancel.Location = new System.Drawing.Point( 198, 68 );
			this.bttCancel.Name = "bttCancel";
			this.bttCancel.Size = new System.Drawing.Size( 75, 23 );
			this.bttCancel.TabIndex = 3;
			this.bttCancel.Text = "Cancel";
			this.bttCancel.UseVisualStyleBackColor = true;
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitle.Location = new System.Drawing.Point( 70, 38 );
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size( 203, 20 );
			this.txtTitle.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point( 12, 41 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 30, 13 );
			this.label2.TabIndex = 1;
			this.label2.Text = "Title:";
			// 
			// CategoryForm
			// 
			this.AcceptButton = this.bttAdd;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bttCancel;
			this.ClientSize = new System.Drawing.Size( 285, 101 );
			this.Controls.Add( this.bttCancel );
			this.Controls.Add( this.bttAdd );
			this.Controls.Add( this.label2 );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.txtTitle );
			this.Controls.Add( this.txtCategory );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "CategoryForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CategoryForm";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bttAdd;
		private System.Windows.Forms.Button bttCancel;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label label2;
	}
}