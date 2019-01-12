using Reporter.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reporter.Layouts
{
    /// <summary>
    /// N fixed elements, one per row.
    /// ################
    /// # ............ #
    /// # .    00    . #
    /// # ............ #
    /// # ............ #
    /// # .    01    . #
    /// # ............ #
    /// # ............ #
    /// # .     n    . #
    /// # ............ #
    /// ################

    /// </summary>
    public partial class LayoutVn: LayoutBase
    {
        public LayoutVn()
        {
            InitializeComponent();
        }        

        internal override UIElement FillPage( double availableHeight, double availableWidth )
        {
            availableHeight -= 10; // this reducing corresponds to the margin that is being applied to the stack panel

            StackPanel pageContent = new StackPanel { Orientation = Orientation.Vertical, Height = availableHeight, Margin = new Thickness( 0, 5, 0, 5 ) };

            for ( int i = 0 ; i < Elements.Count ; i++ )
            {
                if ( CanSetElement( i ) )
                {
                    pageContent.Children.Add( GetElementPart ( i ) );                    
                }
            }
            
            return pageContent;
        }

    }
}
