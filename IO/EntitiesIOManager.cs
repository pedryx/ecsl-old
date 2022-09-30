using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace ECSL.IO
{
    /// <summary>
    /// Represent an io manager for entities.
    /// </summary>
    public class EntitiesIOManager : IOManager<Entity>
    {

        /// <summary>
        /// Local user directory for temporary files.
        /// </summary>
        private String temp;

        /// <summary>
        /// Create new entities io manager.
        /// </summary>
        public EntitiesIOManager() 
            : base("entity", "Entities")
        {
            temp = Path.Combine(Path.GetTempPath(), "tempEntity");
        }

        /// <summary>
        /// Get null.
        /// </summary>
        /// <returns>Null</returns>
        public override Entity GetErrorItem()
        {
            return null;
        }

        /// <summary>
        /// Get clone of loaded entity with specific name.
        /// </summary>
        /// <param name="name">Specific entity name.</param>
        /// <returns>Clone of loaded entity with specific name.</returns>
        public override Entity GetItem(String name)
        {
            return Items[name].Clone(name);
        }

        /// <summary>
        /// Load entity from specific IO file loacted at specific directory and assign specific name to it.
        /// </summary>
        /// <param name="name">Specifc name that will be assigned to entity after load.</param>
        /// <param name="path">Specific path where will from where will be entity loaded.</param>
        /// <returns>Loaded entity.</returns>
        public override Entity Load(String name, String path)
        {
            Entity entity = new Entity(name);
            String componentsPath = Path.Combine(temp, "Components");
            if (Directory.Exists(temp))
                Directory.Delete(temp, true);

            ZipFile.ExtractToDirectory(path, temp);

            var files = Directory.GetFiles(componentsPath, "*.xml");
            foreach (var file in files)
            {
                IComponent component = LoadComponent(file);
                entity.Components.Add(component, component.GetType());
            }

            Directory.Delete(temp, true);
            entity.Update();
            return entity;
        }

        /// <summary>
        /// Load component from specific file.
        /// </summary>
        /// <param name="file">File where component is saved.</param>
        /// <returns>Loaded component.</returns>
        private IComponent LoadComponent(String file)
        {
            String name = file.Split('/', '\\').Last().Split('.').First();

            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(GetType().Assembly.GetType($"ECSL.Components.{name}"));
                return (IComponent)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Save entity to specific file.
        /// </summary>
        /// <param name="item">Entity to save.</param>
        /// <param name="path">Path where will be entity saved.</param>
        public override void Save(Entity item, String path)
        {
            String componentsPath = Path.Combine(temp, "Components");

            if (Directory.Exists(temp))
                Directory.Delete(temp, true);
            Directory.CreateDirectory(temp);
            Directory.CreateDirectory(componentsPath);
            foreach (IComponent component in item.Components)
            {
                SaveComponent(component, componentsPath);
            }

            if (File.Exists(path))
                File.Delete(path);
                ZipFile.CreateFromDirectory(temp, path);

            Directory.Delete(temp, true);
        }

        /// <summary>
        /// Save component at specific path.
        /// </summary>
        /// <param name="component">Component to save.</param>
        /// <param name="path">Path where will be component saved.</param>
        private void SaveComponent(IComponent component, String path)
        {
            using (FileStream stream = new FileStream(Path.Combine(path, $"{component.GetType().Name}.xml"), FileMode.CreateNew))
            {
                XmlSerializer serializer = new XmlSerializer(component.GetType());
                serializer.Serialize(stream, component);
            }
        }

    }
}
