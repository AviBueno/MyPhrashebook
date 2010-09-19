package skydiver.dev;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashSet;
import java.util.List;
import java.util.Random;
import java.util.Set;

import skydiver.dev.MyPhrasebookDB.TblCat2Phrase;
import skydiver.dev.MyPhrasebookDB.TblPhrasebook;
import android.app.Activity;
import android.database.Cursor;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

public class QuizForm extends Activity
{
	static final String LANG_ANY = "ANY";
	private static final int MI_0 = 0;
	private static final int MI_1 = 1;
	private static final int MI_2 = 2;
	private static final int MI_3 = 3;
	private static final int DRAW_NEW_QUESTION = -1;
	
	static Random mRandom = new Random();
	private String mTheQuestion;
	private String mTheAnswer;
	private String mQuestionLanguage = QuizForm.LANG_ANY;
	private SpinnerData mSDCategory;
	private SpinnerData mSDLanguage;
	private Cursor mCat2PhraseRows = null;
	private HashSet<Integer> mAlreadyUsedQuestionRows;
	private ArrayList<Button> mAnswerButtons = new ArrayList<Button>();
	private TextView mTxtQuestion;
	private TextView mTxtQuestionsCounter;
	private TextView mTxtSuccessPercentage;
	private int mNumCorrectlyAnswered;
	private int mNumTotalAnswered;
	private Button mRevealButton;
	private ViewGroup mAnswerButtonsPanel;
	private int mNumAnswers = 4;
	private boolean mGuessingOn;
	private MPBApp mMpbApp = null;
	private boolean mInitialized = false;
	private int mQuestionRowIdx;
	private boolean mQuestionIsInLang1;
	
	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.quiz);

		mMpbApp = (MPBApp)getApplicationContext();	// Save a reference of the app's instance
		
		// Get a reference to certain views
		mTxtQuestion = (TextView)findViewById(R.id.txtQuestion);
		mTxtQuestionsCounter = (TextView)findViewById(R.id.txtQuestionsCounter);
		mTxtSuccessPercentage = (TextView)findViewById(R.id.txtSuccessPercentage);
		
		mRevealButton = (Button)findViewById(R.id.bttRevealAnswer);
		mRevealButton.setOnClickListener( mButtonsListener );

		// Load values that are persisted between application sessions
		mAlreadyUsedQuestionRows = mMpbApp.get("mAlreadyUsedQuestionRows");
		mNumCorrectlyAnswered = mMpbApp.get( "mNumCorrectlyAnswered", 0 );
		mNumTotalAnswered = mMpbApp.get( "mNumTotalAnswered", 0 );
		mNumAnswers = mMpbApp.get( "mNumAnswers", 4 );
		mGuessingOn = mMpbApp.get( "mGuessingOn", true );

		// Load temporary persisted values
		mQuestionRowIdx = savedInstanceState != null ? savedInstanceState.getInt("mQuestionRowIdx") : DRAW_NEW_QUESTION;
		
		InitCategoriesSpinner();
		InitLanguageSpinner();
		InitAnswerButtons();		
		
		ResetQuestions();
		
		mInitialized = true;
	}

	@Override
	protected void onSaveInstanceState(Bundle outState)
	{		
		outState.putInt("mQuestionRowIdx", mQuestionRowIdx);

		super.onSaveInstanceState(outState);
	}

	private OnClickListener mButtonsListener = new OnClickListener() {
		public void onClick(View v) {
			if ( mRevealButton.getVisibility() != View.GONE )
			{
				showGuessingButton( false );
			}
			else
			{
				// Perform action on clicks
				Button b = (Button) v;
				String sUserAnswer = b.getText().toString();
	
				mNumTotalAnswered++;
				
				if ( sUserAnswer.equals(mTheAnswer) ) // Correct!
				{
					mNumCorrectlyAnswered++;
					
					// Add to the hash of already-used questions
					mAlreadyUsedQuestionRows.add( mQuestionRowIdx );

					// TODO TBD...
//					String answer = String.format("%s\n\n%s", mTheQuestion, mTheAnswer);
//					Toast.makeText(QuizForm.this, answer, Toast.LENGTH_SHORT).show();
					
					// Draw a new question
					DrawQuestion( true );
				}
				else
				{
					Toast.makeText(QuizForm.this.getApplicationContext(), R.string.TryAgain, Toast.LENGTH_SHORT).show();
				}

				// Save relevant data
				mMpbApp.set( "mNumCorrectlyAnswered", mNumCorrectlyAnswered );
				mMpbApp.set( "mNumTotalAnswered", mNumTotalAnswered );					
				mMpbApp.set( "mAlreadyUsedQuestionRows", mAlreadyUsedQuestionRows );

				// Update UI
				updatePercentageUI();					
			}
		}
	};
	
	private void showGuessingButton( boolean show )
	{
		mRevealButton.setVisibility( show ? View.VISIBLE : View.GONE );
		mAnswerButtonsPanel.setVisibility( show ? View.GONE : View.VISIBLE );
	}

	private void updatePercentageUI()
	{
		int nPercentage = (int)(((float)mNumCorrectlyAnswered / mNumTotalAnswered) * 100);
		mTxtSuccessPercentage.setText( String.format( "%d / %d (%d%%)", mNumCorrectlyAnswered, mNumTotalAnswered, nPercentage ) );		
	}
	
	private void InitAnswerButtons()
	{
		// Create answer buttons and attach a click listener to them
		mAnswerButtonsPanel = (ViewGroup)findViewById(R.id.panelAnswerButtons);
		mAnswerButtonsPanel.removeAllViews();
		mAnswerButtons.clear();
		for ( int i = 0; i < mNumAnswers; i++ )
		{
			Button b = (Button)this.getLayoutInflater().inflate( R.layout.quiz_answer_button, mAnswerButtonsPanel, false );
	   		mAnswerButtonsPanel.addView( b );
			
			b.setOnClickListener( mButtonsListener );
			mAnswerButtons.add( b );	// Add to buttons array
		}
		
		mMpbApp.set( "mNumAnswers", mNumAnswers );
	}

	private void DrawQuestion( boolean bDrawNewQuestion )
	{
		showGuessingButton( mGuessingOn );
		
		if ( ! mInitialized )
		{
			mQuestionIsInLang1 = mMpbApp.get( "mQuestionIsInLang1", mQuestionIsInLang1 );
		}
		else
		{
			// Decide if to ask in LANG1 or LANG2
			if ( mQuestionLanguage == MyPhrasebookDB.TblPhrasebook.LANG1 )
			{
				mQuestionIsInLang1 = true;
			}
			else if (mQuestionLanguage == MyPhrasebookDB.TblPhrasebook.LANG2 ) 
			{
				mQuestionIsInLang1 = false;
			}
			else // ANY LANGUAGE
			{
				mQuestionIsInLang1 = mRandom.nextBoolean();
			}
			
			mMpbApp.set( "mQuestionIsInLang1", mQuestionIsInLang1 );
		}
		
		// Reset the already used question rows in case no rows are left to choose from.
		if ( mAlreadyUsedQuestionRows.size() >= mCat2PhraseRows.getCount() )
		{
			mAlreadyUsedQuestionRows.clear();
		}

		// Find a question/answer row that was not yet used during
		// the lifetime of this form
		int nCat2PhraseRows = mCat2PhraseRows.getCount();
		
		if ( bDrawNewQuestion )
		{
			// nLastQuestionRowIdx will be used to make sure we don't re-select the 
			// last question (in the case that all questions were asked and we now
			// start a new round of questions).
			int nLastQuestionRowIdx = mQuestionRowIdx; 
			do
			{
				mQuestionRowIdx = mRandom.nextInt( nCat2PhraseRows );
			} while ( mAlreadyUsedQuestionRows.contains( mQuestionRowIdx ) || (mQuestionRowIdx == nLastQuestionRowIdx) );
			
			mMpbApp.set( "mQuestionRowIdx", mQuestionRowIdx );	// Persist the value
		}

		// Read the question/answer pair
		Cursor quizRow = null;
		if ( mCat2PhraseRows.moveToPosition( mQuestionRowIdx ) )
		{
			int phraseRowId = MyPhrasebookDB.getInt( mCat2PhraseRows, TblCat2Phrase.PHRASE_ID );
			quizRow = MyPhrasebookDB.Instance().GetPhraseRowByID( phraseRowId );
		}
		
		if ( quizRow != null )
		{
			quizRow.moveToFirst();
		}

	   	// Select the question text
   		mTheQuestion = getQuestion( mQuestionIsInLang1, quizRow );
		
		// Set the form's question text
		mTxtQuestion.setText( mTheQuestion );
		
		// Select the answer text (opposite language of the question)
		mTheAnswer = getAnswer( mQuestionIsInLang1, quizRow );;

		//////////////////////////////////////////////////////////////////////////
		// ANSWERS
		//////////////////////////////////////////////////////////////////////////
		// Create a hash for already used answer rows
		HashSet<Integer> alreadyUsedAnswerRows = new HashSet<Integer>();
		Cursor answerRows = mCat2PhraseRows;

		// Add the Q/A row to the list of already used rows
		alreadyUsedAnswerRows.add( mQuestionRowIdx );
		
		int nAnswers = mAnswerButtons.size();
		int nFalseAnswers = nAnswers - 1;	// -1 because one option will be the answer
		String[] optionalAnswers = new String[ nFalseAnswers ];
		int numOptionalAnswersInArray = 0;

		// Draw (NUM_OPTIONS - 1) random answers
		while ( numOptionalAnswersInArray < nFalseAnswers )
		{
			// Find a row that was not already used
			int nRowIdx;
			do 
			{
				nRowIdx = mRandom.nextInt( answerRows.getCount() );
			} while ( alreadyUsedAnswerRows.contains(nRowIdx) );

			// Add the idx to the list of already used ones
			alreadyUsedAnswerRows.add( nRowIdx );

			answerRows.moveToPosition(nRowIdx);			
			int phraseRowId = MyPhrasebookDB.getInt( answerRows, TblCat2Phrase.PHRASE_ID );
			Cursor optAnsRow = MyPhrasebookDB.Instance().GetPhraseRowByID( phraseRowId );
			
			if ( optAnsRow != null ) { optAnsRow.moveToFirst(); }

			// First, get the optional row's question
			String sOptionalQuestion = getQuestion( mQuestionIsInLang1, optAnsRow );
			if ( sOptionalQuestion == mTheQuestion) // Same as THE question?
			{
				 // Skip this row because it probably is a different answer for THE SAME question
				continue;
			}

			// Get the optional answer
			String sOptionalAnswer = getAnswer( mQuestionIsInLang1, optAnsRow );

			// Check if this is a new answer, or if it matches any of the
			// already-selected answers.
			// We test this because several different words may have the same meaning.
			boolean bNewAnswer = true;
			for ( String s : optionalAnswers)
			{
				if ( sOptionalAnswer.equals( s ) )
				{
					bNewAnswer = false;
					break;
				}
			}

			if ( bNewAnswer == false )	// Not a new answer?
			{
				continue;				// Continue searching.
			}

			optionalAnswers[ numOptionalAnswersInArray++ ] = sOptionalAnswer;
		}

		// Select the random location of the correct answer
		int nCorrectAnswerIdx = mRandom.nextInt( nAnswers );

		// Fill the answers
		int nOptionalAnswersIdx = 0;	// We need to keep count of which answer we are using next
		for ( int i = 0; i < nAnswers; i++ )
		{
			String sAnswer;
			if ( i == nCorrectAnswerIdx )
			{
				sAnswer = mTheAnswer;
			}
			else
			{
				sAnswer = optionalAnswers[ nOptionalAnswersIdx++ ];
			}

			mAnswerButtons.get( i ).setText(sAnswer);	// Set the answer text to the button
		}

		// Update quiz counter
		mTxtQuestionsCounter.setText( String.format( "%d / %d", mAlreadyUsedQuestionRows.size() + 1, mCat2PhraseRows.getCount() ) );
	}
	
	private String getQuestion( boolean bQuestionIsInLang1, Cursor row )
	{
		return bQuestionIsInLang1 	? MyPhrasebookDB.getString( row, TblPhrasebook.LANG1 )
									: MyPhrasebookDB.getString( row, TblPhrasebook.LANG2 );		
	}
	
	private String getAnswer( boolean bQuestionIsInLang1, Cursor row )
	{
		return bQuestionIsInLang1 	? MyPhrasebookDB.getString( row, TblPhrasebook.LANG2 )
									: MyPhrasebookDB.getString( row, TblPhrasebook.LANG1 );		
	}
	
	private void InitCategoriesSpinner()
	{
		Spinner spinner = (Spinner)findViewById(R.id.spinCategory);

		ArrayAdapter<SpinnerData> adapter = 
			new ArrayAdapter<SpinnerData>( 
					this,
					android.R.layout.simple_spinner_item
				);

		Set<String> categoryNames = MyPhrasebookDB.Instance().getQuizCategoryNames();
		String storedSDCategoryValue = mMpbApp.getQuizCategory();

		SpinnerData allSD = null;
		for (String catName : categoryNames)
		{
			SpinnerData sd = new SpinnerData( catName, catName );
			adapter.add( sd );
			
			// Test if this was the prev. selected category
			if ( storedSDCategoryValue.equals( sd.getValue() ) )
			{
				mSDCategory = sd;
			}
			
			// Remember the "All" object
			if ( catName.equals( MyPhrasebookDB.TblCategories.VAL_ALL ) )
			{
				allSD = sd;
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

					// Save the category
					mSDCategory = sd;
					String sCategory = sd.getValue();					
					mMpbApp.setQuizCategory(sCategory);
					
					ResetQuestions();
				}

				public void onNothingSelected(AdapterView<?> parent) {
				}
			}
		);
		
		// Select the prev. selected category
		int nSelPos = adapter.getPosition(mSDCategory);
		if ( nSelPos >= 0 )
		{
			spinner.setSelection(nSelPos);
		}
	}

	private void InitLanguageSpinner()
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
					if ( sd == mSDLanguage )
					{
						return;
					}

					mSDLanguage = sd;
					mQuestionLanguage = sd.getValue();
					mMpbApp.setQuizLanguage(mQuestionLanguage);

					DrawQuestion( false );
				}

				public void onNothingSelected(AdapterView<?> parent) {
				}
			}
		);
		
		List<SpinnerData> items = new ArrayList<SpinnerData>();
		items.add( new SpinnerData( getString(R.string.LangBoth), QuizForm.LANG_ANY ) );
		items.add( new SpinnerData( getString(R.string.Lang1), MyPhrasebookDB.TblPhrasebook.LANG1 ) );
		items.add( new SpinnerData( getString(R.string.Lang2), MyPhrasebookDB.TblPhrasebook.LANG2 ) );
		
		mQuestionLanguage = mMpbApp.getQuizLanguage();
		for ( SpinnerData sd : items )
		{
			adapter.add( sd );
			if ( sd.getValue().equals(mQuestionLanguage) )
			{
				mSDLanguage = sd;
			}
		}
		
		// Select the prev. selected category
		int nSelPos = adapter.getPosition(mSDLanguage);
		if ( nSelPos >= 0 )
		{
			spinner.setSelection(nSelPos);
		}
	}
	
	private void ResetQuestions()
	{
		String catName = mSDCategory.getValue();
		mCat2PhraseRows = MyPhrasebookDB.Instance().selectCat2PhraseRowsByQuizCatName( catName );
		
		if ( mInitialized )
		{
			mAlreadyUsedQuestionRows.clear();
			mNumCorrectlyAnswered = 0;
			mNumTotalAnswered = 0;
			mQuestionRowIdx = DRAW_NEW_QUESTION;
		}
		
		updatePercentageUI();		

		boolean bDrawNewQuestion = (mQuestionRowIdx == QuizForm.DRAW_NEW_QUESTION) ? true : false;
		DrawQuestion( bDrawNewQuestion );
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		super.onCreateOptionsMenu(menu);
		menu.add(0, MI_0, 0, R.string.Level1);
		menu.add(0, MI_1, 0, R.string.Level2);
		menu.add(0, MI_2, 0, R.string.Level3);
		menu.add(0, MI_3, 0, mGuessingOn ? R.string.GuessingOff : R.string.GuessingOn );
		return true;
	}
	
	@Override
	public boolean onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		MenuItem mi = menu.findItem( MI_3 );
		mi.setTitle( mGuessingOn ? R.string.GuessingOff : R.string.GuessingOn );
		return true;
	}

	@Override
	public boolean onMenuItemSelected(int featureId, MenuItem item) {
		switch(item.getItemId()) {
		case MI_0:
			mNumAnswers = 3;
			InitAnswerButtons();
			DrawQuestion( false );
			return true;
			
		case MI_1:
			mNumAnswers = 4;
			InitAnswerButtons();
			DrawQuestion( false );
			return true;
			
		case MI_2:
			mNumAnswers = 5;
			InitAnswerButtons();
			DrawQuestion( false );
			return true;
			
		case MI_3:
			mGuessingOn = ! mGuessingOn;
			mMpbApp.set( "mGuessingOn", mGuessingOn );
			
			showGuessingButton( mGuessingOn );
			return true;			
		}
	   
		return super.onMenuItemSelected(featureId, item);
	}
	
	/*
	 * Spinner Data: Data structure for spinner controls 
	 */	
	class SpinnerData implements Comparable<SpinnerData> {
		public SpinnerData( String spinnerText, String value ) {
			this.mSpinnerText = spinnerText;
			this.mValue = value;
		}

		public String getSpinnerText() {
			return mSpinnerText;
		}

		public String getValue() {
			return mValue;
		}

		public String toString() {
			return mSpinnerText;
		}

		public int compareTo( SpinnerData other )
		{
			return this.mSpinnerText.compareTo(other.mSpinnerText);
		}
		
		String mSpinnerText;
		String mValue;
	}
}
