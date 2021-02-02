using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrintAShop
{
    public class Entry : ObservableObject
    {
        public int ID { get; set; }

        private string product;
        public string Product
        {
            get
            {
                return product;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Field cannot be empty");

                var regex = new Regex(@"^(\w+(\s\w+)?(-\w+)?(_\w+)?)+$");

                if (!regex.IsMatch(value))
                    throw new ArgumentException("Invalid data");

                if (value.Length > 50)
                    throw new ArgumentException("Data is too long");

                OnPropertyChanged(ref product, value);
            }
        }

        private string producer;
        public string Producer
        {
            get
            {
                return producer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Field cannot be empty");

                var regex = new Regex(@"^(\w+(\s\w+)?(-\w)?(_\w)?)+$");

                if (!regex.IsMatch(value))
                    throw new ArgumentException("Invalid data");

                if (value.Length > 50)
                    throw new ArgumentException("Data is too long");

                OnPropertyChanged(ref producer, value);
            }
        }

        private string sku;
        public string Sku
        {
            get
            {
                return sku;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Field cannot be empty");

                var regex = new Regex(@"\s");

                if (regex.IsMatch(value))
                    throw new ArgumentException("SKU cannot have white characters (space, tab, etc.)");

                regex = new Regex(@"^(\w+(-\w+)?)+$");

                if (!regex.IsMatch(value))
                    throw new ArgumentException("You use invalid characters!");

                if (value.Length > 50)
                    throw new ArgumentException("Data is too long");

                OnPropertyChanged(ref sku, value);
            }
        }

        private string ean;
        public string Ean
        {
            get
            {
                return ean;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Field cannot be empty");

                var regex = new Regex(@"^(\d{8}|\d{13}|\d{14}|\d{128})$");

                if (!regex.IsMatch(value))
                    throw new ArgumentException("It's not a valid EAN");

                if (value.Length > 128)
                    throw new ArgumentException("Data is too long");

                OnPropertyChanged(ref ean, value);
            }
        }

        private float price;
        public float Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Product cannot be free");
                if (value < 0)
                    throw new ArgumentException("You cannot pay your customers!");

                var regex = new Regex(@"^\d+\.\d\d\d+$");

                if (regex.IsMatch(value.ToString()))
                    throw new ArgumentException("Please round to 2 decimal places");

                OnPropertyChanged(ref price, (float)Math.Round(value * 100f) / 100f);
            }
        }

        private float netprice;
        public float NetPrice
        {
            get
            {
                return netprice;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Product cannot be free");
                if (value < 0)
                    throw new ArgumentException("You cannot pay your customers!");

                var regex = new Regex(@"^\d+\.\d\d\d+$");

                if (regex.IsMatch(value.ToString()))
                    throw new ArgumentException("Please round to 2 decimal places");

                OnPropertyChanged(ref netprice, (float)Math.Round(value * 100f) / 100f);
            }
        }
        public bool Invoice { get; set; }

        private string date;
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                var regex = new Regex(@"^\d\d-\d\d-\d\d\d\d$");
                if (!regex.IsMatch(value))
                    throw new ArgumentException("Please enter valid format: DD-MM-YYYY");

                OnPropertyChanged(ref date, value);
            }
        }

        public Entry() { }

        public Entry(
            int id,
            string product,
            string producer,
            string sku,
            string ean,
            float price,
            float netPrice,
            bool invoice,
            string date
            )
        {
            this.ID = id;
            this.Product = product;
            this.Producer = producer;
            this.Sku = sku;
            this.Ean = ean;
            this.Price = price;
            this.NetPrice = netPrice;
            this.Invoice = invoice;
            this.Date = date;
        }
    }
}
