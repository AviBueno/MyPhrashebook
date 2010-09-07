package skydiver.dev;

import java.io.IOException;
import java.util.HashMap;
import java.util.Set;

import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

public class MyPhrasebookDB
{
	public enum QueryMethod { Contains, BeginsWith, EndsWith, Exact };
	
	public static final class TblPhrasebook
	{
		public static final String TABLE_NAME = "Phrasebook";
		public static final String ID = "_id";
		public static final String LANG1 = "_english";
		public static final String LANG2 = "_language";
	}
	
	public static final class TblCategories
	{
		public static final String TABLE_NAME = "Categories";
		public static final String ID = "_id";
		public static final String NAME = "_name";
		
		public static final String VAL_PHRASE = "Phrases";
		public static final Object VAL_ALL = "All";
	}
	
	public static final class TblCat2Phrase
	{
		public static final String TABLE_NAME = "Cat2Phrase";
		public static final String ID = "_id";
		public static final String CATEGORY_ID = "_catID";
		public static final String PHRASE_ID = "_phraseID";
	}
	
	private final static String m_dbFileName = "mpb.db";
	private String m_dbPath;
	private MySQLiteOpenHelper m_dataHelper;
	private Context m_context;
	private QueryMethod m_queryMethod = QueryMethod.Contains;
	private HashMap<String,Integer> mCatNameToCatIdMap = new HashMap<String,Integer>();
	private HashMap<Integer, String> mCatIdToStringMap;
	private final String mStrCatNameWords;
	private final String mStrCatNameUncategorized;

	
	private static MyPhrasebookDB mInstance = null;	
	public static void CreateInstance(Context context) throws InstantiationException
	{
		if ( mInstance != null )
		{
			throw new InstantiationException("MyPhrasebookDB instance already exists");
		}
		
		mInstance = new MyPhrasebookDB( context.getApplicationContext() );
	}

	public static void DestroyInstance()
	{
		if ( mInstance != null )
		{
			if ( mInstance.m_dataHelper != null )
			{
				mInstance.m_dataHelper.close();
				mInstance.m_dataHelper = null;
			}
			
			mInstance = null;
		}
	}

	public static MyPhrasebookDB Instance()
	{
		return mInstance;
	}
	
	private MyPhrasebookDB(Context context)
	{ 
		m_context = context; 

		mStrCatNameWords = context.getString( R.string.catWords );
		mStrCatNameUncategorized = context.getString( R.string.catUncategorized );
		
		// Attempt to create (upon first time) or open (in consecutive launches) the database 
		Package pack = this.getClass().getPackage();
		m_dbPath = "/data/data/" + pack.getName() + "/databases/";
		m_dataHelper = new MySQLiteOpenHelper(m_context, m_dbPath, m_dbFileName);
		
		try {
			m_dataHelper.createDataBase();
		} catch (IOException ioe) {
			throw new Error("Unable to create database");
		}
		
		try {
			m_dataHelper.openDataBase();
		}catch(SQLException sqle){
			throw sqle;
		}
			
		initObjects();
	}
	
	public SQLiteDatabase TheDB()
	{
		return m_dataHelper.myDataBase;
	} 

	private void initObjects()
	{
		// Loop all categories
		Cursor cursor = TheDB().rawQuery( "SELECT * FROM " + TblCategories.TABLE_NAME, null );
	    if (cursor.moveToFirst())
	    {
	        String name; 
	        Integer id; 
	        int nameColumn = cursor.getColumnIndex(TblCategories.NAME); 
	        int idColumn = cursor.getColumnIndex(TblCategories.ID);
	    
	        do {
	            name = cursor.getString(nameColumn);
	            id = cursor.getInt(idColumn);

	            mCatNameToCatIdMap.put( name, id );
	            
				// Compose the query that will be execute on the Cat2Phrase table
				// in order to select lines of a certain category
//	            String queryString = TblCat2Phrase.CATEGORY_ID + " = " + id;  
//	            mCategoryToQueryMap.put( name, queryString );  
	        } while (cursor.moveToNext());

	        // Add special cases
	        mCatNameToCatIdMap.put( mStrCatNameWords, -1 );
	        mCatNameToCatIdMap.put( mStrCatNameUncategorized, -1 );
	        
	        cursor.close();
	    }
	}
	
