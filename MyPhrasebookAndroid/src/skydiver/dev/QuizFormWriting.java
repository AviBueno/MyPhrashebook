package skydiver.dev;

import java.util.Locale;

import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;

public class QuizFormWriting extends QuizFormBase
{
	private ClearableEditText mWrittenAnswer;
	
	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		mWrittenAnswer = (ClearableEditText)findViewById(R.id.writtenAnswer);
		
		MPBApp.getInstance().setQuizLanguage( MyPhrasebookDB.TblPhrasebook.LANG1 );
		
		InitControls();
	}

	@Override
	protected QuizType getQuizType() {
		return QuizType.Written;
	}

	private void InitControls() {
		Button bttOK = (Button)findViewById(R.id.bttSubmit);
		bttOK.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				String userInput = mWrittenAnswer.getText().toString();
				if ( isAnswerCorrect( userInput ) )
				{
					MPBApp.getInstance().ShortToast("Correct"); // TODO: Replace with a string table entry
					mWrittenAnswer.clearText();
					drawNextQuestion();
				}
				else
				{
					MPBApp.getInstance().ShortToast("Try Again"); // TODO: Replace with a string table entry
				}
			}
		});
		
		Button bttPass = (Button)findViewById(R.id.bttPass);
		bttPass.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// Deliberately check a wrong answer in order to fail and calculate percentage
				isAnswerCorrect( "" );
				
				// Toast the right answer
				String answer = getTheAnswer();
				MPBApp.getInstance().ShortToast(answer);
				
				// Next please.
				mWrittenAnswer.clearText(); // Clear the text
				drawNextQuestion();
			}
		});
		
		// In order to ensure the keyboard will pop up, we'll do it as a post event after a small delay 
		Handler handler = new Handler();
		handler.postDelayed(
				    new Runnable() {
				        public void run() {
				            InputMethodManager inputMethodManager =  (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
				            inputMethodManager.toggleSoftInputFromWindow(mWrittenAnswer.getApplicationWindowToken(), InputMethodManager.SHOW_FORCED, 0);
				            mWrittenAnswer.requestFocus();
				        }
				    },
				    500 // msec delay
				);
	}
	
	@Override
	protected boolean onPreDrawQuestion() {
		int nQuestions = getCat2PhraseRows().getCount();
		return nQuestions > 0;
	}	

	@Override
	protected boolean checkAnswer(String userAnswer, String realAnswer, Locale answerLocale) {
		// Chop the answer at the first non-alphanumeric char (if any) in order
		// to approve a userAnswer = "who" when realAnswer = "Who?", for example.
		// (*) Using Character.isLetterOrDigit() to ensure correct results in any language
		for (int i = 0; i < realAnswer.length(); ++i) {
			if (!Character.isLetterOrDigit(realAnswer.charAt(i))) {
				realAnswer = realAnswer.substring(0, i);
				break;
			}
		}
		
		boolean result = userAnswer.toLowerCase(answerLocale).equals( realAnswer.toLowerCase(answerLocale) );
		return result;
	}
}
