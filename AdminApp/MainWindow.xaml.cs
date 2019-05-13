using AdminApp.Model;
using MongoDB.Driver;
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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static MongoClient _client = new MongoClient();

        static IMongoDatabase _db = _client.GetDatabase("RssReaders");

        public IMongoCollection<Address> _addresses = _db.GetCollection<Address>("Address");


        int _selectedAddressIndex;


        public MainWindow()
        {
            InitializeComponent();

            _selectedAddressIndex = dataGrd.SelectedIndex;

            UpdateAddress();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addAddressWindow = new AddAddress(this);

            addAddressWindow.Show();

            UpdateAddress();
        }


        public void UpdateAddress()
        {
            var addresses = _addresses.AsQueryable().ToList<Address>();

            dataGrd.ItemsSource = addresses
                .Select(x => new
                {
                    Adres = x.Link,
                    Kategoria = x.Category
                });

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            var response = MessageBox.Show("Czy na pewno chcesz usunąć?", "", MessageBoxButton.OKCancel);

            _selectedAddressIndex = dataGrd.SelectedIndex;

            if (response == MessageBoxResult.OK && _selectedAddressIndex >= 0 && _selectedAddressIndex < dataGrd.Items.Count)
            {
                var deletedAddress = _addresses.AsQueryable().ToList()[_selectedAddressIndex];

                _addresses.DeleteOne<Address>(x => x.Id == deletedAddress.Id);
                UpdateAddress();
            }

        }
    }
}
