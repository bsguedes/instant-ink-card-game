using System.Windows;

namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for EmptyElementWithTitle.xaml
    /// </summary>
    public partial class EmptyElementWithTitle : ReportElement
    {
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private HorizontalAlignment _TitleHorizontalAlignment;
        public HorizontalAlignment TitleHorizontalAlignment
        {
            get { return _TitleHorizontalAlignment; }
            set
            {
                if (value != _TitleHorizontalAlignment)
                {
                    _TitleHorizontalAlignment = value;
                    NotifyPropertyChanged("TitleHorizontalAlignment");
                }
            }
        }


        private UIElement _ElementContent;
        public UIElement ElementContent
        {
            get { return _ElementContent; }
            set
            {
                if (value != _ElementContent)
                {
                    _ElementContent = value;
                    NotifyPropertyChanged("ElementContent");
                }
            }
        }

        private UIElement _TopElementContent;
        public UIElement TopElementContent
        {
            get { return _TopElementContent; }
            set
            {
                if (value != _TopElementContent)
                {
                    _TopElementContent = value;
                    NotifyPropertyChanged("TopElementContent");
                }
            }
        }

        public EmptyElementWithTitle(string title, double spacing, HorizontalAlignment alignment = System.Windows.HorizontalAlignment.Center)
            : this(title, new BogusElement(spacing), new BogusElement(spacing), alignment)
        {

        }

        public EmptyElementWithTitle(string title, UIElement content, HorizontalAlignment alignment = System.Windows.HorizontalAlignment.Center)
        {
            this.Title = title;
            this.ElementContent = content;
            this.TitleHorizontalAlignment = alignment;
        }

        public EmptyElementWithTitle(string title, UIElement bottomContent, UIElement topContent, HorizontalAlignment alignment = System.Windows.HorizontalAlignment.Center)
            : this(title, bottomContent, alignment)
        {
            this.TopElementContent = topContent;
        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();
        }
    }
}
