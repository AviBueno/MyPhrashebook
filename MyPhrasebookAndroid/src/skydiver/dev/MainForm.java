package skydiver.dev;

import android.app.Activity;
import android.app.Application;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.Toast;

public class MainForm extends Activity {

	/** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
//		launchQuizActivity();
        
        try {
			MyPhrasebookDB.CreateInstance( this.getApplicationContext() );
		} catch (InstantiationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        
        Button dictBtt = (Button)findViewById(R.id.DictionaryButton);
        dictBtt.setOnClickListener( new OnClickListener() {
			@Override
			public void onClick(View v) {
				launchDictionaryActivity();
			}
		});
        
        Button quizBtt = (Button)findViewById(R.id.QuizButton);
        quizBtt.setOnClickListener( new OnClickListener() {
			@Override
			public void onClick(View v) {
				launchQuizActivity();
			}
		});
	}

    @Override
	protected void onDestroy() {
		super.onDestroy();
        MyPhrasebookDB.DestroyInstance();
        Toast.makeText(this.getApplicationContext(), "Thank you for using MPB", Toast.LENGTH_SHORT).show();
	}
    
    private void launchDictionaryActivity()
    {
		Intent i = new Intent(this, PhrasebookForm.class);
		startActivity(i);
    }
    
    private void launchQuizActivity()
    {
    	try
    	{
			Intent i = new Intent(this, QuizForm.class);
			startActivity(i);
        }
    	catch ( Exception e )
    	{
    		String s = e.getMessage();
    		s = s;
    	}
    }
}