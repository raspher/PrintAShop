using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAShop.MySearch
{
    class Binary : MySearch
    {
        public static List<Entry> BinarySearch(List<Entry> entries, EntryFocus entryFocus, string exp)
        {
            entries = Sort(entries, entryFocus);
            List<Entry> searchResults = new List<Entry>();
            int foundIndex = BS(entries, entryFocus, exp, entries.Count() - 1);

            switch (entryFocus)
            {
                case EntryFocus.Product:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Product.Contains(exp) ; i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Product.Contains(exp) ; i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.Producer://Szukanie początku występowania
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Producer.Contains(exp); i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Producer.Contains(exp); i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.EAN:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Ean.Contains(exp); i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Ean.Contains(exp); i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.SKU:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Sku.Contains(exp); i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Sku.Contains(exp); i++)
                    {
                        searchResults.Add(entries[i]);
                    }
                    break;

                case EntryFocus.Date:
                    // przeszukaj w lewo od znalezionego
                    for (int i = foundIndex; i > -1 && entries[i].Date.Contains(exp); i--)
                    {
                        searchResults.Add(entries[i]);
                    }

                    // przeszukaj w prawo od znalezionego
                    for (int i = foundIndex + 1; i < entries.Count && entries[i].Date.Contains(exp); i++)
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

        public static int BS(List<Entry> entries, EntryFocus entryFocus, string exp, int max, int min = 0)
        {
            int mid = (min + max) / 2;
            int checkResult;
            switch (entryFocus)
            {
                case EntryFocus.Product:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].Product.CompareTo(exp);
                    if (entries[mid].Product.Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Producer:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].Producer.CompareTo(exp);
                    if (entries[mid].Producer.Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.EAN:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].Ean.CompareTo(exp);
                    if (entries[mid].Ean.Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.SKU:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].Sku.CompareTo(exp);
                    if (entries[mid].Sku.Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Date:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].Date.CompareTo(exp);
                    if (entries[mid].Date.Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Price:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].Price.ToString().CompareTo(exp);
                    if (entries[mid].Price.ToString().Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.NetPrice:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].NetPrice.ToString().CompareTo(exp);
                    if (entries[mid].NetPrice.ToString().Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;

                case EntryFocus.Invoice:
                    if (mid >= entries.Count() ||
                        min >= entries.Count() ||
                        max >= entries.Count() ||
                        min < 0 || max < 0 || mid < 0)
                        return -1; // not found

                    checkResult = entries[mid].Invoice.ToString().CompareTo(exp);
                    if (entries[mid].Invoice.ToString().Contains(exp))
                        return mid;
                    else if (checkResult == -1)
                        return BS(entries, entryFocus, exp, max, mid + 1);
                    else if (checkResult == 1)
                        return BS(entries, entryFocus, exp, mid - 1, min);
                    break;
            }
            return -1;
        }
    }
}
