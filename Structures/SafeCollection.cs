using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSL.Structures
{
    /// <summary>
    /// Represent a collection that is safe for itterations.
    /// (After adding or removing an item there is need to update colletion via Update method!)
    /// </summary>
    /// <typeparam name="Tkey">Item key type.</typeparam>
    /// <typeparam name="TValue">Item value type.</typeparam>
    public class SafeCollection<Tkey, TValue> : IEnumerable
    {

        /// <summary>
        /// Contains items.
        /// </summary>
        private readonly Dictionary<Tkey, TValue> items;
        /// <summary>
        /// Contains items that will be added in next update call.
        /// </summary>
        private readonly HashSet<ValuePair> toAdd;
        /// <summary>
        /// Contains keys of items that will be removed in next update call.
        /// </summary>
        private readonly HashSet<Tkey> toRemove;

        /// <summary>
        /// Occur when item is added to collection.
        /// </summary>
        public event SafeCollectionEventHandler<Tkey, TValue> OnAdd;
        /// <summary>
        /// Occur when item is removed from collection.
        /// </summary>
        public event SafeCollectionEventHandler<Tkey, TValue> OnRemove;

        /// <summary>
        /// Create new instance of safe collection.
        /// </summary>
        public SafeCollection()
        {
            items = new Dictionary<Tkey, TValue>();
            toAdd = new HashSet<ValuePair>();
            toRemove = new HashSet<Tkey>();
        }

        /// <summary>
        /// Add item to collection.
        /// (Item will be fully added after next update call!)
        /// </summary>
        /// <param name="value">Item value.</param>
        /// <param name="key">Item key.</param>
        public void Add(TValue value, Tkey key)
        {
            toAdd.Add(new ValuePair(value, key));
        }

        /// <summary>
        /// Remove an item with specific key from collection.
        /// (Item will be fully removed after next update call.)
        /// </summary>
        /// <param name="key">Specific item key.</param>
        public void Remove(Tkey key)
        {
            toRemove.Add(key);
        }

        /// <summary>
        /// Determine if collection contains item with specific value.
        /// </summary>
        /// <param name="value">Specific item value.</param>
        /// <returns>True, if collection contains item with specific value.</returns>
        public Boolean HasValue(TValue value)
        {
            return items.ContainsValue(value);
        }

        /// <summary>
        /// Determine if collection contains item with specific key.
        /// </summary>
        /// <param name="key">Specific item key.</param>
        /// <returns>True, if collection contains item with specific key.</returns>
        public Boolean HasKey(Tkey key)
        {
            return items.ContainsKey(key);
        }

        /// <summary>
        /// Get collection of all values that collection contains.
        /// </summary>
        /// <returns>Collection of all values that collection contains.</returns>
        public ICollection<TValue> GetValues()
        {
            return items.Values;
        }

        /// <summary>
        /// Get collection of all keys that collection contains.
        /// </summary>
        /// <returns>Collection of all keys that collection contains.</returns>
        public ICollection<Tkey> GetKeys()
        {
            return items.Keys;
        }

        /// <summary>
        /// Delete all item in collection and erase all adding and removing requests.
        /// </summary>
        public void Clear()
        {
            toRemove.Clear();
            toAdd.Clear();
            foreach (var key in items.Keys)
            {
                toRemove.Add(key);
            }
        }

        /// <summary>
        /// Update current collection state.
        /// (Process all adding and removng requests.)
        /// </summary>
        public void Update()
        {
            foreach (var key in toRemove)
            {
                items.Remove(key);
                OnRemove?.Invoke(this, new SafeCollectionEventArgs<Tkey, TValue>(key, items[key]));
            }
            foreach (var item in toAdd)
            {
                items.Add(item.Key, item.Value);
                OnAdd?.Invoke(this, new SafeCollectionEventArgs<Tkey, TValue>(item.Key, item.Value));
            }
            toRemove.Clear();
            toAdd.Clear();
        }

        /// <summary>
        /// Erase all pending requests for adding and removing.
        /// </summary>
        public void EraseRequests()
        {
            toAdd.Clear();
            toRemove.Clear();
        }

        /// <summary>
        /// Return enumerator that itterates throught the collection of item values.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        /// <summary>
        /// Get value with specific key.
        /// </summary>
        /// <param name="key">Specific item key.</param>
        /// <returns>Value of an item with specific key.</returns>
        public TValue this[Tkey key]
        {
            get
            {
                return items[key];
            }
        }

        /// <summary>
        /// Represent a value-key pair.
        /// </summary>
        private class ValuePair
        {

            /// <summary>
            /// Item value.
            /// </summary>
            public TValue Value { get; private set; }

            /// <summary>
            /// Item key.
            /// </summary>
            public Tkey Key { get; private set; }

            /// <summary>
            /// Create new instance of value-key pair.
            /// </summary>
            /// <param name="value">Item value.</param>
            /// <param name="tkey">Item key.</param>
            public ValuePair(TValue value, Tkey key)
            {
                Value = value;
                Key = key;
            }

        }

    }
}
