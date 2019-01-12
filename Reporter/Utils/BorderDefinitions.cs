using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Reporter.Utils
{
    /// <summary>
    ///         NNW    NNE   
    ///       ****** ****** 
    ///      *     N*      *
    ///  WNW *      *      * ENE
    ///      *   W  *  E   *
    ///       ****** ****** 
    ///      *      *      *
    ///  WSW *      *      * ESE
    ///      *     S*      *
    ///       ****** ****** 
    ///        SSW     SSE
    /// </summary>
    public class BorderDefinition
    {
        public static BorderDefinition NoBorders = new BorderDefinition();
        public static BorderDefinition InternalHorizontal = new BorderDefinition(BorderDirection.W, BorderDirection.E);
        public static BorderDefinition InternalVertical = new BorderDefinition(BorderDirection.N, BorderDirection.S);
        public static BorderDefinition InternalOnly = InternalHorizontal + InternalVertical;
        public static BorderDefinition LeftOnly = new BorderDefinition(BorderDirection.WNW, BorderDirection.WSW);
        public static BorderDefinition TopOnly = new BorderDefinition(BorderDirection.NNW, BorderDirection.NNE);
        public static BorderDefinition RightOnly = new BorderDefinition(BorderDirection.ENE, BorderDirection.ESE);
        public static BorderDefinition BottomOnly = new BorderDefinition(BorderDirection.SSW, BorderDirection.SSE);
        public static BorderDefinition ExternalOnly = LeftOnly + RightOnly + TopOnly + BottomOnly;
        public static BorderDefinition UnderFirstRow = new BorderDefinition(BorderDirection.W0, BorderDirection.E0);
        public static BorderDefinition Inside = new BorderDefinition(BorderDirection.E, BorderDirection.S, BorderDirection.SSE, BorderDirection.ESE);
        public static BorderDefinition AllBorders = InternalOnly + ExternalOnly;

        internal List<BorderDirection> Borders { get; private set; }

        private BorderDefinition()
        {
            this.Borders = new List<BorderDirection>();
        }

        private BorderDefinition(params BorderDirection[] borders)
        {
            this.Borders = borders.ToList();
        }

        public static BorderDefinition operator +(BorderDefinition def1, BorderDefinition def2)
        {
            return new BorderDefinition(def1.Borders.Concat(def2.Borders).ToArray());
        }

        public static BorderDefinition operator -(BorderDefinition def1, BorderDefinition def2)
        {
            BorderDefinition result = new BorderDefinition();
            foreach (var dir in def1.Borders)
            {
                result.Borders.Add(dir);
            }
            foreach (var dir in def2.Borders)
            {
                if (def1.Borders.Contains(dir))
                {
                    result.Borders.Remove(dir);
                }
            }
            return result;
        }

        internal void BuildBorders(System.Windows.Controls.Grid grid)
        {
            foreach (var b in this.Borders)
            {
                BuildBorders(grid, b);
            }
        }

        private void BuildBorders(System.Windows.Controls.Grid grid, BorderDirection bd)
        {
            switch (bd)
            {
                case BorderDirection.W0:
                    BuildBorders_W0(grid);
                    break;
                case BorderDirection.E0:
                    BuildBorders_E0(grid);
                    break;
                case BorderDirection.N:
                    BuildBorders_N(grid);
                    break;
                case BorderDirection.S:
                    BuildBorders_S(grid);
                    break;
                case BorderDirection.E:
                    BuildBorders_E(grid);
                    break;
                case BorderDirection.W:
                    BuildBorders_W(grid);
                    break;
                case BorderDirection.NNW:
                    BuildBorders_NNW(grid);
                    break;
                case BorderDirection.NNE:
                    BuildBorders_NNE(grid);
                    break;
                case BorderDirection.ENE:
                    BuildBorders_ENE(grid);
                    break;
                case BorderDirection.ESE:
                    BuildBorders_ESE(grid);
                    break;
                case BorderDirection.SSE:
                    BuildBorders_SSE(grid);
                    break;
                case BorderDirection.SSW:
                    BuildBorders_SSW(grid);
                    break;
                case BorderDirection.WNW:
                    BuildBorders_WNW(grid);
                    break;
                case BorderDirection.WSW:
                    BuildBorders_WSW(grid);
                    break;
                default:
                    break;
            }
        }

        private void BuildBorders_W0(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count >= 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, thickness, 0, 0);
                System.Windows.Controls.Grid.SetColumn(b, 0);
                System.Windows.Controls.Grid.SetRow(b, 1);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_E0(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count >= 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, thickness, 0, 0);
                System.Windows.Controls.Grid.SetColumn(b, 1);
                System.Windows.Controls.Grid.SetColumnSpan(b, grid.ColumnDefinitions.Count - 1);
                System.Windows.Controls.Grid.SetRow(b, 1);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_WSW(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.RowDefinitions.Count > 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(thickness, 0, 0, 0);
                System.Windows.Controls.Grid.SetColumn(b, 0);
                System.Windows.Controls.Grid.SetRow(b, 1);
                System.Windows.Controls.Grid.SetRowSpan(b, grid.RowDefinitions.Count - 1);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_WNW(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.RowDefinitions.Count >= 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(thickness, 0, 0, 0);
                System.Windows.Controls.Grid.SetColumn(b, 0);
                System.Windows.Controls.Grid.SetRow(b, 0);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_SSW(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count >= 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, 0, 0, thickness);
                System.Windows.Controls.Grid.SetColumn(b, 0);
                System.Windows.Controls.Grid.SetRow(b, grid.RowDefinitions.Count - 1);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_SSE(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count > 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, 0, 0, thickness);
                System.Windows.Controls.Grid.SetColumn(b, 1);
                System.Windows.Controls.Grid.SetRow(b, grid.RowDefinitions.Count - 1);
                System.Windows.Controls.Grid.SetColumnSpan(b, grid.ColumnDefinitions.Count - 1);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_ESE(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.RowDefinitions.Count > 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, 0, thickness, 0);
                System.Windows.Controls.Grid.SetColumn(b, grid.ColumnDefinitions.Count - 1);
                System.Windows.Controls.Grid.SetRow(b, 1);
                System.Windows.Controls.Grid.SetRowSpan(b, grid.RowDefinitions.Count - 1);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_ENE(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.RowDefinitions.Count >= 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, 0, thickness, 0);
                System.Windows.Controls.Grid.SetColumn(b, grid.ColumnDefinitions.Count - 1);
                System.Windows.Controls.Grid.SetRow(b, 0);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_NNE(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count > 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, thickness, 0, 0);
                System.Windows.Controls.Grid.SetColumn(b, 1);
                System.Windows.Controls.Grid.SetColumnSpan(b, grid.ColumnDefinitions.Count - 1);
                System.Windows.Controls.Grid.SetRow(b, 0);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_NNW(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count >= 1)
            {
                System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                b.BorderThickness = new Thickness(0, thickness, 0, 0);
                System.Windows.Controls.Grid.SetColumn(b, 0);
                System.Windows.Controls.Grid.SetRow(b, 0);
                grid.Children.Add(b);
            }
        }

        private void BuildBorders_W(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count >= 1)
            {
                for (int i = 1; i < grid.RowDefinitions.Count; i++)
                {
                    System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                    b.BorderThickness = new Thickness(0, thickness, 0, 0);
                    System.Windows.Controls.Grid.SetColumn(b, 0);
                    System.Windows.Controls.Grid.SetRow(b, i);
                    grid.Children.Add(b);
                }
            }
        }

        private void BuildBorders_E(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.ColumnDefinitions.Count >= 1)
            {
                for (int i = 1; i < grid.RowDefinitions.Count; i++)
                {
                    System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                    b.BorderThickness = new Thickness(0, thickness, 0, 0);
                    System.Windows.Controls.Grid.SetColumn(b, 1);
                    System.Windows.Controls.Grid.SetColumnSpan(b, grid.ColumnDefinitions.Count - 1);
                    System.Windows.Controls.Grid.SetRow(b, i);
                    grid.Children.Add(b);
                }
            }
        }

        private void BuildBorders_S(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.RowDefinitions.Count >= 1)
            {
                for (int i = 1; i < grid.ColumnDefinitions.Count; i++)
                {
                    System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                    b.BorderThickness = new Thickness(thickness, 0, 0, 0);
                    System.Windows.Controls.Grid.SetColumn(b, i);
                    System.Windows.Controls.Grid.SetRow(b, 1);
                    System.Windows.Controls.Grid.SetRowSpan(b, grid.RowDefinitions.Count - 1);
                    grid.Children.Add(b);
                }
            }
        }

        private void BuildBorders_N(System.Windows.Controls.Grid grid, double thickness = 1)
        {
            if (grid.RowDefinitions.Count >= 1)
            {
                for (int i = 1; i < grid.ColumnDefinitions.Count; i++)
                {
                    System.Windows.Controls.Border b = new System.Windows.Controls.Border() { BorderBrush = System.Windows.Media.Brushes.Black };
                    b.BorderThickness = new Thickness(thickness, 0, 0, 0);
                    System.Windows.Controls.Grid.SetColumn(b, i);
                    System.Windows.Controls.Grid.SetRow(b, 0);
                    grid.Children.Add(b);
                }
            }
        }
    }

    internal enum BorderDirection
    {
        N,
        S,
        E,
        E0,
        W,
        W0,
        NNW,
        NNE,
        ENE,
        ESE,
        SSE,
        SSW,
        WNW,
        WSW
    }
}
