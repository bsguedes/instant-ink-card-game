namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for EmptyElement.xaml
    /// </summary>
    public partial class BogusElement : ReportElement
    {
        public BogusElement(double height)
        {
            this.Height = height;
            InitializeElement();
        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();
        }
    }
}
