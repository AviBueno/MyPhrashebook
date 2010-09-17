package skydiver.dev;

import java.util.ArrayList;
import java.util.List;

import skydiver.dev.MyPhrasebookDB.TblCategories;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.DialogInterface.OnMultiChoiceClickListener;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class AddPhraseForm extends Activity {

	public static final String THE_PHRASE = "ThePhrase";
	final int CATEGORIES_DLG_ID = 0;
	CharSequence[] mCatNames = null;
	boolean mCatSelections[] = null;
    boolean mSkipCategories = false;
	
	/** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_phrase);

        InitCategoryObjects();

        Bundle extras = getIntent().getExtras();
        if ( extras != null )
        {
            String thePhrase = extras.getString(THE_PHRASE);
            EditText txtLang1 = (EditText)findViewById(R.id.txtLang1);
            EditText txtLang2 = (EditText)findViewById(R.id.txtLang2);
            txtLang1.setText(thePhrase);
            txtLang2.setText(thePhrase);
        }
        
        Button bttCatSel = (Button)findViewById(R.id.BttSetCategories);
        bttCatSel.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				showDialog( CATEGORIES_DLG_ID );
			}
		});

        Button bttAddPhrase = (Button)findViewById(R.id.BttAddPhrase);
        bttAddPhrase.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				AddPhrase();
			}
		});
    }    
    
    void AddPhrase()
    {
		String lang1 = ((EditText)findViewById(R.id.txtLang1)).getText().toString();
		String lang2 = ((EditText)findViewById(R.id.txtLang2)).getText().toString();
		List<String> catList = new ArrayList<String>();
		int nCategories = mCatNames.length;
		for ( int i = 0; i < nCategories; i++ )
		{
			if ( mCatSelections[i] )
			{
				catList.add( mCatNames[i].toString() );
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
		else if ( ! mSkipCategories && catList.size() == 0 )
		{
			AlertDialog.Builder alt_bld = new AlertDialog.Builder(AddPhraseForm.this);
			alt_bld.setMessage("No category selected.\nWould you like to add with no category?")
				.setCancelable(false)
				.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int id) {
						// Action for 'Yes' Button
						mSkipCategories = true;
						AddPhrase();
					}
				})
				.setNegativeButton("No", new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int id) {
						//  Action for 'NO' Button
						dialog.cancel();
					}
				});
			
			AlertDialog alert = alt_bld.create();
			alert.setTitle("Title");
			alert.show();
			return;
		}
		
		if ( sErrorMessage != null )
		{
			Toast.makeText(AddPhraseForm.this, sErrorMessage, Toast.LENGTH_SHORT).show();
			return;
		}
		
		
		catList.add(TblCategories.VAL_ALL);	// Every phrase is also mapped to "All"
		
		boolean bAdded = MyPhrasebookDB.Instance().AddPhrase(lang1, lang2, catList);
		if ( bAdded )
		{
			AddPhraseForm.this.finish();
		}
    }
    
	private void InitCategoryObjects() {
		List<String> catNames = new ArrayList<String>( MyPhrasebookDB.Instance().getCategoryNames() );
		java.util.Collections.sort(catNames);
		
		mCatNames = new String[ catNames.size() - 1 ];	// Ignoring "All"
		mCatSelections = new boolean[ catNames.size() - 1 ];
		int i = 0;
		for ( String name : catNames )
		{
			if ( ! name.equals(MyPhrasebookDB.TblCategories.VAL_ALL) )
			{
				mCatNames[i] = name;
				mCatSelections[i] = false;
				i++;
			}
		}
	}

	@Override
	protected Dialog onCreateDialog(int id) {
		Dialog dlg = null;
		
		switch ( id ) {
			case CATEGORIES_DLG_ID: {
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
			
			default:
				dlg = super.onCreateDialog(id);
		}
		
		return dlg;
	}
}
