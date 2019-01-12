using System.Windows;
using System.Windows.Controls;

namespace Reporter.Layouts
{
    /// <summary>
    /// One page, two elements horizontally displayed at the top, one element at the bottom
    /// ################
    /// # ............ #
    /// # .    ..    . #
    /// # . 00 .. 01 . #
    /// # .    ..    . #
    /// # ............ #
    /// # ............ #
    /// # .    02    . #
    /// # ............ #
    /// ################
    /// </summary>
    public partial class LayoutH2V1 : LayoutBase
    {
        public LayoutH2V1()
        {
            InitializeComponent();
        }

        internal override UIElement FillPage(double availableHeight, double availableWidth)
        {
            grid.MaxHeight = availableHeight;
            grid.MaxWidth = availableWidth;

            Grid g = new Grid() { Height = availableHeight, Width = availableWidth };

            g.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var topLeft = GetElementPart(0);
            var topRight = GetElementPart(1);
            var bottom = GetElementPart(2);

            Grid.SetColumn(topRight, 1);
            Grid.SetRow(bottom, 1);
            Grid.SetColumnSpan(bottom, 2);

            g.Children.Add(topLeft);
            g.Children.Add(topRight);
            g.Children.Add(bottom);

            return g;
        }

        internal override bool HasMoreContent()
        {
            return false;
        }
    }
}
