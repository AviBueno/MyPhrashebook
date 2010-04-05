using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MyFinnishPhrasebookNamespace
{
	public partial class DBWrapper : UserControl
	{
		#region Singleton
		private DBWrapper()
		{
			InitializeComponent();
			PopulateDatabase();
		}

		static DBWrapper m_Instance;
		public static DBWrapper Instance
		{
			get
			{
				if ( m_Instance == null )
				{
					m_Instance = new DBWrapper();
				}
				return m_Instance;
			}
		}
		#endregion

		public MyPhrasebookDataSet MyDataSet
		{
			get { return this.MyPhrasebookDataSet1; }
		}

		public MyPhrasebookDataSet.DBTablePhrasebookDataTable MyDataTable
		{
			get { return MyDataSet.DBTablePhrasebook; }
		}

		public MyPhrasebookDataSetTableAdapters.DBTablePhrasebookTableAdapter MyTableAdapter
		{
			get { return this.DBTablePhrasebookTableAdapter1; }
		}

		public bool InsertRow( string txtEnglish, string txtFinnish )
		{
			bool bAdded = false;
			if ( txtEnglish.Length > 0 && txtFinnish.Length > 0 )
			{
				MyTableAdapter.Insert( txtFinnish, txtEnglish );
				CommitChanges();
				bAdded = true;
			}

			return bAdded;
		}

		public void DeleteRowAt( MyPhrasebookDataSet.DBTablePhrasebookRow dataRow )
		{
			dataRow.Delete();
			CommitChanges();
		}

		public void CommitChanges()
		{
			MyTableAdapter.Update( MyDataSet );
		}

		public IEnumerable FilterRows( string filterQuery )
		{
			return MyDataTable.Select( filterQuery );
		}

		public void PopulateDatabase()
		{
			MyTableAdapter.Fill( MyDataTable );
		}
	}
}
