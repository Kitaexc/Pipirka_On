using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Pipirka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Table.SelectedIndex)
            {
                case 0:
                    InTable.Navigate(new Page1());
                    break;
                case 1:
                    InTable.Navigate(new Page2());
                    break;
                case 2:
                    InTable.Navigate(new Page3());
                    break;
                case 3:
                    InTable.Navigate(new Page4());
                    break;
                case 4:
                    InTable.Navigate(new Page5());
                    break;
                default:
                    break;
            }

        }
    }
}
