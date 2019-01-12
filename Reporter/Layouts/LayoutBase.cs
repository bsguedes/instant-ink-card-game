using Reporter.Elements;
using Reporter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Reporter.Layouts
{
    public abstract class LayoutBase : Grid, INotifyPropertyChanged
    {
        #region Properties

        public List<ReportElement> Elements { get; private set; }

        #endregion

        #region Constructors

        public LayoutBase()
        {
            this.Elements = new List<ReportElement>();
        }

        #endregion

        #region General-purpose methods for external components

        public void LayElement(ReportElement re)
        {
            LayElement(re, this.Elements.Count);
        }

        public void LayElement(ReportElement re, int elementPosition)
        {
            if (re != null)
            {
                if (this.Elements.Count <= elementPosition)
                {
                    this.Elements.Resize<ReportElement>(elementPosition + 1, default(ReportElement));
                }
                this.Elements[elementPosition] = re;

                NotifyPropertyChanged("Elements");
            }
        }

        public void LayElements(params ReportElement[] elements)
        {
            int i = 0;
            foreach (var element in elements)
            {
                if (element != null)
                {
                    this.LayElement(element, i++);
                }
            }
        }

        public void InsertElement(ReportElement re, int position)
        {
            for (int i = Elements.Count; i > position; i--)
            {
                this.LayElement(Elements[i - 1], i);
            }
            this.LayElement(re, position);
        }

        #endregion

        #region Abstract methods for our layouts

        internal abstract UIElement FillPage(double availableHeight, double availableWidth);

        #endregion

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region General-purpose methods for our layouts

        internal virtual bool HasMoreContent()
        {
            return !Enumerable.Range(0, Elements.Count).All(x => !CanSetElement(x));
        }

        protected bool CanSetElement(int index)
        {
            if (Elements.ElementAtOrDefault(index) != null)
            {
                return !((ReportElement)Elements[index]).HasBeenAlreadySet;
            }
            return false;
        }

        protected FrameworkElement GetElementPart(int elementIndex)
        {
            return Elements[elementIndex].GetSlice();
        }

        protected FrameworkElement GetElementPart(int elementIndex, double availableHeight, double availableWidth)
        {
            return availableHeight > 0
             ? ((IMultiPageReportElement)Elements[elementIndex]).GetSlice(availableHeight, availableWidth)
             : null;
        }

        #endregion
    }

    public interface ILayoutWithPageNumber
    {
        PageNumberPosition PageNumberPosition { get; }
        bool ShowFirstPagePageNumber { get; }
    }

    public enum PageNumberPosition
    {
        TopLeft,
        TopRight,
        UnderHeaderLeft,
        UnderHeaderRight,
        OverFooterLeft,
        OverFooterRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        InsideHeader,
        InsideFooter
    }

    public class ChildrenElementConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(parameter is string && int.TryParse(parameter as string, out int position)))
            {
                return ReportElement.DefaultReportElement;
            }

            IEnumerable<ReportElement> children = value as IEnumerable<ReportElement>;

            if (children == null)
            {
                return ReportElement.DefaultReportElement;
            }

            return children.ElementAtOrDefault(position) ?? ReportElement.DefaultReportElement;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
