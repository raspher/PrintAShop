using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAShop.MySearch
{
    class BazaPocz
    {
        public string nazwa { get; set; }
        public int indeks { get; set; }

        public BazaPocz(string nazwa_, int index_)
        {
            this.nazwa = nazwa_;
            this.indeks = index_;
        }
    }

    public class Chain : MySearch
    {
        private List<List<BazaPocz>> bazaPocz { get; set; }
        private List<List<int>> bazaChain { get; set; }

        public Chain()
        {
            this.bazaPocz = new List<List<BazaPocz>>();
            this.bazaChain = new List<List<int>>();

            // dla każdej kolumny utwórz bazę początków i łańcuchów
            for (var i = 0; i < 8; i++)
            {
                this.bazaPocz.Add(new List<BazaPocz>());
                this.bazaChain.Add(new List<int>());
            }
        }

        ~Chain()
        {
            Clear();
        }

        public void dodaj(Entry entry, int entryID)
        {
            /* Product, <- 0
             * SKU, <- 1
             * EAN, <- 2
             * Price, <- 3
             * NetPrice, <- 4
             * Invoice, <- 5 
             * Producer, <- 6
             * Date <- 7
            */

            IndexIt(entry.Product, entryID, 0);
            IndexIt(entry.Sku, entryID, 1);
            IndexIt(entry.Ean, entryID, 2);
            IndexIt(entry.Price.ToString(), entryID, 3);
            IndexIt(entry.NetPrice.ToString(), entryID, 4);
            IndexIt(entry.Invoice.ToString(), entryID, 5);
            IndexIt(entry.Producer, entryID, 6);
            IndexIt(entry.Date, entryID, 7);
        }

        private void IndexIt(string query, int entryID, int entryFocus)
        {
            int found = bazaPocz[entryFocus].FindIndex(e => e.nazwa == query);
            if (found >= 0)
            {
                // znajdź ostatni element łańcucha
                int ostatni = bazaPocz[entryFocus][found].indeks; //bazaChain[entryFocus][bazaPocz[entryFocus][found].indeks];
                int po = ostatni;

                while (ostatni >= 0)
                {
                    po = ostatni;
                    ostatni = bazaChain[entryFocus][ostatni];
                }

                bazaChain[entryFocus][po] = entryID;
            }
            else
            {
                bazaPocz[entryFocus].Add(new BazaPocz(query, entryID));
                
            }
            bazaChain[entryFocus].Add(-1);
        }


        public List<Entry> ChainSearch(List<Entry> entries, MySearch.EntryFocus entryFocus, string exp)
        {
            List<Entry> searchResults = new List<Entry>();

            var found = bazaPocz[(int)entryFocus].Where(e => e.nazwa.Contains(exp));

            foreach(var pocz in found)
            {
                Console.Write(pocz.nazwa + ": ");
                int ostatni = pocz.indeks;
                while (true)
                {
                    Console.Write(ostatni + " ");

                    if (ostatni == -1)
                        break;

                    searchResults.Add(entries[ostatni]);

                    ostatni = bazaChain[(int)entryFocus][ostatni];
                }
                Console.Write("\n");
            }

            return searchResults;
        }

        public void Clear()
        {
            bazaPocz.ForEach(e => e.Clear());
            bazaChain.ForEach(e => e.Clear());
        }
    }
}
