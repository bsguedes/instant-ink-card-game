using Reporter;
using Reporter.Elements;
using Reporter.Layouts;
using Reporter.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace Examples
{
    public class CardElement : TableElement
    {
        public CardElement()
        {

        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            LayoutVn layout = new LayoutVn();
            FlowTextElementWithTitle el = new FlowTextElementWithTitle("Hello world!", "My first report");
            el.InitializeElement();
            layout.LayElement(el);
            Report r = new Report(PageSize.A4, PageOrientation.Portrait, PageMargins.CentimeterMargin, layout);
            r.CreateReport();

            content.Content = new DocumentViewer { Document = r.FixedDocument };
        }

        /*
        public MainWindow()
        {
            
            InitializeComponent();
            /*
            LayoutH2V1 layout = new LayoutH2V1();

            EmptyElementWithTitle el = new EmptyElementWithTitle( "Hello world!", 10 );
            el.InitializeElement();
            layout.LayElement( el, 0 );

            TableElement tel = new TableElement( "tutd", System.Windows.HorizontalAlignment.Center );
            PowerReport.Data.Table table = new PowerReport.Data.Table();
            table.Columns = new string[] { "a", "b" };
            table.CornerHeader = "ahdaiea";
            table.Rows = new PowerReport.Data.TableRow[] { new PowerReport.Data.TableRow { HeaderRow = "haeh", Values = new string[] { "#FF123456", "2" } } };
            tel.SetDataTable( table );


            tel.SelectCells( TableSelectionMode.FirstColumn );
            tel.FormatSelection( TableCellFormat.SelfBackground( ) );

            tel.SelectCells( TableSelectionMode.FirstColumn );
            tel.FormatSelection( TableCellFormat.ColumnAuto );

            tel.SelectCells( TableSelectionMode.All );
            tel.FormatSelection( TableCellFormat.MinimumMarginColumns( 100 ) );

            tel.SelectCells( TableSelectionMode.All );
            tel.FormatSelection( TableCellFormat.MinimumMarginRows( 100 ) );

            tel.InitializeElement();

            layout.LayElement( tel, 1 );

            EmptyElementWithTitle el2 = new EmptyElementWithTitle( "Hello world!", 10 );
            el2.InitializeElement();
            layout.LayElement( el2, 2 );

            Report r = new Report( PageSize.A4, PageOrientation.Portrait, PageMargins.CentimeterMargin, layout );

            Func<int, ReportElement> hd = new Func<int, ReportElement>(
                ( i ) =>
                {
                    Image img = new Image { Source = new BitmapImage( new Uri( "/IReportU;component/test.png", UriKind.RelativeOrAbsolute ) ) { CacheOption = BitmapCacheOption.OnLoad } };
                    ImageTextHeaderElement hdr = new ImageTextHeaderElement( "HUEHUEHUEHUE", new Thickness( 0, 0, 0, 2 ), imgLeft: img );
                    hdr.InitializeElement();
                    return hdr;
                } );


            Func<int, ReportElement> ft = new Func<int, ReportElement>(
                ( i ) =>
                {
                    TextHeaderElement foo = new TextHeaderElement( "TESTE", System.Windows.HorizontalAlignment.Right );
                    foo.InitializeElement();
                    return foo;
                } );

            Func<int, ReportElement> wm = new Func<int, ReportElement>(
                ( i ) =>
                {
                    Image wat = new Image { Source = new BitmapImage( new Uri( "/IReportU;component/test.png", UriKind.RelativeOrAbsolute ) ) { CacheOption = BitmapCacheOption.OnLoad } };
                    EmptyElement cc = new EmptyElement();
                    cc.Content = wat;
                    cc.InitializeElement();
                    return cc;
                } );
            
            r.AddDecorations( header: hd, footer: ft, pageNumberFormat: "Página {0} de {1}", watermark: wm );
            try
            {
                r.CreateReport();
            }
            catch ( Exception exc )
            {
                MessageBox.Show( exc.Message );
                MessageBox.Show( exc.StackTrace );
            }

            content.Content = new DocumentViewer { Document = r.FixedDocument };

            
            LayoutH2V1 layout = new LayoutH2V1();

            
            Brush[] brushes = new Brush[] { Brushes.AliceBlue, Brushes.Tomato, Brushes.Turquoise, Brushes.Lavender };
            for ( int i = 0 ; i < 10 ; i++ )
            {

                EmptyElement ee = new EmptyElement() { Width = 200, Height = 200, Background = brushes[ i % brushes.Count() ] };
                ee.InitializeElement();
                layout.LayElement( ee );

            }

            for ( int i = 0 ; i < 5 ; i++ )
            {



                EmptyElement ee = new EmptyElement() { Width = 200, Height = 1000, Background = Brushes.AliceBlue };
                ee.InitializeElement();
                layout.LayElement( ee );


                //ItemsControlElement e4 = new ItemsControlElement();
                //e4.SetItemsPanel( ItemsControlLayout.WrapPanel );
                //e4.SetDataSource( Enumerable.Range( 0, 920 ).Select( x => new { Value = x.ToString() } ), Application.Current.Resources[ "ItemsControlElementTestTemplate" ] as DataTemplate );
                //e4.InitializeElement();

            }
            

            TableElement e4 = new TableElement();

            e4.ShowHeaders = true;
            e4.ShowRowHeaders = true;                        

            e4.BorderDefinition = BorderDefinition.AllBorders;

            e4.SetDataTable( new Reporter.Data.Table()
            {
                CornerHeader = "l",                
                Columns = Enumerable.Range( 0, 20 ).Select( x => x.ToString() ).ToArray(),
                Rows = Enumerable.Range( 0, 10 ).Select( x => new Reporter.Data.TableRow { HeaderRow = x.ToString(), Values = new string[ 20 ] } )
            } );

           // e4.Title = "titulo";
            e4.TitleAlignment = System.Windows.HorizontalAlignment.Right;
            e4.SelectCells( TableSelectionMode.FirstRow );
            e4.SelectCells( TableSelectionMode.FirstColumn );
            e4.FormatSelection( TableCellFormat.Bold );

            e4.SelectCells( TableSelectionMode.Columns, 5 );
            e4.FormatSelection( TableCellFormat.MinimumMarginColumns( 50 ) );
            e4.SelectCells( TableSelectionMode.Columns, 5 );
            e4.FormatSelection( TableCellFormat.ColumnAuto );

            e4.InitializeElement();

            layout.LayElement( e4 );

            EmptyElement hist = new EmptyElement { Height = 250, Width = 600, Background = Brushes.Green };
            hist.InitializeElement();
            layout.LayElement( hist, 2 );

            FlowTextElementWithTitle txt = new FlowTextElementWithTitle( "lala ", string.Join( "", Enumerable.Range( 0, 10000 ).Select( x => x.ToString() ) ) ) { MaxHeight = 300 };
            txt.InitializeElement();
            layout.LayElement( txt, 1 );
            

            Func<int,ReportElement> hd = new Func<int,ReportElement>( 
                ( i ) => 
                { 
                    Image img = new Image { Source = new BitmapImage( new Uri( "/IReportU;component/test.png", UriKind.RelativeOrAbsolute ) ) { CacheOption = BitmapCacheOption.OnLoad } };            
                    ImageTextHeaderElement hdr = new ImageTextHeaderElement( "HUEHUEHUEHUE", new Thickness( 0, 0, 0, 2 ) , imgLeft: img );            
                    hdr.InitializeElement();
                    return hdr;
                } );


            Func<int,ReportElement> ft = new Func<int,ReportElement>(
                ( i ) =>
                {
                    TextHeaderElement foo = new TextHeaderElement( "TESTE", System.Windows.HorizontalAlignment.Right );           
                    foo.InitializeElement();
                    return foo;
                } );

            Func<int, ReportElement> wm = new Func<int, ReportElement>(
                ( i ) =>
                {
                    Image wat = new Image { Source = new BitmapImage( new Uri( "/IReportU;component/test.png", UriKind.RelativeOrAbsolute ) ) { CacheOption = BitmapCacheOption.OnLoad } };
                    EmptyElement cc = new EmptyElement();
                    cc.Content = wat;
                    cc.InitializeElement();
                    return cc;
                } );

            Report r = new Report( PageSize.A4, PageOrientation.Portrait, PageMargins.CentimeterMargin, layout );
            r.AddDecorations( header: hd, footer: ft, pageNumberFormat: "Página from hell {0} de {1}", watermark: wm );
            r.CreateReport();            

            content.Content = new DocumentViewer { Document = r.FixedDocument };

        }
        */
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
