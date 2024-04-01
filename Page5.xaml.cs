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
    /// Логика взаимодействия для Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        private UNLV_STOREEntities context = new UNLV_STOREEntities();
        public Page5()
        {
            InitializeComponent();
            Orders.ItemsSource = context.Orders.ToList();
        }

        private void BlockTextBoxesExcept(System.Windows.Controls.TextBox exception)
        {
            One.IsEnabled = true;
            Two.IsEnabled = true;
            Three.IsEnabled = true;
            Four.IsEnabled = true;
            Five.IsEnabled = true;
        }

        private void Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Orders.SelectedItem == null) return;

            var row = (Orders)Orders.SelectedItem;

            One.Text = row.ID_Client.ToString();
            Two.Text = row.ID_Product.ToString();
            Three.Text = row.OrderDate.ToString();
            Four.Text = row.Quantity.ToString();
            Five.Text = row.TotalPrice.ToString();
        }


        private void izmen_Click(object sender, RoutedEventArgs e)
        {
            if (Orders.SelectedItem != null)
            {
                var selected = (Orders)Orders.SelectedItem;

                if (int.TryParse(One.Text, out int idClient))
                {
                    selected.ID_Client = idClient;
                }
                else
                {
                    MessageBox.Show("ID клиента должен быть числом.");
                    return;
                }
                if (int.TryParse(Two.Text, out int idProduct))
                {
                    selected.ID_Product = idProduct;
                }
                else
                {
                    MessageBox.Show("ID продукта должен быть числом.");
                    return;
                }
                if (DateTime.TryParse(Three.Text, out DateTime orderDate))
                {
                    selected.OrderDate = orderDate;
                }
                else
                {
                    MessageBox.Show("Дата заказа должна быть допустимой датой.");
                    return;
                }
                if (int.TryParse(Four.Text, out int quantity))
                {
                    selected.Quantity = quantity;
                }
                else
                {
                    MessageBox.Show("Количество должно быть числом.");
                    return;
                }
                if (decimal.TryParse(Five.Text, out decimal totalPrice))
                {
                    selected.TotalPrice = totalPrice;
                }
                else
                {
                    MessageBox.Show("Общая цена должна быть числом.");
                    return;
                }
                context.SaveChanges();
                Orders.ItemsSource = context.Orders.ToList();
            }
        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Orders.SelectedItem == null) return;
            {
                context.Orders.Remove(Orders.SelectedItem as Orders);
                context.SaveChanges();
                Orders.ItemsSource = context.Orders.ToList();
            }
        }


        private void insert_Click(object sender, RoutedEventArgs e)
        {
            Orders a = new Orders();
            if (!int.TryParse(One.Text, out int idClient))
            {
                MessageBox.Show("ID клиента должно быть целым числом.");
                return;
            }
            a.ID_Client = idClient;
            if (!int.TryParse(Two.Text, out int idProduct))
            {
                MessageBox.Show("ID продукта должно быть целым числом.");
                return;
            }
            a.ID_Product = idProduct;
            if (!DateTime.TryParse(Three.Text, out DateTime orderDate))
            {
                MessageBox.Show("Дата заказа введена некорректно.");
                return;
            }
            a.OrderDate = orderDate;
            if (!int.TryParse(Four.Text, out int quantity))
            {
                MessageBox.Show("Количество должно быть целым числом.");
                return;
            }
            a.Quantity = quantity;
            if (!decimal.TryParse(Five.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal totalPrice))
            {
                MessageBox.Show("Общая цена должна быть числом.");
                return;
            }
            a.TotalPrice = totalPrice;

            context.Orders.Add(a);
            try
            {
                context.SaveChanges();
                Orders.ItemsSource = context.Orders.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении заказа: {ex.Message}");
            }
        }
    }
}

