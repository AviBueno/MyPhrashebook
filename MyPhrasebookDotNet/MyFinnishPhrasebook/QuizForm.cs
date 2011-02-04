using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyFinnishPhrasebookNamespace
{
	public partial class QuizForm : DialogForm
	{
		const int NUM_OPTIONS = 4;
		const int MIN_RECORDS_FOR_QUIZ = NUM_OPTIONS + 1; // NUM_OPTIONS = The Answers; + 1 = The Question
		Control[] m_AnswerButtons = new Control[ NUM_OPTIONS ];
		HashSet<int> m_AlreadyUsedQuestionRows = new HashSet<int>();
		string m_TheQuestion;
		string m_TheAnswer;
		string[] m_TheOptionalAnswers = new string[ NUM_OPTIONS ];

		enum Language { Finnish, English, Both };
		static Language m_QuestionLanguage = Language.Both;
		Dictionary<object, int> m_Obj2LanguageMap = new Dictionary<object, int>();

		public QuizForm()
		{
			InitializeComponent();

			int nControlIdx = 0;
			foreach ( Button button in tableLayoutPanel1.Controls )
			{
				button.Click += new EventHandler( AnswerButton_Click );	// Assign a click event
				m_AnswerButtons[ nControlIdx++ ] = button;				// And add to buttons array
			}

			// Attach event handlers and map object tags to enums
			InitDropDownMenuItems( ddBttLanguage, new EventHandler( OnLanguageItemClick ), typeof( Language ), m_Obj2LanguageMap );

			foreach ( string categoryId in DBWrapper.Instance.FilterFieldNameToQueryString.Keys )
			{
				string sFilterText = DBWrapper.Instance.FilterFieldNameToQueryString[ categoryId ];
				AddCategoryDropMenu( ddBttCategory, categoryId, sFilterText );
			}

			// Simulate a click in order to update display strings
			OnCategoryItemClick( null, null );
			OnLanguageItemClick( null, null );

			DrawWords();
		}

		private void AddCategoryDropMenu( ToolStripDropDownButton ddButton, string categoryText, string sFilterText )
		{
			ToolStripDropDownItem tsddi = new ToolStripMenuItem();
			tsddi.DisplayStyle = ToolStripItemDisplayStyle.Text;
			tsddi.Text = categoryText;
			tsddi.Tag = sFilterText;
			tsddi.Click += new EventHandler( OnCategoryItemClick );
			ddButton.DropDownItems.Add( tsddi );
		}

		private void InitDropDownMenuItems( ToolStripDropDownButton dropDownButton, EventHandler eventHandler, Type enumType, Dictionary<object, int> map )
		{
			string[] names = Enum.GetNames( enumType );
			Array values = Enum.GetValues( enumType );

			foreach ( ToolStripItem tsi in dropDownButton.DropDownItems )
			{
				ToolStripDropDownItem tsddi = tsi as ToolStripDropDownItem;
				if ( tsddi != null )	// e.g. Not Seperator
				{
					tsddi.Click += eventHandler;
					map[ tsddi ] = (int)Enum.Parse( enumType, tsddi.Tag.ToString() );
				}
			}
		}

		string m_CategoryFilter = "_catID = 0";
		void OnCategoryItemClick( object sender, EventArgs e )
		{
			string sCategoryText = "All";
			string sCategoryFilter = "";

			ToolStripDropDownItem tsddi = sender as ToolStripDropDownItem;
			if ( tsddi != null )
			{
				sCategoryFilter = tsddi.Tag.ToString();
				if ( sCategoryFilter != m_CategoryFilter )
				{
					m_CategoryFilter = sCategoryFilter;
					m_AlreadyUsedQuestionRows.Clear();	// New quiz - any row is possible
					sCategoryText = tsddi.Text;
					DrawWords();
				}
			}

			ddBttCategory.Text = sCategoryText;
		}

		void OnLanguageItemClick( object sender, EventArgs e )
		{
			if ( sender != null )
			{
				Language ql = (Language)m_Obj2LanguageMap[ sender ];

				if ( ql != m_QuestionLanguage )
				{
					m_QuestionLanguage = ql;
					DrawWords();
				}
			}

			ddBttLanguage.Text = m_QuestionLanguage.ToString();
		}

		private void DrawWords()
		{
			Random r = new Random();

			bool bFinnishQuestion;

			// Decide if to ask in Finnish or English
			if ( m_QuestionLanguage == Language.Finnish)
			{
				bFinnishQuestion = true;
			}
			else if (m_QuestionLanguage == Language.English) 
			{
				bFinnishQuestion = false;
			}
			else // Both
			{
				bFinnishQuestion = (r.Next( 2 ) == 0) ? false : true;
			}

			MPBDataSet.Cat2PhraseDataTable dt = DBWrapper.Instance.Cat2PhraseDataTable;
			DataRow[] quizRows = dt.Select( m_CategoryFilter );


			if ( quizRows.Length < MIN_RECORDS_FOR_QUIZ )
			{
				MessageBox.Show(
								string.Format( "There are not enough db. records in the selected category!\nMinimum number of records is {0}",
												MIN_RECORDS_FOR_QUIZ )
							);

				return;
			}

			if ( m_AlreadyUsedQuestionRows.Count >= quizRows.Length )
			{
				// Reset the already used question rows in case no rows are left to choose from.
				m_AlreadyUsedQuestionRows.Clear();
			}

			// Find a question/answer row that was not yet used during
			// the lifetime of this form
			int nQuestionRowIdx;
			do
			{
				nQuestionRowIdx = r.Next( quizRows.Length );
			} while ( m_AlreadyUsedQuestionRows.Contains( nQuestionRowIdx ) );

			// Add to the hash of already-used questions
			m_AlreadyUsedQuestionRows.Add( nQuestionRowIdx );

			// Read the question/answer pair
			MPBDataSet.PhrasebookRow questionRow = DBWrapper.Instance.GetPhraseRowByC2PRow( quizRows[ nQuestionRowIdx ] );

			m_TheQuestion = bFinnishQuestion ? questionRow._language : questionRow._english;
			labelQuestion.Text = m_TheQuestion;
			m_TheAnswer = bFinnishQuestion ? questionRow._english : questionRow._language;

			//////////////////////////////////////////////////////////////////////////
			// ANSWERS
			//////////////////////////////////////////////////////////////////////////
			// Create a hash for already used answer rows
			HashSet<int> alreadyUsedAnswerRows = new HashSet<int>();
			DataRow[] answerRows = quizRows;

			// Add the Q/A row to the list of already used rows
			alreadyUsedAnswerRows.Add( nQuestionRowIdx );

			// Create the answers array with (NUM_OPTIONS - 1) cells because one answer
			// will be TheAnswer the we already chose.
			int nAnswers = (NUM_OPTIONS - 1);
			string[] optionalAnswers = new string[ nAnswers ];
			int numOptionalAnswersInArray = 0;

			// Draw (NUM_OPTIONS - 1) random answers
			while ( numOptionalAnswersInArray < nAnswers )
			{
				// Find a row that was not already used
				int nRowIdx;
				do 
				{
					nRowIdx = r.Next( answerRows.Length );
				} while ( alreadyUsedAnswerRows.Contains(nRowIdx) );

				// Add the idx to the list of already used ones
				alreadyUsedAnswerRows.Add( nRowIdx );

				MPBDataSet.PhrasebookRow optAnsRow = DBWrapper.Instance.GetPhraseRowByC2PRow( answerRows[ nRowIdx ] );

				// First, get the optional row's question
				string sOptionalQuestion = bFinnishQuestion ? optAnsRow._language : optAnsRow._english;
				if ( sOptionalQuestion == m_TheQuestion) // Same as THE question?
				{
					 // Skip this row because it probably is a different answer for THE SAME question
					continue;
				}

				// Get the optional answer
				string sOptionalAnswer = bFinnishQuestion ? optAnsRow._english : optAnsRow._language;

				// Check if this is a new answer, or if it matches any of the
				// already-selected answers.
				// We test this because several different words may have the same meaning.
				bool bNewAnswer = true;
				foreach (string s in optionalAnswers)
				{
					if ( sOptionalAnswer == s )
					{
						bNewAnswer = false;
						break;
					}
				}

				if ( bNewAnswer == false )	// Not a new answer?
				{
					continue;				// Continue searching.
				}

				optionalAnswers[ numOptionalAnswersInArray++ ] = sOptionalAnswer;
			}


			// Select the random location of the correct answer
			int nCorrectAnswerIdx = r.Next( NUM_OPTIONS );

			// Fill the answers
			int nOptionalAnswersIdx = 0;	// We need to keep count of which answer we are using next
			for ( int i = 0; i < NUM_OPTIONS; i++ )
			{
				string sAnswer;
				if ( i == nCorrectAnswerIdx )
				{
					sAnswer = m_TheAnswer;
				}
				else
				{
					sAnswer = optionalAnswers[ nOptionalAnswersIdx++ ];
				}

				m_AnswerButtons[ i ].Text = string.Empty;	// Clean the button's text
				m_TheOptionalAnswers[ i ] = sAnswer;		// Save the answer's text for later (see timer1_Tick)
			}

			questionsCountLabel.Text = string.Format( "{0} / {1}", m_AlreadyUsedQuestionRows.Count, quizRows.Length );
		}

		void AnswerButton_Click( object sender, EventArgs e )
		{
			Button b = sender as Button;
			if ( b.Text == string.Empty )
			{
				RevealAnswers();
			}
			else if ( b.Text == m_TheAnswer )	// Correct answer?
			{
				DrawWords();
			}
			else // Wrong answer
			{
			}

			bttDone.Focus();
		}

		private void bttDone_Click( object sender, EventArgs e )
		{
			OnOK();
		}

		private void RevealAnswers()
		{
			for ( int i = 0; i < NUM_OPTIONS; i++ )
			{
				m_AnswerButtons[ i ].Text = m_TheOptionalAnswers[ i ];
			}
			bttDone.Focus();
		}

		private void QuizForm_Click( object sender, EventArgs e )
		{
			RevealAnswers();
		}

		private void labelQuestion_Click( object sender, EventArgs e )
		{
			RevealAnswers();
		}
	}
}
