using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAShop.MySearch
{
    class Linear : MySearch
    {
        public static List<Entry> LinearSearch(List<Entry> entries, EntryFocus entryFocus, string exp)
        {
            List<Entry> searchResult = new List<Entry>();

            for (int i = 0; i < entries.Count; i++)
            {
                switch (entryFocus)
                {
                    case EntryFocus.Product:
                        if (entries[i].Product.Contains(exp))
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.Producer:
                        if (entries[i].Producer.Contains(exp))
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.EAN:
                        if (entries[i].Ean.Contains(exp))
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.SKU:
                        if (entries[i].Sku.Contains(exp))
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.Date:
                        if (entries[i].Date.Contains(exp))
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.Price:
                        if (entries[i].Price.ToString() == exp)
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.NetPrice:
                        if (entries[i].NetPrice.ToString() == exp)
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.Invoice:
                        if (entries[i].Invoice.ToString() == exp)
                            searchResult.Add(entries[i]);
                        break;
                }
            }
            return searchResult;
        }
    }
}
