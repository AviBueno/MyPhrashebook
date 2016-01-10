package skydiver.dev;

import java.util.HashSet;
import java.util.Locale;
import java.util.Random;

import android.app.Application;
import android.content.SharedPreferences;
import android.util.Log;
import android.view.Gravity;
import android.widget.Toast;

public class MPBApp extends Application {
	public static final String keyQUIZ_LEVEL = "QuizLevel";
	public static final String keyQUIZ_CATEGORY = "QuizCategory";
	public static final String keyPHRASEBOOK_CATEGORY = "PhrasebookCategory";
	public static final String keyLANGUAGE = "Language";

	public static final Locale LANG1_LOCALE = new Locale("en");
	public static final Locale LANG2_LOCALE = new Locale("fi");

	private static MPBApp mInstance = null;
	public MPBApp()
	{
		if ( mInstance == null )
		{
			mInstance = this;
		}
	}

	public static MPBApp getInstance()
	{
		return mInstance;
	}

	static private Random smRandom = new Random();
	public static Random RNG() { return smRandom; }

	public QuizFormMultiChoice.QuizLevel getQuizLevel( QuizFormMultiChoice.QuizLevel defaultQuizLevel )
	{
		int nQL = get( keyQUIZ_LEVEL, defaultQuizLevel.ordinal() );
		QuizFormMultiChoice.QuizLevel ql = QuizFormMultiChoice.QuizLevel.values()[nQL];
		return ql;
	}

	public void setQuizLevel( QuizFormMultiChoice.QuizLevel ql )
	{
		set( keyQUIZ_LEVEL, ql.ordinal() );
	}

	String mQuizCategory = null;
	public String getQuizCategory( String pageId )
	{
		String sCategory = get( keyQUIZ_CATEGORY + pageId, MyPhrasebookDB.TblCategories.VAL_ALL );
		if (! sCategory.equals( mQuizCategory ) )
		{
			Log.v( "MPBApp-Get", String.format("cat.s:%s ; cat.m:%s", sCategory, mQuizCategory ) );
			mQuizCategory = sCategory;
		}
		Log.v( "MPBApp-Get", sCategory );
		return sCategory;
	}

	public void setQuizCategory( String sCategory, String pageId )
	{
		Log.v( "MPBApp-Get", String.format("cat.s:%s ; cat.m:%s", sCategory, mQuizCategory ) );
		Log.v( "MPBApp-Set", sCategory );
		set(keyQUIZ_CATEGORY + pageId, sCategory);
	}

	String mPhrasebookCategory = null;
	public String getPhrasebookCategory()
	{
		String sCategory = get( keyPHRASEBOOK_CATEGORY, MyPhrasebookDB.TblCategories.VAL_ALL );
		if (! sCategory.equals( mPhrasebookCategory ) )
		{
			Log.v( "MPBApp-Get", String.format("cat.s:%s ; cat.m:%s", sCategory, mPhrasebookCategory ) );
			mPhrasebookCategory = sCategory;
		}
		Log.v( "MPBApp-Get", sCategory );
		return sCategory;
	}

	public void setPhrasebookCategory( String sCategory )
	{
		Log.v( "MPBApp-Get", String.format("cat.s:%s ; cat.m:%s", sCategory, mPhrasebookCategory ) );
		Log.v( "MPBApp-Set", sCategory );
		set(keyPHRASEBOOK_CATEGORY, sCategory);
	}

	public String getQuizLanguage(String pageId)
	{
		String sLanguage = get(keyLANGUAGE + pageId, QuizFormBase.LANG_ANY);
		return sLanguage;
	}

	public void setQuizLanguage( String sLanguage, String pageId )
	{
		set(keyLANGUAGE + pageId, sLanguage);
	}

	public void set( String fieldName, HashSet<Integer> set )
	{
		String setAsString = set.toString();
		set(fieldName, setAsString);
	}

	public HashSet<Integer> get( String fieldName )
	{
		HashSet<Integer> resultSet = new HashSet<Integer>();

		String data = get( fieldName, "" ).replace("[", "").replace("]", "");

		if ( data.length() > 0 )	// Not empty?
		{
	        String[] strings = data.split(", ");

			for (int i = 0; i < strings.length; i++) {
	        	resultSet.add( Integer.parseInt(strings[i]) );
	        }
		}

		return resultSet;
	}

	public int get(String fieldName, int defaultValue)
	{
		SharedPreferences prefs = getSharedPreferences();
		int value = prefs.getInt(fieldName, defaultValue);
		return value;
	}

	public void set(String fieldName, int value)
	{
		SharedPreferences.Editor prefsEditor = getSharedPreferencesEditor();
		prefsEditor.putInt( fieldName, value );
		prefsEditor.commit();
	}

	public boolean get(String fieldName, boolean defaultValue)
	{
		SharedPreferences prefs = getSharedPreferences();
		boolean value = prefs.getBoolean(fieldName, defaultValue);
		return value;
	}

	public void set(String fieldName, boolean value)
	{
		SharedPreferences.Editor prefsEditor = getSharedPreferencesEditor();
		prefsEditor.putBoolean( fieldName, value );
		prefsEditor.commit();
	}

	private void set( String fieldName, String value )
	{
		SharedPreferences.Editor prefsEditor = getSharedPreferencesEditor();
		prefsEditor.putString( fieldName, value );
		prefsEditor.commit();
	}

	private String get( String fieldName, String defaultValue )
	{
		SharedPreferences prefs = getSharedPreferences();
		String value = prefs.getString(fieldName, defaultValue);
		return value;
	}

	private SharedPreferences getSharedPreferences()
	{
		SharedPreferences prefs = getSharedPreferences("mpb", MODE_PRIVATE);
		return prefs;
	}

	private SharedPreferences.Editor getSharedPreferencesEditor()
	{
		SharedPreferences.Editor prefsEditor = getSharedPreferences().edit();
		return prefsEditor;
	}

	public void clearAllSharedSettings() {
		SharedPreferences.Editor prefsEditor = getSharedPreferencesEditor();
		prefsEditor.clear();
		prefsEditor.commit();
	}

	private void ShowToast( String text, boolean shortToast )
	{
        Toast toast = Toast.makeText(this, text, shortToast ? Toast.LENGTH_SHORT : Toast.LENGTH_LONG );
        toast.setGravity(Gravity.CENTER, 0, 0);
        toast.show();
	}

	public void ShortToast( String text )
	{
		ShowToast(text, true);
	}

	public void ShortToast( int stringResourceId )
	{
		ShowToast(getString(stringResourceId), true);
	}

	public void LongToast( String text )
	{
		ShowToast(text, false);
	}

	public void LongToast( int stringResourceId )
	{
		ShowToast(getString(stringResourceId), false);
	}
}
