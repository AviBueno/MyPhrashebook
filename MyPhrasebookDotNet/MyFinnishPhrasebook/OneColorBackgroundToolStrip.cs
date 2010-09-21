using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MyFinnishPhrasebookNamespace
{
	public class OneColorBackgroundToolStrip : ToolStrip
	{
		public OneColorBackgroundToolStrip()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// CustomColorsToolStrip
			// 

			CustomColorTable colorTable = new CustomColorTable( this.BackColor );
			this.Renderer = new ToolStripProfessionalRenderer( colorTable );

			this.ResumeLayout( false );

		}

		class CustomColorTable : ProfessionalColorTable
		{
			Color m_toolstripColor;
			public CustomColorTable( Color toolstripColor )
			{
				m_toolstripColor = toolstripColor;
			}

/*
			public override Color ButtonCheckedGradientBegin { get { return m_toolstripColor; } }
			public override Color ButtonCheckedGradientEnd { get { return m_toolstripColor; } }
			public override Color ButtonCheckedGradientMiddle { get { return m_toolstripColor; } }
			public override Color ButtonCheckedHighlight { get { return m_toolstripColor; } }
			public override Color ButtonCheckedHighlightBorder { get { return m_toolstripColor; } }
			public override Color ButtonPressedBorder { get { return m_toolstripColor; } }
			public override Color ButtonPressedGradientBegin { get { return m_toolstripColor; } }
			public override Color ButtonPressedGradientEnd { get { return m_toolstripColor; } }
			public override Color ButtonPressedGradientMiddle { get { return m_toolstripColor; } }
			public override Color ButtonPressedHighlight { get { return m_toolstripColor; } }
			public override Color ButtonPressedHighlightBorder { get { return m_toolstripColor; } }
			public override Color ButtonSelectedBorder { get { return m_toolstripColor; } }
			public override Color ButtonSelectedGradientBegin { get { return m_toolstripColor; } }
			public override Color ButtonSelectedGradientEnd { get { return m_toolstripColor; } }
			public override Color ButtonSelectedGradientMiddle { get { return m_toolstripColor; } }
			public override Color ButtonSelectedHighlight { get { return m_toolstripColor; } }
			public override Color ButtonSelectedHighlightBorder { get { return m_toolstripColor; } }
			public override Color CheckBackground { get { return m_toolstripColor; } }
			public override Color CheckPressedBackground { get { return m_toolstripColor; } }
			public override Color CheckSelectedBackground { get { return m_toolstripColor; } }
			public override Color GripDark { get { return m_toolstripColor; } }
			public override Color GripLight { get { return m_toolstripColor; } }
			public override Color ImageMarginGradientBegin { get { return m_toolstripColor; } }
			public override Color ImageMarginGradientEnd { get { return m_toolstripColor; } }
			public override Color ImageMarginGradientMiddle { get { return m_toolstripColor; } }
			public override Color ImageMarginRevealedGradientBegin { get { return m_toolstripColor; } }
			public override Color ImageMarginRevealedGradientEnd { get { return m_toolstripColor; } }
			public override Color ImageMarginRevealedGradientMiddle { get { return m_toolstripColor; } }
			public override Color MenuBorder { get { return m_toolstripColor; } }
			public override Color MenuItemBorder { get { return m_toolstripColor; } }
			public override Color MenuItemPressedGradientBegin { get { return m_toolstripColor; } }
			public override Color MenuItemPressedGradientEnd { get { return m_toolstripColor; } }
			public override Color MenuItemPressedGradientMiddle { get { return m_toolstripColor; } }
			public override Color MenuItemSelected { get { return m_toolstripColor; } }
			public override Color MenuItemSelectedGradientBegin { get { return m_toolstripColor; } }
			public override Color MenuItemSelectedGradientEnd { get { return m_toolstripColor; } }
			public override Color MenuStripGradientBegin { get { return m_toolstripColor; } }
			public override Color MenuStripGradientEnd { get { return m_toolstripColor; } }
			public override Color RaftingContainerGradientBegin { get { return m_toolstripColor; } }
			public override Color RaftingContainerGradientEnd { get { return m_toolstripColor; } }
			public override Color SeparatorDark { get { return m_toolstripColor; } }
			public override Color SeparatorLight { get { return m_toolstripColor; } }
			public override Color StatusStripGradientBegin { get { return m_toolstripColor; } }
			public override Color StatusStripGradientEnd { get { return m_toolstripColor; } }
			public override Color ToolStripBorder { get { return m_toolstripColor; } }
			public override Color ToolStripContentPanelGradientBegin { get { return m_toolstripColor; } }
			public override Color ToolStripContentPanelGradientEnd { get { return m_toolstripColor; } }
			public override Color ToolStripDropDownBackground { get { return m_toolstripColor; } }
*/
			public override Color ToolStripGradientBegin { get { return m_toolstripColor; } }
			public override Color ToolStripGradientEnd { get { return m_toolstripColor; } }
			public override Color ToolStripGradientMiddle { get { return m_toolstripColor; } }
			public override Color OverflowButtonGradientBegin { get { return m_toolstripColor; } }
			public override Color OverflowButtonGradientEnd { get { return m_toolstripColor; } }
			public override Color OverflowButtonGradientMiddle { get { return m_toolstripColor; } }
/*
			public override Color ToolStripPanelGradientBegin { get { return m_toolstripColor; } }
			public override Color ToolStripPanelGradientEnd { get { return m_toolstripColor; } }
*/

		}
	}
}
