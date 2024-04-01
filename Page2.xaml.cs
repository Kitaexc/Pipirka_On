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
    public partial class Page2 : Page
    {
        private UNLV_STOREEntities context = new UNLV_STOREEntities();
        public Page2()
        {
            InitializeComponent();
            Clients.ItemsSource = context.Clients.ToList();
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
            if (Clients.SelectedItem == null) return;

            var row = (Clients)Clients.SelectedItem;

            One.Text = row.ClientName;
            Two.Text = row.ClientSurName;
            Three.Text = row.ID_Tag.ToString();
            Four.Text = row.ClientNumberPhone;

        }


        private void izmen_Click(object sender, RoutedEventArgs e)
        {
            if (Clients.SelectedItem != null)
            {
                var selected = (Clients)Clients.SelectedItem;
                selected.ClientName = One.Text;
                selected.ClientSurName = Two.Text;
                if (int.TryParse(Three.Text, out int idTag))
                {
                    selected.ID_Tag = idTag;
                }
                else
                {
                    MessageBox.Show("ID_Tag должен быть числом");
                    return;
                }

                selected.ClientNumberPhone = Four.Text;
                context.SaveChanges();
                Clients.ItemsSource = context.Clients.ToList();
            }

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Clients.SelectedItem == null) return;
            {
                context.Clients.Remove(Clients.SelectedItem as Clients);
                context.SaveChanges();
                Clients.ItemsSource = context.Clients.ToList();
            }
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            Clients a = new Clients();
            a.ClientName = One.Text;
            a.ClientSurName = Two.Text;
            if (int.TryParse(Three.Text, out int idTagValue))
            {
                a.ID_Tag = idTagValue;
            }
            else
            {
                MessageBox.Show("Значение ID_Tag должно быть числом.");
                return;
            }

            a.ClientNumberPhone = Four.Text;

            context.Clients.Add(a);
            context.SaveChanges();
            Clients.ItemsSource = context.Clients.ToList();
        }


    }
}

