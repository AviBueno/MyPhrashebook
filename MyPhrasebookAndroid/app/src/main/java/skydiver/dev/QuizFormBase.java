package skydiver.dev;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Set;

import skydiver.dev.MyPhrasebookDB.TblCat2Phrase;
import skydiver.dev.MyPhrasebookDB.TblPhrasebook;
import android.database.Cursor;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.ViewFlipper;

public abstract class QuizFormBase extends BaseActivity {
	public enum QuizType { MultiChoice, Written };
	
	static final String LANG_ANY = "ANY";
	private static final int DRAW_NEW_QUESTION = -1;
	
	private String mTheQuestion;
	private String mTheAnswer;
	private String mQuestionLanguage = QuizFormBase.LANG_ANY;
	private SpinnerData mSDCategory;
	private SpinnerData mSDLanguage;
	private Cursor mCat2PhraseRows = null;
	private HashSet<Integer> mAlreadyUsedQuestionRows;
	private TextView mTxtQuestion;
	private TextView mTxtQuestionsCounter;
	private TextView mTxtSuccessPercentage;
	private int mNumCorrectlyAnswered;
	private int mNumTotalAnswered;
	private MPBApp mMpbApp = MPBApp.getInstance();
	private int mQuestionRowIdx = -1;
	private boolean mQuestionIsInLang1;
	private ViewFlipper mViewFlipper;

	// Public API
	protected String getTheQuestion() {
		return mTheQuestion;
	}
	
	protected int getQuestionRowIdx() {
		return mQuestionRowIdx;
	}
	
	protected String getTheAnswer() {
		return mTheAnswer;
	}
	
	protected Cursor getCat2PhraseRows() {
		return mCat2PhraseRows;
	}

