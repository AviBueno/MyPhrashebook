package skydiver.dev;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;

public class DialogBuilder {
	//private static final DialogCommandWrapper DISMISS_DLG_CMD = new DialogCommandWrapper(Command.NO_OP);

	public static AlertDialog.Builder buildMessageBox(
			final Context context,
			final String titleText,
			final String bodyText
		)
	{
		return DialogBuilder.buildMessageBox(
				context,
				titleText,
				bodyText,
				R.string.OK,
				Command.NO_OP
			);
	}
	
	public static AlertDialog.Builder buildYesNoDialog(
				final Context context,
				final String titleText,
				final String bodyText,
				final Command yesCommand,
				final Command noCommand
			)
	{
		return DialogBuilder.buildYesNoDialog(
				context,
				titleText,
				bodyText,
				R.string.Yes,
				yesCommand,
				R.string.No,
				noCommand
			);
	}	
	
	public static AlertDialog.Builder buildMessageBox(
			final Context context,
			final String titleText,
			final String bodyText,
			final int okStringId,
			final Command okCommand
		)
	{
		AlertDialog.Builder builder = new AlertDialog.Builder(context);
		builder.setCancelable(true);
		builder.setTitle(titleText);
		builder.setMessage(bodyText);
		builder.setInverseBackgroundForced(true);
		builder.setPositiveButton(context.getString(okStringId), new DialogCommandWrapper(okCommand));
		return builder;
	}

	public static AlertDialog.Builder buildYesNoDialog(
			final Context context,
			final String titleText,
			final String bodyText,
			final int yesStringId,
			final Command yesCommand,
			final int noStringId,
			final Command noCommand
		)
	{
		AlertDialog.Builder builder = new AlertDialog.Builder(context);
		builder.setCancelable(true);
		builder.setTitle(titleText);
		builder.setMessage(bodyText);
		builder.setInverseBackgroundForced(true);
		builder.setPositiveButton(context.getString(yesStringId), new DialogCommandWrapper(yesCommand));
		builder.setNegativeButton(context.getString(noStringId), new DialogCommandWrapper(noCommand));
		
		return builder;
	}

	static final class DialogCommandWrapper implements DialogInterface.OnClickListener {
		private Command command;

		public DialogCommandWrapper(Command command) {
			this.command = command;
		}

		@Override
		public void onClick(DialogInterface dialog, int which) {
			dialog.dismiss();
			command.execute();
		}
	}
}
