package skydiver.dev;

import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Set;

import skydiver.dev.MyPhrasebookDB.TblCategories;
import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.KeyEvent;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnCreateContextMenuListener;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.AdapterView.AdapterContextMenuInfo;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleCursorAdapter;
import android.widget.Spinner;

public class PhrasebookForm extends Activity {

	final static int ACTIVITY_ADD_PHRASE = 0;
	final static int ACTIVITY_EDIT_PHRASE = 1;
	
	final static int CONTEXTMENU_EDITITEM = 0;
	final static int CONTEXTMENU_ADD_PRACTICE = 1;
	final static int CONTEXTMENU_REMOVE_PRACTICE = 2;
	final static int CONTEXTMENU_DELETEITEM = 3;
	
	List<HashMap<String, String>> m_items;
	SimpleCursorAdapter m_itemsAdapter;
	EditText mThePhrase;
	ListView mPhrasesList;
	private SpinnerData mSDCategory;
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
		try
		{
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.phrasebook);
	        
	        InitCategoriesSpinner();
	        
			Cursor cursor = MyPhrasebookDB.Instance().FilterPhrasebookRows("", TblCategories.VAL_ALL);
	
			// create the adapter and assign it to the list view
			m_itemsAdapter = null;
			m_itemsAdapter = new SimpleCursorAdapter(
					this,
					R.layout.pb_list_item,
					cursor,
					new String[] {MyPhrasebookDB.TblPhrasebook.LANG1, MyPhrasebookDB.TblPhrasebook.LANG2},
					new int[] {R.id.LANG1, R.id.LANG2}
				);
			
			// get a reference to the ListView
			mPhrasesList = (ListView)findViewById(android.R.id.list);
			mPhrasesList.setAdapter(m_itemsAdapter);
			
