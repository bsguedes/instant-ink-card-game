using Reporter.Elements;
using System.Windows;

namespace Reporter.Layouts
{
    /// <summary>
    /// One fixed element at the top, a flow element separated in two columns.
    /// 
    /// ################
    /// # ............ #
    /// # .    00    . #
    /// # ............ #
    /// # ............ #
    /// # . 01 .. || . #
    /// # . || .. || . #
    /// # . || .. \/ . #
    /// # . \/ ..    . #
    /// ##.#        #.##
    /// ##.#        #.##
    /// # .    ..    . #
    /// # . 01 ..    . #
    /// # .    ....... #
    /// # ......       #
    /// # . n  .       #
    /// # . || .       #
    /// # . || .       #
    /// # . \/ .       #
    /// ################
    /// </summary>
    public partial class LayoutV1Vfnc2 : LayoutBase, ILayoutWithPageNumber
    {
        public LayoutV1Vfnc2()
        {
            InitializeComponent();
        }

        internal override UIElement FillPage(double availableHeight, double availableWidth)
        {
            availableHeight -= 10; // reduces space taken by margins, etc
            availableWidth -= 20;

            LayoutV1Vfnc2 pageContent = new LayoutV1Vfnc2();
            pageContent.Height = availableHeight;

            if (CanSetElement(0))
            {
                pageContent.topElement.Content = GetElementPart(0);
                availableHeight -= Elements[0].ActualHeight;
            }

            if (availableHeight <= 0)
                return pageContent;

            double lastHeight = 0;
            bool useLastHeight = false;
            bool tryRightColumn = false;
            for (int i = 1; i < Elements.Count; i++)
            {
                if (CanSetElement(i))
                {
                    if (Elements[i] is IMultiPageReportElement)
                    {
                        FrameworkElement rep;
                        if (!tryRightColumn)
                        {
                            rep = GetElementPart(i, availableHeight - (useLastHeight ? lastHeight : 0), availableWidth / 2);
                            if (rep != null)
                            {
                                pageContent.leftColumn.Children.Add(rep);
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
                            }
                        }

                        rep = GetElementPart(i, availableHeight - (useLastHeight ? lastHeight : 0), availableWidth / 2);
                        if (rep != null)
                        {
                            pageContent.rightColumn.Children.Add(rep);
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

                        // if the control has been set there can be still some space in the right column                    
                        tryRightColumn = ((ReportElement)Elements[i]).HasBeenAlreadySet;
                        useLastHeight = tryRightColumn;

                        if (!tryRightColumn)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (!tryRightColumn)
                        {
                            pageContent.leftColumn.Children.Add(GetElementPart(i));
                        }
                        else
                        {
                            pageContent.rightColumn.Children.Add(GetElementPart(i));
                        }
                        lastHeight = Elements[i].ActualHeight + (useLastHeight ? lastHeight : 0);
                        useLastHeight = (lastHeight < availableHeight);
                        if (tryRightColumn && !useLastHeight) break;
                        if (!tryRightColumn && !useLastHeight) tryRightColumn = true;
                    }
                }
            }

            return pageContent;
        }

        public PageNumberPosition PageNumberPosition
        {
            get { return Layouts.PageNumberPosition.BottomLeft; }
        }

        public bool ShowFirstPagePageNumber
        {
            get { return false; }
        }
    }
}
