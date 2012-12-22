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

		public static string DatabaseFileName { get { return "mpb.db"; } }

		#region Singleton
		private DBWrapper()
		{
			InitializeComponent();
			PopulateDatabase();

			InitIdColumn( MyDataSet.Phrasebook );
			InitIdColumn( MyDataSet.Cat2Phrase );
			InitIdColumn( MyDataSet.Categories );

			SetDefaultColumnValues();

			ReadAndMapCategories();

/*
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
*/
		}

		void InitIdColumn( DataTable dt )
		{
			int nIdColIdx = dt.Columns.IndexOf( "_id" );
			DataColumn _id = dt.Columns[ nIdColIdx ];
			_id.AllowDBNull = false;
			_id.Unique = true;
			_id.AutoIncrement = true;
			_id.AutoIncrementSeed = -1;
			_id.AutoIncrementStep = -1;
			dt.PrimaryKey = new DataColumn[] { _id };
		}

		private void ReadAndMapCategories()
		{
			m_FilterFieldNameToQueryString.Clear();

			foreach ( MPBDataSet.CategoriesRow catRow in MyDataSet.Categories )
			{
				// Compose the query that will be execute on the Cat2Phrase table
				// in order to select lines of a certain category
				m_FilterFieldNameToQueryString[ catRow._name ] = string.Format( "_catID = {0}", catRow._id );

				if ( catRow._name == "Phrase" )
				{
					// Artificial query for single words.
					m_FilterFieldNameToQueryString[ "Word" ] = string.Format( "_catID <> {0}", catRow._id );
				}
			}

			// Reset the map such that it will be re-read the next time it's needed.
			m_CategoriesMap = null;
		}

		private void SetDefaultColumnValues()
		{
			foreach ( DataColumn col in PhrasebookDataTable.Columns )
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

		public MPBDataSet.PhrasebookDataTable PhrasebookDataTable
		{
			get { return MyDataSet.Phrasebook; }
		}

		public MPBDataSet.Cat2PhraseDataTable Cat2PhraseDataTable
		{
			get { return MyDataSet.Cat2Phrase; }
		}

		public MPBDataSet.PhrasebookRow CreateNewDataRow()
		{
			MPBDataSet.PhrasebookRow row = PhrasebookDataTable.NewPhrasebookRow();

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

		public bool InsertRow( RowWithCategoryInfo rwci )
		{
			bool updated = false;
			try
			{
//				MPBDataSet.PhrasebookRow newRow = PhrasebookDataTable.AddPhrasebookRow( rwci.Row._english, rwci.Row._language );
				string _english = rwci.Row._english;
				string _language = rwci.Row._language;

				rwci.Row._id = this.phrasebookTableAdapter.Insert( rwci.Row._english, rwci.Row._language );
				this.phrasebookTableAdapter.Update( MyDataSet );
				CommitChanges();

//				newRow = PhrasebookDataTable.Select( string.Format( "_english LIKE '{0}' AND _language LIKE '{1}'", _english, _language ) )[ 0 ] as MPBDataSet.PhrasebookRow;
//				RowWithCategoryInfo newRWCI = new RowWithCategoryInfo( newRow );

//				UpdateRow( newRWCI );
				UpdateRow( rwci );

				updated = true;
			}
			catch (System.Exception ex)
			{
				MessageBox.Show( ex.Message );
			}

			return updated;
		}

		public void UpdateRow( RowWithCategoryInfo rwci )
		{
			DeleteRowFromCat2Phrase( rwci.Row );

			rwci.CatID2CheckboxMap[ 0 ] = true;	// This is the "All" category which every record should have.

			// Add Cat2Phrase lines
			foreach ( KeyValuePair<long, bool> kvp in rwci.CatID2CheckboxMap )
			{
				if ( kvp.Value == true )
				{
					long _catID = kvp.Key;
					long _phraseID = rwci.Row._id;
					cat2PhraseTableAdapter.Insert( _catID, _phraseID );
				}
			}
			
			// Now update the database with any changes that occurred on rwci.Row
			this.cat2PhraseTableAdapter.Update( MyDataSet );
			CommitChanges();

			DataRow[] c2pRows = MyDataSet.Cat2Phrase.Select( "_phraseID = 0" );
			DataRow[] categories = DBWrapper.Instance.Cat2PhraseDataTable.Select( string.Format( "_phraseID = {0}", rwci.Row._id ) );
		}

		private void DeleteRowFromCat2Phrase( MPBDataSet.PhrasebookRow phrasebookRow )
		{
			// Clear prev. data
			DataRow[] c2pRows = MyDataSet.Cat2Phrase.Select( string.Format( "_phraseID = {0}", phrasebookRow._id ) );
			if ( c2pRows.Length > 0 )
			{
				foreach ( DataRow row in c2pRows )
				{
					row.Delete();
				}

				this.cat2PhraseTableAdapter.Update( MyDataSet );
				CommitChanges();
			}
		}

		public void DeleteRowAt( MPBDataSet.PhrasebookRow dataRow )
		{
			DeleteRowFromCat2Phrase( dataRow );
			dataRow.Delete();

			this.phrasebookTableAdapter.Update( MyDataSet ); CommitChanges();
		}

		public void CommitChanges()
		{
/*
//			PopulateDatabase();
			this.phrasebookTableAdapter.Update( MyDataSet );
			this.cat2PhraseTableAdapter.Update( MyDataSet );
			this.categoriesTableAdapter.Update( MyDataSet );
*/

			this.tableAdapterManager.UpdateAll( MyDataSet );
			MyDataSet.AcceptChanges();
		}

		public MPBDataSet.PhrasebookRow GetPhraseRowByID( long id )
		{
			DataRow[] rows = PhrasebookDataTable.Select( string.Format( "_id = {0}", id ) );
			if ( rows.Length != 1 )
			{
				throw new ArgumentException();
			}

			return (MPBDataSet.PhrasebookRow)rows[ 0 ];
		}

		public MPBDataSet.PhrasebookRow GetPhraseRowByC2PRow( DataRow row )
		{
			MPBDataSet.Cat2PhraseRow c2pRow = row as MPBDataSet.Cat2PhraseRow;
			MPBDataSet.PhrasebookRow questionRow = DBWrapper.Instance.GetPhraseRowByID( c2pRow._phraseID );

			return questionRow;
		}

		public IEnumerable FilterRows( string filterQuery )
		{
			return PhrasebookDataTable.Select( filterQuery );
		}

		private Dictionary<string, long> m_CategoriesMap = null;
		public Dictionary<string, long> CategoriesMap
		{
			get
			{
				if ( m_CategoriesMap == null )
				{
					m_CategoriesMap = new Dictionary<string, long>();
					foreach ( MPBDataSet.CategoriesRow row in MyDataSet.Categories.Select() )
					{
						m_CategoriesMap[ row._name ] = row._id;
					}
				}

				return m_CategoriesMap;
			}
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
			phrasebookTableAdapter.Fill( PhrasebookDataTable );
			this.cat2PhraseTableAdapter.Fill( MyDataSet.Cat2Phrase );

			categoriesTableAdapter.Fill( MyDataSet.Categories );
			ReadAndMapCategories();
		}

		public class RowWithCategoryInfo
		{
			MPBDataSet.PhrasebookRow m_row;
			public MPBDataSet.PhrasebookRow Row { get { return m_row; } }

			Dictionary<long, bool> m_CatID2CheckboxMap = new Dictionary<long, bool>();

			public Dictionary<long, bool> CatID2CheckboxMap
			{
				get
				{
					return m_CatID2CheckboxMap;
				}
			}

			public RowWithCategoryInfo( MPBDataSet.PhrasebookRow row )
			{
				m_row = row;
				DataRow[] categories = DBWrapper.Instance.Cat2PhraseDataTable.Select( string.Format( "_phraseID = {0}", row._id ) );

				foreach ( MPBDataSet.Cat2PhraseRow c2pRow in categories )
				{
					m_CatID2CheckboxMap[ c2pRow._catID ] = true;
				}
			}
		}

		internal bool AddCategory( string newCategoryName, string title )
		{
			try
			{
				this.categoriesTableAdapter.Insert( newCategoryName, title );
				this.categoriesTableAdapter.Update( MyDataSet );
				CommitChanges();

				ReadAndMapCategories();	// Remap categories

				return true;
			}
			catch (System.Exception e)
			{
				MessageBox.Show( e.Message );
			}

			return false;
		}
	}
}
