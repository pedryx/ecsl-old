using System;

namespace ECSL.Structures
{

    /// <summary>
    /// Event handler for safe collection events.
    /// </summary>
    /// <typeparam name="TKey">Item key type.</typeparam>
    /// <typeparam name="TValue">Item value type.</typeparam>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void SafeCollectionEventHandler<TKey, TValue>
        (Object sender, SafeCollectionEventArgs<TKey, TValue> e);

    /// <summary>
    /// Represent an arguments for safe collection events.
    /// </summary>
    /// <typeparam name="Tkey">Item key type.</typeparam>
    /// <typeparam name="TValue">Item value <typeparamref name="Tkey"/>.</typeparam>
    public class SafeCollectionEventArgs<Tkey, TValue> : EventArgs
    {

        /// <summary>
        /// Item key.
        /// </summary>
        public Tkey Key { get; private set; }

        /// <summary>
        /// Item value.
        /// </summary>
        public TValue Value { get; private set; }

        /// <summary>
        /// Create new instance of safe collection event arguments.
        /// </summary>
        /// <param name="key">Item key.</param>
        /// <param name="value">Item value.</param>
        public SafeCollectionEventArgs(Tkey key, TValue value)
        {
            Key = key;
            Value = value;
        }

    }
}
