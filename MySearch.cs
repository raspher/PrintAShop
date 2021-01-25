using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAShop
{
    public class MySearchA
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
                        if (entries[i].Price == double.Parse(exp))
                            searchResult.Add(entries[i]);
                        break;
                    case EntryFocus.NetPrice:
                        if (entries[i].NetPrice == double.Parse(exp))
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

        public static List<Entry> BinarySearch(List<Entry> entries, EntryFocus entryFocus, string exp)
        {
            entries = Sort(entries, entryFocus);
            List<Entry> searchResults = new List<Entry>();
            int foundIndex = BS(entries, entryFocus, exp, entries.Count() - 1);

            switch (entryFocus)
            {
                case EntryFocus.Product:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Product == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Product == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.Producer://Szukanie początku występowania
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Producer == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Producer == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.EAN:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Ean == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Ean == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.SKU:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Sku == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Sku == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.Date:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Date == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Date == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.Price://Szukanie początku występowania
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Price.ToString() == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Price.ToString() == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.NetPrice:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].NetPrice.ToString() == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].NetPrice.ToString() == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.Invoice:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Invoice.ToString() == exp; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Invoice.ToString() == exp; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;
            }

            return searchResults;

        }

        private static int BSIsItInRight(char[] _string, char[] exp, int num = 0)
        {
            if (_string.Length - 1 < num || exp.Length - 1 < num)
                return -1;
            if (_string[num] > exp[num])
                return 0;
            if (_string[num] < exp[num])
                return 1;


            return BSIsItInRight(_string, exp, num + 1);
        }

        public static int BS(List<Entry> entries, EntryFocus entryFocus, string exp, int max, int min = 0)
        {
            int mid = (min + max) / 2;
            int checkResult;
            switch (entryFocus)
            {
                case EntryFocus.Product:
                    checkResult = BSIsItInRight(entries[mid].Product.ToArray(), exp.ToArray());
                    if (entries[mid].Product == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Producer:
                    checkResult = BSIsItInRight(entries[mid].Producer.ToArray(), exp.ToArray());

                    if (entries[mid].Producer == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.EAN:
                    checkResult = BSIsItInRight(entries[mid].Ean.ToArray(), exp.ToArray());

                    if (entries[mid].Ean == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.SKU:
                    checkResult = BSIsItInRight(entries[mid].Sku.ToArray(), exp.ToArray());

                    if (entries[mid].Sku == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Date:
                    checkResult = BSIsItInRight(entries[mid].Date.ToArray(), exp.ToArray());

                    if (entries[mid].Date == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Price:
                    checkResult = BSIsItInRight(entries[mid].Price.ToString().ToArray(), exp.ToArray());

                    if (entries[mid].Price.ToString() == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.NetPrice:
                    checkResult = BSIsItInRight(entries[mid].NetPrice.ToString().ToArray(), exp.ToArray());

                    if (entries[mid].NetPrice.ToString() == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Invoice:
                    checkResult = BSIsItInRight(entries[mid].Invoice.ToString().ToArray(), exp.ToArray());

                    if (entries[mid].Invoice.ToString() == exp)
                        return mid;
                    else if (checkResult == -1)
                        return -1;
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 0)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;
            }
            return -1;
        }

        public static int ChainSearchPointer(List<Entry> entries, EntryFocus entryFocus, string exp)
        {
            for (int i = entries.Count - 1; i >= 0; i--)
            {
                switch (entryFocus)
                {
                    case EntryFocus.Product:
                        if (entries[i].Product == exp)
                            return i;
                        break;
                    case EntryFocus.Producer:
                        if (entries[i].Producer == exp)
                            return i;
                        break;
                    case EntryFocus.EAN:
                        if (entries[i].Ean == exp)
                            return i;
                        break;
                    case EntryFocus.SKU:
                        if (entries[i].Sku == exp)
                            return i;
                        break;
                    case EntryFocus.Date:
                        if (entries[i].Date == exp)
                            return i;
                        break;
                    case EntryFocus.Price:
                        if (entries[i].Price.ToString() == exp)
                            return i;
                        break;
                    case EntryFocus.NetPrice:
                        if (entries[i].NetPrice.ToString() == exp)
                            return i;
                        break;
                    case EntryFocus.Invoice:
                        if (entries[i].Invoice.ToString() == exp)
                            return i;
                        break;
                }
            }

            return -1;
        }

        
        

        

        
    }
}
