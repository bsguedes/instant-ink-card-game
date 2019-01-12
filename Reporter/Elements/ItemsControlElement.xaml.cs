using Reporter.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for ItemsControlElement.xaml
    /// </summary>
    public partial class ItemsControlElement : ReportElement, IMultiPageReportElement
    {
        public bool IsBeingSet { get; set; }

        internal bool InternalCall { get; private set; }
        private int _elementsTaken;
        private List<int> _elementsPerPage;
        private ItemsControlLayout PanelTemplate;

        public IEnumerable DataSource { get; private set; }
        public DataTemplate DataTemplate { get; private set; }

        public bool ShowHeaders { get; set; }

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


        public ItemsControlElement(double maxWidth = double.PositiveInfinity)
        {
            this.MaxWidth = maxWidth;
        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();
        }

        protected override void BuildComponent()
        {
            itemsControl.ItemsPanel = GetItemsPanelTemplate(PanelTemplate);
            if (this.InternalCall)
                base.BuildComponent();
        }

        public void SetDataSource(IEnumerable source, DataTemplate template)
        {
            this.DataSource = source;
            this.DataTemplate = template;
            this._elementsPerPage = new List<int>();
        }

        public FrameworkElement GetSlice(double availableHeight, double availableWidth)
        {
            if (!IsBeingSet)
            {
                IsBeingSet = true;
                _elementsTaken = 0;
            }
            int elementCount = DataSource.Cast<object>().Count();
            if (_elementsTaken < elementCount)
            {
                ItemsControlElement te = new ItemsControlElement(availableWidth);
                te.SetItemsPanel(this.PanelTemplate);
                if (this.Header != null) te.SetHeader(this.Header.Clone() as ReportElement);

                int guess = _elementsPerPage.Count == 0 ? elementCount - _elementsTaken : _elementsPerPage.Max();

                te.SetDataSource((this.DataSource as IEnumerable).Cast<object>().Skip(_elementsTaken).Take(guess), this.DataTemplate);
                te.InternalCall = true;
                te.InitializeElement();
                double maxElementHeight = te.ActualHeight;

                double heightPerElement = maxElementHeight / guess;

                int i = Math.Min(elementCount - _elementsTaken, Math.Min((int)(availableHeight / heightPerElement), guess));

                te = new ItemsControlElement(availableWidth);
                te.SetItemsPanel(this.PanelTemplate);
                if (this.Header != null) te.SetHeader(this.Header.Clone() as ReportElement);
                te.SetDataSource((this.DataSource as IEnumerable).Cast<object>().Skip(_elementsTaken).Take(i), this.DataTemplate);
                te.InternalCall = true;
                te.InitializeElement();

                bool foundDecreasing = false;

                ItemsControlElement last = te;
                while (te.ActualHeight > availableHeight && i >= 0)
                {
                    i--;
                    te = new ItemsControlElement(availableWidth);
                    te.SetItemsPanel(this.PanelTemplate);
                    if (this.Header != null) te.SetHeader(this.Header.Clone() as ReportElement);
                    te.SetDataSource((this.DataSource as IEnumerable).Cast<object>().Skip(_elementsTaken).Take(i), this.DataTemplate);
                    te.InternalCall = true;
                    te.InitializeElement();
                    last = te;

                    foundDecreasing = true;
                }

                while (te.ActualHeight <= availableHeight && !foundDecreasing)
                {
                    if ((_elementsTaken + i) > elementCount) break;
                    last = te;
                    te = new ItemsControlElement(availableWidth);
                    te.SetItemsPanel(this.PanelTemplate);
                    if (this.Header != null) te.SetHeader(this.Header.Clone() as ReportElement);
                    te.SetDataSource((this.DataSource as IEnumerable).Cast<object>().Skip(_elementsTaken).Take(i), this.DataTemplate);
                    te.InternalCall = true;
                    te.InitializeElement();
                    i++;
                }

                int take = foundDecreasing ? i : i - (te.ActualHeight > availableHeight ? 2 : 1);
                if (take == 0) return new BogusElement(availableHeight);
                _elementsTaken = _elementsTaken + take;
                _elementsPerPage.Add(take);
                this.HasBeenAlreadySet = _elementsTaken == elementCount;

                return last;
            }
            else
            {
                this.HasBeenAlreadySet = true;
                return null;
            }
        }

        public void SetHeader(ReportElement header)
        {
            this.Header = header;
        }

        public void SetItemsPanel(ItemsControlLayout itemsControlLayout)
        {
            this.PanelTemplate = itemsControlLayout;
        }

        private ItemsPanelTemplate GetItemsPanelTemplate(ItemsControlLayout itemsControlLayout)
        {
            string panel;

            switch (itemsControlLayout)
            {
                case ItemsControlLayout.WrapPanel:
                default:
                    panel = @"<WrapPanel/>";
                    break;
                case ItemsControlLayout.HorizontalStackPanel:
                    panel = @"<VirtualizingStackPanel Orientation='Horizontal'/>";
                    break;
                case ItemsControlLayout.VerticalStackPanel:
                    panel = @"<VirtualizingStackPanel Orientation='Vertical'/>";
                    break;
            }

            string xaml = string.Format(@"<ItemsPanelTemplate   xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
                                {0}
                            </ItemsPanelTemplate>", panel);
            return System.Windows.Markup.XamlReader.Parse(xaml) as ItemsPanelTemplate;
        }
    }
}
