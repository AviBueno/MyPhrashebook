package skydiver.dev;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

import skydiver.dev.MyPhrasebookDB.TblCat2Phrase;
import skydiver.dev.MyPhrasebookDB.TblPhrasebook;
import android.database.Cursor;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageButton;

public class QuizFormMultiChoice extends QuizFormBase
{
	public enum QuizLevel { Easy, Medium, Hard };

	private QuizLevel mQuizLevel;
	private ArrayList<Button> mAnswerButtons = new ArrayList<Button>();
	private ImageButton mRevealButton;
	private ViewGroup mAnswerButtonsPanel;
	private boolean mGuessingOn;
	private HashMap<QuizLevel, QuizLevelData> mQuizLevelDataMap;

	@Override
	protected QuizType getQuizType() {
		return QuizType.MultiChoice;
	}

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		// Get a reference to certain views
		mRevealButton = (ImageButton)findViewById(R.id.bttRevealAnswer);
		mRevealButton.setOnClickListener( mButtonsListener );
		mAnswerButtonsPanel = (ViewGroup)findViewById(R.id.panelAnswerButtons);
	}

	private static final String GUESSING_FLAG = "GF";

	@Override
	protected void saveState(String pageId)
	{
		super.saveState(pageId);

		// Save relevant data
		MPBApp.getInstance().set( GUESSING_FLAG + pageId, mGuessingOn );
	}

	@Override
	protected void restoreState(String pageId)
	{
		// Load values that are persisted between application sessions
		mGuessingOn = MPBApp.getInstance().get( GUESSING_FLAG + pageId, true );
		InitQuizLevels();
		InitAnswerButtons();

		super.restoreState(pageId);
	}

//	@Override
//	protected void onPause() {
//		super.onPause();
//		saveState();
//	}
//
//	@Override
//	protected void onResume() {
//		restoreState();
//		super.onResume();
//	}

	@Override
	protected boolean onPreDrawQuestion() {
		super.onPreDrawQuestion();
		showGuessingButton( mGuessingOn );
		return true;
	}

	@Override
	protected void onPostDrawQuestion() {
		initAnswers();
		super.onPostDrawQuestion();
	}

	private void initAnswers() {
		// Create a hash for already used answer rows
		HashSet<Integer> alreadyUsedAnswerRows = new HashSet<Integer>();
		Cursor answerRows = getCat2PhraseRows();

		// Add the Q/A row to the list of already used rows
		alreadyUsedAnswerRows.add( getQuestionRowIdx() );

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
			String sOptionalQuestion = getQuestion( isQuestionInLang1(), optAnsRow );
			if ( sOptionalQuestion.equals( getTheQuestion() ) ) // Same as THE question?
			{
				 // Skip this row because it probably is a different answer for THE SAME question
				continue;
			}

			// Get the optional answer
			String sOptionalAnswer = getAnswer( isQuestionInLang1(), optAnsRow );

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
				sAnswer = getTheAnswer();
			}
			else
			{
				sAnswer = optionalAnswers[ nOptionalAnswersIdx++ ];
			}

			mAnswerButtons.get( i ).setText(sAnswer);	// Set the answer text to the button
		}
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

				if ( isAnswerCorrect( sUserAnswer ) ) // Correct!
				{
					drawNextQuestion();
				}
				else
				{
					MPBApp.getInstance().ShortToast( R.string.TryAgain );
				}
			}
		}
	};

	private void showGuessingButton( boolean show )
	{
		mRevealButton.setVisibility( show ? View.VISIBLE : View.GONE );
		mAnswerButtonsPanel.setVisibility( show ? View.GONE : View.VISIBLE );
	}

	private void InitAnswerButtons()
	{
		// Create answer buttons and attach a click listener to them
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

	private void InitQuizLevels()
	{
		mQuizLevelDataMap = new HashMap<QuizLevel, QuizLevelData>();
		mQuizLevelDataMap.put( QuizLevel.Easy, 		new QuizLevelData( getString( R.string.QuizLevelEasy ), 	3 ) );
		mQuizLevelDataMap.put( QuizLevel.Medium, 	new QuizLevelData( getString( R.string.QuizLevelMedium ), 	4 ) );
		mQuizLevelDataMap.put( QuizLevel.Hard,		new QuizLevelData( getString( R.string.QuizLevelHard ), 	5 ) );

		mQuizLevel = MPBApp.getInstance().getQuizLevel( QuizLevel.Medium );
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

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
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
//			DrawQuestion( false );
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
