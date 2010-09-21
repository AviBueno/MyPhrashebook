namespace MyFinnishPhrasebookNamespace
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label englishLabel;
			System.Windows.Forms.Label finnishLabel;
			System.Windows.Forms.Label searchLabel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
			this.formBindingSource = new System.Windows.Forms.BindingSource( this.components );
			this.dataGridView = new MyFinnishPhrasebookNamespace.MyDataGridView();
			this.timerFilterText = new System.Windows.Forms.Timer( this.components );
			this.txtEnglish = new System.Windows.Forms.TextBox();
			this.txtFinnish = new System.Windows.Forms.TextBox();
			this.bttAddWord = new System.Windows.Forms.Button();
			this.labelNumEntries = new System.Windows.Forms.Label();
			this.timerMessageQueue = new System.Windows.Forms.Timer( this.components );
			this.textBoxDBFilePath = new System.Windows.Forms.TextBox();
			this.txtBoxSearch = new MyFinnishPhrasebookNamespace.TextBoxFinnish();
			this.mainToolbar = new System.Windows.Forms.ToolStrip();
			this.tsBttRandom = new System.Windows.Forms.ToolStripButton();
			this.tsBttQuiz = new System.Windows.Forms.ToolStripButton();
			englishLabel = new System.Windows.Forms.Label();
			finnishLabel = new System.Windows.Forms.Label();
			searchLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.formBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.mainToolbar.SuspendLayout();
			this.SuspendLayout();
			// 
			// englishLabel
			// 
			englishLabel.AutoSize = true;
			englishLabel.Location = new System.Drawing.Point( 12, 73 );
			englishLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
			englishLabel.Name = "englishLabel";
			englishLabel.Size = new System.Drawing.Size( 65, 20 );
			englishLabel.TabIndex = 11;
			englishLabel.Text = "English:";
			// 
			// finnishLabel
			// 
			finnishLabel.AutoSize = true;
			finnishLabel.Location = new System.Drawing.Point( 13, 41 );
			finnishLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
			finnishLabel.Name = "finnishLabel";
			finnishLabel.Size = new System.Drawing.Size( 64, 20 );
			finnishLabel.TabIndex = 13;
			finnishLabel.Text = "Finnish:";
			// 
			// searchLabel
			// 
			searchLabel.AutoSize = true;
			searchLabel.Location = new System.Drawing.Point( 12, 105 );
			searchLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
			searchLabel.Name = "searchLabel";
			searchLabel.Size = new System.Drawing.Size( 64, 20 );
			searchLabel.TabIndex = 13;
			searchLabel.Text = "Search:";
			// 
			// formBindingSource
			// 
			this.formBindingSource.DataMember = "DBTablePhrasebook";
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.AutoGenerateColumns = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.DataSource = this.formBindingSource;
			this.dataGridView.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView.Location = new System.Drawing.Point( 14, 171 );
			this.dataGridView.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size( 428, 307 );
			this.dataGridView.StandardTab = true;
			this.dataGridView.TabIndex = 6;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler( this.dataGridView_CellDoubleClick );
			this.dataGridView.SelectionChanged += new System.EventHandler( this.dataGridView_SelectionChanged );
			// 
			// timerFilterText
			// 
			this.timerFilterText.Interval = 250;
			this.timerFilterText.Tick += new System.EventHandler( this.timerFilterText_Tick );
			// 
			// txtEnglish
			// 
			this.txtEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtEnglish.Location = new System.Drawing.Point( 86, 70 );
			this.txtEnglish.Name = "txtEnglish";
			this.txtEnglish.ReadOnly = true;
			this.txtEnglish.Size = new System.Drawing.Size( 268, 26 );
			this.txtEnglish.TabIndex = 1;
			// 
			// txtFinnish
			// 
			this.txtFinnish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFinnish.Location = new System.Drawing.Point( 86, 38 );
			this.txtFinnish.Name = "txtFinnish";
			this.txtFinnish.ReadOnly = true;
			this.txtFinnish.Size = new System.Drawing.Size( 268, 26 );
			this.txtFinnish.TabIndex = 2;
			// 
			// bttAddWord
			// 
			this.bttAddWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bttAddWord.Location = new System.Drawing.Point( 363, 104 );
			this.bttAddWord.Name = "bttAddWord";
			this.bttAddWord.Size = new System.Drawing.Size( 79, 26 );
			this.bttAddWord.TabIndex = 5;
			this.bttAddWord.Text = "Add...";
			this.bttAddWord.UseVisualStyleBackColor = true;
			this.bttAddWord.Click += new System.EventHandler( this.bttAddWord_Click );
			// 
			// labelNumEntries
			// 
			this.labelNumEntries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelNumEntries.Location = new System.Drawing.Point( 15, 483 );
			this.labelNumEntries.Name = "labelNumEntries";
			this.labelNumEntries.Size = new System.Drawing.Size( 428, 21 );
			this.labelNumEntries.TabIndex = 19;
			this.labelNumEntries.Text = "The database contains #d entries";
			this.labelNumEntries.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// timerMessageQueue
			// 
			this.timerMessageQueue.Interval = 1;
			this.timerMessageQueue.Tick += new System.EventHandler( this.timerMessageQueue_Tick );
			// 
			// textBoxDBFilePath
			// 
			this.textBoxDBFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDBFilePath.Location = new System.Drawing.Point( 15, 506 );
			this.textBoxDBFilePath.Name = "textBoxDBFilePath";
			this.textBoxDBFilePath.ReadOnly = true;
			this.textBoxDBFilePath.Size = new System.Drawing.Size( 428, 26 );
			this.textBoxDBFilePath.TabIndex = 7;
			// 
			// txtBoxSearch
			// 
			this.txtBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.txtBoxSearch.CurrentSearchOption = MyFinnishPhrasebookNamespace.TextBoxFinnish.SearchOption.Contains;
			this.txtBoxSearch.Location = new System.Drawing.Point( 86, 104 );
			this.txtBoxSearch.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.txtBoxSearch.Name = "txtBoxSearch";
			this.txtBoxSearch.Size = new System.Drawing.Size( 268, 55 );
			this.txtBoxSearch.TabIndex = 0;
			// 
			// mainToolbar
			// 
			this.mainToolbar.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tsBttRandom,
            this.tsBttQuiz} );
			this.mainToolbar.Location = new System.Drawing.Point( 0, 0 );
			this.mainToolbar.Name = "mainToolbar";
			this.mainToolbar.ShowItemToolTips = false;
			this.mainToolbar.Size = new System.Drawing.Size( 458, 25 );
			this.mainToolbar.TabIndex = 20;
			this.mainToolbar.Text = "toolStrip1";
			// 
			// tsBttRandom
			// 
			this.tsBttRandom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsBttRandom.Image = ((System.Drawing.Image)(resources.GetObject( "tsBttRandom.Image" )));
			this.tsBttRandom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttRandom.Name = "tsBttRandom";
			this.tsBttRandom.Size = new System.Drawing.Size( 128, 22 );
			this.tsBttRandom.Text = "Random Word/Phrase";
			this.tsBttRandom.ToolTipText = " Random Word";
			this.tsBttRandom.Click += new System.EventHandler( this.tsBttRandom_Click );
			// 
			// tsBttQuiz
			// 
			this.tsBttQuiz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsBttQuiz.Image = ((System.Drawing.Image)(resources.GetObject( "tsBttQuiz.Image" )));
			this.tsBttQuiz.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBttQuiz.Name = "tsBttQuiz";
			this.tsBttQuiz.Size = new System.Drawing.Size( 35, 22 );
			this.tsBttQuiz.Text = "Quiz";
			this.tsBttQuiz.Click += new System.EventHandler( this.tsBttQuiz_Click );
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 9F, 20F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 458, 535 );
			this.Controls.Add( this.mainToolbar );
			this.Controls.Add( this.txtBoxSearch );
			this.Controls.Add( this.textBoxDBFilePath );
			this.Controls.Add( this.labelNumEntries );
			this.Controls.Add( this.bttAddWord );
			this.Controls.Add( this.txtFinnish );
			this.Controls.Add( this.txtEnglish );
			this.Controls.Add( englishLabel );
			this.Controls.Add( searchLabel );
			this.Controls.Add( finnishLabel );
			this.Controls.Add( this.dataGridView );
			this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "My Finnish Phrasebook";
			this.Load += new System.EventHandler( this.MainForm_Load );
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.MainForm_FormClosing );
			((System.ComponentModel.ISupportInitialize)(this.formBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.mainToolbar.ResumeLayout( false );
			this.mainToolbar.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.BindingSource formBindingSource;
		private MyDataGridView dataGridView;
		private System.Windows.Forms.Timer timerFilterText;
		private System.Windows.Forms.TextBox txtEnglish;
		private System.Windows.Forms.TextBox txtFinnish;
		private System.Windows.Forms.Button bttAddWord;
		private System.Windows.Forms.Label labelNumEntries;
		private System.Windows.Forms.Timer timerMessageQueue;
		private System.Windows.Forms.TextBox textBoxDBFilePath;
		private TextBoxFinnish txtBoxSearch;
		private System.Windows.Forms.ToolStrip mainToolbar;
		private System.Windows.Forms.ToolStripButton tsBttRandom;
		private System.Windows.Forms.ToolStripButton tsBttQuiz;
	}
}