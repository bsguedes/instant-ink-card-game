using InstantInkCardGame.Sets;
using Reporter;
using Reporter.Elements;
using Reporter.Layouts;
using Reporter.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace InstantInkCardGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int MAJOR_VERSION = 1;
        public int MINOR_VERSION = 0;

        public IEnumerable<InstantInkCardGameSet> SETLIST
        {
            get
            {
                yield return new BaseSet();
                yield return new DuelistSet();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            IncrementVersion();

            LayoutV1Vfnc2 layout_first_page = new LayoutV1Vfnc2();

            TextHeaderElement el = new TextHeaderElement(string.Format("Instant Ink Card Game, v: {0}.{1} rev. {2}, date: {3}", MAJOR_VERSION, MINOR_VERSION, Properties.Settings.Default.Version, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            el.InitializeElement();
            layout_first_page.LayElement(el);

            List<LayoutBase> layouts = new List<LayoutBase>
            {
                layout_first_page
            };

            foreach (InstantInkCardGameSet set in SETLIST)
            {
                LayoutVfn layout = new LayoutVfn();
                TableElement te = new TableElement(set.SetName, HorizontalAlignment.Center);
                te.SetDataTable(new Reporter.Data.Table()
                {
                    CornerHeader = "",
                    Columns = Enumerable.Range(0, 3).Select(x => x.ToString()).ToArray(),
                    Rows = set.GetRows(3)
                });

                te.InitializeElement();
                layout.LayElement(te);
                layouts.Add(layout);
            }


            Report r = new Report(PageSize.A4, PageOrientation.Portrait, PageMargins.CentimeterMargin, layouts.ToArray());
            r.CreateReport();

            content.Content = new DocumentViewer { Document = r.FixedDocument };
        }

        private void IncrementVersion()
        {
            Properties.Settings.Default.Version++;
            Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var source = (IDocumentPaginatorSource)((content.Content as DocumentViewer).Document as Report).FixedDocument;
            var paginator = source.DocumentPaginator;

            using (XpsDocument xpsFile = new XpsDocument("test.xps", System.IO.FileAccess.Write))
            {
                var writer = XpsDocument.CreateXpsDocumentWriter(xpsFile);
                writer.Write(paginator);
            }
        }
    }
}
