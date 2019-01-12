using System.Windows;

namespace Reporter.Layouts
{
    /// <summary>
    /// One page, two elements horizontally displayed at the top, three elements at the bottom
    /// ################
    /// # ............ #
    /// # . 00 .. 01 . #
    /// # ............ #
    /// # .    02    . #
    /// # ............ #
    /// # ............ #
    /// # .    03    . #
    /// # ............ #
    /// # ............ #
    /// # .    04    . #
    /// # ............ #
    /// ################
    /// </summary>
    public partial class LayoutH2V3 : LayoutBase
    {
        public LayoutH2V3()
        {
            InitializeComponent();
        }

        internal override UIElement FillPage(double availableHeight, double availableWidth)
        {
            return this;
        }

        internal override bool HasMoreContent()
        {
            return false;
        }
    }
}
