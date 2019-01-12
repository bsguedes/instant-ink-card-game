using System.Windows;

namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for TextHeaderElement.xaml
    /// </summary>
    public partial class TextHeaderElement : ReportElement
    {
        public string Title1
        {
            get { return (string)GetValue(Title1Property); }
            set { SetValue(Title1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Title1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Title1Property =
            DependencyProperty.Register("Title1", typeof(string), typeof(TextHeaderElement), new PropertyMetadata(string.Empty));

        public string Title2
        {
            get { return (string)GetValue(Title2Property); }
            set { SetValue(Title2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Title2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Title2Property =
            DependencyProperty.Register("Title2", typeof(string), typeof(TextHeaderElement), new PropertyMetadata(string.Empty));

        public HorizontalAlignment Title1HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(Title1HorizontalAlignmentProperty); }
            set { SetValue(Title1HorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title1HorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Title1HorizontalAlignmentProperty =
            DependencyProperty.Register("Title1HorizontalAlignment", typeof(HorizontalAlignment), typeof(TextHeaderElement), new PropertyMetadata(HorizontalAlignment.Left));

        public HorizontalAlignment Title2HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(Title2HorizontalAlignmentProperty); }
            set { SetValue(Title2HorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title2HorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Title2HorizontalAlignmentProperty =
            DependencyProperty.Register("Title2HorizontalAlignment", typeof(HorizontalAlignment), typeof(TextHeaderElement), new PropertyMetadata(HorizontalAlignment.Right));

        public TextHeaderElement() { }

        public TextHeaderElement(string title, HorizontalAlignment alignment = HorizontalAlignment.Left)
        {
            this.Title1 = title;
            this.Title1HorizontalAlignment = alignment;
        }

        public TextHeaderElement(string titleLeft, string titleRight)
            : this(titleLeft)
        {
            this.Title2 = titleRight;
        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();
        }
    }
}
