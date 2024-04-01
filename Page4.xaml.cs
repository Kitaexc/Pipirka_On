using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        private UNLV_STOREEntities context = new UNLV_STOREEntities();
        public Page4()
        {
            InitializeComponent();
            Products.ItemsSource = context.Products.ToList();
            BlockTextBoxesExcept(Five);
        }

        private void BlockTextBoxesExcept(System.Windows.Controls.TextBox exception)
        {
            One.IsEnabled = true;
            Two.IsEnabled = true;
            Three.IsEnabled = true;
            Four.IsEnabled = true;
            Five.IsEnabled = true;

            if (exception != null)
            {
                exception.IsEnabled = false;
            }
        }

        private void Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Products.SelectedItem == null) return;

            var row = (Products)Products.SelectedItem;

            One.Text = row.ProductName;
            Two.Text = row.ProductPrice.ToString();
            Three.Text = row.ID_ProductType.ToString();
            Four.Text = row.QuantityInStock.ToString();
        }


        private void izmen_Click(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem != null)
            {
                var selected = (Products)Products.SelectedItem;
                selected.ProductName = One.Text;
                if (decimal.TryParse(Two.Text, out decimal productPrice))
                {
                    selected.ProductPrice = productPrice;
                }
                else
                {
                    MessageBox.Show("Цена продукта должна быть числом с плавающей запятой.");
                    return;
                }
                if (int.TryParse(Three.Text, out int idProductType))
                {
                    selected.ID_ProductType = idProductType;
                }
                else
                {
                    MessageBox.Show("ID типа продукта должен быть целым числом.");
                    return;
                }
                if (int.TryParse(Four.Text, out int quantityInStock))
                {
                    selected.QuantityInStock = quantityInStock;
                }
                else
                {
                    MessageBox.Show("Количество на складе должно быть целым числом.");
                    return;
                }
                context.SaveChanges();
                Products.ItemsSource = context.Products.ToList();
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem == null) return;
            {
                context.Products.Remove(Products.SelectedItem as Products);
                context.SaveChanges();
                Products.ItemsSource = context.Products.ToList();
            }
        }


        private void insert_Click(object sender, RoutedEventArgs e)
        {
            Products a = new Products();
            a.ProductName = One.Text;
            if (decimal.TryParse(Two.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
            {
                a.ProductPrice = price;
            }
            else
            {
                MessageBox.Show("Введите корректную цену.");
                return;
            }
            if (int.TryParse(Three.Text, out int idProductType))
            {
                a.ID_ProductType = idProductType;
            }
            else
            {
                MessageBox.Show("ID типа продукта должен быть числом.");
                return;
            }
            if (int.TryParse(Four.Text, out int quantity))
            {
                a.QuantityInStock = quantity;
            }
            else
            {
                MessageBox.Show("Количество на складе должно быть числом.");
                return;
            }

            context.Products.Add(a);
            try
            {
                context.SaveChanges();
                Products.ItemsSource = context.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

    }
}

