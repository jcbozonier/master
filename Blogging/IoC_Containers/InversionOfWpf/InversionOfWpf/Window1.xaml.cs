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
using InversionOfWpf.BusinessObjects;
using InversionOfWpf.Container;

namespace InversionOfWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            MyContainerBootStrapper.BootstrapStructureMap();
        }

        private void InvokeButton_Click(object sender, RoutedEventArgs e)
        {
            new NotifierModel().Notify("You've been clicked!");
        }
    }
}
