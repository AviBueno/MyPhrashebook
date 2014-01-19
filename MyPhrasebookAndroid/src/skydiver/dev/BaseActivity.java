package skydiver.dev;

import android.app.Activity;
import android.os.Handler;
import android.view.View;
import android.view.inputmethod.InputMethodManager;

public class BaseActivity extends Activity {

	/**
	 * Ensure the keyboard is popped up upon activity start
	 * by executing a delayed event after a short timeout.
	 * @param v
	 */
	protected void popupKeyboard(final View v) {
		// In order to ensure the keyboard will pop up, we'll do it as a post event after a short delay
		Handler handler = new Handler();
		handler.postDelayed(
					new Runnable() {
						public void run() {
							InputMethodManager inputMethodManager =  (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
							inputMethodManager.toggleSoftInputFromWindow(v.getApplicationWindowToken(), InputMethodManager.SHOW_IMPLICIT, 0);
							v.requestFocus();
						}
					},
					500 // msec delay
				);
	}

}
