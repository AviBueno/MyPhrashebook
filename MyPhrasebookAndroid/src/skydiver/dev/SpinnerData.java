package skydiver.dev;

/*
 * Spinner Data: Data structure for spinner controls 
 */	
public class SpinnerData implements Comparable<SpinnerData> {
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

	public int compareTo( SpinnerData other )
	{
		return this.mSpinnerText.compareTo(other.mSpinnerText);
	}
	
	private String mSpinnerText;
	private String mValue;
}
