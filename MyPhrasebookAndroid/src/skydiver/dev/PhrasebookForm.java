package skydiver.dev;

import java.util.HashMap;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleCursorAdapter;

public class PhrasebookForm extends Activity {

	final static int ADD_PHRASE = 0;
	
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
					Intent i = new Intent(PhrasebookForm.this.getApplicationContext(), AddPhraseForm.class);
					i.putExtra(AddPhraseForm.THE_PHRASE, mThePhrase.getText().toString());
					startActivityForResult(i, ADD_PHRASE);
				}
			});
		}
		catch ( Exception e )
		{
		}
	}

	private void refreshList() {
		Cursor cursor = MyPhrasebookDB.Instance().FilterPhrasebookRows( mThePhrase.getText().toString() );
		m_itemsAdapter.changeCursor( cursor );
	}
    
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if ( requestCode == ADD_PHRASE )
		{
			refreshList();
		}
		else
		{
			super.onActivityResult(requestCode, resultCode, data);
		}
	}
    
    
}