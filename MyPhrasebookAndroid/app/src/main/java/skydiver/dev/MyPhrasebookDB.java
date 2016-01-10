package skydiver.dev;

import java.io.IOException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Set;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

public class MyPhrasebookDB
{
	public enum QueryMethod { Contains, BeginsWith, EndsWith, Exact };
	public static final long INVALID_ROW_ID = -1;
	
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
		public static final String TITLE = "_title";
		
		public static final String VAL_PHRASE = "Phrase";
		public static final String VAL_ALL = "All";
		public static final String VAL_PRACTICE = "_Practice";
	}
	
	public static final class TblCat2Phrase
	{
		public static final String TABLE_NAME = "Cat2Phrase";
		
		public static final String ID = "_id";
		public static final String CATEGORY_ID = "_catID";
		public static final String PHRASE_ID = "_phraseID";
	}
	
	public static final class PhrasebookRow
	{
		private long	_id = INVALID_ROW_ID;
		private String 	_lang1 = "";
		private String 	_lang2 = "";

		PhrasebookRow() {}
		PhrasebookRow(long id, String lang1, String lang2)
		{
			_id = id;
			_lang1 = lang1;
			_lang2 = lang2;
		}
		
		public long 	_id_get() { return _id; }
		public void 	_id_set(long id) { _id = id; }

		public String 	_lang1_get() { return _lang1; }		
		public void		_lang1_set(String lang1) { _lang1 = lang1; }		

		public String 	_lang2_get() { return _lang2; }		
		public void		_lang2_set(String lang2) { _lang2 = lang2; }		
	}
	
	public static final class Cat2PhraseRow
	{
		private long	_id = INVALID_ROW_ID;
		private long 	_catID = INVALID_ROW_ID;
		private long 	_phraseID = INVALID_ROW_ID;

		Cat2PhraseRow() {}
		Cat2PhraseRow(long id, long catID, long phraseID)
		{
			_id = id;
			_catID = catID;
			_phraseID = phraseID;
		}
		
		public long 	_id_get() { return _id; }
		public void 	_id_set(long id) { _id = id; }

		public long 	_catID_get() { return _catID; }		
		public void		_catID_set(long catID) { _catID = catID; }		

		public long 	_phraseID_get() { return _phraseID; }		
		public void		_phraseID_set(long phraseID) { _phraseID = phraseID; }		
	}
	
	public static final class CategoriesRow
	{
		private long	_id = INVALID_ROW_ID;
		private String 	_name = "";
		private String 	_title = "";

		CategoriesRow() {}
		CategoriesRow(long id, String name, String title)
		{
			_id = id;
			_name = name;
			_title = title;
		}
		
		public long 	_id_get() { return _id; }
		public void 	_id_set(long id) { _id = id; }

		public String 	_name_get() { return _name; }		
		public void		_name_set(String name) { _name = name; }		

		public String 	_title_get() { return _title; }		
		public void		_title_set(String title) { _title = title; }		
	}
	
	private final static String m_dbFileName = "mpb.db";
	private String m_dbPath;
	private MySQLiteOpenHelper m_dataHelper;
	private Context m_context;
	private HashMap<String,Long> mCatNameToCatIdMap = new HashMap<String,Long>();
	private HashMap<String,Long> mCatTitleToCatIdMap = new HashMap<String,Long>();
	private final String mStrCatTitleWords;
	private final String mStrCatTitleUncategorized;
	private long mPracticeCategoryID;

	
	private static MyPhrasebookDB mInstance = null;	
	public static MyPhrasebookDB Instance()
	{
		if ( mInstance == null )
		{
			mInstance = new MyPhrasebookDB( MPBApp.getInstance() );
		}
		
		return mInstance;
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

	private MyPhrasebookDB(Context context)
	{ 
		m_context = context; 

		mStrCatTitleWords = context.getString( R.string.catWords );
		mStrCatTitleUncategorized = context.getString( R.string.catUncategorized );
		
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

	HashMap<String,Long> getCategoryNamesMap()	{ return mCatNameToCatIdMap; }
	Set<String> getCategoryNames()	 			{ return mCatNameToCatIdMap.keySet(); }
	Set<String> getCategoryTitles()				{ return mCatTitleToCatIdMap.keySet(); }
	
	private Cursor doRawQuery( String query, String[] args )
	{
		Cursor cursor = TheDB().rawQuery( query, args );
		
		if ( cursor != null )
	    {
			cursor.moveToFirst();
	    }
		
	    return cursor;
	}
	
	private Cursor doRawQuery( String query )
	{
	    return doRawQuery( query, null );
	}

	private void initObjects()
	{
		// Loop all categories
		Cursor cursor = doRawQuery( "SELECT * FROM " + TblCategories.TABLE_NAME );
	    if (cursor.moveToFirst())
	    {
	        String name;
	        String title;
	        Long id; 
	        int nameColumn = cursor.getColumnIndex(TblCategories.NAME);
	        int titleColumn = cursor.getColumnIndex(TblCategories.TITLE);
	        int idColumn = cursor.getColumnIndex(TblCategories.ID);
	    
	        do {
	            id = cursor.getLong(idColumn);

	            name = cursor.getString(nameColumn);
	            mCatNameToCatIdMap.put( name, id );
	            
	            if ( name.equals(TblCategories.VAL_PRACTICE) )
	            {
	            	mPracticeCategoryID = id;
	            }

	            title = cursor.getString(titleColumn);
	            mCatTitleToCatIdMap.put( title, id );
	        } while (cursor.moveToNext());

	        // Add special cases for quiz
	        mCatTitleToCatIdMap.put( mStrCatTitleWords, INVALID_ROW_ID );
	        mCatTitleToCatIdMap.put( mStrCatTitleUncategorized, INVALID_ROW_ID );
	        
	        cursor.close();
	    }
	}
	
	public Cursor FilterPhrasebookRows(String text, int categoryID, QueryMethod queryMethod )
	{
        String queryString =
			        		"SELECT * FROM " + TblPhrasebook.TABLE_NAME
			        		+ " WHERE ("
			        		+ 			"("
			        		+ 				getQueryStringMatchQuery( MyPhrasebookDB.TblPhrasebook.LANG1, text, queryMethod )
			        		+ 				" OR "
			        		+ 				getQueryStringMatchQuery( MyPhrasebookDB.TblPhrasebook.LANG2, text, queryMethod )
			        		+ 			")"
			        		+ 			" AND " + TblPhrasebook.ID + " IN ("
			        		+ 				"SELECT " + TblCat2Phrase.PHRASE_ID + " FROM " + TblCat2Phrase.TABLE_NAME + " WHERE ("
			        		+ 					TblCat2Phrase.CATEGORY_ID + " = " + Integer.toString( categoryID )
			        		+ 				")"
			        		+ 			")"
			        		+ 		")"
			        		+ " ORDER BY " + MyPhrasebookDB.TblPhrasebook.LANG1 + " ASC"
		        		;
		
        Cursor cursor = doRawQuery( queryString );
        
        return cursor;
	}
	
	private String getQueryStringMatchQuery( String identifier, String text, QueryMethod queryMethod )
	{
		String queryString = "";
        switch ( queryMethod )
        {
	        case BeginsWith: queryString = identifier + " like '" + text + "%'"; break; 
	        case EndsWith: queryString = identifier + " like '%" + text + "'"; break; 
	        case Contains: queryString = identifier + " like '%" + text + "%'"; break;
	        
	        case Exact:
	        	// Note: 	For now the way to isolate the text is by white space.
	        	//			This is not a good solution to isolate text with special chars,
	        	//			E.g: The string "What?" won't pass this filter: "What" because we will look for
	        	//			either 'What' , 'What ...' , '... What' or '... What ...'
	        	queryString =
	        						identifier + " like '" + text + "'"			// Exact match
   	        			+ " OR " + 	identifier + " like '" + text + " %'"		// Isolated text at the beginning of a sentence
	        			+ " OR " + 	identifier + " like '% " + text + "'"		// Isolated text at the end of a sentence
	        			+ " OR " + 	identifier + " like '% " + text + " %'"		// Isolated text at the middle of a sentence
	        		;
	        	break;
        }
		
		return queryString;
	}
	
	public Cursor FilterPhrasebookRows(String queryString, String categoryTitle, QueryMethod queryMethod)
	{
        Cursor cursor = doRawQuery(
        			"SELECT " + TblCategories.ID + " FROM " + TblCategories.TABLE_NAME + " WHERE " + TblCategories.TITLE + " = '" + categoryTitle + "'"
        		);

        int categoryID = getInt( cursor, TblCategories.ID );
        
        return FilterPhrasebookRows(queryString, categoryID, queryMethod );
	}
	
	public Cursor GetPhraseRowByID( int phraseRowId )
	{
        Cursor cursor = doRawQuery( "SELECT * FROM " + TblPhrasebook.TABLE_NAME + " WHERE " + TblPhrasebook.ID + " = " + Integer.toString( phraseRowId ) );        
        return cursor;
	}
	
	public PhrasebookRow getPhrasebookRowByID( long phraseRowId )
	{
        Cursor cursor = doRawQuery( "SELECT * FROM " + TblPhrasebook.TABLE_NAME + " WHERE " + TblPhrasebook.ID + " = " + Long.toString( phraseRowId ) );
        
        PhrasebookRow row = null;
        if ( cursor != null && cursor.moveToFirst() )
        {
            row = new PhrasebookRow(
	        			getInt(cursor, TblPhrasebook.ID),
	        			getString(cursor, TblPhrasebook.LANG1),
	        			getString(cursor, TblPhrasebook.LANG2)
            		);
        }
        
        return row;
	}
	
	public Set<Long> getPhraseCategoryIDs( long phraseRowId )
	{
        Set<Long> catIDsSet = new HashSet<Long>();
        
        if ( phraseRowId != INVALID_ROW_ID )
        {
        	Cursor cursor = doRawQuery( "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + TblCat2Phrase.PHRASE_ID + " = " + Long.toString( phraseRowId ) );
            if ( cursor != null && cursor.moveToFirst() )
            {
            	do {
                    long catID = getLong(cursor, TblCat2Phrase.CATEGORY_ID);
                    catIDsSet.add( catID );
            	} while ( cursor.moveToNext() );
            }
        }
        
        return catIDsSet;
	}
	
	public Cursor selectCat2PhraseRowsByCategoryTitle( String catTitle )
	{
		return selectCat2PhraseRowsByCategoryTitle( catTitle, false );
	}
	
	public Cursor selectCat2PhraseRowsByCategoryTitle( String catTitle, boolean singleWordsOnly )
	{
		String query = null;

		if ( singleWordsOnly )
		{
			// Search for phrases with no space chars in any language
			Long categoryID = mCatTitleToCatIdMap.get( catTitle );
			query = "SELECT * FROM " + TblCat2Phrase.TABLE_NAME
						+ " WHERE " + TblCat2Phrase.CATEGORY_ID + " = " + categoryID
						+ " AND " + TblCat2Phrase.PHRASE_ID + " IN ( "
									+ "SELECT " + TblPhrasebook.ID + " FROM " + TblPhrasebook.TABLE_NAME 
										+ " WHERE " + TblPhrasebook.LANG1 + " NOT LIKE '% %'"
											+ " AND " + TblPhrasebook.LANG2 + " NOT LIKE '% %'"
								+ " ) "
					;
		}		
		else if ( catTitle.equals( mStrCatTitleUncategorized ) )
		{
			// Select all phrases which have exactly one category --> i.e. the "All" category.
			// These ones were never categorized and live in their own "uncategorized" category.
			query = "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " GROUP BY _phraseID HAVING COUNT(" + TblCat2Phrase.PHRASE_ID + ") = 1";
		}
		else if ( catTitle.equals( mStrCatTitleWords ) )
		{
			// A "word" is any entry that was not categorized as a "phrase"
			Long catPhraseID = mCatNameToCatIdMap.get( TblCategories.VAL_PHRASE );
			query = "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + TblCat2Phrase.ID + " NOT IN ( "
						+ "SELECT " + TblCat2Phrase.ID + " FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + TblCat2Phrase.CATEGORY_ID + " = " + catPhraseID.toString()
					+ ")";			
		}
		else
		{
			// If we got so far, catTitle represents a database-value category title
			Long categoryID = mCatTitleToCatIdMap.get( catTitle );
			query = "SELECT * FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + TblCat2Phrase.CATEGORY_ID + " = " + categoryID;
		}
		
		// Select the values based on the query
        Cursor cursor = doRawQuery( query );
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

	public static long getLong(Cursor cursor, String columnName)
	{
		int nColIdx = cursor.getColumnIndex(columnName);
		return cursor.getLong( nColIdx );
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

	public void AddPhrase(String lang1, String lang2, Set<Long> categoriesSet) throws Exception {
		// Call updatePhrase with an INVALID_ROW_ID to indicate that a new row should be added
		UpdatePhrase(INVALID_ROW_ID, lang1, lang2, categoriesSet);
	}
	
	public void UpdatePhrase(long nPhrasebookRowId, String lang1, String lang2, Set<Long> categoriesSet) throws Exception {
		SQLiteDatabase db = TheDB();
		ContentValues values = new ContentValues();
		values.put( TblPhrasebook.LANG1, lang1 );
		values.put( TblPhrasebook.LANG2, lang2 );
		
		try
		{
			db.beginTransaction();
			
			if ( nPhrasebookRowId == INVALID_ROW_ID )	// Adding a new row?
			{
				// Insert a new row to Phrasebook table
				nPhrasebookRowId = db.insert(TblPhrasebook.TABLE_NAME, null, values);
			}
			else	// Updating an existing row?
			{
				// Update Phrasebook row's values
				db.update(TblPhrasebook.TABLE_NAME, values, TblPhrasebook.ID + "=" + Long.toString(nPhrasebookRowId), null);

				deleteAllPhraseCategories( nPhrasebookRowId );
			}
			
			long allCatId = mCatNameToCatIdMap.get(TblCategories.VAL_ALL);
			categoriesSet.add(allCatId);	// Every phrase is also mapped to "All"			
			
			// Now add each category mapping as a row to Cat2Phrase table
			for ( long nCatId : categoriesSet )
			{
				values = new ContentValues();								// Reuse the values object
				
				values.put( TblCat2Phrase.CATEGORY_ID, nCatId ); 			// Add _catID
				values.put( TblCat2Phrase.PHRASE_ID, nPhrasebookRowId );	// Add _phraseID
				
				db.insert(TblCat2Phrase.TABLE_NAME, null, values);			// Add the table row.  
			}
			
			// If we got so far, the transaction was successful
			db.setTransactionSuccessful();
		}
		catch ( Exception e )
		{	
			throw e;
		}
		finally {
			db.endTransaction();
		}
	}

	private void deleteAllPhraseCategories(long nPhrasebookRowId) {
		// Delete all categories from Cat2Phrase
        this.TheDB().execSQL(
        		"DELETE FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + TblCat2Phrase.PHRASE_ID + "=" + Long.toString(nPhrasebookRowId)
        	);  
	}

	public void deletePhrase(long nPhrasebookRowId) throws Exception
	{
		SQLiteDatabase db = TheDB();
		try {
			db.beginTransaction();
			
			// Delete the row from Phrasebook table
			db.execSQL(
	        		"DELETE FROM " + TblPhrasebook.TABLE_NAME + " WHERE " + TblPhrasebook.ID + "=" + Long.toString(nPhrasebookRowId)
				);
			
			// And remove all associated categories
			deleteAllPhraseCategories(nPhrasebookRowId);

			// If we got so far, the transaction was successful
			db.setTransactionSuccessful();
		}
		catch ( Exception e )
		{	
			throw e;
		}
		finally {
			db.endTransaction();
		}
	}
	
	public boolean isPhraseInPracticeCategory( long phraseID )
	{
		boolean bInPracticeCategory = false;
		
		Cursor c = doRawQuery(
						"SELECT " + TblCat2Phrase.ID + " FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + TblCat2Phrase.CATEGORY_ID + " = " + Long.toString( mPracticeCategoryID ) + " AND " + TblCat2Phrase.PHRASE_ID + " = " + Long.toString( phraseID )
					);
		
		if ( c != null && c.moveToFirst() )
		{
			bInPracticeCategory = true;
		}
		
		return bInPracticeCategory;
	}
	
	public void addPhraseToPracticeCategory( long phraseID ) throws Exception
	{
		SQLiteDatabase db = TheDB();
		
		try
		{
			db.beginTransaction();
			
			removePhraseFromPracticeCategory( phraseID );
			
			ContentValues values = new ContentValues();
			
			values.put( TblCat2Phrase.CATEGORY_ID, mPracticeCategoryID ); 	// Add _catID
			values.put( TblCat2Phrase.PHRASE_ID, phraseID );				// Add _phraseID
			
			db.insert(TblCat2Phrase.TABLE_NAME, null, values);			// Add the table row.  

			// If we got so far, the transaction was successful
			db.setTransactionSuccessful();
		}
		catch ( Exception e )
		{	
			throw e;
		}
		finally {
			db.endTransaction();
		}
	}
	
	public void removePhraseFromPracticeCategory( long phraseID ) throws Exception
	{
		SQLiteDatabase db = TheDB();
		
		try
		{
			db.beginTransaction();
			
	        this.TheDB().execSQL(
	        		"DELETE FROM " + TblCat2Phrase.TABLE_NAME + " WHERE " + TblCat2Phrase.PHRASE_ID + "=" + Long.toString(phraseID) + " AND " + TblCat2Phrase.CATEGORY_ID + "=" + Long.toString( mPracticeCategoryID )
	        	);  

			// If we got so far, the transaction was successful
			db.setTransactionSuccessful();
		}
		catch ( Exception e )
		{	
			throw e;
		}
		finally {
			db.endTransaction();
		}
	}
	
	public boolean isRealCategoryTitle( String catTitle )
	{
		boolean bIsReal = (mCatTitleToCatIdMap.get( catTitle ) != INVALID_ROW_ID);
		return bIsReal;
	}
}
