using Reporter.Elements;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using Reporter.Utils;

namespace Reporter.Pages
{
    /// <summary>
    /// Interaction logic for Page.xaml
    /// </summary>
    public partial class Page : Grid, INotifyPropertyChanged
    {
        #region Properties        

        private bool _HasHeader;
        public bool HasHeader
        {
            get { return _HasHeader; }
            set
            {
                if (value != _HasHeader)
                {
                    _HasHeader = value;
                    NotifyPropertyChanged("HasHeader");
                }
            }
        }

        private bool _HasFooter;
        public bool HasFooter
        {
            get { return _HasFooter; }
            set
            {
                if (value != _HasFooter)
                {
                    _HasFooter = value;
                    NotifyPropertyChanged("HasFooter");
                }
            }
        }

        private bool _HasWatermark;
        public bool HasWatermark
        {
            get { return _HasWatermark; }
            set
            {
                if (value != _HasWatermark)
                {
                    _HasWatermark = value;
                    NotifyPropertyChanged("HasWatermark");
                }
            }
        }

        private bool _HasPageNumber;
        public bool HasPageNumber
        {
            get { return _HasPageNumber; }
            set
            {
                if (value != _HasPageNumber)
                {
                    _HasPageNumber = value;
                    NotifyPropertyChanged("HasPageNumber");
                }
            }
        }

        private int _internalPageNumber;

        public string PageNumberFormatString { get; set; }

        #endregion

        #region PropertyChanged's properties

        private TextBlock _PageNumber;
        public TextBlock PageNumber
        {
            get { return _PageNumber; }
            set
            {
                if (value != _PageNumber)
                {
                    _PageNumber = value;
                    NotifyPropertyChanged("PageNumber");
                }
            }
        }

        private ReportElement _Header;
        public ReportElement Header
        {
            get { return _Header; }
            set
            {
                if (value != _Header)
                {
                    _Header = value;
                    NotifyPropertyChanged("Header");
                }
            }
        }

        private ReportElement _Footer;
        public ReportElement Footer
        {
            get { return _Footer; }
            set
            {
                if (value != _Footer)
                {
                    _Footer = value;
                    NotifyPropertyChanged("Footer");
                }
            }
        }

        private ReportElement _Watermark;
        public ReportElement Watermark
        {
            get { return _Watermark; }
            set
            {
                if (value != _Watermark)
                {
                    _Watermark = value;
                    NotifyPropertyChanged("Watermark");
                }
            }
        }


        private UIElement _PageContent;
        public UIElement PageContent
        {
            get { return _PageContent; }
            set
            {
                if (value != _PageContent)
                {
                    _PageContent = value;
                    NotifyPropertyChanged("PageContent");
                }
            }
        }

        private double _ReportWatermarkOpacity;
        public double ReportWatermarkOpacity
        {
            get { return _ReportWatermarkOpacity; }
            set
            {
                if (value != _ReportWatermarkOpacity)
                {
                    _ReportWatermarkOpacity = value;
                    NotifyPropertyChanged("ReportWatermarkOpacity");
                }
            }
        }


        #endregion

        private FixedPage fp;

        public Page(int pn, PageMargins pageMargins, PageSize pageSize, PageOrientation pageOrientation)
        {
            fp = new FixedPage();
            fp.Margin = pageMargins.Thickness;
            fp.Width = GetPageWidth(pageSize, pageOrientation);
            fp.Height = GetPageHeight(pageSize, pageOrientation);
            this.Width = fp.Width - fp.Margin.Left - fp.Margin.Right;
            this.Height = fp.Height - fp.Margin.Bottom - fp.Margin.Top;

            fp.Children.Add(this);

            this._internalPageNumber = pn;

            InitializeComponent();
        }

        internal void SetLayout(Layouts.LayoutBase layout)
        {
            PageContent = layout;

            // sets reportlayout dimensions
            layout.Width = fp.Width;
        }

        internal PageContent GetPageContent()
        {
            PageContent documentPage = new PageContent();
            ((IAddChild)documentPage).AddChild(fp);
            return documentPage;
        }

