package skydiver.dev;

import java.io.IOException;

import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

public class MyPhrasebookDB
{
	public enum QueryMethod { Contains, BeginsWith, EndsWith, Exact };
	public static final String FLD_LANG1 = "_english";
	public static final String FLD_LANG2 = "_language";
	
	private final static String m_dbFileName = "mpb.db";
	private String m_dbPath;
	private MySQLiteOpenHelper m_dataHelper;
	private Context m_context;
	private QueryMethod m_queryMethod = QueryMethod.Contains;
	
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
	}
	
	public SQLiteDatabase TheDB()
	{
		if ( m_dataHelper == null ) // DB doesn't exist yet?
		{
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
		}
		
		return m_dataHelper.myDataBase;
	} 

	public void setQueryMethod( QueryMethod queryMethod )
	{
		m_queryMethod = queryMethod;
	}
	
	public QueryMethod getQueryMethod()
	{
		return m_queryMethod;
	}
	
	public Cursor FilterData(String queryString)
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
        		"SELECT * FROM Phrasebook WHERE ((" + MyPhrasebookDB.FLD_LANG1 + " like ?) OR (" + MyPhrasebookDB.FLD_LANG2 + " like ?)) ORDER BY " + MyPhrasebookDB.FLD_LANG1 + " ASC",
        		new String[] {queryParam, queryParam} );
        
        return cursor;
	}
}
