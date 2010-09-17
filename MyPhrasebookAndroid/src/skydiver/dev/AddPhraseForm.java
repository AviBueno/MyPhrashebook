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

public class AddPhraseForm extends Activity {

	final int CATEGORIES_DLG_ID = 0;
	CharSequence[] mCatNames = null;
	boolean mCatSelections[] = null;
	
	/** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_phrase);

        InitCategoryObjects();
        
        Button bttCatSel = (Button)findViewById(R.id.BttSetCategories);
        bttCatSel.setOnClickListener( new OnClickListener() {
			@Override
			public void onClick(View v) {
				showDialog( CATEGORIES_DLG_ID );
			}
		});

        Button bttAddPhrase = (Button)findViewById(R.id.BttAddPhrase);
        bttAddPhrase.setOnClickListener( new OnClickListener() {
			@Override
			public void onClick(View v) {
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
				
				catList.add(TblCategories.VAL_ALL);	// Every phrase is also mapped to "All"
				
				boolean bAdded = MyPhrasebookDB.Instance().AddPhrase(lang1, lang2, catList);
				if ( bAdded )
				{
					AddPhraseForm.this.finish();
				}
			}
		});
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

//	@Override
//	protected void onPrepareDialog(int id, Dialog dialog) {
//		switch ( id )
//		{
//			case CATEGORIES_DLG_ID:
//				break;
//			
//			default:
//				super.onPrepareDialog(id, dialog);
//		}
//	}
   
//  //////////////////////////////////////////////////////////        
//  ListView lView = (ListView)findViewById(R.id.CatList);
//
//  // Set option as Multiple Choice. So that user can able to select more the one option from list
//  HashMap<String,Integer> cat2idMap = MyPhrasebookDB.Instance().getCategoryToIdMap();
//  List<LVItem> items = new ArrayList<LVItem>();
//  for( String cat : cat2idMap.keySet() )
//  {
//  	items.add( new LVItem(cat, cat2idMap.get(cat)) );
//  }
//  
//  Collections.sort( items, new Comparator<LVItem>() {
//  	public int compare(LVItem object1, LVItem object2) {
//  		return object1.mText.compareTo(object2.mText);
//  	};
//  } );
//  
//	lView.setAdapter(
//			new ArrayAdapter<LVItem>(
//					this,
//					//android.R.layout.simple_list_item_multiple_choice,
//					R.layout.category_list_item,
//					items
//				)
//		);
//	
//	lView.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);        

//    class LVItem
//    {
//    	String mText;
//    	int mId;
//    	public LVItem( String text, int id )
//    	{
//    		mText = text;
//    		mId = id;
//    	}
//    	
//    	public String getText() { return mText; }
//    	public int getId() 		{ return mId; }
//    	
//    	public String toString() { return getText(); }
//    }
}
