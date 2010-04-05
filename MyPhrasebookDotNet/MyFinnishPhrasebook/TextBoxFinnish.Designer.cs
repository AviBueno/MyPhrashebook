namespace MyFinnishPhrasebookNamespace
{
	partial class TextBoxFinnish
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.bttA = new System.Windows.Forms.Button();
			this.bttO = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtBox = new MyFinnishPhrasebookNamespace.TextBoxSelectAll();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bttA
			// 
			this.bttA.AutoSize = true;
			this.bttA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bttA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bttA.Location = new System.Drawing.Point( 0, 0 );
			this.bttA.Margin = new System.Windows.Forms.Padding( 0 );
			this.bttA.Name = "bttA";
			this.bttA.Size = new System.Drawing.Size( 23, 22 );
			this.bttA.TabIndex = 2;
			this.bttA.Text = "ä";
			this.bttA.UseVisualStyleBackColor = true;
			// 
			// bttO
			// 
			this.bttO.AutoSize = true;
			this.bttO.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bttO.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bttO.Location = new System.Drawing.Point( 23, 0 );
			this.bttO.Margin = new System.Windows.Forms.Padding( 0 );
			this.bttO.Name = "bttO";
			this.bttO.Size = new System.Drawing.Size( 23, 22 );
			this.bttO.TabIndex = 3;
			this.bttO.Text = "ö";
			this.bttO.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
			this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
			this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
			this.tableLayoutPanel1.Controls.Add( this.bttO, 1, 0 );
			this.tableLayoutPanel1.Controls.Add( this.bttA, 0, 0 );
			this.tableLayoutPanel1.Controls.Add( this.txtBox, 2, 0 );
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
			this.tableLayoutPanel1.Size = new System.Drawing.Size( 198, 22 );
			this.tableLayoutPanel1.TabIndex = 21;
			// 
			// txtBox
			// 
			this.txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBox.Location = new System.Drawing.Point( 46, 0 );
			this.txtBox.Margin = new System.Windows.Forms.Padding( 0 );
			this.txtBox.Name = "txtBox";
			this.txtBox.Size = new System.Drawing.Size( 152, 20 );
			this.txtBox.TabIndex = 1;
			// 
			// TextBoxFinnish
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.tableLayoutPanel1 );
			this.Name = "TextBoxFinnish";
			this.Size = new System.Drawing.Size( 198, 22 );
			this.tableLayoutPanel1.ResumeLayout( false );
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bttA;
		private System.Windows.Forms.Button bttO;
		private TextBoxSelectAll txtBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

	}
}
