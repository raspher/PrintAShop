using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAShop.MySearch
{
    public class MySearch
    {
        public enum EntryFocus
        {
            Product,
            SKU,
            EAN,
            Price,
            NetPrice,
            Invoice,
            Producer,
            Date
        }

        public static List<Entry> Sort(List<Entry> Entries, EntryFocus entryFocus)
        {
            List<Entry> SortedList = new List<Entry>();

            switch (entryFocus)
            {
                case EntryFocus.Product:
                    SortedList = Entries.OrderBy(o => o.Product).ToList(); return SortedList;
                case EntryFocus.Producer:
                    SortedList = Entries.OrderBy(o => o.Producer).ToList(); return SortedList;
                case EntryFocus.EAN:
                    SortedList = Entries.OrderBy(o => o.Ean).ToList(); return SortedList;
                case EntryFocus.SKU:
                    SortedList = Entries.OrderBy(o => o.Sku).ToList(); return SortedList;
                case EntryFocus.Price:
                    SortedList = Entries.OrderBy(o => o.Price).ToList(); return SortedList;
                case EntryFocus.NetPrice:
                    SortedList = Entries.OrderBy(o => o.NetPrice).ToList(); return SortedList;
                case EntryFocus.Invoice:
                    SortedList = Entries.OrderBy(o => o.Invoice).ToList(); return SortedList;
                case EntryFocus.Date:
                    SortedList = Entries.OrderBy(o => o.Date).ToList(); return SortedList;
            }
            return SortedList;
        }
    }
}
