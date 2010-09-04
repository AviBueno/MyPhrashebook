package skydiver.dev;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.Toast;

public class QuizForm extends Activity {
	private String mTheQuestion;
	private String mTheAnswer;
	
	private OnClickListener mButtonsListener = new OnClickListener() {
	    public void onClick(View v) {
	        // Perform action on clicks
	        Button b = (Button) v;
	        String sUserAnswer = b.getText().toString();
	        String toastMsg = sUserAnswer.equals(mTheAnswer) ? "Correct" : "Try Again";
	        Toast.makeText(QuizForm.this, toastMsg, Toast.LENGTH_SHORT).show();
	    }
	};	
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.quiz);
        
        InitCategoriesSpinner();
        InitLanguageSpinner();

//        ViewGroup buttonsPanel = (ViewGroup)findViewById(R.id.panelAnswerButtons);
//        LayoutInflater li = getLayoutInflater();
//        //Button b = (Button)li.inflate(R.layout.quiz_answer_button, null);
//        Button b = null;
//        b = (Button)ViewGroup.inflate(getApplicationContext(), R.layout.quiz_answer_button, null);
//        b.setText("Hallo!");
//
//        LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FILL_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT);
//        buttonsPanel.addView( b, layoutParams );
//        //buttonsPanel.addView( (Button)ViewGroup.inflate(getApplicationContext(), R.layout.quiz_answer_button, null) );
//        buttonsPanel.recomputeViewAttributes(b);
        ViewGroup buttonsPanel = (ViewGroup)findViewById(R.id.panelAnswerButtons);
        int nChildren = buttonsPanel.getChildCount();
        for ( int i = 0; i < nChildren; i++ )
        {
        	View child = buttonsPanel.getChildAt(i);
        	if ( child instanceof Button )
        	{
        		Button b = (Button)child;
        		b.setOnClickListener( mButtonsListener );
        	}
        }
	}
    
    private void InitCategoriesSpinner()
    {
        Spinner langSpinner = (Spinner)findViewById(R.id.spinCategory);

        ArrayAdapter<SpinnerData> adapter = 
            new ArrayAdapter<SpinnerData>( 
	                this,
	                android.R.layout.simple_spinner_item
                );

        adapter.add(new SpinnerData( "Food", "Food" ));
        adapter.add(new SpinnerData( "Questions", "Question" ));
        adapter.add(new SpinnerData( "Colors", "Color" ));
        
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        langSpinner.setAdapter(adapter);
        langSpinner.setOnItemSelectedListener(
            new AdapterView.OnItemSelectedListener()
            {
                public void onItemSelected(
                        AdapterView<?> parent, 
                        View view, 
                        int position, 
                        long id)
                {
                    SpinnerData d = (SpinnerData)parent.getAdapter().getItem( position );
                    Toast.makeText( QuizForm.this, d.getValue(), Toast.LENGTH_SHORT ).show();
                }

                public void onNothingSelected(AdapterView<?> parent) {
                }
            }
        );
    }

    private void InitLanguageSpinner()
    {
        Spinner langSpinner = (Spinner)findViewById(R.id.spinLanguage);
        ArrayAdapter<SpinnerData> adapter = 
            new ArrayAdapter<SpinnerData>( 
	                this,
	                android.R.layout.simple_spinner_item
                );
        
        adapter.add(new SpinnerData( "Both", "Both" ));
        adapter.add(new SpinnerData( "English", MyPhrasebookDB.FLD_LANG1 ));
        adapter.add(new SpinnerData( "Finnish", MyPhrasebookDB.FLD_LANG2 ));
        
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        langSpinner.setAdapter(adapter);
        langSpinner.setOnItemSelectedListener(
            new AdapterView.OnItemSelectedListener()
            {
                public void onItemSelected(
                        AdapterView<?> parent, 
                        View view, 
                        int position, 
                        long id)
                {
                    SpinnerData d = (SpinnerData)parent.getAdapter().getItem( position );
                    Toast.makeText(QuizForm.this, d.getValue(), Toast.LENGTH_SHORT).show();
                }

                public void onNothingSelected(AdapterView<?> parent) {
                }
            }
        );
    }
    
    class SpinnerData {
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

        String mSpinnerText;
        String mValue;
    }
}
