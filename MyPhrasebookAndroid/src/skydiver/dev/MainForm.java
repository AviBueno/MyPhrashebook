package skydiver.dev;

import android.app.Activity;
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
				Intent i = new Intent(MainForm.this.getApplicationContext(), PhrasebookForm.class);
				startActivity(i);
			}
		});
        
        Button quizBtt = (Button)findViewById(R.id.QuizButton);
        quizBtt.setOnClickListener( new OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent i = new Intent(MainForm.this.getApplicationContext(), QuizForm.class);
				startActivity(i);
			}
		});

        Button maintenanceBtt = (Button)findViewById(R.id.MaintenanceButton);
        maintenanceBtt.setOnClickListener( new OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent i = new Intent(MainForm.this.getApplicationContext(), AddPhraseForm.class);
				startActivity(i);
			}
		});
	}

    @Override
	protected void onDestroy() {
		super.onDestroy();
        MyPhrasebookDB.DestroyInstance();
	}
}