	protected abstract QuizType getQuizType(); 
	
	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.quiz);

		// Get a reference to certain views
		mTxtQuestion = (TextView)findViewById(R.id.txtQuestion);
		mTxtQuestionsCounter = (TextView)findViewById(R.id.txtQuestionsCounter);
		mTxtSuccessPercentage = (TextView)findViewById(R.id.txtSuccessPercentage);
		mViewFlipper = (ViewFlipper)findViewById( R.id.viewFlipper );
	}

	
	protected boolean isAnswerCorrect(String answer) {
		mNumTotalAnswered++;

		boolean isCorrectAnswer = checkAnswer( answer );
		if ( isCorrectAnswer ) {
			mNumCorrectlyAnswered++;
		}

		// Update UI
		updatePercentageUI();
		
		return isCorrectAnswer;
	}
	
	private boolean checkAnswer(String userAnswer) {
		Locale answerLocale   = mQuestionIsInLang1 ? MPBApp.LANG2_LOCALE : MPBApp.LANG1_LOCALE;
		return checkAnswer(userAnswer, getTheAnswer(), answerLocale);
	}

	protected boolean checkAnswer(String userAnswer, String realAnswer, Locale answerLocale) {
		return userAnswer.toLowerCase(answerLocale).equals(realAnswer.toLowerCase(answerLocale));
	}

	protected void drawNextQuestion() {
		if ( mQuestionRowIdx >= 0 ) {
			// Add to the hash of already-used questions
			mAlreadyUsedQuestionRows.add( mQuestionRowIdx );
		}
		
		// Draw a new question
		DrawQuestion( true );
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
	}
	
	private void restoreState()
	{
		// Load values that are persisted between application sessions
		mAlreadyUsedQuestionRows = mMpbApp.get("mAlreadyUsedQuestionRows");
		mNumCorrectlyAnswered = mMpbApp.get( "mNumCorrectlyAnswered", 0 );
		mNumTotalAnswered = mMpbApp.get( "mNumTotalAnswered", 0 );
		mQuestionRowIdx = mMpbApp.get( "mQuestionRowIdx", DRAW_NEW_QUESTION );
		
		InitCategoriesSpinner();
		InitLanguageSpinner();
		
		LoadQuestions( false );
		
		mViewFlipper.setDisplayedChild( getQuizType().ordinal() );
	}

	@Override
	protected void onPause() {
		super.onPause();
		saveState();
	}

	@Override
	protected void onResume() {
		super.onResume();
		restoreState();
	}

	private void updatePercentageUI()
	{
		int nPercentage = mNumTotalAnswered > 0 ? (int)(((float)mNumCorrectlyAnswered / mNumTotalAnswered) * 100) : 0;
		mTxtSuccessPercentage.setText( String.format( "%d / %d (%d%%)", mNumCorrectlyAnswered, mNumTotalAnswered, nPercentage ) );
		int nAnswers = mCat2PhraseRows.getCount();
		int nQuestionIndex = nAnswers > 0 ? mAlreadyUsedQuestionRows.size() + 1 : 0;
		mTxtQuestionsCounter.setText( String.format( "%d / %d", nQuestionIndex, nAnswers ) );
	}
	
	/**
	 * Actions to perform before drawing a question
	 * @return boolean true = OK to continue, false = do not draw a question
	 */
	protected boolean onPreDrawQuestion() { return true; }	
	protected void onPostDrawQuestion() {}
	
	protected boolean isQuestionInLang1() {
		return mQuestionIsInLang1;
	}
	
	private void DrawQuestion( boolean bDrawNewQuestion )
	{
		boolean bDrawQuestion = onPreDrawQuestion();
		if ( ! bDrawQuestion )
		{
			mTxtQuestion.setText( getString( R.string.NoRelevantQuestionsFound ) );
			updatePercentageUI();
			return;
		}
		
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

		int nCat2PhraseRows = mCat2PhraseRows.getCount();
		
		// Reset the already used question rows in case no rows are left to choose from.
		if ( mAlreadyUsedQuestionRows.size() >= nCat2PhraseRows )
		{
			mAlreadyUsedQuestionRows.clear();
		}

		// Find a question/answer row that was not yet used during
		// the lifetime of this form		
		if ( bDrawNewQuestion )
		{
			// nLastQuestionRowIdx will be used to make sure we don't re-select the 
			// last question (in the case that all questions were asked and we now
			// start a new round of questions).
			int nLastQuestionRowIdx = mQuestionRowIdx; 
			do
			{
				//Log.v("MPBApp-Quiz", String.format("Number of rows in category: %i", nCat2PhraseRows));
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

		// Update quiz counter
		mTxtQuestionsCounter.setText( String.format( "%d / %d", mAlreadyUsedQuestionRows.size() + 1, mCat2PhraseRows.getCount() ) );
		
		onPostDrawQuestion();
		
		updatePercentageUI();
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
					changeCategory( sd );
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

	protected void onCategoryChanged() {}

	private void changeCategory(SpinnerData sd) {
		if ( sd == mSDCategory ) {
			return;
		}
		
		// Save the category
		mSDCategory = sd;
		String sCategory = sd.getValue();
		mMpbApp.setQuizCategory(sCategory);
		
		LoadQuestions( true );
		
		onCategoryChanged(); // Allow derived classes to perform additional tasks
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
					changeLanguage( sd );
				}

				public void onNothingSelected(AdapterView<?> parent) {
				}
			}
		);
		
		List<SpinnerData> items = new ArrayList<SpinnerData>();
		items.add( new SpinnerData( getString(R.string.LangBoth), QuizFormBase.LANG_ANY ) );
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

	protected void onLanguageChange() {}
	
	private void changeLanguage(SpinnerData sd) {
		if ( sd == mSDLanguage )
		{
			return;
		}

		mSDLanguage = sd;
		mQuestionLanguage = sd.getValue();
		mMpbApp.setQuizLanguage(mQuestionLanguage);

		DrawQuestion( false );
		
		onLanguageChange(); // Allow derived classes to perform additional tasks
	}
	
	private void LoadQuestions( boolean bReset )
	{
		String catTitle = mSDCategory.getValue();
		boolean selectSingleWordsOnly = (getQuizType() == QuizType.Written) ? true : false;
		mCat2PhraseRows = MyPhrasebookDB.Instance().selectCat2PhraseRowsByCategoryTitle( catTitle, selectSingleWordsOnly );
		
		if ( bReset )
		{
			mAlreadyUsedQuestionRows.clear();
			mNumCorrectlyAnswered = 0;
			mNumTotalAnswered = 0;
			mQuestionRowIdx = DRAW_NEW_QUESTION;
		}
		
		updatePercentageUI();		

		boolean bDrawNewQuestion = (mQuestionRowIdx == QuizFormBase.DRAW_NEW_QUESTION) ? true : false;
		DrawQuestion( bDrawNewQuestion );
	}
}