            // Add Context-Menu listener to the ListView.
			mPhrasesList.setOnCreateContextMenuListener( new OnCreateContextMenuListener() {
				
				@Override
				public void onCreateContextMenu(ContextMenu menu, View v, ContextMenuInfo menuInfo) {
                    menu.setHeaderTitle("ContextMenu");

                    AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) menuInfo;
	                long phraseID = info.id;
                
                    menu.add(0, CONTEXTMENU_EDITITEM, 0, R.string.PBMenuEditItem);
                    
                    if ( ! MyPhrasebookDB.Instance().isPhraseInPracticeCategory( phraseID ) )
                    {
                        menu.add(0, CONTEXTMENU_ADD_PRACTICE, 1, R.string.PBMenuAddToPractice);
                    }
                    else
                    {
                        menu.add(0, CONTEXTMENU_REMOVE_PRACTICE, 1, R.string.PBMenuRemoveFromPractice);
                    }
                    
                    menu.add(0, CONTEXTMENU_DELETEITEM, 2, R.string.PBMenuDeleteItem);
				}
			});
					
					
			// Hook the EditText's text-changed event handler
			mThePhrase = (EditText) findViewById(R.id.EditedPhrase);
			mThePhrase.requestFocus();
			mThePhrase.addTextChangedListener(new TextWatcher(){
		        public void afterTextChanged(Editable s) {}
		        public void beforeTextChanged(CharSequence s, int start, int count, int after) {}
		        public void onTextChanged(CharSequence s, int start, int before, int count) {
		        	refreshList();
		        }
		    });
			
			Button bttAdd = (Button)findViewById(R.id.BttAdd);
			bttAdd.setOnClickListener( new OnClickListener() {
				public void onClick(View v) {
					launchAddEditPhraseActivity( MyPhrasebookDB.INVALID_ROW_ID );
				}
			});
			
			// Prevent the keyboard from auto-popping-up when entering the activity.
			this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN); 
			
			// Refresh the list (will filter according to mSDCategory)
			refreshList();  
		}
		catch ( Exception e )
		{
			Log.d( "EXCEPTION", e.getMessage() );
		}
	}

	private void launchAddEditPhraseActivity( long nEditedRowId )
	{
		Intent i = new Intent(PhrasebookForm.this.getApplicationContext(), AddEditPhraseForm.class);
		i.putExtra(AddEditPhraseForm.IPARAM_LANG1_TEXT, mThePhrase.getText().toString());
		i.putExtra(AddEditPhraseForm.IPARAM_LANG2_TEXT, mThePhrase.getText().toString());

		int activityId = ACTIVITY_ADD_PHRASE;
		if ( MyPhrasebookDB.INVALID_ROW_ID != nEditedRowId )	// Editing an existing row
		{
			i.putExtra(AddEditPhraseForm.IPARAM_DB_ROW_ID, nEditedRowId);
			activityId = ACTIVITY_EDIT_PHRASE;
		}
		
		startActivityForResult(i, activityId);
	}
    
	private void refreshList() {
		Cursor cursor = MyPhrasebookDB.Instance().FilterPhrasebookRows( mThePhrase.getText().toString(), mSDCategory.getValue() );
		m_itemsAdapter.changeCursor( cursor );
	}
    
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
        	
        	setResult(RESULT_CANCELED);
			this.finish();			
			return true;
        }

        return super.onKeyDown(keyCode, event);
    }
    
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		switch ( requestCode )
		{
			case ACTIVITY_ADD_PHRASE:
			case ACTIVITY_EDIT_PHRASE:
				if ( resultCode != RESULT_CANCELED )
				{
					refreshList();
				}
				break;
				
			default:
				super.onActivityResult(requestCode, resultCode, data);
				break;
		}
	}
	
	AdapterContextMenuInfo mOnContextItemSelectedMenuInfo;
	@Override
	public boolean onContextItemSelected(MenuItem aItem) {
		mOnContextItemSelectedMenuInfo = (AdapterContextMenuInfo) aItem.getMenuInfo();
		long phraseID = mOnContextItemSelectedMenuInfo.id;
		
		try {
			// Switch on the ID of the item, to get what the user selected.
			switch (aItem.getItemId()) {
			case CONTEXTMENU_EDITITEM:
				launchAddEditPhraseActivity( mOnContextItemSelectedMenuInfo.id );
				return true; // true means: "we handled the event".

			case CONTEXTMENU_ADD_PRACTICE:
				MyPhrasebookDB.Instance().addPhraseToPracticeCategory( phraseID );
				return true; // true means: "we handled the event".

			case CONTEXTMENU_REMOVE_PRACTICE:
				MyPhrasebookDB.Instance().removePhraseFromPracticeCategory( phraseID );
				return true; // true means: "we handled the event".

			case CONTEXTMENU_DELETEITEM:
					Command deleteCommand = new Command() {
						public void execute(){
							try {
								MyPhrasebookDB.Instance().deletePhrase( mOnContextItemSelectedMenuInfo.id );
								refreshList();
							}
							catch ( Exception e )
							{
								DialogBuilder.buildMessageBox(PhrasebookForm.this, "Error", "An error occured:\n" + e.getMessage()).create().show();
							}
							
						}
					};
					
					DialogBuilder.buildYesNoDialog(
							this,
							"Delete",
							"Are you sure you want to delete the entry?",
							deleteCommand,
							Command.NO_OP
						)
						.create()
						.show();
					return true; // true means: "we handled the event".
			}
		}
		catch ( Exception e )
		{
			DialogBuilder.buildMessageBox(PhrasebookForm.this, "Error", "An error occured:\n" + e.getMessage()).create().show();
		}
		
		return false;
	}
	
	private void InitCategoriesSpinner()
	{
		Spinner spinner = (Spinner)findViewById(R.id.spinCategory);

		ArrayAdapter<SpinnerData> adapter = 
			new ArrayAdapter<SpinnerData>( 
					this,
					android.R.layout.simple_spinner_item
				);

		Set<String> categoryTitles = MyPhrasebookDB.Instance().getCategoryTitles();
		String storedSDCategoryValue = MPBApp.getInstance().getPhrasebookCategory();

		SpinnerData allSD = null;
		for (String catTitle : categoryTitles)
		{
			if ( MyPhrasebookDB.Instance().isRealCategoryTitle( catTitle ) )
			{
				SpinnerData sd = new SpinnerData( catTitle, catTitle );
				adapter.add( sd );
				
				// Test if this was the prev. selected category
				if ( storedSDCategoryValue.equals( sd.getValue() ) )
				{
					mSDCategory = sd;
				}
				
				// Remember the "All" object
				if ( catTitle.equals( MyPhrasebookDB.TblCategories.VAL_ALL ) )
				{
					allSD = sd;
				}
			}
		}
		
		adapter.sort(new Comparator<SpinnerData>() {
			public int compare(SpinnerData object1, SpinnerData object2) {
				return object1.compareTo(object2);
			};
		});

		// Put the "All" category first one on the list
		adapter.remove(allSD);
		adapter.insert(allSD, 0);
		
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		
		spinner.setAdapter(adapter);
		spinner.setOnItemSelectedListener(
			new AdapterView.OnItemSelectedListener()
			{
				public void onItemSelected(
						AdapterView<?> parent, 
						View view, 
						int position, 
						long id)
				{
					SpinnerData sd = (SpinnerData)parent.getAdapter().getItem( position );
					if ( sd == mSDCategory )
					{
						return;
					}

					mSDCategory = sd;	// Save the selected category
					
					String sCategory = sd.getValue();					
					MPBApp.getInstance().setPhrasebookCategory( sCategory );
					
					refreshList();		// Refresh the list (will filter according to mSDCategory)  
				}

				public void onNothingSelected(AdapterView<?> parent) {
				}
			}
		);
		
		// Select the prev. selected category
		int nSelPos = adapter.getPosition(mSDCategory);
		if ( nSelPos >= 0 )
		{
			// NOTE: For some reason, setSelection(int position) displayed the text of position=0 instead of position.
			// Specifically: when switching off the screen are switching it on again.
			// In order to overcome this (android bug?), we use setSelection(int position, boolean animate).
			spinner.setSelection(nSelPos, true);
		}
	}
}