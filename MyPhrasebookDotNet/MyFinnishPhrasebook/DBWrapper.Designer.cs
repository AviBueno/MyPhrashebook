namespace MyFinnishPhrasebookNamespace
{
	partial class DBWrapper
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
			this.MPBDataSet1 = new MyFinnishPhrasebookNamespace.MPBDataSet();
			this.PhrasebookTableAdapter1 = new MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.PhrasebookTableAdapter();
			this.tableAdapterManager1 = new MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.TableAdapterManager();
			this.categoriesTableAdapter = new MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.CategoriesTableAdapter();
			this.cat2PhraseTableAdapter = new MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.Cat2PhraseTableAdapter();
			((System.ComponentModel.ISupportInitialize)(this.MPBDataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// MPBDataSet1
			// 
			this.MPBDataSet1.DataSetName = "MPBDataSet";
			this.MPBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// PhrasebookTableAdapter1
			// 
			this.PhrasebookTableAdapter1.ClearBeforeFill = true;
			// 
			// tableAdapterManager1
			// 
			this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
			this.tableAdapterManager1.Cat2PhraseTableAdapter = null;
			this.tableAdapterManager1.CategoriesTableAdapter = null;
			this.tableAdapterManager1.PhrasebookTableAdapter = this.PhrasebookTableAdapter1;
			this.tableAdapterManager1.UpdateOrder = MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
			// 
			// categoriesTableAdapter
			// 
			this.categoriesTableAdapter.ClearBeforeFill = true;
			// 
			// cat2PhraseTableAdapter
			// 
			this.cat2PhraseTableAdapter.ClearBeforeFill = true;
			// 
			// DBWrapper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "DBWrapper";
			this.Size = new System.Drawing.Size( 138, 150 );
			((System.ComponentModel.ISupportInitialize)(this.MPBDataSet1)).EndInit();
			this.ResumeLayout( false );

		}

		#endregion

		private MPBDataSet MPBDataSet1;
		private MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.PhrasebookTableAdapter PhrasebookTableAdapter1;
		private MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
		private MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.CategoriesTableAdapter categoriesTableAdapter;
		private MyFinnishPhrasebookNamespace.MPBDataSetTableAdapters.Cat2PhraseTableAdapter cat2PhraseTableAdapter;
	}
}
