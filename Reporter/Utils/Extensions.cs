using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Reporter.Utils
{
    public static class Extensions
    {
        public static void Resize<T>(this List<T> list, int sz, T c)
        {
            int cur = list.Count;
            if (sz < cur)
                list.RemoveRange(sz, cur - sz);
            else if (sz > cur)
            {
                if (sz > list.Capacity)
                    list.Capacity = sz;
                list.AddRange(Enumerable.Repeat(c, sz - cur));
            }
        }

        public static void Resize<T>(this List<T> list, int sz) where T : new()
        {
            Resize(list, sz, new T());
        }

        internal static void MeasureAndArrange(this UIElement element)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(0, 0, element.DesiredSize.Width, element.DesiredSize.Height));
        }

        internal static void ApplyBorders(this System.Windows.Controls.Grid grid, BorderDefinition border)
        {
            #region Removes existing borders

            List<System.Windows.Controls.Border> borders = new List<System.Windows.Controls.Border>();

            foreach (var child in grid.Children)
            {
                if (child is System.Windows.Controls.Border && (child as System.Windows.Controls.Border).Tag == BorderDefinition.NoBorders)
                {
                    borders.Add(child as System.Windows.Controls.Border);
                }
            }

            for (int i = 0; i < borders.Count; i++)
            {
                grid.Children.Remove(borders[i]);
            }

            #endregion

            #region Add borders according to borderdefinition

            border.BuildBorders(grid);

            #endregion

        }

    }
}
