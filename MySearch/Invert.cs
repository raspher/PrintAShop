using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAShop.MySearch
{
    class BazaInvert
    {
        public List<string> nazwy { get; set; }
        public List<List<int>> indeksy { get; set; }

        public BazaInvert(){
            this.nazwy = new List<string>();
            this.indeksy = new List<List<int>>();
        }
    }

    public class Invert
    {
        private List<BazaInvert> bazaInvert { get; set; }

        public Invert()
        {
            this.bazaInvert = new List<BazaInvert>();

            // dla każdej kolumny utwórz bazę
            for (var i = 0; i < 8; i++)
            {
                this.bazaInvert.Add(new BazaInvert());
            }
        }

        ~Invert()
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
            if (bazaInvert[entryFocus].nazwy.Contains(query))
            {
                bazaInvert[(int)entryFocus].indeksy[bazaInvert[entryFocus].nazwy.IndexOf(query)].Add(entryID);
            }
            else
            {
                // numer nazwy w liście jest jednocześnie numerem indeksu słowa
                // pierwszy wymiar w "indeksy" to słowo, drugi to indeksy występowania słowa
                bazaInvert[(int)entryFocus].nazwy.Add(query);
                bazaInvert[(int)entryFocus].indeksy.Add(new List<int> { entryID });
            }
        }


        public List<Entry> InvertSearch(List<Entry> entries, MySearch.EntryFocus entryFocus, string exp)
        {
            List<Entry> searchResults = new List<Entry>();

            // wyszukaj indeksy - entryfocus to kolumna
            for (var i = 0; i < bazaInvert[(int)entryFocus].nazwy.Count; i++)
            {
                if (bazaInvert[(int)entryFocus].nazwy[i].Contains(exp))
                {
                    Console.Write(bazaInvert[(int)entryFocus].nazwy[i] + ": ");
                    foreach (var index in bazaInvert[(int)entryFocus].indeksy[i])
                    {
                        searchResults.Add(entries[index]);
                        Console.Write(index + ", ");
                    }
                    Console.Write("\n");
                    break;
                }
            }

            return searchResults;
        }

        public void Clear()
        {
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < bazaInvert[i].nazwy.Count; j++)
                {
                    bazaInvert[i].indeksy[j].Clear();
                }
                bazaInvert[i].nazwy.Clear();
                bazaInvert[i].indeksy.Clear();
            }
        }
    }
}