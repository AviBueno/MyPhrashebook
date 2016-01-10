package skydiver.dev;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.DialogInterface.OnMultiChoiceClickListener;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;

public class AddEditPhraseForm extends Activity {

	public static final String IPARAM_DB_ROW_ID = "id";
	public static final String IPARAM_LANG1_TEXT = "L1";
	public static final String IPARAM_LANG2_TEXT = "L2";
	
	static final int DLG_CATEGORIES = 0;
	static final int DLG_NO_CATEGORY_SELECTED = 1;

	CharSequence[] mCatNames = null;
	boolean mCatSelections[] = null;
	long mCatIDs[] = null;
    boolean mSkipCategories = false;
    long mDbRowID = MyPhrasebookDB.INVALID_ROW_ID;
	HashMap<String,Long> mCategoriesMap = MyPhrasebookDB.Instance().getCategoryNamesMap();
	EditText mTxtViewLang1;
	EditText mTxtViewLang2;
	
	
	/** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_edit_phrase);

        ClearableEditText cet = (ClearableEditText)findViewById(R.id.txtLang1);
        mTxtViewLang1 = cet.edit_text;

        cet = (ClearableEditText)findViewById(R.id.txtLang2);
        mTxtViewLang2 = cet.edit_text;
        
        // 2011-02-12: When typing text that is wider than the visible text area, the text control widens up to the
        // until it reaches the width of the whole screen. The problem is that by doing so it exceeded the screen area.
        // setMaxWidth(  getWidth() ) is used in order to harden the width of the control such that it won't expand
        // beyond the width of the screen anymore, rather downwards to another line. 
        mTxtViewLang1.setMaxWidth( mTxtViewLang1.getWidth() ); 
        mTxtViewLang2.setMaxWidth( mTxtViewLang2.getWidth() ); 

        
        Bundle extras = getIntent().getExtras();
        if ( extras != null )
        {
        	mDbRowID = extras.getLong( IPARAM_DB_ROW_ID, MyPhrasebookDB.INVALID_ROW_ID );
        	String txtLang1 = null, txtLang2 = null;
        	if ( mDbRowID == MyPhrasebookDB.INVALID_ROW_ID )	// Adding a new record
        	{
        		txtLang1 = extras.getString(IPARAM_LANG1_TEXT);
        		txtLang2 = extras.getString(IPARAM_LANG2_TEXT);
        	}
        	else	// Editing an existing record
        	{
        		MyPhrasebookDB.PhrasebookRow row = MyPhrasebookDB.Instance().getPhrasebookRowByID( mDbRowID );
        		txtLang1 = row._lang1_get();
        		txtLang2 = row._lang2_get();
        	}
        	
        	mTxtViewLang1.setText(txtLang1 != null ? txtLang1 : "");
        	mTxtViewLang2.setText(txtLang2 != null ? txtLang2 : "");
        }
        
        InitCategoryObjects( mDbRowID );
        
        Button bttCatSel = (Button)findViewById(R.id.BttSetCategories);
        bttCatSel.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				showDialog( DLG_CATEGORIES );
			}
		});

        
        Button bttAddPhrase = (Button)findViewById(R.id.BttAddOrUpdatePhrase);

        // Set text to Add / Update
        bttAddPhrase.setText( mDbRowID == MyPhrasebookDB.INVALID_ROW_ID ? R.string.Add : R.string.Update );
        
        // Set event listener
        bttAddPhrase.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				OnAddOrUpdatePhrase();
			}
		});

        // In order to ensure the keyboard will pop up, we'll do it as a post event after a small delay
		Handler handler = new Handler();
		handler.postDelayed(
				    new Runnable() {
				        public void run() {
				            InputMethodManager inputMethodManager =  (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);

				            View v = findViewById(R.id.txtLang1);
				            inputMethodManager.toggleSoftInputFromWindow(v.getApplicationWindowToken(), InputMethodManager.SHOW_IMPLICIT, 0);
				            v.requestFocus();
				        }
				    },
				    500 // msec delay
				);
    }    
    
    void OnAddOrUpdatePhrase()
    {
		String lang1 = mTxtViewLang1.getText().toString();
		String lang2 = mTxtViewLang2.getText().toString();
		Set<Long> categoriesSet = new HashSet<Long>();
		int nCategories = mCatNames.length;
		
		for ( int i = 0; i < nCategories; i++ )
		{
			if ( mCatSelections[i] )
			{
				categoriesSet.add( mCatIDs[i] );
			}
		}

		// Validations
		String sErrorMessage = null;
		if ( lang1.length() == 0 )
		{
			sErrorMessage = String.format( getString( R.string.LangTextIsEmpty ), getString( R.string.Lang1 ) );
		}
		else if ( lang2.length() == 0 )
		{
			sErrorMessage = String.format( getString( R.string.LangTextIsEmpty ), getString( R.string.Lang2 ) );
		}
		else if ( ! mSkipCategories && categoriesSet.size() == 0 )
		{
			showDialog( DLG_NO_CATEGORY_SELECTED );
			return;
		}
		
		if ( sErrorMessage != null )
		{
			MPBApp.getInstance().ShortToast( sErrorMessage );
			return;
		}
		
		
		try
		{
			if ( mDbRowID == MyPhrasebookDB.INVALID_ROW_ID )	// Adding a new record
			{
				MyPhrasebookDB.Instance().AddPhrase(lang1, lang2, categoriesSet);
			}
			else	// Updating an existing record.
			{
				MyPhrasebookDB.Instance().UpdatePhrase(mDbRowID, lang1, lang2, categoriesSet);
			}
			
			setResult(RESULT_OK);
			AddEditPhraseForm.this.finish();
		}
		catch ( Exception e )
		{
			DialogBuilder.buildMessageBox(this, "Error", "An error occured:\n" + e.getMessage()).create().show();
		}
    }
    
	private void InitCategoryObjects( long dbRowIdx ) {
		// Create a sorted list of categories  
		List<String> sortedCatNamesList = new ArrayList<String>( MyPhrasebookDB.Instance().getCategoryNames() );
		java.util.Collections.sort(sortedCatNamesList);

		// Create arrays
		int nItems = sortedCatNamesList.size() - 1; // -1 because we're ignoring the "All" category
		mCatNames = new String[ nItems ];	
		mCatSelections = new boolean[ nItems ];
		mCatIDs = new long[ nItems ];

		Set<Long> catIDsSet = MyPhrasebookDB.Instance().getPhraseCategoryIDs( dbRowIdx );
		
		// Iterate the sorted names list and build arrays to be used for AlertDialog's setMultiChoiceItems() 
		int i = 0;
		for ( String name : sortedCatNamesList )
		{
			if ( ! name.equals(MyPhrasebookDB.TblCategories.VAL_ALL) )
			{
				mCatNames[i] = name;
				long catID = mCategoriesMap.get( name );
				mCatSelections[i] = catIDsSet.contains(catID) ? true : false;
				mCatIDs[i] = catID;
				i++;
			}
		}
	}

	@Override
	protected Dialog onCreateDialog(int id) {
		Dialog dlg = null;
		
		switch ( id ) {
			case DLG_CATEGORIES: {
					AlertDialog.Builder builder = new AlertDialog.Builder( this );
					builder.setTitle( getString(R.string.SelectCategories) );
					
					builder.setMultiChoiceItems(
									mCatNames,
									mCatSelections,
									new OnMultiChoiceClickListener() {
										public void onClick(DialogInterface dialog, int which, boolean isChecked) {
											mCatSelections[which] = isChecked;
										}
									}
								);
					
					dlg = builder.create();
				}
				break;
				
			case DLG_NO_CATEGORY_SELECTED: {
					Command yesCommand = new Command() {
						public void execute() {
							mSkipCategories = true;
							OnAddOrUpdatePhrase();
						}
					};
					
					AlertDialog.Builder builder = DialogBuilder.buildYesNoDialog(
									this,
									getString(R.string.NoCatSelTitle),
									getString(R.string.NoCatSelBody),
									yesCommand,
									Command.NO_OP // Will dismiss the dialog but perform no operation
								);
					
					dlg = builder.create();
				}
				break;
			
			default:
				dlg = super.onCreateDialog(id);
		}
		
		return dlg;
	}
}
