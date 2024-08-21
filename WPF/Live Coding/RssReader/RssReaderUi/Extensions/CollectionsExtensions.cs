using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReaderUi.Extensions
{
    public static class CollectionsExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> elements)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            foreach (T item in elements)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
