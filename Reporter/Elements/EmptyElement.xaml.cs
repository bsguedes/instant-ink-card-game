namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for EmptyElement.xaml
    /// </summary>
    public partial class EmptyElement : ReportElement
    {
        public EmptyElement()
        {
            
        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();            
        }
    }
}
