using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleMaths;

namespace HopfieldDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //var network = new MyFirstHopfieldNetwork();
        }



        private UIElement _GetControlIn(Grid grid, GridCoordinates controlCoordinates)
        {
            foreach (UIElement control in grid.Children)
            {
                var gridCoordinates = _GetGridCoordinatesOf(control);
                if (gridCoordinates.Equals(controlCoordinates))
                    return control;
            }

            return null;
        }

        private GridCoordinates _GetGridCoordinatesOf(UIElement toggleButton)
        {
            var rowIndex = Grid.GetRow(toggleButton);
            var colIndex = Grid.GetColumn(toggleButton);

            return new GridCoordinates(rowIndex, colIndex);
        }

        private HopfieldNetwork hopfieldNetwork;

        private void TrainButton_Click(object sender, RoutedEventArgs e)
        {
            var colCount = 5;
            var rowCount = 5;

            var pattern = new double[colCount * rowCount];

            for(var row=0; row<rowCount; row++)
            {
                for(var col=0; col<colCount; col++)
                {
                    pattern[row*colCount + col] = ((ToggleButton)_GetControlIn(DrawingGrid, new GridCoordinates(row, col))).IsChecked.Value 
                        ? 1.0
                        : 0.0;
                }
            }

            var hopfieldPattern = new HopfieldPattern(pattern);
            hopfieldNetwork = hopfieldNetwork ?? new HopfieldNetwork(25);
            hopfieldNetwork.Train(hopfieldPattern);
        }

        private void RecognizeButton_Click(object sender, RoutedEventArgs e)
        {
            var colCount = 5;
            var rowCount = 5;

            var pattern = new double[colCount * rowCount];

            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    pattern[row * colCount + col] = ((ToggleButton)_GetControlIn(DrawingGrid, new GridCoordinates(row, col))).IsChecked.Value
                        ? 1.0
                        : 0.0;
                }
            }

            var hopfieldPattern = new HopfieldPattern(pattern);
            var recognizedPattern = hopfieldNetwork.Recognize(hopfieldPattern);

            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    ((ToggleButton) _GetControlIn(DrawingGrid, new GridCoordinates(row, col))).IsChecked =
                        recognizedPattern[row*colCount + col] <= 0
                            ? false
                            : true;
                }
            }
        }
    }

    public class GridCoordinates
    {
        private int _row;
        private int _col;

        public GridCoordinates(int row, int col)
        {
            _row = row;
            _col = col;
        }

        public override bool Equals(object obj)
        {
            var typedObj = obj as GridCoordinates;
            if(typedObj == null) return false;

            return (typedObj._row == _row && typedObj._col == _col);
        }
    }
}
