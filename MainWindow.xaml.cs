using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
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
using PrintAShop.MySearch;


namespace PrintAShop
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>


    public static class Draw
    {
        private static readonly Random random = new Random();

        public static float Price()
        {
            return random.Next(1, 100) / 100f;
        }

        public static string EAN()
        {
            return random.Next(10000000, 10000050).ToString();
        }

        public static string SKU()
        {
            return random.Next(1000, 1100).ToString();// + "-" + random.Next(1000, 9999).ToString();
        }

        public static string Date()
        {
            return 
                random.Next(0, 1).ToString() +
                random.Next(1, 2).ToString() +
                "-" + random.Next(0, 1).ToString() +
                random.Next(0,1).ToString() +
                "-" + random.Next(1950, 1960).ToString();
        }

        public static string Name()
        {
            string[] names = {"Drukarka-Laserowa-", "Drukarka-igłowa-", "Drukarka-atramentowa-"};
            string name = names[random.Next(names.Length - 1)];
            for (int i = 0; i < random.Next(2, 3); i++)
            {
                    name += random.Next(9).ToString();
            }

            return name;
        }

        public static string Producer()
        {
            string[] names = { "Dell", "HP", "ZenTec", "Brother", "LG" };
            string name = names[random.Next(names.Length - 1)];

            return name;
        }

        public static bool Invoice()
        {
            return random.Next(1, 10) % 2 == 0;
        }
    }

    public partial class MainWindow : Window
    {
        public List<Entry> Entries { get; set; }
        public int EntriesNum { get; set; }
        public Invert InvertList { get; set; }

        public Chain ChainList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Entries = new List<Entry>();
            this.EntriesNum = 0;
            this.InvertList = new Invert();
            this.ChainList = new Chain();

        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {

            try
            {
                Entries.Add(new Entry()
                {
                    ID = EntriesNum + 1,
                    Product = inProduct.Text.ToString(),
                    Producer = inProducer.Text.ToString(),
                    Sku = inSKU.Text.ToString(),
                    Ean = inEAN.Text.ToString(),
                    Price = float.Parse(inPrice.Text.ToString()),
                    NetPrice = float.Parse(inNetPrice.Text.ToString()),
                    Date = inDate.Text.ToString(),
                    Invoice = inInvoice.IsChecked.Value

                });
                EntriesNum++;
                dgBilling.Items.Add(Entries[Entries.Count - 1]);
                InvertList.dodaj(Entries[Entries.Count - 1], Entries.Count - 1);
                ChainList.dodaj(Entries[Entries.Count - 1], Entries.Count - 1);
                Console.WriteLine($"Tworzenie: {Entries.Count}");
            }

            catch
            {

            }
        }

        private void Button_Click_Remove_ALL(object sender, RoutedEventArgs e)
        {
            Entries.Clear();
            dgBilling.Items.Clear();
            EntriesNum = 0;
            InvertList.Clear();
            ChainList.Clear();
        }

        private void Button_Click_Remove_ID(object sender, RoutedEventArgs e)
        {
            Entries.RemoveAll(r => r.ID == int.Parse(inRemoveID.Text));
            dgBilling.Items.Clear();
            InvertList.Clear();
            ChainList.Clear();

            for (int i = 1; i <= Entries.Count; i++)
            {
                dgBilling.Items.Add(Entries[i - 1]);
                InvertList.dodaj(Entries[i - 1], i-1);
                ChainList.dodaj(Entries[i - 1], i - 1);
            }
        }

        private void Button_Click_Randomize(object sender, RoutedEventArgs e)
        {
            int HowMany = int.TryParse(inAmmount.Text.ToString(), out _) ? int.Parse(inAmmount.Text.ToString()) : 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < HowMany; i++)
            {
                try
                {
                    Entries.Add(new Entry()
                    {
                        ID = EntriesNum + 1,
                        Product = Draw.Name(),
                        Producer = Draw.Producer(),
                        Sku = Draw.SKU(),
                        Ean = Draw.EAN(),
                        Price = Draw.Price(),
                        NetPrice = Draw.Price(),
                        Date = Draw.Date(),
                        Invoice = Draw.Invoice()

                    });
                    EntriesNum++;
                    dgBilling.Items.Add(Entries[Entries.Count - 1]);
                    InvertList.dodaj(Entries[Entries.Count - 1], Entries.Count - 1);
                    ChainList.dodaj(Entries[Entries.Count - 1], Entries.Count - 1);
                    Console.WriteLine($"Tworzenie: {Entries.Count}");
                }

                catch
                {

                }
            }
            stopwatch.Stop();
            MessageBox.Show($"{stopwatch.Elapsed}");
        }
        private void Button_Click_Export(object sender, RoutedEventArgs e)
        {
            System.IO.File.WriteAllText(@"C:\Users\szymo\source\repos\PWSZ\PrintAShop\database.json", JsonConvert.SerializeObject(Entries.ToArray()));
        }

        private void Button_Click_Import(object sender, RoutedEventArgs e)
        {
            Entries.Clear();
            dgBilling.Items.Clear();
            InvertList.Clear();
            ChainList.Clear();

            string json = System.IO.File.ReadAllText(@"C:\Users\szymo\source\repos\PWSZ\PrintAShop\database.json");
            Entries = JsonConvert.DeserializeObject<List<Entry>>(json);

            for (int i = 1; i <= Entries.Count; i++)
            {
                dgBilling.Items.Add(Entries[i - 1]);
                InvertList.dodaj(Entries[i - 1], i - 1);
                ChainList.dodaj(Entries[i - 1], i - 1);
            }
        }

        private void Button_Click_BringBackData(object sender, RoutedEventArgs e)
        {
            dgBilling.Visibility = Visibility.Visible;
            dgSearchResults.Items.Clear();
        }



        private void Button_Click_LinearSearch(object sender, RoutedEventArgs e)
        {
            dgSearchResults.Items.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            switch (cb.SelectedIndex)
            {
                case (int)MySearch.MySearch.EntryFocus.Product:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.Product, inProduct.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.SKU:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.SKU, inSKU.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.EAN:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.EAN, inEAN.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Price:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.Price, inPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.NetPrice:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.NetPrice, inNetPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Invoice:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.Invoice, inInvoice.IsChecked.ToString()).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Producer:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.Producer, inProducer.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Date:
                    MySearch.Linear.LinearSearch(Entries, MySearch.MySearch.EntryFocus.Date, inDate.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
            }

            stopwatch.Stop();
            MessageBox.Show($"{stopwatch.Elapsed}");
            dgBilling.Visibility = Visibility.Hidden;
        }

        private void Button_Click_BinarySearch(object sender, RoutedEventArgs e)
        {
            dgSearchResults.Items.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            switch (cb.SelectedIndex)
            {
                case (int)MySearch.MySearch.EntryFocus.Product:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.Product, inProduct.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.SKU:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.SKU, inSKU.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.EAN:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.EAN, inEAN.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Price:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.Price, inPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.NetPrice:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.NetPrice, inNetPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Invoice:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.Invoice, inInvoice.IsChecked.ToString()).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Producer:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.Producer, inProducer.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Date:
                    MySearch.Binary.BinarySearch(Entries, MySearch.MySearch.EntryFocus.Date, inDate.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
            }

            stopwatch.Stop();
            MessageBox.Show($"{stopwatch.Elapsed}");
            dgBilling.Visibility = Visibility.Hidden;
        }

        private void Button_Click_ChainSearch(object sender, RoutedEventArgs e)
        {
            dgSearchResults.Items.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            switch (cb.SelectedIndex)
            {
                case (int)MySearch.MySearch.EntryFocus.Product:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.Product, inProduct.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.SKU:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.SKU, inSKU.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.EAN:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.EAN, inEAN.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Price:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.Price, inPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.NetPrice:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.NetPrice, inNetPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Invoice:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.Invoice, inInvoice.IsChecked.ToString()).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Producer:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.Producer, inProducer.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Date:
                    ChainList.ChainSearch(Entries, MySearch.MySearch.EntryFocus.Date, inDate.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
            }

            stopwatch.Stop();
            MessageBox.Show($"{stopwatch.Elapsed}");
            dgBilling.Visibility = Visibility.Hidden;
        }

        private void Button_Click_InvertedSearch(object sender, RoutedEventArgs e)
        {
            dgSearchResults.Items.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            switch (cb.SelectedIndex)
            {
                case (int)MySearch.MySearch.EntryFocus.Product:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.Product, inProduct.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.SKU:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.SKU, inSKU.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.EAN:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.EAN, inEAN.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Price:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.Price, inPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.NetPrice:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.NetPrice, inNetPrice.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Invoice:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.Invoice, inInvoice.IsChecked.ToString()).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Producer:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.Producer, inProducer.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
                case (int)MySearch.MySearch.EntryFocus.Date:
                    InvertList.InvertSearch(Entries, MySearch.MySearch.EntryFocus.Date, inDate.Text).ForEach(single => dgSearchResults.Items.Add(single));
                    break;
            }

            stopwatch.Stop();
            MessageBox.Show($"{stopwatch.Elapsed}");
            dgBilling.Visibility = Visibility.Hidden;
        }
    }
}
