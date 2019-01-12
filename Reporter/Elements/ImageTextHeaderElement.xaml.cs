using System.Windows;
using System.Windows.Controls;

namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for ImageTextHeaderElement.xaml
    /// </summary>
    public partial class ImageTextHeaderElement : ReportElement
    {

        private const int MAX_HEIGHT = 35;

        public string Title
        {
            get { return ( string ) GetValue( TitleProperty ); }
            set { SetValue( TitleProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register( "Title", typeof( string ), typeof( ImageTextHeaderElement ), new PropertyMetadata( string.Empty ) );

        public Image ImageLeft
        {
            get { return ( Image ) GetValue( ImageLeftProperty ); }
            set { SetValue( ImageLeftProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for ImageLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageLeftProperty =
            DependencyProperty.Register( "ImageLeft", typeof( Image ), typeof( ImageTextHeaderElement ), new PropertyMetadata( null ) );

        public Image ImageRight
        {
            get { return ( Image ) GetValue( ImageRightProperty ); }
            set { SetValue( ImageRightProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for ImageRight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageRightProperty =
            DependencyProperty.Register( "ImageRight", typeof( Image ), typeof( ImageTextHeaderElement ), new PropertyMetadata( null ) );


        public ImageTextHeaderElement() { }

        public ImageTextHeaderElement( string title, Thickness margin, int? maxHeight = MAX_HEIGHT, Image imgLeft = null, Image imgRight = null )            
        {
            this.MaxHeight = maxHeight ?? MAX_HEIGHT;
            this.ImageLeft = imgLeft;
            this.ImageRight = imgRight;
            this.Title = title;
            this.BorderThickness = margin;
        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();            
        }
    }
}
