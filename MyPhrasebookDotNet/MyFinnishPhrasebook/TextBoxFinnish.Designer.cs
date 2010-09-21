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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( TextBoxFinnish ) );
			this.toolStrip1 = new MyFinnishPhrasebookNamespace.OneColorBackgroundToolStrip();
			this.tsBttA = new System.Windows.Forms.ToolStripButton();
			this.tsBttO = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsBttContains = new System.Windows.Forms.ToolStripButton();
			this.tsBttExactMatch = new System.Windows.Forms.ToolStripButton();
			this.tsBttStartsWith = new System.Windows.Forms.ToolStripButton();
			this.tsBttEndsWith = new System.Windows.Forms.ToolStripButton();
			this.txtBox = new MyFinnishPhrasebookNamespace.TextBoxSelectAll();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Font = new System.Drawing.Font( "Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tsBttA,
            this.tsBttO,
            this.toolStripSeparator1,
            this.tsBttContains,
            this.tsBttExactMatch,
            this.tsBttStartsWith,
            this.tsBttEndsWith} );
			this.toolStrip1.Location = new System.Drawing.Point( 0, 19 );
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size( 147, 28 );
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsBttA
			// 
			this.tsBttA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsBttA.Image = ((System.Drawing.Image)(resources.GetObject( "tsBttA.Image" )));
			this.tsBttA.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttA.Name = "tsBttA";
			this.tsBttA.Size = new System.Drawing.Size( 23, 25 );
			this.tsBttA.Text = "ä";
			// 
			// tsBttO
			// 
			this.tsBttO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsBttO.Image = ((System.Drawing.Image)(resources.GetObject( "tsBttO.Image" )));
			this.tsBttO.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttO.Name = "tsBttO";
			this.tsBttO.Size = new System.Drawing.Size( 23, 25 );
			this.tsBttO.Text = "ö";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size( 6, 28 );
			// 
			// tsBttContains
			// 
			this.tsBttContains.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBttContains.Image = global::MyFinnishPhrasebookNamespace.Properties.Resources.Contains;
			this.tsBttContains.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttContains.Name = "tsBttContains";
			this.tsBttContains.Size = new System.Drawing.Size( 23, 25 );
			this.tsBttContains.Text = "Contains";
			// 
			// tsBttExactMatch
			// 
			this.tsBttExactMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBttExactMatch.Image = global::MyFinnishPhrasebookNamespace.Properties.Resources.ExactMatch;
			this.tsBttExactMatch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttExactMatch.Name = "tsBttExactMatch";
			this.tsBttExactMatch.Size = new System.Drawing.Size( 23, 25 );
			this.tsBttExactMatch.Text = "Exact Match";
			// 
			// tsBttStartsWith
			// 
			this.tsBttStartsWith.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBttStartsWith.Image = global::MyFinnishPhrasebookNamespace.Properties.Resources.StartsWith;
			this.tsBttStartsWith.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttStartsWith.Name = "tsBttStartsWith";
			this.tsBttStartsWith.Size = new System.Drawing.Size( 23, 25 );
			this.tsBttStartsWith.Text = "Starts With";
			// 
			// tsBttEndsWith
			// 
			this.tsBttEndsWith.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBttEndsWith.Image = global::MyFinnishPhrasebookNamespace.Properties.Resources.EndsWith;
			this.tsBttEndsWith.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttEndsWith.Name = "tsBttEndsWith";
			this.tsBttEndsWith.Size = new System.Drawing.Size( 23, 25 );
			this.tsBttEndsWith.Text = "Ends With";
			// 
			// txtBox
			// 
			this.txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBox.HideSelection = false;
			this.txtBox.Location = new System.Drawing.Point( 0, 0 );
			this.txtBox.Margin = new System.Windows.Forms.Padding( 0 );
			this.txtBox.Name = "txtBox";
			this.txtBox.Size = new System.Drawing.Size( 255, 20 );
			this.txtBox.TabIndex = 0;
			// 
			// TextBoxFinnish
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add( this.toolStrip1 );
			this.Controls.Add( this.txtBox );
			this.Name = "TextBoxFinnish";
			this.Size = new System.Drawing.Size( 255, 48 );
			this.toolStrip1.ResumeLayout( false );
			this.toolStrip1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private TextBoxSelectAll txtBox;
		private OneColorBackgroundToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsBttA;
		private System.Windows.Forms.ToolStripButton tsBttO;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsBttContains;
		private System.Windows.Forms.ToolStripButton tsBttExactMatch;
		private System.Windows.Forms.ToolStripButton tsBttStartsWith;
		private System.Windows.Forms.ToolStripButton tsBttEndsWith;

	}
}