        private double GetPageHeight(PageSize pageSize, PageOrientation pageOrientation)
        {
            switch (pageOrientation)
            {
                case PageOrientation.Portrait:
                    return pageSize.Height;
                case PageOrientation.Landscape:
                    return pageSize.Width;
                default:
                    return 0;
            }
        }

        private double GetPageWidth(PageSize pageSize, PageOrientation pageOrientation)
        {
            switch (pageOrientation)
            {
                case PageOrientation.Portrait:
                    return pageSize.Width;
                case PageOrientation.Landscape:
                    return pageSize.Height;
                default:
                    return 0;
            }
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        internal double AvailableHeight
        {
            get
            {
                double availableSpace = this.fp.Height;
                availableSpace -= this.fp.Margin.Top;
                availableSpace -= this.fp.Margin.Bottom;
                availableSpace -= this.Header != null ? this.Header.DesiredSize.Height : 0;
                availableSpace -= this.Footer != null ? this.Footer.DesiredSize.Height : 0;
                availableSpace -= this.PageNumber != null ? this.PageNumber.DesiredSize.Height : 0;
                return availableSpace;
            }
        }

        internal double AvailableWidth
        {
            get
            {
                return this.fp.Width - this.fp.Margin.Left - this.fp.Margin.Right;
            }
        }


        internal void SetDecorationElements(Func<int, ReportElement> header, Func<int, ReportElement> footer, Func<int, ReportElement> watermark)
        {
            if (header != null)
            {
                HasHeader = true;
                this.Header = header(this._internalPageNumber);
                this.Header.MeasureAndArrange();
            }

            if (footer != null)
            {
                HasFooter = true;
                this.Footer = footer(this._internalPageNumber);
                this.Footer.MeasureAndArrange();
            }

            if (watermark != null)
            {
                HasWatermark = true;
                this.Watermark = watermark(this._internalPageNumber);
                this.Watermark.MeasureAndArrange();
            }
        }

        internal void SetPageNumber(Layouts.LayoutBase layout, string pageNumberFormat)
        {
            this.HasPageNumber = true;
            this.PageNumberFormatString = pageNumberFormat;

            int column, row;
            HorizontalAlignment align;

            PageNumber = new TextBlock();

            switch ((layout as Layouts.ILayoutWithPageNumber).PageNumberPosition)
            {
                case Reporter.Layouts.PageNumberPosition.TopLeft:
                    column = 0; row = 0; align = System.Windows.HorizontalAlignment.Left;
                    break;
                case Reporter.Layouts.PageNumberPosition.TopRight:
                    column = 2; row = 0; align = System.Windows.HorizontalAlignment.Right;
                    break;
                case Reporter.Layouts.PageNumberPosition.UnderHeaderLeft:
                    column = 0; row = 2; align = System.Windows.HorizontalAlignment.Left;
                    break;
                case Reporter.Layouts.PageNumberPosition.UnderHeaderRight:
                    column = 2; row = 2; align = System.Windows.HorizontalAlignment.Right;
                    break;
                case Reporter.Layouts.PageNumberPosition.OverFooterLeft:
                    column = 0; row = 4; align = System.Windows.HorizontalAlignment.Left;
                    break;
                case Reporter.Layouts.PageNumberPosition.OverFooterRight:
                    column = 2; row = 4; align = System.Windows.HorizontalAlignment.Right;
                    break;
                case Reporter.Layouts.PageNumberPosition.BottomLeft:
                    column = 0; row = 6; align = System.Windows.HorizontalAlignment.Left;
                    break;
                case Reporter.Layouts.PageNumberPosition.BottomCenter:
                    column = 1; row = 6; align = System.Windows.HorizontalAlignment.Center;
                    break;
                case Reporter.Layouts.PageNumberPosition.BottomRight:
                default:
                    column = 2; row = 6; align = System.Windows.HorizontalAlignment.Right;
                    break;
            }

            PageNumber.Text = string.Format("fill page number here");
            PageNumber.HorizontalAlignment = align;

            Grid.SetColumn(PageNumber, column);
            Grid.SetRow(PageNumber, row);
            this.PageNumber.MeasureAndArrange();

            page.Children.Add(PageNumber);
        }
    }
}
