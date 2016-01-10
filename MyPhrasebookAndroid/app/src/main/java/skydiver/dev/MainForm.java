package skydiver.dev;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;

public class MainForm extends Activity {

	/** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
        Button dictBtt = (Button)findViewById(R.id.PhrasebookButton);
        dictBtt.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				Intent i = new Intent(MainForm.this.getApplicationContext(), PhrasebookForm.class);
				startActivity(i);
			}
		});
        
        Button quizBtt = (Button)findViewById(R.id.QuizButtonMultiChoice);
        quizBtt.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				Intent i = new Intent(MainForm.this.getApplicationContext(), QuizFormMultiChoice.class);
				startActivity(i);
			}
		});

        quizBtt = (Button)findViewById(R.id.QuizButtonWriting);
        quizBtt.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				Intent i = new Intent(MainForm.this.getApplicationContext(), QuizFormWriting.class);
				startActivity(i);
			}
		});

        Button maintenanceBtt = (Button)findViewById(R.id.MaintenanceButton);
        maintenanceBtt.setOnClickListener( new OnClickListener() {
			public void onClick(View v) {
				Intent i = new Intent(MainForm.this.getApplicationContext(), AddEditPhraseForm.class);
				startActivity(i);
			}
		});
	}

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
        	// Clear application preferences
        	MPBApp.getInstance().clearAllSharedSettings();

			MPBApp.getInstance().ShortToast( "Thank you for using MPB" );

			
			this.finish();
			
			return true;
        }

        return super.onKeyDown(keyCode, event);
    }
    
    @Override
	protected void onDestroy() {
        MyPhrasebookDB.DestroyInstance();
		super.onDestroy();
	}
}