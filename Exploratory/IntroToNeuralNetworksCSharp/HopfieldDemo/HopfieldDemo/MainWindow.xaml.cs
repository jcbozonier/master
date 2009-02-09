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

            var controlToFind = (ToggleButton)_GetControlIn(DrawingGrid, new GridCoordinates(1, 2));
            if((string)controlToFind.Content == "Yup")
            {
                var a = 0;
            }
            
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
