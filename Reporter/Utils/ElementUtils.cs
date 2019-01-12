using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reporter.Utils
{
    public enum TableSelectionMode
    {
        FirstColumn,
        FirstRow,
        FirstCell,
        Columns,
        Rows,
        Cells,
        All
    }

    public class TableCell
    {
        public TableCell( int row, int column )
        {
            this.Row = row;
            this.Column = column;
        }

        public int Column { get; set; }
        public int Row { get; set; }
    }

    public class TableCellFormat
    {
        private bool changedHorizontalAlignment;
        private bool changedFontWeight;
        private bool changedFontSize;
        private bool changedColumnWidth;
        private bool changedRowHeight;
        private bool changedThickness;
        private bool changedBackgroundToShow;
        private bool changedBackgroundToHide;

        public HorizontalAlignment HorizontalAlignment { get; private set; }
        public FontWeight FontWeight { get; private set; }
        public int FontSize { get; private set; }
        public GridLength ColumnWidth { get; private set; }
        public GridLength RowHeight { get; private set; }
        public Thickness Thickness { get; private set; }        
        public double SelfBackgroundWidth { get; private set; }

        internal TableCellFormat ( bool selfBackground, double width = 30 )
        {            
            this.changedBackgroundToShow = selfBackground;
            this.changedBackgroundToHide = !selfBackground;
            if ( selfBackground )
            {
                this.SelfBackgroundWidth = width;
            }
        }

        internal TableCellFormat ( Thickness margin )
        {
            this.Thickness = margin;
            changedThickness = true;
        }

        internal TableCellFormat( HorizontalAlignment alignment )
        {
            this.HorizontalAlignment = alignment;
            changedHorizontalAlignment = true;
        }

        internal TableCellFormat( FontWeight? weight )
        {
            this.FontWeight = weight ?? FontWeights.Normal;
            changedFontWeight = true;
        }

        internal TableCellFormat( int fontSize )
        {
            this.FontSize = fontSize;
            changedFontSize = true;
        }

        internal TableCellFormat( int? columnWidth, int? rowHeight )
        {
            if ( columnWidth == null || columnWidth.Value >= 0 )
            {
                this.ColumnWidth = columnWidth != null ? ( columnWidth == 0 ? new GridLength( 1, GridUnitType.Auto ) : new GridLength( columnWidth.Value ) ) : new GridLength( 1, GridUnitType.Star );
                changedColumnWidth = true;
            }
            if ( rowHeight == null || rowHeight.Value >= 0 )
            {
                this.RowHeight = rowHeight != null ? ( rowHeight == 0 ? new GridLength( 1, GridUnitType.Auto ) : new GridLength( rowHeight.Value ) ) : new GridLength( 1, GridUnitType.Star );
                changedRowHeight = true;
            }
        }

        public static TableCellFormat Bold = new TableCellFormat( FontWeights.Bold );
        public static TableCellFormat Left = new TableCellFormat( HorizontalAlignment.Left );
        public static TableCellFormat BigFontSize = new TableCellFormat( 80 );
        public static TableCellFormat ColumnAuto = new TableCellFormat( 0, -1 );
        public static TableCellFormat RowAuto = new TableCellFormat( -1, 0 );
        public static TableCellFormat SelfBackground( double width = 30 ) { return new TableCellFormat( true, width ); }
        public static TableCellFormat ClearBackground = new TableCellFormat( false );
        public static TableCellFormat ColumnRow( int? columnWidth, int? rowHeight ) { return new TableCellFormat( columnWidth, rowHeight ); }
        public static TableCellFormat MinimumMarginColumns( double margin ) { return new TableCellFormat( new Thickness( margin, -1, margin, -1 ) ); }
        public static TableCellFormat MinimumMarginRows( double margin ) { return new TableCellFormat( new Thickness( -1, margin, -1, margin ) ); }
        
        internal void ApplyRowAndColumnFormat( TableCell cell, Grid grid )
        {
            if ( this.changedRowHeight ) grid.RowDefinitions.ElementAt( cell.Row ).Height = this.RowHeight;
            if ( this.changedColumnWidth ) grid.ColumnDefinitions.ElementAt( cell.Column ).Width = this.ColumnWidth;
        }

        internal void ApplyFormat( TextBlock txt )
        {
            if ( changedHorizontalAlignment ) txt.HorizontalAlignment = this.HorizontalAlignment;
            if ( changedFontWeight ) txt.FontWeight = this.FontWeight;
            if ( changedFontSize ) txt.FontSize = this.FontSize;
            if ( changedThickness ) txt.Margin = new Thickness( this.Thickness.Left == -1 ? txt.Margin.Left : this.Thickness.Left,
                                                                this.Thickness.Top == -1 ? txt.Margin.Top : this.Thickness.Top,
                                                                this.Thickness.Right == -1 ? txt.Margin.Right : this.Thickness.Right,
                                                                this.Thickness.Bottom == -1 ? txt.Margin.Bottom : this.Thickness.Bottom );
            if ( changedBackgroundToShow )
            {
                if ( txt.Text.StartsWith( "#" ) && txt.Text.Length == 9 )
                {
                    txt.Foreground = new SolidColorBrush( ( Color ) ColorConverter.ConvertFromString( txt.Text ) );
                    txt.Background = new SolidColorBrush( ( Color ) ColorConverter.ConvertFromString( txt.Text ) );
                    txt.Width = this.SelfBackgroundWidth ;
                }
            }
            if ( changedBackgroundToHide )
            {
                txt.Background = Brushes.Transparent;
                txt.Foreground = Brushes.Black;
                txt.Width = double.NaN;
            }
        }

    }

    public enum ItemsControlLayout
    {
        WrapPanel,
        HorizontalStackPanel,
        VerticalStackPanel
    }
}
