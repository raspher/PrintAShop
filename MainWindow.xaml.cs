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

namespace PrintAShop
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
    public class Entry
    {
        int ID;
        string name;
        string producer;
        string sku;
        int ean;
        float price;
        float net_price;
        string date;

        public Entry(
            int id_,
            string name_,
            string producer_,
            string sku_,
            int ean_,
            float price_,
            float net_price_,
            string date_
            )
        {
            ID = id_;
            name = name_;
            producer = producer_;
            sku = sku_;
            ean = ean_;
            price = price_;
            net_price = net_price_;
            date = date_;
        }
        
        // TODO: update feature
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    // TODO: validation

}
