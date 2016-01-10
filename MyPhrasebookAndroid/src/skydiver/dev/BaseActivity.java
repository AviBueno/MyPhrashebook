package skydiver.dev;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Set;

import android.app.Activity;
import android.os.Handler;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

public class BaseActivity extends Activity {

	private SpinnerData mSDCategory;
	private SpinnerData mSDLanguage;

	/**
	 * Ensure the keyboard is popped up upon activity start
	 * by executing a delayed event after a short timeout.
	 * @param v
	 */
	protected void popupKeyboard(final View v) {
		// In order to ensure the keyboard will pop up, we'll do it as a post event after a short delay
		Handler handler = new Handler();
		handler.postDelayed(
					new Runnable() {
						public void run() {
							InputMethodManager inputMethodManager =  (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
							inputMethodManager.toggleSoftInputFromWindow(v.getApplicationWindowToken(), InputMethodManager.SHOW_IMPLICIT, 0);
							v.requestFocus();
						}
					},
					500 // msec delay
				);
	}


	/*
	 * Category Spinner
	 */

	protected SpinnerData getCategorySpinnerData() { return mSDCategory; }

	protected interface CategoryChangedListener {
		public void onCategoryChanged( SpinnerData sd );
	}

	protected void initCategoriesSpinner( String defaultValue, final CategoryChangedListener listener )
	{
		Spinner spinner = (Spinner)findViewById(R.id.spinCategory);

		ArrayAdapter<SpinnerData> adapter =
			new ArrayAdapter<SpinnerData>(
					this,
					android.R.layout.simple_spinner_item
				);

		Set<String> categoryTitles = MyPhrasebookDB.Instance().getCategoryTitles();

		SpinnerData allSD = null;
		for (String catTitle : categoryTitles)
		{
			if ( MyPhrasebookDB.Instance().isRealCategoryTitle( catTitle ) )
			{
				SpinnerData sd = new SpinnerData( catTitle, catTitle );
				adapter.add( sd );

				// Test if this was the prev. selected category
				if ( defaultValue.equals( sd.getValue() ) )
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

					// Fire the event
					listener.onCategoryChanged( mSDCategory );
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

	/**
	 * Language Spinner
	 */

	protected SpinnerData getLanguageSpinnerData() { return mSDLanguage; }

	protected interface LanguageChangedListener {
		public void onLanguageChanged( SpinnerData sd );
	}

	protected void initLanguageSpinner( String defaultValue, final LanguageChangedListener listener )
	{
		Spinner spinner = (Spinner)findViewById(R.id.spinLanguage);
		ArrayAdapter<SpinnerData> adapter =
			new ArrayAdapter<SpinnerData>(
					this,
					android.R.layout.simple_spinner_item
				);

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
					listener.onLanguageChanged( sd );
				}

				public void onNothingSelected(AdapterView<?> parent) {
				}
			}
		);

		List<SpinnerData> items = new ArrayList<SpinnerData>();
		items.add( new SpinnerData( getString(R.string.LangBoth), QuizFormBase.LANG_ANY ) );
		items.add( new SpinnerData( getString(R.string.Lang1), MyPhrasebookDB.TblPhrasebook.LANG1 ) );
		items.add( new SpinnerData( getString(R.string.Lang2), MyPhrasebookDB.TblPhrasebook.LANG2 ) );

		for ( SpinnerData sd : items )
		{
			adapter.add( sd );
			if ( sd.getValue().equals(defaultValue) )
			{
				mSDLanguage = sd;
			}
		}

		// Select the prev. selected category
		int nSelPos = adapter.getPosition(mSDLanguage);
		if ( nSelPos >= 0 )
		{
			// NOTE: For some reason, setSelection(int position) displayed the text of position=0 instead of position.
			// Specifically: when switching off the screen are switching it on again.
			// In order to overcome this (android bug?), we use setSelection(int position, boolean animate).
			spinner.setSelection(nSelPos, true);
		}
	}
}
