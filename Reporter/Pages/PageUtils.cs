using System.Windows;

namespace Reporter.Pages
{

    public enum PageOrientation
    {
        Portrait,
        Landscape
    }

    public class PageMargins
    {
        const double dpi = 96; // dots per inch
        const double ipc = 0.393701; // inches per centimeter
        static double dpc { get { return dpi * ipc; } } // dots per centimeter

        public Thickness Thickness { get; private set; }

        public double Left { get { return Thickness.Left; } }
        public double Top { get { return Thickness.Top; } }
        public double Right { get { return Thickness.Right; } }
        public double Bottom { get { return Thickness.Bottom; } }

        private PageMargins(double uniformInCm)
        {
            this.Thickness = new Thickness(uniformInCm * dpc);
        }

        public static PageMargins InchMargin = new PageMargins(2.54);
        public static PageMargins QuartInchMargin = new PageMargins(2.54 / 4);
        public static PageMargins CentimeterMargin = new PageMargins(1.00);

    }

    public class PageSize
    {
        const double dpi = 96; // dots per inch
        const double ipc = 0.393701; // inches per centimeter
        static double dpc { get { return dpi * ipc; } } // dots per centimeter

        private Size _size;

        public double Width { get { return _size.Width; } }
        public double Height { get { return _size.Height; } }

        private PageSize(double widthInCm, double heightInCm)
        {
            this._size = new Size(widthInCm * dpc, heightInCm * dpc);
        }

        public static PageSize A3 = new PageSize(29.70, 42.00);
        public static PageSize A4 = new PageSize(21.00, 29.70);
        public static PageSize A5 = new PageSize(14.85, 21.00);
    }
}
