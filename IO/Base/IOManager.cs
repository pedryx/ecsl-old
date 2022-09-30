using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ECSL.IO
{
    /// <summary>
    /// Represent a base class for io managers.
    /// </summary>
    /// <typeparam name="T">Type of object that will be savable and loadable by this manager.</typeparam>
    public abstract class IOManager<T>
        where T : IIdentity
    {

        /// <summary>
        /// Contains loaded items.
        /// </summary>
        protected Dictionary<String, T> Items { get; private set; }

        /// <summary>
        /// File extension of IO files.
        /// </summary>
        public String FileExtension { get; set; }

        /// <summary>
        /// Default directory of IO files.
        /// </summary>
        public String DefaultDirectory { get; set; }

        /// <summary>
        /// Create new IO manager.
        /// </summary>
        /// <param name="fileExtension">File extension of IO files.</param>
        /// <param name="defaultDirectory">Default directory of IO files.</param>
        public IOManager(String fileExtension, String defaultDirectory)
        {
            FileExtension = fileExtension;
            DefaultDirectory = defaultDirectory;

            Items = new Dictionary<String, T>();
        }

        /// <summary>
        /// Load item with specific name from specific path.
        /// </summary>
        /// <param name="name">Specific item name that will be assigned to item after his load.</param>
        /// <param name="stream">File stream with file.</param>
        /// <returns>Loaded item.</returns>
        public abstract T Load(String name, String path);

        /// <summary>
        /// Save specific item to specific path.
        /// </summary>
        /// <param name="item">Item to save.</param>
        /// <param name="path">Path where will be item saved.</param>
        public abstract void Save(T item, String path);

        /// <summary>
        /// Get special error item that is used when searched item is not found.
        /// </summary>
        /// <returns>Special eror item.</returns>
        public abstract T GetErrorItem();

        /// <summary>
        /// Get a loaded item with specific name.
        /// </summary>
        /// <param name="name">Specific loaded item name.</param>
        /// <returns>Loaded item with specific name.</returns>
        public abstract T GetItem(String name);

        /// <summary>
        /// Load item from specific path and assign name that is based on that specific path to it.
        /// </summary>
        /// <param name="path">Specific path where IO file of otem to load is located.</param>
        /// <returns>Loaded item.</returns>
        public T Load(String path) => Load(path.Split('/', '\\').Last().Split('.').First(), path);

        /// <summary>
        /// Load all items, which has IO files in default directory.
        /// </summary>
        public void LoadAll()
        {
            String[] files = Directory.GetFiles(DefaultDirectory,
                        $"*.{FileExtension}", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                T item = Load(file);
                Items.Add(item.Name, item);
            }
        }

        /// <summary>
        /// Get loaded item with specific name.
        /// </summary>
        /// <param name="name">Specific loaded item name.</param>
        /// <returns>Item with specific name, if exist, else error item.</returns>
        public T this[String name]
        {
            get
            {
                if (!Items.ContainsKey(name))
                    return GetErrorItem();

                return GetItem(name);
            }
        }

    }
}
