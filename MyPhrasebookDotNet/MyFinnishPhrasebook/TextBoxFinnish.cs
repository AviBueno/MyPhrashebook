using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyFinnishPhrasebookNamespace
{
	public partial class TextBoxFinnish : UserControl
	{
		public TextBox TxtBox { get { return txtBox; } }

		List<ToolStripItem> m_SearchOptionButtons;
		public enum SearchOption { Exact, Contains, StartsWith, EndsWith };
		Dictionary<SearchOption, ToolStripButton> SearchOptionToButtonMap;
		Dictionary<ToolStripButton, SearchOption> ButtonToSearchOptionMap;

		public event EventHandler OnSearchOptionChanged;

		SearchOption m_SearchOption = SearchOption.Contains;
		public SearchOption CurrentSearchOption
		{
			get { return m_SearchOption; }
			set
			{
				m_SearchOption = value;
				CheckSearchButton( SearchOptionToButtonMap[ m_SearchOption ] );
				if ( OnSearchOptionChanged != null )
				{
					OnSearchOptionChanged( this, EventArgs.Empty );
				}
			}
		}

		public TextBoxFinnish()
		{
			InitializeComponent();

			tsBttA.Click += new EventHandler( langToolStripBttClick );
			tsBttO.Click += new EventHandler( langToolStripBttClick );

			InitSearchButtons();
		}

		private void InitSearchButtons()
		{
			SearchOptionToButtonMap = new Dictionary<SearchOption, ToolStripButton>();
			ButtonToSearchOptionMap = new Dictionary<ToolStripButton, SearchOption>();

			m_SearchOptionButtons = new List<ToolStripItem>();
			foreach ( ToolStripItem item in toolStrip1.Items )
			{
				ToolStripButton tsi = item as ToolStripButton;
				if ( tsi != null && tsi != tsBttA && tsi != tsBttO )
				{
					m_SearchOptionButtons.Add( tsi );
					tsi.Click += new EventHandler( OnSearchOptionButtonClick );
					if ( tsi == tsBttExactMatch )
					{
						SearchOptionToButtonMap[ SearchOption.Exact ] = tsi;
						ButtonToSearchOptionMap[ tsi ] = SearchOption.Exact;
					}
					else if ( tsi == tsBttContains )
					{
						SearchOptionToButtonMap[ SearchOption.Contains ] = tsi;
						ButtonToSearchOptionMap[ tsi ] = SearchOption.Contains;
					}
					else if ( tsi == tsBttStartsWith )
					{
						SearchOptionToButtonMap[ SearchOption.StartsWith ] = tsi;
						ButtonToSearchOptionMap[ tsi ] = SearchOption.StartsWith;
					}
					else if ( tsi == tsBttEndsWith )
					{
						SearchOptionToButtonMap[ SearchOption.EndsWith ] = tsi;
						ButtonToSearchOptionMap[ tsi ] = SearchOption.EndsWith;
					}
				}
			}
		}

		void OnSearchOptionButtonClick( object sender, EventArgs e )
		{
			ToolStripButton button = sender as ToolStripButton;
			CurrentSearchOption = ButtonToSearchOptionMap[ button ]; // And update the selected search option
		}

		void CheckSearchButton( ToolStripButton button )
		{
			foreach ( ToolStripButton tsi in m_SearchOptionButtons )
			{
				tsi.Checked = (tsi == button);
			}
		}

		bool m_SearchButtonsVisible = true;
		public bool SearchButtonsVisible
		{
			get { return m_SearchButtonsVisible; }

			set
			{
				if ( value != m_SearchButtonsVisible )
				{
					foreach ( ToolStripItem tsi in toolStrip1.Items )
					{
						if ( tsi != tsBttA && tsi != tsBttO )
						{
							tsi.Visible = value;
						}
					}

					m_SearchButtonsVisible = value;
				}
			}
		}

		private void AddText( string text )
		{
			int i = txtBox.SelectionStart;
			txtBox.SelectedText = text;
			txtBox.Focus();
			txtBox.SelectionStart = i + 1;
			txtBox.SelectionLength = 0;
		}

		private void langToolStripBttClick( object sender, EventArgs e )
		{
			ToolStripButton btt = sender as ToolStripButton;
			if ( btt != null )
			{
				AddText( btt.Text );
			}
		}
	}
}
