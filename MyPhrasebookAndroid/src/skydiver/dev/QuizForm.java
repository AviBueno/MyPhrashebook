package skydiver.dev;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import skydiver.dev.MyPhrasebookDB.TblCat2Phrase;
import skydiver.dev.MyPhrasebookDB.TblPhrasebook;
import android.app.Activity;
import android.database.Cursor;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.TextView;

public class QuizForm extends Activity
{
	public enum QuizLevel { Easy, Medium, Hard }; 
	static final String LANG_ANY = "ANY";
	private static final int DRAW_NEW_QUESTION = -1;
	
	private QuizLevel mQuizLevel;
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
	private ImageButton mRevealButton;
	private ViewGroup mAnswerButtonsPanel;
	private boolean mGuessingOn;
	private MPBApp mMpbApp = MPBApp.getInstance();
	private int mQuestionRowIdx;
	private boolean mQuestionIsInLang1;
	private HashMap<QuizLevel, QuizLevelData> mQuizLevelDataMap;
	
	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.quiz);

		// Get a reference to certain views
		mTxtQuestion = (TextView)findViewById(R.id.txtQuestion);
		mTxtQuestionsCounter = (TextView)findViewById(R.id.txtQuestionsCounter);
		mTxtSuccessPercentage = (TextView)findViewById(R.id.txtSuccessPercentage);
		
		mRevealButton = (ImageButton)findViewById(R.id.bttRevealAnswer);
		mRevealButton.setOnClickListener( mButtonsListener );
	}

	private void saveState()
	{
		// Save relevant data
		mMpbApp.set( "mNumCorrectlyAnswered", mNumCorrectlyAnswered );
		mMpbApp.set( "mNumTotalAnswered", mNumTotalAnswered );					
		mMpbApp.set( "mAlreadyUsedQuestionRows", mAlreadyUsedQuestionRows );

		mMpbApp.setQuizCategory( mSDCategory.getValue() );
		mMpbApp.setQuizLanguage( mSDLanguage.getValue() );
		mMpbApp.set( "mQuestionRowIdx", mQuestionRowIdx );	// Persist the value
		mMpbApp.set( "mGuessingOn", mGuessingOn );
	}
	
	private void restoreState()
	{
		// Load values that are persisted between application sessions
		mAlreadyUsedQuestionRows = mMpbApp.get("mAlreadyUsedQuestionRows");
		mNumCorrectlyAnswered = mMpbApp.get( "mNumCorrectlyAnswered", 0 );
		mNumTotalAnswered = mMpbApp.get( "mNumTotalAnswered", 0 );
		mGuessingOn = mMpbApp.get( "mGuessingOn", true );
		mQuestionRowIdx = mMpbApp.get( "mQuestionRowIdx", DRAW_NEW_QUESTION );
		
		InitCategoriesSpinner();
		InitLanguageSpinner();
		InitQuizLevels();
		InitAnswerButtons();

		LoadQuestions( false );
	}
	
	@Override
	protected void onPause() {
		saveState();
		super.onPause();
	}

	@Override
	protected void onResume() {
		restoreState();
		super.onResume();
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
//					MPBApp.getInstance().ShortToast( answer );
					
					// Draw a new question
					DrawQuestion( true );
				}
				else
				{
					MPBApp.getInstance().ShortToast( R.string.TryAgain );
				}

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
		int nAnswers = getQuizLevelNumAnswers();
		for ( int i = 0; i < nAnswers; i++ )
		{
			Button b = (Button)this.getLayoutInflater().inflate( R.layout.quiz_answer_button, mAnswerButtonsPanel, false );
	   		mAnswerButtonsPanel.addView( b );
			
			b.setOnClickListener( mButtonsListener );
			mAnswerButtons.add( b );	// Add to buttons array
		}
	}

	private void DrawQuestion( boolean bDrawNewQuestion )
	{
		showGuessingButton( mGuessingOn );
		
		// Decide if to ask in LANG1 or LANG2
		if ( mQuestionLanguage.equals( MyPhrasebookDB.TblPhrasebook.LANG1 ) )
		{
			mQuestionIsInLang1 = true;
		}
		else if (mQuestionLanguage.equals( MyPhrasebookDB.TblPhrasebook.LANG2 ) ) 
		{
			mQuestionIsInLang1 = false;
		}
		else // ANY LANGUAGE
		{
			mQuestionIsInLang1 = MPBApp.RNG().nextBoolean();
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
				mQuestionRowIdx = MPBApp.RNG().nextInt( nCat2PhraseRows );
			} while ( mAlreadyUsedQuestionRows.contains( mQuestionRowIdx ) || (mQuestionRowIdx == nLastQuestionRowIdx) );
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
		mTheAnswer = getAnswer( mQuestionIsInLang1, quizRow );

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
				nRowIdx = MPBApp.RNG().nextInt( answerRows.getCount() );
			} while ( alreadyUsedAnswerRows.contains(nRowIdx) );

			// Add the idx to the list of already used ones
			alreadyUsedAnswerRows.add( nRowIdx );

			answerRows.moveToPosition(nRowIdx);			
			int phraseRowId = MyPhrasebookDB.getInt( answerRows, TblCat2Phrase.PHRASE_ID );
			Cursor optAnsRow = MyPhrasebookDB.Instance().GetPhraseRowByID( phraseRowId );
			
			if ( optAnsRow != null ) { optAnsRow.moveToFirst(); }

			// First, get the optional row's question
			String sOptionalQuestion = getQuestion( mQuestionIsInLang1, optAnsRow );
			if ( sOptionalQuestion.equals( mTheQuestion) ) // Same as THE question?
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
		int nCorrectAnswerIdx = MPBApp.RNG().nextInt( nAnswers );

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
		mTxtQuestionsCounter.setText( String.format( "%d / %d [%s]", mAlreadyUsedQuestionRows.size() + 1, mCat2PhraseRows.getCount(), getQuizLevelString() ) );
	}
	
	private void InitQuizLevels()
	{
		mQuizLevelDataMap = new HashMap<QuizLevel, QuizLevelData>();
		mQuizLevelDataMap.put( QuizLevel.Easy, 		new QuizLevelData( getString( R.string.QuizLevelEasy ), 	3 ) );
		mQuizLevelDataMap.put( QuizLevel.Medium, 	new QuizLevelData( getString( R.string.QuizLevelMedium ), 	4 ) );
		mQuizLevelDataMap.put( QuizLevel.Hard,		new QuizLevelData( getString( R.string.QuizLevelHard ), 	5 ) );
		
		mQuizLevel = MPBApp.getInstance().getQuizLevel( QuizLevel.Medium );
	}

	private String getQuizLevelString()
	{
		return mQuizLevelDataMap.get(mQuizLevel).getText();
	}
	
	private int getQuizLevelNumAnswers()
	{
		return mQuizLevelDataMap.get(mQuizLevel).getNumAnswers();
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

		Set<String> categoryTitles = MyPhrasebookDB.Instance().getCategoryTitles();
		String storedSDCategoryValue = mMpbApp.getQuizCategory();

		SpinnerData allSD = null;
		for (String catTitle : categoryTitles)
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
					
					LoadQuestions( true );
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
			// NOTE: For some reason, setSelection(int position) displayed the text of position=0 instead of position.
			// Specifically: when switching off the screen are switching it on again.
			// In order to overcome this (android bug?), we use setSelection(int position, boolean animate).
			spinner.setSelection(nSelPos, true); 
		}
	}
	
	private void LoadQuestions( boolean bReset )
	{
		String catTitle = mSDCategory.getValue();
		mCat2PhraseRows = MyPhrasebookDB.Instance().selectCat2PhraseRowsByCategoryTitle( catTitle );
		
		if ( bReset )
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
		/*
		super.onCreateOptionsMenu(menu);
		menu.add(0, MI_0, 0, R.string.Level1);
		menu.add(0, MI_1, 0, R.string.Level2);
		menu.add(0, MI_2, 0, R.string.Level3);
		menu.add(1, MI_3, 0, mGuessingOn ? R.string.GuessingOff : R.string.GuessingOn );
		return true;
		*/
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.quiz_menu, menu);
        return true;
	}
	
	@Override
	public boolean onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		MenuItem mi = menu.findItem( R.id.GuessingFlag );
		mi.setTitle( mGuessingOn ? R.string.GuessingOff : R.string.GuessingOn );
		return true;
	}

	@Override
	public boolean onMenuItemSelected(int featureId, MenuItem item) {
		QuizLevel newQuizLevel = mQuizLevel;
		switch(item.getItemId()) {
		case R.id.QuizLevelEasy: newQuizLevel = QuizLevel.Easy; break;
		case R.id.QuizLevelMedium: newQuizLevel = QuizLevel.Medium; break;
		case R.id.QuizLevelHard: newQuizLevel = QuizLevel.Hard; break;
			
		case R.id.GuessingFlag:
			mGuessingOn = ! mGuessingOn;
			
			showGuessingButton( mGuessingOn );
			return true;			
		}
		
		if ( newQuizLevel != mQuizLevel )
		{
			mQuizLevel = newQuizLevel;
			MPBApp.getInstance().setQuizLevel( mQuizLevel );
			InitAnswerButtons();
			DrawQuestion( false );
			return true;
		}
	   
		return super.onMenuItemSelected(featureId, item);
	}
	
	class QuizLevelData
	{
		public QuizLevelData( String text, int nAnswers )
		{
			mText = text;
			mNumAnswers = nAnswers;
		}
		
		public String 		getText() { return mText; }
		public int			getNumAnswers() { return mNumAnswers; }

		private String mText;
		private int mNumAnswers;
	}
}
