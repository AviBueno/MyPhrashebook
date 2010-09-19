package skydiver.dev;

import java.util.HashMap;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.KeyEvent;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnCreateContextMenuListener;
import android.widget.AdapterView.AdapterContextMenuInfo;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleCursorAdapter;

public class PhrasebookForm extends Activity {

	final static int ACTIVITY_ADD_PHRASE = 0;
	final static int ACTIVITY_EDIT_PHRASE = 1;
	
	final static int CONTEXTMENU_EDITITEM = 0;
	final static int CONTEXTMENU_DELETEITEM = 1;
	
	List<HashMap<String, String>> m_items;
	SimpleCursorAdapter m_itemsAdapter;
	EditText mThePhrase;
	ListView mPhrasesList;
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
		try
		{
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.phrasebook);
	        
			Cursor cursor = MyPhrasebookDB.Instance().FilterPhrasebookRows("");
	
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
            /* Add Context-Menu listener to the ListView. */
			mPhrasesList.setOnCreateContextMenuListener( new OnCreateContextMenuListener() {
				
				@Override
				public void onCreateContextMenu(ContextMenu menu, View v, ContextMenuInfo menuInfo) {
                    menu.setHeaderTitle("ContextMenu");

                    menu.add(0, CONTEXTMENU_EDITITEM, 0, "Edit");
                    menu.add(0, CONTEXTMENU_DELETEITEM, 1, "Delete");
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
		}
		catch ( Exception e )
		{
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
		Cursor cursor = MyPhrasebookDB.Instance().FilterPhrasebookRows( mThePhrase.getText().toString() );
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
		
		// Switch on the ID of the item, to get what the user selected.
		switch (aItem.getItemId()) {
			case CONTEXTMENU_EDITITEM:
				launchAddEditPhraseActivity( mOnContextItemSelectedMenuInfo.id );
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
								MPBApp.buildMessageBox(PhrasebookForm.this, "Error", "An error occured:\n" + e.getMessage()).create().show();
							}
						}
					};
					
					MPBApp.buildYesNoDialog(
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

		return false;
	}
}