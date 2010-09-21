using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
using MyFinnishPhrasebookNamespace.Properties;

namespace MyFinnishPhrasebookNamespace
{
	static class Program
	{
		public static bool StartInQuizMode { get; set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				foreach ( string param in args )
				{
					if ( param.ToLower() == "-quiz" )
					{
						StartInQuizMode = true;
					}
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				Application.Run( new MainForm() );
			}
			catch (System.Exception ex)
			{
				MessageBox.Show( string.Format( "{0}\n\n{1}", ex.Message, ex.StackTrace ) );
			}
		}

/*
        static void Main(string[] args)
        {
            //var path_to_db = @"C:\places.sqlite"; // copied here to avoid long path
            SQLiteConnection sqlite_connection = new SQLiteConnection(Settings.Default.MPBConnectionString);

            SQLiteCommand sqlite_command = sqlite_connection.CreateCommand();

            sqlite_connection.Open();

            sqlite_command.CommandText = "select * from Categories";

            SQLiteDataReader sqlite_datareader = sqlite_command.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                // Prints out the url field from the table:
                System.Console.WriteLine(sqlite_datareader["_name"]);
            }
        }
*/
	}
}
