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
			this.MyPhrasebookDataSet1 = new MyFinnishPhrasebookNamespace.MyPhrasebookDataSet();
			this.DBTablePhrasebookTableAdapter1 = new MyFinnishPhrasebookNamespace.MyPhrasebookDataSetTableAdapters.DBTablePhrasebookTableAdapter();
			this.tableAdapterManager1 = new MyFinnishPhrasebookNamespace.MyPhrasebookDataSetTableAdapters.TableAdapterManager();
			((System.ComponentModel.ISupportInitialize)(this.MyPhrasebookDataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// MyPhrasebookDataSet1
			// 
			this.MyPhrasebookDataSet1.DataSetName = "MyPhrasebookDataSet";
			this.MyPhrasebookDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// DBTablePhrasebookTableAdapter1
			// 
			this.DBTablePhrasebookTableAdapter1.ClearBeforeFill = true;
			// 
			// tableAdapterManager1
			// 
			this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
			this.tableAdapterManager1.DBTablePhrasebookTableAdapter = this.DBTablePhrasebookTableAdapter1;
			this.tableAdapterManager1.UpdateOrder = MyFinnishPhrasebookNamespace.MyPhrasebookDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
			// 
			// DatabaseWrapper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "DatabaseWrapper";
			((System.ComponentModel.ISupportInitialize)(this.MyPhrasebookDataSet1)).EndInit();
			this.ResumeLayout( false );

		}

		#endregion

		private MyPhrasebookDataSet MyPhrasebookDataSet1;
		private MyFinnishPhrasebookNamespace.MyPhrasebookDataSetTableAdapters.DBTablePhrasebookTableAdapter DBTablePhrasebookTableAdapter1;
		private MyFinnishPhrasebookNamespace.MyPhrasebookDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
	}
}
