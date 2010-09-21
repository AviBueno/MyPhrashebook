using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace MyFinnishPhrasebookNamespace
{
	public partial class DBWrapper : UserControl
	{
		Dictionary<string, string> m_FilterFieldNameToQueryString = new Dictionary<string, string>();
		public Dictionary<string, string> FilterFieldNameToQueryString { get { return m_FilterFieldNameToQueryString; } }

		#region Singleton
		private DBWrapper()
		{
			InitializeComponent();
			PopulateDatabase();

			SetDefaultColumnValues();

			Type boolType = typeof( bool );
			Type rowType = typeof( MPBDataSet.PhrasebookRow );
			foreach ( PropertyInfo pi in rowType.GetProperties() )
			{
				if ( pi.CanWrite && pi.PropertyType == boolType )
				{
					m_FilterFieldNameToQueryString[ pi.Name ] = string.Format( "{0} <> 0", pi.Name );
				}
			}

			m_FilterFieldNameToQueryString[ "Word" ] = "Phrase = 0";
		}

		private void SetDefaultColumnValues()
		{
			foreach ( DataColumn col in MyDataTable.Columns )
			{
				if ( !col.AutoIncrement )
				{
					Type type = col.DataType;
					object defaultValue = null;
					if ( type.IsValueType )
					{
						defaultValue = Activator.CreateInstance( type );
					}

					col.DefaultValue = defaultValue;
				}
			}
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

		public MPBDataSet MyDataSet
		{
			get { return this.MPBDataSet1; }
		}

		public MPBDataSet.PhrasebookDataTable MyDataTable
		{
			get { return MyDataSet.Phrasebook; }
		}

		public MPBDataSetTableAdapters.PhrasebookTableAdapter MyTableAdapter
		{
			get { return this.PhrasebookTableAdapter1; }
		}

		public MPBDataSet.PhrasebookRow CreateNewDataRow()
		{
			MPBDataSet.PhrasebookRow row = MyDataTable.NewPhrasebookRow();

			// Set defaults
			foreach ( DataColumn col in row.Table.Columns )
			{
				if ( !col.AutoIncrement)
				{
					row[ col ] = col.DefaultValue;
				}
			}

			return row;
		}

		public bool InsertRow( MPBDataSet.PhrasebookRow dataRow )
		{
			MyDataTable.AddPhrasebookRow( dataRow );
			CommitChanges();
			return true;
		}

		public void DeleteRowAt( MPBDataSet.PhrasebookRow dataRow )
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

		List<string> m_CategoryNamesList;
		public List<string> CategoryNamesList
		{
			get
			{
				if ( m_CategoryNamesList == null )
				{
					m_CategoryNamesList = DBWrapper.Instance.FilterFieldNameToQueryString.Keys.ToList();
					m_CategoryNamesList.Sort();
				}

				return m_CategoryNamesList;
			}
		}

		public void PopulateDatabase()
		{
			MyTableAdapter.Fill( MyDataTable );
		}
	}
}
