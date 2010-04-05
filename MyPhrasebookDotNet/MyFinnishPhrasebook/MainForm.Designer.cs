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
			this.formBindingSource = new System.Windows.Forms.BindingSource( this.components );
			this.dataGridView = new MyFinnishPhrasebookNamespace.MyDataGridView();
			this.timerFilterText = new System.Windows.Forms.Timer( this.components );
			this.bttRandom = new System.Windows.Forms.Button();
			this.txtEnglish = new System.Windows.Forms.TextBox();
			this.txtFinnish = new System.Windows.Forms.TextBox();
			this.bttO = new System.Windows.Forms.Button();
			this.bttA = new System.Windows.Forms.Button();
			this.bttAddWord = new System.Windows.Forms.Button();
			this.txtSearch = new MyFinnishPhrasebookNamespace.TextBoxSelectAll();
			this.labelNumEntries = new System.Windows.Forms.Label();
			this.timerMessageQueue = new System.Windows.Forms.Timer( this.components );
			englishLabel = new System.Windows.Forms.Label();
			finnishLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.formBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// englishLabel
			// 
			englishLabel.AutoSize = true;
			englishLabel.Location = new System.Drawing.Point( 13, 17 );
			englishLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
			englishLabel.Name = "englishLabel";
			englishLabel.Size = new System.Drawing.Size( 65, 20 );
			englishLabel.TabIndex = 11;
			englishLabel.Text = "English:";
			// 
			// finnishLabel
			// 
			finnishLabel.AutoSize = true;
			finnishLabel.Location = new System.Drawing.Point( 14, 53 );
			finnishLabel.Margin = new System.Windows.Forms.Padding( 4, 0, 4, 0 );
			finnishLabel.Name = "finnishLabel";
			finnishLabel.Size = new System.Drawing.Size( 64, 20 );
			finnishLabel.TabIndex = 13;
			finnishLabel.Text = "Finnish:";
			// 
			// formBindingSource
			// 
			this.formBindingSource.DataMember = "DBTablePhrasebook";
			// 
			// dataGridView
			// 
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
			this.dataGridView.Location = new System.Drawing.Point( 15, 129 );
			this.dataGridView.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size( 708, 421 );
			this.dataGridView.StandardTab = true;
			this.dataGridView.TabIndex = 10;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler( this.dataGridView_CellDoubleClick );
			this.dataGridView.SelectionChanged += new System.EventHandler( this.dataGridView_SelectionChanged );
			// 
			// timerFilterText
			// 
			this.timerFilterText.Interval = 250;
			this.timerFilterText.Tick += new System.EventHandler( this.timer1_Tick );
			// 
			// bttRandom
			// 
			this.bttRandom.Location = new System.Drawing.Point( 641, 12 );
			this.bttRandom.Name = "bttRandom";
			this.bttRandom.Size = new System.Drawing.Size( 82, 67 );
			this.bttRandom.TabIndex = 15;
			this.bttRandom.Text = "Random";
			this.bttRandom.UseVisualStyleBackColor = true;
			this.bttRandom.Click += new System.EventHandler( this.bttRandom_Click );
			// 
			// txtEnglish
			// 
			this.txtEnglish.Location = new System.Drawing.Point( 87, 14 );
			this.txtEnglish.Name = "txtEnglish";
			this.txtEnglish.ReadOnly = true;
			this.txtEnglish.Size = new System.Drawing.Size( 548, 26 );
			this.txtEnglish.TabIndex = 16;
			// 
			// txtFinnish
			// 
			this.txtFinnish.Location = new System.Drawing.Point( 87, 50 );
			this.txtFinnish.Name = "txtFinnish";
			this.txtFinnish.ReadOnly = true;
			this.txtFinnish.Size = new System.Drawing.Size( 548, 26 );
			this.txtFinnish.TabIndex = 16;
			// 
			// bttO
			// 
			this.bttO.Location = new System.Drawing.Point( 52, 89 );
			this.bttO.Name = "bttO";
			this.bttO.Size = new System.Drawing.Size( 31, 29 );
			this.bttO.TabIndex = 17;
			this.bttO.Text = "ö";
			this.bttO.UseVisualStyleBackColor = true;
			this.bttO.Click += new System.EventHandler( this.bttO_Click );
			// 
			// bttA
			// 
			this.bttA.Location = new System.Drawing.Point( 15, 89 );
			this.bttA.Name = "bttA";
			this.bttA.Size = new System.Drawing.Size( 31, 29 );
			this.bttA.TabIndex = 17;
			this.bttA.Text = "ä";
			this.bttA.UseVisualStyleBackColor = true;
			this.bttA.Click += new System.EventHandler( this.bttA_Click );
			// 
			// bttAddWord
			// 
			this.bttAddWord.Location = new System.Drawing.Point( 644, 90 );
			this.bttAddWord.Name = "bttAddWord";
			this.bttAddWord.Size = new System.Drawing.Size( 79, 26 );
			this.bttAddWord.TabIndex = 18;
			this.bttAddWord.Text = "Add...";
			this.bttAddWord.UseVisualStyleBackColor = true;
			this.bttAddWord.Click += new System.EventHandler( this.bttAddWord_Click );
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Location = new System.Drawing.Point( 87, 90 );
			this.txtSearch.Margin = new System.Windows.Forms.Padding( 6, 8, 6, 8 );
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size( 548, 26 );
			this.txtSearch.TabIndex = 8;
			this.txtSearch.TextChanged += new System.EventHandler( this.txtSearch_TextChanged );
			// 
			// labelNumEntries
			// 
			this.labelNumEntries.Location = new System.Drawing.Point( 15, 555 );
			this.labelNumEntries.Name = "labelNumEntries";
			this.labelNumEntries.Size = new System.Drawing.Size( 708, 21 );
			this.labelNumEntries.TabIndex = 19;
			this.labelNumEntries.Text = "The database contains #d entries";
			this.labelNumEntries.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// timerMessageQueue
			// 
			this.timerMessageQueue.Interval = 1;
			this.timerMessageQueue.Tick += new System.EventHandler( this.timerMessageQueue_Tick );
			// 
			// CombinedForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 9F, 20F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 738, 594 );
			this.Controls.Add( this.labelNumEntries );
			this.Controls.Add( this.bttAddWord );
			this.Controls.Add( this.bttA );
			this.Controls.Add( this.bttO );
			this.Controls.Add( this.txtFinnish );
			this.Controls.Add( this.txtEnglish );
			this.Controls.Add( this.bttRandom );
			this.Controls.Add( englishLabel );
			this.Controls.Add( finnishLabel );
			this.Controls.Add( this.dataGridView );
			this.Controls.Add( this.txtSearch );
			this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.Name = "CombinedForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CombinedForm";
			this.Load += new System.EventHandler( this.CombinedForm_Load );
			((System.ComponentModel.ISupportInitialize)(this.formBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private TextBoxSelectAll txtSearch;
		private System.Windows.Forms.BindingSource formBindingSource;
		private MyDataGridView dataGridView;
		private System.Windows.Forms.Timer timerFilterText;
		private System.Windows.Forms.Button bttRandom;
		private System.Windows.Forms.TextBox txtEnglish;
		private System.Windows.Forms.TextBox txtFinnish;
		private System.Windows.Forms.Button bttO;
		private System.Windows.Forms.Button bttA;
		private System.Windows.Forms.Button bttAddWord;
		private System.Windows.Forms.Label labelNumEntries;
		private System.Windows.Forms.Timer timerMessageQueue;
	}
}