using System;
using System.Linq;
using System.Windows;

namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for FlowTextElementWithTitle.xaml
    /// </summary>
    public partial class FlowTextElementWithTitle : ReportElement, IMultiPageReportElement
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


        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }

        private FlowTextElementWithTitle(bool calculate, string title, string text, HorizontalAlignment alignment = System.Windows.HorizontalAlignment.Center, double maxWidth = double.PositiveInfinity)
        {
            this.Title = title;
            this.Text = text;
            this.TitleHorizontalAlignment = alignment;
            this.MaxWidth = maxWidth;

            int i = this.Text.IndexOf(Environment.NewLine);
            while (i == 0)
            {
                this.Text = this.Text.Substring(Environment.NewLine.Length);
                i = this.Text.IndexOf(Environment.NewLine);
            }

            if (calculate)
            {
                int newLineStringLength = Environment.NewLine.Length;
                int p = this.Text.IndexOf(Environment.NewLine);
                while (p != -1)
                {
                    this.Text = this.Text.Insert(p, " ");
                    int q = this.Text.IndexOf(Environment.NewLine, p + 1 + newLineStringLength);
                    while (q == p + 1 + newLineStringLength)
                    {
                        p = q - 1;
                        q = this.Text.IndexOf(Environment.NewLine, p + 1 + newLineStringLength);
                    }
                    if (q == -1) break;
                    p = this.Text.IndexOf(Environment.NewLine, q);
                }
            }
        }

        public FlowTextElementWithTitle(string title, string text, HorizontalAlignment alignment = System.Windows.HorizontalAlignment.Center, double maxWidth = double.PositiveInfinity)
            : this(true, title, text, alignment, maxWidth)
        {

        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();
        }

        public bool IsBeingSet { get; set; }

        private int _elementsTaken;

        public FrameworkElement GetSlice(double availableHeight, double availableWidth)
        {
            if (!IsBeingSet)
            {
                IsBeingSet = true;
                _elementsTaken = 0;
            }

            string[] textTokens = this.Text.Split(' ');
            int elementCount = textTokens.Count();
            if (_elementsTaken < elementCount)
            {
                FlowTextElementWithTitle te = new FlowTextElementWithTitle(false, _elementsTaken == 0 ? this.Title : null, string.Join(" ", textTokens.Skip(_elementsTaken).Take(elementCount - _elementsTaken)), this.TitleHorizontalAlignment, availableWidth);
                te.InitializeElement();
                double maxElementHeight = te.ActualHeight;

                double heightPerElement = (maxElementHeight / (elementCount - _elementsTaken));

                int i = Math.Min(elementCount - _elementsTaken, (int)(availableHeight / heightPerElement));

                te = new FlowTextElementWithTitle(false, _elementsTaken == 0 ? this.Title : null, string.Join(" ", textTokens.Skip(_elementsTaken).Take(i)), this.TitleHorizontalAlignment, availableWidth);
                te.InitializeElement();

                while (te.ActualHeight > availableHeight && i >= 0)
                {
                    i--;
                    te = new FlowTextElementWithTitle(false, _elementsTaken == 0 ? this.Title : null, string.Join(" ", textTokens.Skip(_elementsTaken).Take(i)), this.TitleHorizontalAlignment, availableWidth);
                    te.InitializeElement();
                }

                while (te.ActualHeight <= availableHeight)
                {
                    if ((_elementsTaken + i) > elementCount) break;
                    te = new FlowTextElementWithTitle(false, _elementsTaken == 0 ? this.Title : null, string.Join(" ", textTokens.Skip(_elementsTaken).Take(i)), this.TitleHorizontalAlignment, availableWidth);
                    te.InitializeElement();
                    i++;
                }

                int take = i - (te.ActualHeight > availableHeight ? 2 : 1);
                te = new FlowTextElementWithTitle(false, _elementsTaken == 0 ? this.Title : null, string.Join(" ", textTokens.Skip(_elementsTaken).Take(take)), this.TitleHorizontalAlignment, availableWidth);
                te.InitializeElement();
                _elementsTaken = _elementsTaken + take;

                this.HasBeenAlreadySet = _elementsTaken == elementCount;

                return te;
            }
            else
            {
                return null;
            }
        }
    }
}
