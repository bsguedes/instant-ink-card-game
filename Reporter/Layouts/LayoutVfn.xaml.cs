using Reporter.Elements;
using System.Windows;
using System.Windows.Controls;

namespace Reporter.Layouts
{

    public partial class LayoutVfn : LayoutBase, ILayoutWithPageNumber
    {
        public LayoutVfn()
        {
            InitializeComponent();
        }

        internal override UIElement FillPage(double availableHeight, double availableWidth)
        {
            availableHeight -= 10; // this reducing corresponds to the margin that is being applied to the stack panel

            StackPanel pageContent = new StackPanel { Orientation = Orientation.Vertical, Height = availableHeight, Margin = new Thickness(0, 5, 0, 5), MaxWidth = availableWidth };

            double lastHeight = 0;
            bool useLastHeight = false;

            for (int i = 0; i < Elements.Count; i++)
            {
                if (CanSetElement(i))
                {
                    if (Elements[i] is IMultiPageReportElement)
                    {
                        FrameworkElement rep = GetElementPart(i, availableHeight - (useLastHeight ? lastHeight : 0), availableWidth);
                        if (rep != null)
                        {
                            pageContent.Children.Add(rep);
                            lastHeight = rep.ActualHeight + (useLastHeight ? lastHeight : 0);
                            useLastHeight = false;
                        }
                        else
                        {
                            if (((ReportElement)Elements[i]).HasBeenAlreadySet)
                            {
                                useLastHeight = true;
                                continue;
                            }
                            break;
                        }

                        if (!((ReportElement)Elements[i]).HasBeenAlreadySet)
                        {
                            break;
                        }
                        else
                        {
                            useLastHeight = true;
                        }
                    }
                    else
                    {
                        if (availableHeight - (useLastHeight ? lastHeight : 0) < Elements[i].ActualHeight && lastHeight != 0)
                        {
                            useLastHeight = false;
                        }
                        else
                        {
                            pageContent.Children.Add(GetElementPart(i));
                            lastHeight = Elements[i].ActualHeight + (useLastHeight ? lastHeight : 0);
                            useLastHeight = (lastHeight < availableHeight);
                        }
                        if (!useLastHeight) break;
                    }
                }

            }
            return pageContent;
        }

        public PageNumberPosition PageNumberPosition
        {
            get { return Layouts.PageNumberPosition.BottomRight; }
        }

        public bool ShowFirstPagePageNumber
        {
            get { return false; }
        }
    }
}
