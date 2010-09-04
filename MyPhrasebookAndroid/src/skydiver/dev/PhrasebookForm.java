package skydiver.dev;

import java.util.HashMap;
import java.util.List;

import skydiver.dev.MyPhrasebookDB.QueryMethod;
import android.app.Activity;
import android.database.Cursor;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleCursorAdapter;

public class PhrasebookForm extends Activity {
	List<HashMap<String, String>> m_items;
	SimpleCursorAdapter m_itemsAdapter;
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
			try
			{
		        super.onCreate(savedInstanceState);
		        setContentView(R.layout.phrasebook);
		        
				Cursor cursor = MyPhrasebookDB.Instance().FilterData("");
		
				// create the adapter and assign it to the list view
				m_itemsAdapter = null;
				m_itemsAdapter = new SimpleCursorAdapter(
						this,
						R.layout.pb_list_item,
						cursor,
						new String[] {MyPhrasebookDB.FLD_LANG1, MyPhrasebookDB.FLD_LANG2},
						new int[] {R.id.LANG1, R.id.LANG2}
					);
				
				// get a reference to the ListView
				ListView lv = (ListView)findViewById(R.id.ListView);
				lv.setAdapter(m_itemsAdapter);        

				// Hook the EditText's text-changed event handler
				EditText editText = (EditText) findViewById(R.id.EditText);
				editText.addTextChangedListener(new TextWatcher(){
			        public void afterTextChanged(Editable s) {}
			        public void beforeTextChanged(CharSequence s, int start, int count, int after) {}
			        public void onTextChanged(CharSequence s, int start, int before, int count) {
						Cursor cursor = MyPhrasebookDB.Instance().FilterData( s.toString() );
						m_itemsAdapter.changeCursor( cursor );
			        }
			    });				
			}
			catch ( Exception e )
			{
				String m = e.getMessage();
				m = m;
			}
		}
}