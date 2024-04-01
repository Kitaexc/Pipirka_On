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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        private UNLV_STOREEntities context = new UNLV_STOREEntities();
        public Page3()
        {
            InitializeComponent();
            ProductTypes.ItemsSource = context.ProductTypes.ToList();
            BlockTextBoxesExcept(One);
        }
        private void BlockTextBoxesExcept(System.Windows.Controls.TextBox exception)
        {
            if (One != exception) One.IsEnabled = false;
            if (Two != exception) Two.IsEnabled = false;
            if (Three != exception) Three.IsEnabled = false;
            if (Four != exception) Four.IsEnabled = false;
            if (Five != exception) Five.IsEnabled = false;
        }

        private void Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductTypes.SelectedItem == null) return;

            var row = (ProductTypes)ProductTypes.SelectedItem;

            One.Text = row.PrType;
        }


        private void izmen_Click(object sender, RoutedEventArgs e)
        {
            var selected = (ProductTypes)ProductTypes.SelectedItem;
            selected.PrType = One.Text;
            context.SaveChanges();
            ProductTypes.ItemsSource = context.ProductTypes.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductTypes.SelectedItem == null) return;

            try
            {
                var selectedProductType = ProductTypes.SelectedItem as ProductTypes;
                if (selectedProductType != null)
                {
                    context.ProductTypes.Remove(selectedProductType);
                    context.SaveChanges();
                    ProductTypes.ItemsSource = context.ProductTypes.ToList();
                }
                else
                {
                    MessageBox.Show("Не удалось привести выбранный элемент к нужному типу.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}");
            }
        }



        private void insert_Click(object sender, RoutedEventArgs e)
        {
            ProductTypes a = new ProductTypes();
            a.PrType = One.Text;
            context.ProductTypes.Add(a);
            context.SaveChanges();
            ProductTypes.ItemsSource = context.ProductTypes.ToList();
        }


    }
}

