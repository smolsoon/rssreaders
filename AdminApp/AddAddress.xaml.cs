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
using System.Windows.Shapes;

namespace AdminApp
{

    public class AddAddressWindowEventArgs : EventArgs
    {
        private readonly string address;
        private readonly string category;
        public string Address { get; private set; }
        public string Category { get; private set; }

        public AddAddressWindowEventArgs(string address, string category)
        {
            this.address = address;
            this.category = category;
        }
    }


    /// <summary>
    /// Interaction logic for AddAddress.xaml
    /// </summary>
    public partial class AddAddress : Window
    {
        //static IMongoCollection<Address> _addresses;

        MainWindow parentWindow;

        public AddAddress(MainWindow parent)
        {
            parentWindow = parent;

                        InitializeComponent();
        }


        //public event EventHandler<AddAddressWindowEventArgs> DialogFinished;

        //public void OnDialogFinished()
        //{
        //    if (DialogFinished != null)
        //        DialogFinished(this, new AddAddressWindowEventArgs(address.Text, category.Text));
        //}

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            parentWindow
                ._addresses
            .InsertOne(new Address
            {
                Link = address.Text,
                Category = category.Text
            });
                       
            this.Close();

            parentWindow.UpdateAddress();
            
        }
    }
}
