namespace MyFinnishPhrasebookNamespace
{
	partial class QuizForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( QuizForm ) );
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.labelQuestion = new System.Windows.Forms.Label();
			this.bttDone = new System.Windows.Forms.Button();
			this.quizToolbar = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.ddBttCategory = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsCatAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.ddBttLanguage = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsLangFinnish = new System.Windows.Forms.ToolStripMenuItem();
			this.tsLangEnglish = new System.Windows.Forms.ToolStripMenuItem();
			this.tsLangBoth = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.quizToolbar.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
			this.tableLayoutPanel1.Controls.Add( this.button1, 0, 0 );
			this.tableLayoutPanel1.Controls.Add( this.button2, 0, 1 );
			this.tableLayoutPanel1.Controls.Add( this.button3, 0, 2 );
			this.tableLayoutPanel1.Controls.Add( this.button4, 0, 3 );
			this.tableLayoutPanel1.Location = new System.Drawing.Point( 12, 96 );
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
			this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
			this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
			this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
			this.tableLayoutPanel1.Size = new System.Drawing.Size( 370, 152 );
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.Location = new System.Drawing.Point( 3, 3 );
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size( 364, 32 );
			this.button1.TabIndex = 1;
			this.button1.Tag = "1";
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button2.Location = new System.Drawing.Point( 3, 41 );
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size( 364, 32 );
			this.button2.TabIndex = 2;
			this.button2.Tag = "2";
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button3.Location = new System.Drawing.Point( 3, 79 );
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size( 364, 32 );
			this.button3.TabIndex = 3;
			this.button3.Tag = "3";
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button4.Location = new System.Drawing.Point( 3, 117 );
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size( 364, 32 );
			this.button4.TabIndex = 4;
			this.button4.Tag = "4";
			this.button4.Text = "button4";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// labelQuestion
			// 
			this.labelQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelQuestion.Location = new System.Drawing.Point( 12, 43 );
			this.labelQuestion.Name = "labelQuestion";
			this.labelQuestion.Size = new System.Drawing.Size( 370, 29 );
			this.labelQuestion.TabIndex = 0;
			this.labelQuestion.Text = "Question";
			this.labelQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelQuestion.Click += new System.EventHandler( this.labelQuestion_Click );
			// 
			// bttDone
			// 
			this.bttDone.Location = new System.Drawing.Point( 288, 266 );
			this.bttDone.Name = "bttDone";
			this.bttDone.Size = new System.Drawing.Size( 91, 32 );
			this.bttDone.TabIndex = 0;
			this.bttDone.Text = "Done";
			this.bttDone.UseVisualStyleBackColor = true;
			this.bttDone.Click += new System.EventHandler( this.bttDone_Click );
			// 
			// quizToolbar
			// 
			this.quizToolbar.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ddBttCategory,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.ddBttLanguage} );
			this.quizToolbar.Location = new System.Drawing.Point( 0, 0 );
			this.quizToolbar.Name = "quizToolbar";
			this.quizToolbar.Size = new System.Drawing.Size( 394, 25 );
			this.quizToolbar.TabIndex = 2;
			this.quizToolbar.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size( 58, 22 );
			this.toolStripLabel1.Text = "Category:";
			// 
			// ddBttCategory
			// 
			this.ddBttCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ddBttCategory.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tsCatAll,
            this.toolStripSeparator1} );
			this.ddBttCategory.Name = "ddBttCategory";
			this.ddBttCategory.Size = new System.Drawing.Size( 68, 22 );
			this.ddBttCategory.Text = "Category";
			// 
			// tsCatAll
			// 
			this.tsCatAll.Name = "tsCatAll";
			this.tsCatAll.Size = new System.Drawing.Size( 152, 22 );
			this.tsCatAll.Tag = "All";
			this.tsCatAll.Text = "All";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size( 149, 6 );
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size( 6, 25 );
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size( 62, 22 );
			this.toolStripLabel2.Text = "Language:";
			// 
			// ddBttLanguage
			// 
			this.ddBttLanguage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ddBttLanguage.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tsLangFinnish,
            this.tsLangEnglish,
            this.tsLangBoth} );
			this.ddBttLanguage.Image = ((System.Drawing.Image)(resources.GetObject( "ddBttLanguage.Image" )));
			this.ddBttLanguage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ddBttLanguage.Name = "ddBttLanguage";
			this.ddBttLanguage.Size = new System.Drawing.Size( 72, 22 );
			this.ddBttLanguage.Text = "Language";
			// 
			// tsLangFinnish
			// 
			this.tsLangFinnish.Name = "tsLangFinnish";
			this.tsLangFinnish.Size = new System.Drawing.Size( 112, 22 );
			this.tsLangFinnish.Tag = "Finnish";
			this.tsLangFinnish.Text = "Finnish";
			// 
			// tsLangEnglish
			// 
			this.tsLangEnglish.Name = "tsLangEnglish";
			this.tsLangEnglish.Size = new System.Drawing.Size( 112, 22 );
			this.tsLangEnglish.Tag = "English";
			this.tsLangEnglish.Text = "English";
			// 
			// tsLangBoth
			// 
			this.tsLangBoth.Name = "tsLangBoth";
			this.tsLangBoth.Size = new System.Drawing.Size( 112, 22 );
			this.tsLangBoth.Tag = "Both";
			this.tsLangBoth.Text = "Both";
			// 
			// QuizForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 9F, 20F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 394, 316 );
			this.Controls.Add( this.quizToolbar );
			this.Controls.Add( this.bttDone );
			this.Controls.Add( this.labelQuestion );
			this.Controls.Add( this.tableLayoutPanel1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "QuizForm";
			this.Text = "QuizForm";
			this.Click += new System.EventHandler( this.QuizForm_Click );
			this.tableLayoutPanel1.ResumeLayout( false );
			this.quizToolbar.ResumeLayout( false );
			this.quizToolbar.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label labelQuestion;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button bttDone;
		private System.Windows.Forms.ToolStrip quizToolbar;
		private System.Windows.Forms.ToolStripDropDownButton ddBttCategory;
		private System.Windows.Forms.ToolStripMenuItem tsCatAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripDropDownButton ddBttLanguage;
		private System.Windows.Forms.ToolStripMenuItem tsLangFinnish;
		private System.Windows.Forms.ToolStripMenuItem tsLangEnglish;
		private System.Windows.Forms.ToolStripMenuItem tsLangBoth;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
	}
}