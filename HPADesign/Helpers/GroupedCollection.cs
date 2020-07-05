using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Helpers
{
    public class Grouping<TKey, TElement> : ObservableCollection<TElement>, IGrouping<TKey, TElement>
    {
        public Grouping(TKey key)
        {
            this.Key = key;
        }

        public Grouping(TKey key, IEnumerable<TElement> items)
            : this(key)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public TKey Key { get; }
    }
    public class GroupedCollection<TKey, TElement> : ObservableCollection<Grouping<TKey, TElement>>
    {
        public void Add(TKey key, TElement element)
        {
            FindOrCreateGroup(key).Add(element);
        }
        private Grouping<TKey, TElement> FindOrCreateGroup(TKey key)
        {
            var match = this.Select((group, index) => new { group, index }).FirstOrDefault(i => i.group.Key.Equals(key));
            Grouping<TKey, TElement> result;
            if (match == null)
            {
                // Group doesn't exist and the new group needs to go at the end
                result = new Grouping<TKey, TElement>(key);
                this.Add(result);
            }
            else
            {
                result = match.group;
            }
            return result;
        }
        public bool Remove(TKey key, TElement element)
        {
            var group = this.FirstOrDefault(i => i.Key.Equals(key));
            var success = group != null && group.Remove(element);

            if (group != null && group.Count == 0)
            {
                Remove(group);
            }

            return success;
        }
    }
}
