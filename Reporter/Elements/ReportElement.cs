using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Reporter.Utils;

namespace Reporter.Elements
{
    public abstract class ReportElement : UserControl, INotifyPropertyChanged, ICloneable
    {
        public static ReportElement DefaultReportElement = new EmptyElement();

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        protected virtual void BuildComponent()
        {
            this.MeasureAndArrange();
        }

        internal virtual FrameworkElement GetSlice()
        {
            this.HasBeenAlreadySet = true;
            return this;
        }

        protected abstract void InitializeComponentInternal();

        public void InitializeElement()
        {
            this.InitializeComponentInternal();
        }

        public object Clone()
        {
            string saved = System.Windows.Markup.XamlWriter.Save(this);
            System.IO.StringReader sReader = new System.IO.StringReader(saved);
            System.Xml.XmlReader xReader = System.Xml.XmlReader.Create(sReader);
            ReportElement rep = (ReportElement)System.Windows.Markup.XamlReader.Load(xReader);
            return rep;
        }

        protected internal bool HasBeenAlreadySet { get; protected set; }
    }

    public interface IMultiPageReportElement
    {
        bool IsBeingSet { get; set; }
        FrameworkElement GetSlice(double availableSpace, double availableWidth);
    }

    public class IsStringNullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value is string && !string.IsNullOrEmpty((string)value)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
