package skydiver.dev;

import android.content.Context;
import android.graphics.Rect;
import android.text.Editable;
import android.text.InputType;
import android.text.TextWatcher;
import android.util.AttributeSet;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RelativeLayout;

/**
 * EditText wrapper with an X button to clear all text at once.
 * Based on http://arunbadole1209.wordpress.com/2011/12/16/how-to-create-edittext-with-crossx-button-at-end-of-it/
 */
public class ClearableEditText extends RelativeLayout {
	private static final String TAG = "ClearableEditText";
	
	LayoutInflater inflater = null;
	EditText edit_text;
	Button btn_clear;
	boolean mShowKeyboardOnFocus = true;

	public ClearableEditText(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		// TODO Auto-generated constructor stub
		initViews();
	}

	public ClearableEditText(Context context, AttributeSet attrs) {
		super(context, attrs);
		// TODO Auto-generated constructor stub
		initViews();

	}

	public ClearableEditText(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
		initViews();
	}

	void initViews() {
		inflater = (LayoutInflater) getContext().getSystemService(
				Context.LAYOUT_INFLATER_SERVICE);
		inflater.inflate(R.layout.clearable_edit_text, this, true);
		edit_text = (EditText) findViewById(R.id.clearable_edit);
		edit_text.setSingleLine();
		edit_text.setInputType(InputType.TYPE_TEXT_FLAG_NO_SUGGESTIONS);
		
		btn_clear = (Button) findViewById(R.id.clearable_button_clear);
		btn_clear.setVisibility(RelativeLayout.INVISIBLE);
		setClearTextButtonHandler();
		showHideClearButton();
	}

	private void setClearTextButtonHandler() {
		btn_clear.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				clearText();
			}
		});
	}

	public void clearText() {
		edit_text.setText("");
		setFocusToEditText();
	}

	@Override
	protected void onFocusChanged(boolean gainFocus, int direction, Rect previouslyFocusedRect) {
		super.onFocusChanged(gainFocus, direction, previouslyFocusedRect);
		
		if ( gainFocus ) {
			Log.d(TAG, "Control Focus gained");
			setFocusToEditText();
		}
	}

	private void setFocusToEditText() {
		// Set the focus to the edit text box
		if ( edit_text.requestFocus() ) {
			if ( mShowKeyboardOnFocus ) {
	            showInputMethod(edit_text.findFocus());
			}
		}
	}

	private void showInputMethod(View view) {
        InputMethodManager imm = (InputMethodManager) getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
        if (imm != null) {
          imm.showSoftInput(view, 0);
        }
    }
	
	private void showHideClearButton() {
		edit_text.addTextChangedListener(new TextWatcher() {

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				if (s.length() > 0)
					btn_clear.setVisibility(RelativeLayout.VISIBLE);
				else
					btn_clear.setVisibility(RelativeLayout.INVISIBLE);
			}

			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub

			}

			@Override
			public void afterTextChanged(Editable s) {
				// TODO Auto-generated method stub

			}
		});
	}

	public Editable getText() {
		Editable text = edit_text.getText();
		return text;
	}

	/**
	 * Enable/Disable popping up the keyboard upon focus on the text box (default = enabled)
	 * 
	 * @param bShowKeyboardOnFocusFlag
	 */
	public void setShowKeyboardOnFocusFlag( boolean bShowKeyboardOnFocusFlag ) {
		mShowKeyboardOnFocus = bShowKeyboardOnFocusFlag;
	}
}