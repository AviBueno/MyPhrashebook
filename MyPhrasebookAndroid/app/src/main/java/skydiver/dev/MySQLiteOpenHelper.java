package skydiver.dev;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import android.content.Context;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class MySQLiteOpenHelper extends SQLiteOpenHelper
{
	//The Android's default system path of your application database.
	protected SQLiteDatabase myDataBase; 
	private final Context myContext;
	protected final String m_dbPath;
	protected final String m_dbFileName;
	protected final String m_dbFullPath;

	/**
	 * Constructor
	 * Takes and keeps a reference of the passed context in order to access to the application assets and resources.
	 * @param context
	 */
	public MySQLiteOpenHelper(Context context, String dbPath, String dbFileName)
	{ 
		super(context, new File(dbPath, dbFileName).toString(), null, 1);
		
		this.myContext = context;
		this.m_dbPath = dbPath;
		this.m_dbFileName = dbFileName;
		this.m_dbFullPath = new File(dbPath, dbFileName).toString();
	}
 
  /**
	 * Creates a empty database on the system and rewrites it with your own database.
	 * */
	public void createDataBase() throws IOException
	{ 
		boolean dbExist = checkDataBase();
 
		if(dbExist)
		{
			//do nothing - database already exist
		}
		else
		{ 
			try
			{	 
				//By calling this method and empty database will be created into the default system path
				//of your application so we are gonna be able to overwrite that database with our database.
				this.getReadableDatabase();
			}
			catch (Exception e)
			{
				Log.d("ERROR", "Can't get readable database..");
			}
 
			try
			{	 
				copyDataBase();	 
			}
			catch (IOException e)
			{ 
				throw new Error("Error copying database");	 
			}
		}
		
		this.close();
	}
 
	/**
	 * Check if the database already exist to avoid re-copying the file each time you open the application.
	 * @return true if it exists, false if it doesn't
	 */
	private boolean checkDataBase()
	{ 
		SQLiteDatabase checkDB = null;
 
		try
		{
			checkDB = SQLiteDatabase.openDatabase(m_dbFullPath, null, SQLiteDatabase.OPEN_READONLY); 
		}
		catch(SQLiteException e)
		{ 
			//database does't exist yet. 
		}
 
		if(checkDB != null)
		{ 
			checkDB.close(); 
		}
 
		return checkDB != null ? true : false;
	}
 
	/**
	 * Copies your database from your local assets-folder to the just created empty database in the
	 * system folder, from where it can be accessed and handled.
	 * This is done by transferring bytestream.
	 * */
	private void copyDataBase() throws IOException
	{ 
		//Open your local db as the input stream
		InputStream myInput = myContext.getAssets().open(m_dbFileName);
 
		// Path to the just created empty db
		String outFileName = m_dbFullPath;
 
		//Open the empty db as the output stream
		OutputStream myOutput = new FileOutputStream(outFileName);
 
		//transfer bytes from the inputfile to the outputfile
		byte[] buffer = new byte[1024];
		int length;
		while ((length = myInput.read(buffer)) > 0)
		{
			myOutput.write(buffer, 0, length);
		}
 
		//Close the streams
		myOutput.flush();
		myOutput.close();
		myInput.close();
	}
 
	public void openDataBase() throws SQLException
	{ 
		//Open the database
		myDataBase = SQLiteDatabase.openDatabase(m_dbFullPath, null, SQLiteDatabase.OPEN_READWRITE);
	}
 
	public void open() throws SQLException
	{ 
		this.openDataBase();
	}
 
	@Override
	public synchronized void close()
	{ 
		if(myDataBase != null)
		{
			myDataBase.close();
		}
 
		super.close();
	}
 
	@Override
	public void onCreate(SQLiteDatabase db)
	{
	}
 
	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
	{
	}
}
