using Reporter.Elements;
using Reporter.Layouts;
using Reporter.Pages;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Reporter
{
    public class Report
    {
        public IList<Page> PageList { get; private set; }

        public FixedDocument FixedDocument { get; private set; }

        public PageSize PageSize { get; set; }
        public PageOrientation PageOrientation { get; set; }
        public PageMargins PageMargins { get; set; }
        public double WatermarkOpacity { get; set; }
        public IEnumerable<LayoutBase> Layouts { get; set; }

        private Func<int, ReportElement> header;
        private Func<int, ReportElement> footer;
        private string pageNumberFormat;
        private Func<int, ReportElement> watermark;

        public Report()
        {
            PageList = new List<Page>();
            this.FixedDocument = new FixedDocument();
            this.WatermarkOpacity = 0.2;
        }

        public Report(PageSize pageSize, PageOrientation pageOrientation, PageMargins pageMargins, params LayoutBase[] layout)
            : this()
        {
            InitializeReport(pageSize, pageOrientation, pageMargins, layout);
        }

        protected void InitializeReport(PageSize pageSize, PageOrientation pageOrientation, PageMargins pageMargins, params LayoutBase[] layout)
        {
            this.PageSize = pageSize;
            this.PageOrientation = pageOrientation;
            this.PageMargins = pageMargins;
            this.Layouts = layout ?? new LayoutBase[0];
        }

        public void CreateReport()
        {
            int pages = 0;
            foreach (var layout in Layouts)
            {
                do
                {
                    Page p = new Page(pages++, PageMargins, PageSize, PageOrientation)
                    {
                        ReportWatermarkOpacity = this.WatermarkOpacity
                    };
                    p.SetDecorationElements(header, footer, watermark);
                    if (layout is ILayoutWithPageNumber && !string.IsNullOrEmpty(pageNumberFormat))
                    {
                        p.SetPageNumber(layout, pageNumberFormat);
                    }
                    p.PageContent = layout.FillPage(p.AvailableHeight, p.AvailableWidth);
                    this.FixedDocument.Pages.Add(p.GetPageContent());
                    this.PageList.Add(p);
                }
                while (layout.HasMoreContent());
            }

            for (int i = 0; i < this.PageList.Count; i++)
            {
                if (this.PageList[i].HasPageNumber)
                {
                    this.PageList[i].PageNumber.Text = string.Format(this.PageList[i].PageNumberFormatString, i + 1, this.PageList.Count);
                }
            }
        }

        public void ClearLayout()
        {
            PageList = new List<Page>();
            this.FixedDocument = new FixedDocument();
            this.WatermarkOpacity = 0.2;
        }

        public virtual void Refresh()
        {

        }

        public void AddDecorations(Func<int, ReportElement> header = null, Func<int, ReportElement> footer = null, string pageNumberFormat = null, Func<int, ReportElement> watermark = null)
        {
            this.header = header;
            this.footer = footer;
            this.pageNumberFormat = pageNumberFormat;
            this.watermark = watermark;
        }
    }
}
