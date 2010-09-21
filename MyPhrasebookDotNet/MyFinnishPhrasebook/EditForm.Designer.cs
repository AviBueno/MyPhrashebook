﻿namespace MyFinnishPhrasebookNamespace
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label finnishLabel;
			System.Windows.Forms.Label englishLabel;
			this.bttOK = new System.Windows.Forms.Button();
			this.bttCancel = new System.Windows.Forms.Button();
			this.myFinnishPhrasebookDBDataSet = new MyFinnishPhrasebookNamespace.MyFinnishPhrasebookDBDataSet();
			this.dBTablePhrasebookBindingSource = new System.Windows.Forms.BindingSource( this.components );
			this.dBTablePhrasebookTableAdapter = new MyFinnishPhrasebookNamespace.MyFinnishPhrasebookDBDataSetTableAdapters.DBTablePhrasebookTableAdapter();
			this.tableAdapterManager = new MyFinnishPhrasebookNamespace.MyFinnishPhrasebookDBDataSetTableAdapters.TableAdapterManager();
			this.textBoxFinnish1 = new MyFinnishPhrasebookNamespace.TextBoxFinnish();
			this.txtEnglish = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			finnishLabel = new System.Windows.Forms.Label();
			englishLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.myFinnishPhrasebookDBDataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dBTablePhrasebookBindingSource)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// finnishLabel
			// 
			finnishLabel.AutoSize = true;
			finnishLabel.Location = new System.Drawing.Point( 15, 11 );
			finnishLabel.Margin = new System.Windows.Forms.Padding( 6, 0, 6, 0 );
			finnishLabel.Name = "finnishLabel";
			finnishLabel.Size = new System.Drawing.Size( 64, 20 );
			finnishLabel.TabIndex = 22;
			finnishLabel.Text = "Finnish:";
			// 
			// englishLabel
			// 
			englishLabel.AutoSize = true;
			englishLabel.Location = new System.Drawing.Point( 15, 87 );
			englishLabel.Margin = new System.Windows.Forms.Padding( 6, 0, 6, 0 );
			englishLabel.Name = "englishLabel";
			englishLabel.Size = new System.Drawing.Size( 65, 20 );
			englishLabel.TabIndex = 21;
			englishLabel.Text = "English:";
			// 
			// bttOK
			// 
			this.bttOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bttOK.Location = new System.Drawing.Point( 144, 211 );
			this.bttOK.Name = "bttOK";
			this.bttOK.Size = new System.Drawing.Size( 75, 27 );
			this.bttOK.TabIndex = 2;
			this.bttOK.Text = "OK";
			this.bttOK.UseVisualStyleBackColor = true;
			this.bttOK.Click += new System.EventHandler( this.bttOK_Click );
			// 
			// bttCancel
			// 
			this.bttCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bttCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bttCancel.Location = new System.Drawing.Point( 225, 211 );
			this.bttCancel.Name = "bttCancel";
			this.bttCancel.Size = new System.Drawing.Size( 75, 27 );
			this.bttCancel.TabIndex = 3;
			this.bttCancel.Text = "Cancel";
			this.bttCancel.UseVisualStyleBackColor = true;
			// 
			// myFinnishPhrasebookDBDataSet
			// 
			this.myFinnishPhrasebookDBDataSet.DataSetName = "MyFinnishPhrasebookDBDataSet";
			this.myFinnishPhrasebookDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dBTablePhrasebookBindingSource
			// 
			this.dBTablePhrasebookBindingSource.DataMember = "DBTablePhrasebook";
			this.dBTablePhrasebookBindingSource.DataSource = this.myFinnishPhrasebookDBDataSet;
			// 
			// dBTablePhrasebookTableAdapter
			// 
			this.dBTablePhrasebookTableAdapter.ClearBeforeFill = true;
			// 
			// tableAdapterManager
			// 
			this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
			this.tableAdapterManager.DBTablePhrasebookTableAdapter = this.dBTablePhrasebookTableAdapter;
			this.tableAdapterManager.UpdateOrder = MyFinnishPhrasebookNamespace.MyFinnishPhrasebookDBDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
			// 
			// textBoxFinnish1
			// 
			this.textBoxFinnish1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFinnish1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.textBoxFinnish1.CurrentSearchOption = MyFinnishPhrasebookNamespace.TextBoxFinnish.SearchOption.Contains;
			this.textBoxFinnish1.Location = new System.Drawing.Point( 91, 9 );
			this.textBoxFinnish1.Margin = new System.Windows.Forms.Padding( 6, 8, 6, 8 );
			this.textBoxFinnish1.Name = "textBoxFinnish1";
			this.textBoxFinnish1.SearchButtonsVisible = false;
			this.textBoxFinnish1.Size = new System.Drawing.Size( 338, 61 );
			this.textBoxFinnish1.TabIndex = 1;
			// 
			// txtEnglish
			// 
			this.txtEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtEnglish.Location = new System.Drawing.Point( 91, 83 );
			this.txtEnglish.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.txtEnglish.Name = "txtEnglish";
			this.txtEnglish.Size = new System.Drawing.Size( 338, 26 );
			this.txtEnglish.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Location = new System.Drawing.Point( 19, 117 );
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size( 410, 88 );
			this.flowLayoutPanel1.TabIndex = 60;
			// 
			// EditForm
			// 
			this.AcceptButton = this.bttOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 9F, 20F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bttCancel;
			this.ClientSize = new System.Drawing.Size( 444, 249 );
			this.Controls.Add( this.flowLayoutPanel1 );
			this.Controls.Add( this.textBoxFinnish1 );
			this.Controls.Add( finnishLabel );
			this.Controls.Add( this.txtEnglish );
			this.Controls.Add( englishLabel );
			this.Controls.Add( this.bttCancel );
			this.Controls.Add( this.bttOK );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
			this.Name = "EditForm";
			this.Text = "Phrase Dialog";
			((System.ComponentModel.ISupportInitialize)(this.myFinnishPhrasebookDBDataSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dBTablePhrasebookBindingSource)).EndInit();
			this.flowLayoutPanel1.ResumeLayout( false );
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bttOK;
		private System.Windows.Forms.Button bttCancel;
		private MyFinnishPhrasebookDBDataSet myFinnishPhrasebookDBDataSet;
		private System.Windows.Forms.BindingSource dBTablePhrasebookBindingSource;
		private MyFinnishPhrasebookNamespace.MyFinnishPhrasebookDBDataSetTableAdapters.DBTablePhrasebookTableAdapter dBTablePhrasebookTableAdapter;
		private MyFinnishPhrasebookNamespace.MyFinnishPhrasebookDBDataSetTableAdapters.TableAdapterManager tableAdapterManager;
		private TextBoxFinnish textBoxFinnish1;
		private System.Windows.Forms.TextBox txtEnglish;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	}
}