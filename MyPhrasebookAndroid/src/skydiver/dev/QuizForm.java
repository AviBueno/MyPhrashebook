package skydiver.dev;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Random;

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
	private static final String LANG_ANY = "ANY";
	private static final int MI_0 = 0;
	private static final int MI_1 = 1;
	private static final int MI_2 = 2;
	private static final int MI_3 = 3;
	
	static Random mRandom = new Random();
	private String mTheQuestion;
	private String mTheAnswer;
	private String mQuestionLanguage = QuizForm.LANG_ANY;
	private SpinnerData mSDCategory;
	private Cursor mCat2PhraseRows = null;
	private HashSet<Integer> mAlreadyUsedQuestionRows = new HashSet<Integer>();
	private ArrayList<Button> mAnswerButtons = new ArrayList<Button>();
	private HashMap<Integer, String> mTheOptionalAnswers;
	private TextView mTxtQuestion;
	private TextView mTxtQuestionsCounter;
	private TextView mTxtSuccessPercentage;
	private int mNumCorrectlyAnswered;
	private int mNumTotalAnswered;
	private Button mRevealButton;
	private ViewGroup mAnswerButtonsPanel;
	private int mNumAnswers = 4;
	private boolean mGuessingOn;
	
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
		        	
		        	DrawQuestion();
		        }
		        else
		        {
			        Toast.makeText(QuizForm.this.getApplicationContext(), R.string.TryAgain, Toast.LENGTH_SHORT).show();
			    }
		        
		        setPercentage( mNumCorrectlyAnswered, mNumTotalAnswered );
		    }
	    }
	};
	
	private void showGuessingButton( boolean show )
	{
		mRevealButton.setVisibility( show ? View.VISIBLE : View.GONE );
		mAnswerButtonsPanel.setVisibility( show ? View.GONE : View.VISIBLE );
	}

	private void setPercentage(int nCorrectlyAnswered, int nTotalAnswered)
	{
        int nPercentage = (int)(((float)nCorrectlyAnswered / nTotalAnswered) * 100);
		mTxtSuccessPercentage.setText( String.format( "%d / %d (%d%%)", nCorrectlyAnswered, nTotalAnswered, nPercentage ) );
	}
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.quiz);

        // Get a reference to certain views
        mTxtQuestion = (TextView)findViewById(R.id.txtQuestion);
        mTxtQuestionsCounter = (TextView)findViewById(R.id.txtQuestionsCounter);
        mTxtSuccessPercentage = (TextView)findViewById(R.id.txtSuccessPercentage);
        
        mRevealButton = (Button)findViewById(R.id.bttRevealAnswer);
        mRevealButton.setOnClickListener( mButtonsListener );
        
        mGuessingOn = true;
        
        InitCategoriesSpinner();
        InitLanguageSpinner();
        InitAnswerButtons();        
        
        ResetQuestions();
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
	}

	private void DrawQuestion()
    {
		if ( mGuessingOn )
		{
			showGuessingButton( true );
		}
		
		boolean bQuestionIsInLang1;
		
		// Decide if to ask in LANG1 or LANG2
		if ( mQuestionLanguage == MyPhrasebookDB.TblPhrasebook.LANG1 )
		{
			bQuestionIsInLang1 = true;
		}
		else if (mQuestionLanguage == MyPhrasebookDB.TblPhrasebook.LANG2 ) 
		{
			bQuestionIsInLang1 = false;
		}
		else // ANY LANGUAGE
		{
			bQuestionIsInLang1 = mRandom.nextBoolean();
		}

		// Reset the already used question rows in case no rows are left to choose from.
		if ( mAlreadyUsedQuestionRows.size() >= mCat2PhraseRows.getCount() )
		{
			mAlreadyUsedQuestionRows.clear();
		}

		// Find a question/answer row that was not yet used during
		// the lifetime of this form
		int nQuestionRowIdx;
		do
		{
			nQuestionRowIdx = mRandom.nextInt( mCat2PhraseRows.getCount() );
		} while ( mAlreadyUsedQuestionRows.contains( nQuestionRowIdx ) );

		// Add to the hash of already-used questions
		mAlreadyUsedQuestionRows.add( nQuestionRowIdx );

		// Read the question/answer pair
		Cursor quizRow = null;
	    if ( mCat2PhraseRows.moveToPosition( nQuestionRowIdx ) )
	    {
	    	int phraseRowId = MyPhrasebookDB.getInt( mCat2PhraseRows, TblCat2Phrase.PHRASE_ID );
            quizRow = MyPhrasebookDB.Instance().GetPhraseRowByID( phraseRowId );
	    }
	    
        if ( quizRow != null )
        {
        	quizRow.moveToFirst();
        }

       	// Select the question text
   	    mTheQuestion = getQuestion( bQuestionIsInLang1, quizRow );
	    
	    // Set the form's question text
		mTxtQuestion.setText( mTheQuestion );
		
		// Select the answer text (opposite language of the question)
		mTheAnswer = getAnswer( bQuestionIsInLang1, quizRow );;

		//////////////////////////////////////////////////////////////////////////
		// ANSWERS
		//////////////////////////////////////////////////////////////////////////
		// Create a hash for already used answer rows
		HashSet<Integer> alreadyUsedAnswerRows = new HashSet<Integer>();
		Cursor answerRows = mCat2PhraseRows;

		// Add the Q/A row to the list of already used rows
		alreadyUsedAnswerRows.add( nQuestionRowIdx );
		
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
			String sOptionalQuestion = getQuestion( bQuestionIsInLang1, optAnsRow );
			if ( sOptionalQuestion == mTheQuestion) // Same as THE question?
			{
				 // Skip this row because it probably is a different answer for THE SAME question
				continue;
			}

			// Get the optional answer
			String sOptionalAnswer = getAnswer( bQuestionIsInLang1, optAnsRow );

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
		mTheOptionalAnswers = new HashMap<Integer, String>();
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

			mAnswerButtons.get( i ).setText(sAnswer); // TODO cleanup: mAnswerButtons.get( i ).setText(sAnswer);
			mTheOptionalAnswers.put( i, sAnswer );	// Save the answer's text for later
		}

		mTxtQuestionsCounter.setText( String.format( "%d / %d", mAlreadyUsedQuestionRows.size(), mCat2PhraseRows.getCount() ) );
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

        HashMap<String,String> categoryToQueryMap = MyPhrasebookDB.Instance().getCategoryToQueryMap();
        
        for (HashMap.Entry<String, String> entry : categoryToQueryMap.entrySet())
        {
            String name = entry.getKey();
            String query = entry.getValue();
            SpinnerData sd = new SpinnerData( name, query );
            adapter.add( sd );
            			
            if ( name.equals("All") ) // TODO Get the string from strings.xml
            {
            	mSDCategory = sd; // Default upon first time
            }
        }
        
        adapter.sort(new Comparator<SpinnerData>() {
        	public int compare(SpinnerData object1, SpinnerData object2) {
        		return object1.compareTo(object2);
        	};
        });
        
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
                	mSDCategory = (SpinnerData)parent.getAdapter().getItem( position );
                    ResetQuestions();
                }

				public void onNothingSelected(AdapterView<?> parent) {
                }
            }
        );
    }

    private void InitLanguageSpinner()
    {
        Spinner spinner = (Spinner)findViewById(R.id.spinLanguage);
        ArrayAdapter<SpinnerData> adapter = 
            new ArrayAdapter<SpinnerData>( 
	                this,
	                android.R.layout.simple_spinner_item
                );

        mQuestionLanguage = QuizForm.LANG_ANY; // Save default upon first time
        adapter.add( new SpinnerData( getString(R.string.LangBoth), QuizForm.LANG_ANY ) );
        adapter.add( new SpinnerData( getString(R.string.Lang1), MyPhrasebookDB.TblPhrasebook.LANG1 ) );
        adapter.add( new SpinnerData( getString(R.string.Lang2), MyPhrasebookDB.TblPhrasebook.LANG2 ) );
        
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
                    mQuestionLanguage = sd.getValue();
                   	ResetQuestions();
                }

                public void onNothingSelected(AdapterView<?> parent) {
                }
            }
        );
        
        spinner.setSelection(0);
    }
    
    private void ResetQuestions()
    {
    	mCat2PhraseRows = MyPhrasebookDB.Instance().SelectCat2PhraseRowsByFilter( mSDCategory.getValue() );
    	
    	mAlreadyUsedQuestionRows.clear();
    	
    	mNumCorrectlyAnswered = 0;
    	mNumTotalAnswered = 0;
        setPercentage( mNumCorrectlyAnswered, mNumTotalAnswered );
    	
        DrawQuestion();
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
	        ResetQuestions();
            return true;
            
        case MI_1:
	        mNumAnswers = 4;
	        InitAnswerButtons();
	        ResetQuestions();
            return true;
            
        case MI_2:
	        mNumAnswers = 5;
	        InitAnswerButtons();
	        ResetQuestions();
            return true;
            
        case MI_3:
        	mGuessingOn = ! mGuessingOn;
        	
        	showGuessingButton( mGuessingOn );
            return true;            
        }
       
        return super.onMenuItemSelected(featureId, item);
    }
    
    class SpinnerData implements Comparable {
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

        public int compareTo( Object o )
        {
        	SpinnerData other = (SpinnerData)o;
        	return this.mSpinnerText.compareTo(other.mSpinnerText);
        }
        
        String mSpinnerText;
        String mValue;
    }
}
