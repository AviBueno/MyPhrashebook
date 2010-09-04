package skydiver.dev;

import java.io.IOException;
import java.util.HashMap;

import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

public class MyPhrasebookDB
{
	public enum QueryMethod { Contains, BeginsWith, EndsWith, Exact };
	
	public class TblPhrasebook
	{
		public static final String TABLE_NAME = "Phrasebook";
		public static final String ID = "_id";
		public static final String LANG1 = "_english";
		public static final String LANG2 = "_language";
	}
	
	public class TblCategories
	{
		public static final String TABLE_NAME = "Categories";
		public static final String ID = "_id";
		public static final String NAME = "_name";
	}
	
	public class TblCat2Phrase
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
	private HashMap<String,String> mCategoryToQueryMap = new HashMap<String,String>();
	
	private static MyPhrasebookDB mInstance = null;	
	public static void CreateInstance(Context context) throws InstantiationException
	{
		if ( mInstance != null )
		{
			throw new InstantiationException("MyPhrasebookDB instance already exists");
		}
		
		mInstance = new MyPhrasebookDB( context );
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
	        String id; 
	        int nameColumn = cursor.getColumnIndex(TblCategories.NAME); 
	        int idColumn = cursor.getColumnIndex(TblCategories.ID); 
	    
	        do {
	            name = cursor.getString(nameColumn);
	            id = cursor.getString(idColumn);

				// Compose the query that will be execute on the Cat2Phrase table
				// in order to select lines of a certain category
	            String queryString = TblCat2Phrase.CATEGORY_ID + " = " + id;  
	            mCategoryToQueryMap.put( name, queryString );  

				if ( name.equals("Phrase") )
				{
					// Artificial query for single words.
					queryString = TblCat2Phrase.CATEGORY_ID + " <> " + id;					
					mCategoryToQueryMap.put( "Word", queryString );  // TODO Get the string from strings.xml
				}
	        } while (cursor.moveToNext());		
	    }
	}
	
	HashMap<String,String> getCategoryToQueryMap() { return mCategoryToQueryMap; }
	
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
	
	public Cursor SelectCat2PhraseRowsByFilter( String categoryQuery )
	{
        Cursor cursor = this.TheDB().rawQuery( "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + categoryQuery, null );        
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
}
