using Reporter.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Reporter.Elements
{
    /// <summary>
    /// Interaction logic for TableElement.xaml
    /// </summary>
    public partial class TableElement : ReportElement, IMultiPageReportElement
    {
        public bool IsBeingSet { get; set; }

        private int _elementsTaken;

        public string Title { get; set; }
        public HorizontalAlignment TitleAlignment { get; set; }

        public Reporter.Data.Table DataTable { get; private set; }

        private Grid _TableGrid;
        public Grid TableGrid
        {
            get { return _TableGrid; }
            set
            {
                if (value != _TableGrid)
                {
                    _TableGrid = value;
                    NotifyPropertyChanged("TableGrid");
                }
            }
        }

        private bool _ShowHeaders;
        public bool ShowHeaders
        {
            get { return _ShowHeaders; }
            set
            {
                if (value != _ShowHeaders)
                {
                    _ShowHeaders = value;
                    NotifyPropertyChanged("ShowHeaders");
                }
            }
        }

        private bool _ShowRowHeaders;
        public bool ShowRowHeaders
        {
            get { return _ShowRowHeaders; }
            set
            {
                if (value != _ShowRowHeaders)
                {
                    _ShowRowHeaders = value;
                    NotifyPropertyChanged("ShowRowHeaders");
                }
            }
        }

        private BorderDefinition _BorderDefinition;
        public BorderDefinition BorderDefinition
        {
            get { return _BorderDefinition; }
            set
            {
                if (value != _BorderDefinition)
                {
                    _BorderDefinition = value;
                    NotifyPropertyChanged("BorderDefinition");
                }
            }
        }


        public TableElement(string title, HorizontalAlignment alignment)
        {
            this.Title = title;
            this.TitleAlignment = alignment;
        }

        public TableElement()
            : this(null, HorizontalAlignment.Center)
        {
        }

        protected override void InitializeComponentInternal()
        {
            InitializeComponent();
            BuildComponent();
        }

        public void SetDataTable(Reporter.Data.Table table)
        {
            this.DataTable = table;
            GenerateContentGrid();
        }

        public void SelectCells(TableSelectionMode selection, params int[] args)
        {
            switch (selection)
            {
                case TableSelectionMode.FirstColumn:
                    SelectCells(TableSelectionMode.Columns, 0);
                    break;
                case TableSelectionMode.FirstRow:
                    SelectCells(TableSelectionMode.Rows, 0);
                    break;
                case TableSelectionMode.FirstCell:
                    SelectCells(TableSelectionMode.Cells, 0, 0);
                    break;
                case TableSelectionMode.Columns:
                    SelectCells(Enumerable.Range(0, this.TableGrid.RowDefinitions.Count()).SelectMany(x => args, (row, column) => new TableCell(row, column)));
                    break;
                case TableSelectionMode.Rows:
                    SelectCells(Enumerable.Range(0, this.TableGrid.ColumnDefinitions.Count()).SelectMany(x => args, (column, row) => new TableCell(row, column)));
                    break;
                case TableSelectionMode.Cells:
                    SelectCells(Enumerable.Range(0, args.Count() / 2).Select(x => args.Skip(2 * x).Take(2)).Select(y => new TableCell(y.First(), y.Last())));
                    break;
                case TableSelectionMode.All:
                    SelectCells(TableSelectionMode.Rows, Enumerable.Range(0, this.TableGrid.RowDefinitions.Count).ToArray());
                    break;
                default:
                    break;
            }
        }

        private void SelectCells(IEnumerable<TableCell> cells)
        {
            if (this.SelectedCells == null)
            {
                this.SelectedCells = new List<TableCell>();
            }
            this.SelectedCells = this.SelectedCells.Concat(cells);

        }

        IEnumerable<TableCell> SelectedCells;

        public void FormatSelection(TableCellFormat format)
        {
            if (SelectedCells != null && TableGrid != null)
            {
                foreach (var cell in SelectedCells)
                {
                    foreach (var txt in TableGrid.Children.Cast<UIElement>().Where(x => x is TextBlock).Cast<TextBlock>())
                    {
                        if (Grid.GetRow(txt) == cell.Row && Grid.GetColumn(txt) == cell.Column)
                        {
                            format.ApplyFormat(txt);
                        }
                    }
                    format.ApplyRowAndColumnFormat(cell, this.TableGrid);
                }
            }
            SelectedCells = null;
        }

        private void GenerateContentGrid()
        {
            Grid g = new Grid();
            int columns = this.DataTable.Columns.Count();
            int rows = this.DataTable.Rows.Count();
            if (this.ShowHeaders) rows++;
            if (this.ShowRowHeaders) columns++;

            Enumerable.Range(0, rows).ToList().ForEach(x => g.RowDefinitions.Add(new RowDefinition()));
            Enumerable.Range(0, columns).ToList().ForEach(x => g.ColumnDefinitions.Add(new ColumnDefinition()));

            g.ApplyBorders(this.BorderDefinition ?? BorderDefinition.NoBorders);

            if (!string.IsNullOrEmpty(this.DataTable.CornerHeader) && this.ShowHeaders && this.ShowRowHeaders)
            {
                g.Children.Add(GenerateCell(this.DataTable.CornerHeader, 0, 0));
            }
            if (this.ShowHeaders)
            {
                for (int i = 0; i < this.DataTable.Columns.Count(); i++)
                {
                    g.Children.Add(GenerateCell(this.DataTable.Columns.ElementAt(i), 0, i + (this.ShowRowHeaders ? 1 : 0)));
                }
            }
            if (this.ShowRowHeaders)
            {
                for (int i = 0; i < this.DataTable.Rows.Count(); i++)
                {
                    g.Children.Add(GenerateCell(this.DataTable.Rows.ElementAt(i).HeaderRow, i + (this.ShowHeaders ? 1 : 0), 0));
                }
            }

            for (int i = 0; i < this.DataTable.Rows.Count(); i++)
            {
                var row = this.DataTable.Rows.ElementAtOrDefault(i);
                if (row != null)
                {
                    for (int j = 0; j < this.DataTable.Columns.Count(); j++)
                    {
                        g.Children.Add(GenerateCell(row.Values.ElementAtOrDefault(j), i + (this.ShowHeaders ? 1 : 0), j + (this.ShowRowHeaders ? 1 : 0)));
                    }
                }
            }

            this.TableGrid = g;
        }

        private FrameworkElement GenerateCell(object content, int row, int column, bool isBold = false, VerticalAlignment alignment = System.Windows.VerticalAlignment.Center)
        {
            if (content is string)
            {
                TextBlock tb = new TextBlock { HorizontalAlignment = System.Windows.HorizontalAlignment.Center, VerticalAlignment = alignment };
                tb.Text = content as string ?? string.Empty;
                tb.FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal;
                Grid.SetRow(tb, row);
                Grid.SetColumn(tb, column);
                return tb;
            }
            else
            {
                if (content is FrameworkElement)
                {
                    Grid.SetRow(content as FrameworkElement, row);
                    Grid.SetColumn(content as FrameworkElement, column);
                    return content as FrameworkElement;
                }
                else
                {
                    return new TextBlock();
                }
            }
        }

        public FrameworkElement GetSlice(double availableHeight, double availableWidth)
        {
            if (!IsBeingSet)
            {
                IsBeingSet = true;
                _elementsTaken = 0;
            }
            int elementCount = this.DataTable.Rows.Count();
            if (_elementsTaken < elementCount)
            {

                Grid outerGrid = new Grid();
                outerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                outerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                if (!string.IsNullOrEmpty(this.Title))
                {
                    TextBlock title = new TextBlock { FontWeight = FontWeights.Bold, VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = this.TitleAlignment };
                    title.Text = this.Title;
                    outerGrid.Children.Add(title);
                }

                Grid grid = new Grid();
                Grid.SetRow(grid, 1);
                outerGrid.Children.Add(grid);

                foreach (var cd in this.TableGrid.ColumnDefinitions)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = cd.Width });
                }


                if (this.ShowHeaders)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = this.TableGrid.RowDefinitions.First().Height });
                    foreach (var child in this.TableGrid.Children.Cast<UIElement>().Where(x => Grid.GetRow(x) == 0))
                    {
                        UIElement element = null;
                        if (child is Border)
                        {
                            var bord = child as Border;
                            element = new Border { BorderThickness = bord.BorderThickness, BorderBrush = bord.BorderBrush };
                        }
                        else if (child is TextBlock)
                        {
                            var txt = child as TextBlock;
                            element = new TextBlock { Text = txt.Text, FontSize = txt.FontSize, FontWeight = txt.FontWeight, HorizontalAlignment = txt.HorizontalAlignment };
                        }
                        else
                        {
                            continue;
                        }
                        Grid.SetColumn(element, Grid.GetColumn(child));
                        grid.Children.Add(element);
                    }
                }

                grid.MeasureAndArrange();
                outerGrid.MeasureAndArrange();

                int i = 0;
                while (outerGrid.ActualHeight <= availableHeight)
                {
                    if ((_elementsTaken + i) >= elementCount) break;
                    grid.RowDefinitions.Add(new RowDefinition { Height = this.TableGrid.RowDefinitions.ElementAt(_elementsTaken + i + (this.ShowHeaders ? 1 : 0)).Height });

                    var ctrl = new Label { Height = this.TableGrid.Children.Cast<UIElement>().Where(x => Grid.GetRow(x) == _elementsTaken + i + (this.ShowHeaders ? 1 : 0)).Max(x => x.DesiredSize.Height) };
                    Grid.SetRow(ctrl, i + (this.ShowHeaders ? 1 : 0));
                    grid.Children.Add(ctrl);

                    grid.MeasureAndArrange();
                    outerGrid.MeasureAndArrange();

                    i++;
                }

                int take;

                if (outerGrid.ActualHeight > availableHeight)
                {
                    grid.RowDefinitions.RemoveAt(i - (this.ShowHeaders ? 0 : 1));
                    take = i - 1;
                }
                else
                {
                    take = i;
                }

                for (int n = 0; n < take; n++)
                {
                    List<UIElement> ctrls = new List<UIElement>();
                    ctrls.AddRange(this.TableGrid.Children.Cast<UIElement>().Where(x => Grid.GetRow(x) == _elementsTaken + n + (this.ShowHeaders ? 1 : 0) && !(x is Border)));

                    foreach (var child in ctrls)
                    {
                        this.TableGrid.Children.Remove(child);
                        Grid.SetRow(child, n + (this.ShowHeaders ? 1 : 0));
                        grid.Children.Add(child);
                    }

                    grid.ApplyBorders(this.BorderDefinition ?? BorderDefinition.NoBorders);
                }

                _elementsTaken = _elementsTaken + take;

                this.HasBeenAlreadySet = _elementsTaken == elementCount;

                return outerGrid;
            }
            else
            {
                return null;
            }

        }
    }
}

