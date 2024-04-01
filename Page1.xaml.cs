using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Page1 : Page
    {
        private UNLV_STOREEntities context = new UNLV_STOREEntities();
        public Page1()
        {
            InitializeComponent();
            ClientTags.ItemsSource = context.ClientTags.ToList();
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
            if (ClientTags.SelectedItem == null) return;
            
            var clientTag = (ClientTags)ClientTags.SelectedItem;

            One.Text = clientTag.TagName;
        }


        private void izmen_Click(object sender, RoutedEventArgs e)
        {
            if (ClientTags.SelectedItem != null)
            {
                var selected = (ClientTags)ClientTags.SelectedItem;
                selected.TagName = One.Text;
                context.SaveChanges();
                ClientTags.ItemsSource = context.ClientTags.ToList();
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ClientTags.SelectedItem == null) return;
            {
                context.ClientTags.Remove(ClientTags.SelectedItem as ClientTags);
                context.SaveChanges();
                ClientTags.ItemsSource = context.ClientTags.ToList();
            }
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            ClientTags a = new ClientTags();
            a.TagName = One.Text;

            context.ClientTags.Add(a);
            context.SaveChanges();
            ClientTags.ItemsSource = context.ClientTags.ToList();
        }

    }
}
