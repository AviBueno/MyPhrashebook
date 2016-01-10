package skydiver.dev;

import java.util.HashMap;
import java.util.List;

import skydiver.dev.MyPhrasebookDB.TblCategories;
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
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleCursorAdapter;

public class PhrasebookForm extends BaseActivity {

	final static int ACTIVITY_ADD_PHRASE = 0;
	final static int ACTIVITY_EDIT_PHRASE = 1;

	final static int CONTEXTMENU_EDITITEM = 0;
	final static int CONTEXTMENU_ADD_PRACTICE = 1;
	final static int CONTEXTMENU_REMOVE_PRACTICE = 2;
	final static int CONTEXTMENU_DELETEITEM = 3;

    private static final String keyPBQueryMethod = "PBPBQueryMethod";

	List<HashMap<String, String>> m_items;
	SimpleCursorAdapter m_itemsAdapter;
	EditText mThePhrase;
	ListView mPhrasesList;
	MyPhrasebookDB.QueryMethod m_queryMethod = MyPhrasebookDB.QueryMethod.Contains;

    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
		try
		{
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.phrasebook);

	        // Read query method from the app store
	        m_queryMethod = getQueryMethod();

	        String storedSDCategoryValue = MPBApp.getInstance().getPhrasebookCategory();
	        initCategoriesSpinner( storedSDCategoryValue, new CategoryChangedListener() {
				@Override
				public void onCategoryChanged(SpinnerData sd) {
					String sCategory = sd.getValue();
					MPBApp.getInstance().setPhrasebookCategory( sCategory );

					refreshList(); // Refresh the list (will filter according to active category)
				}
			});

			Cursor cursor = MyPhrasebookDB.Instance().FilterPhrasebookRows("", TblCategories.VAL_ALL, m_queryMethod);

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
			ClearableEditText cet = (ClearableEditText) findViewById(R.id.EditedPhrase);
			mThePhrase = cet.edit_text;
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

			Button bttQueryMethod = (Button)findViewById(R.id.BttQueryMethod);
			bttQueryMethod.setOnClickListener( new OnClickListener() {
				public void onClick(View v) {
					// Flip the value
			    	m_queryMethod = (m_queryMethod == MyPhrasebookDB.QueryMethod.Contains) ? MyPhrasebookDB.QueryMethod.Exact : MyPhrasebookDB.QueryMethod.Contains;
			    	setQueryMethod( m_queryMethod );

					updateTextMatchButton(); // Update the button control

					refreshList(); // And force the list to refresh according to new query method
				}
			});
			updateTextMatchButton(); // Call the first time update

			// Prevent the keyboard from auto-popping-up when entering the activity.
			this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);

			// Refresh the list (will filter according to active category)
			refreshList();
		}
		catch ( Exception e )
		{
			Log.d( "EXCEPTION", e.getMessage() );
		}
	}

	public MyPhrasebookDB.QueryMethod getQueryMethod()
	{
		// Get the ordinal index of the stored valud and convert to enum value
		int nQueryMethodOrdinal = MPBApp.getInstance().get( keyPBQueryMethod, MyPhrasebookDB.QueryMethod.Contains.ordinal() );
		MyPhrasebookDB.QueryMethod[] values = MyPhrasebookDB.QueryMethod.values();
		return values[ nQueryMethodOrdinal ];
	}

	public void setQueryMethod( MyPhrasebookDB.QueryMethod queryMethod )
	{
		MPBApp.getInstance().set( keyPBQueryMethod, queryMethod.ordinal() );
	}

    private void updateTextMatchButton()
    {
		Button bttQueryMethod = (Button)findViewById(R.id.BttQueryMethod);

    	bttQueryMethod.setText( (m_queryMethod == MyPhrasebookDB.QueryMethod.Contains) ? "!" : "*" );
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
		Cursor cursor = MyPhrasebookDB.Instance().FilterPhrasebookRows( mThePhrase.getText().toString(), getCategorySpinnerData().getValue(), m_queryMethod );
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
}