	Set<String> getCategoryToQueryMap() { return mCatNameToCatIdMap.keySet(); }
	
	public void setQueryMethod( QueryMethod queryMethod )
	{
		m_queryMethod = queryMethod;
	}
	
	public QueryMethod getQueryMethod()
	{
		return m_queryMethod;
	}
	
	public Cursor FilterPhrasebookRows(String queryString)
	{
        String queryParam = "";
        switch ( m_queryMethod )
        {
	        case Exact: queryParam = queryString; break;				// Exact
	        case BeginsWith: queryParam = queryString + "%"; break;		// Begins with 
	        case EndsWith: queryParam = "%" + queryString; break;		// Ends with 
	        case Contains: queryParam = "%" + queryString + "%"; break;	// Contains 
        }
        
        Cursor cursor = this.TheDB().rawQuery(
        		"SELECT * FROM " + TblPhrasebook.TABLE_NAME + " WHERE ((" + MyPhrasebookDB.TblPhrasebook.LANG1 + " like ?) OR (" + MyPhrasebookDB.TblPhrasebook.LANG2 + " like ?)) ORDER BY " + MyPhrasebookDB.TblPhrasebook.LANG1 + " ASC",
        		new String[] {queryParam, queryParam} );
        
        return cursor;
	}
	
	public Cursor GetPhraseRowByID( int phraseRowId )
	{
        Cursor cursor = this.TheDB().rawQuery( "SELECT * FROM " + TblPhrasebook.TABLE_NAME + " WHERE _id = " + Integer.toString( phraseRowId ), null );        
        return cursor;
	}
	
	public Cursor selectCat2PhraseRowsByCatName( String catName )
	{
		String query = null;
		
		if ( catName.equals( mStrCatNameUncategorized ) )
		{
			// Select all phrases which have exactly one category --> i.e. the "All" category.
			// These ones were never categorized and live in their own "uncategorized" category.
			query = "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " GROUP BY _phraseID HAVING COUNT(_phraseID) = 1";
		}
		else if ( catName.equals( mStrCatNameWords ) )
		{
			// A "word" is any entry that was not categorized as a "phrase"
			Integer catPhraseID = mCatNameToCatIdMap.get( TblCategories.VAL_PHRASE );
			query = "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " WHERE _catID <> " + catPhraseID.toString();
		}
		else
		{
			// If we got so far, catName represents a database-value category name
			Integer catPhraseID = mCatNameToCatIdMap.get( catName );
			query = "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " WHERE _catID = " + catPhraseID;
		}
		
		// Select the values based on the query
        Cursor cursor = this.TheDB().rawQuery( query, null );
        return cursor;
	}

	public static String getString(Cursor cursor, String columnName)
	{
		int nColIdx = cursor.getColumnIndex(columnName);
		return cursor.getString( nColIdx );		
	}

	public static int getInt(Cursor cursor, String columnName)
	{
		int nColIdx = cursor.getColumnIndex(columnName);
		return cursor.getInt( nColIdx );
	}

	public static String dumpCursor(Cursor c)
	{
		String s = "";
		int nCols = c.getColumnCount();
		for ( int i = 0; i < nCols; i++ )
		{
			s += c.getColumnName(i) + "\t";
		}
		s += "\n";
		
		if ( c.moveToFirst() )
		{
			for ( int i = 0; i < nCols; i++ )
			{
				s += c.getString(i) + "\t";
			}
			s += "\n";
		} while ( c.moveToNext() );
		
		return s;
	}

	public static String getUncategorizedQuery() {
		return "_id = _id group by _phraseID having count(_phraseID) = 1";
	}
}
