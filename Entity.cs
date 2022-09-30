using ECSL.IO;
using ECSL.Structures;

using System;

namespace ECSL
{
    /// <summary>
    /// Represent an entity.
    /// </summary>
    public class Entity : IIdentity
    {

        /// <summary>
        /// Local entity pool that owns entity.
        /// </summary>
        public EntityPool Owner { get; private set; }

        /// <summary>
        /// Name of an entity.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Id of an enetiy.
        /// </summary>
        public UInt32 Id { get; private set; }

        /// <summary>
        /// Contains components that belongs to this entity.
        /// </summary>
        public SafeCollection<Type, IComponent> Components { get; private set; }

        /// <summary>
        /// Determine if entity has been loaded.
        /// </summary>
        public Boolean Loaded { get; private set; }

        /// <summary>
        /// Occur when entity is loaded.
        /// </summary>
        public event EventHandler OnLoad;

        /// <summary>
        /// Create new instance of an entity.
        /// </summary>
        /// <param name="name">Entity name.</param>
        public Entity(String name)
        {
            Components = new SafeCollection<Type, IComponent>();
            Name = name;
            Id = 0;
            Loaded = false;

            Components.OnAdd += Components_OnAdd;
            Components.OnRemove += Components_OnRemove;
        }

        /// <summary>
        /// Associate entity to a pool.
        /// </summary>
        /// <param name="owner">Local entity pool thats will now owns the entity.</param>
        public void Associate(EntityPool owner)
        {
            Owner = owner;

            Id = owner.GetNewId();
            owner.Entities.Add(this, Id);
        }

        /// <summary>
        /// Clone entity.
        /// </summary>
        /// <param name="name">Name of cloned entity.</param>
        /// <returns>Cloned entity with speicifc name.</returns>
        public Entity Clone(String name)
        {
            Entity clone = new Entity(name);
            foreach (var component in Components)
            {
                IComponent clonedComponnent = (IComponent)Activator
                    .CreateInstance(component.GetType());

                foreach (var property in component.GetType().GetProperties())
                {
                    property.SetValue(clonedComponnent, property.GetValue(component));
                }
                clone.Components.Add(clonedComponnent, component.GetType());
            }
            return clone;
        }

        /// <summary>
        /// Update current entity state.
        /// </summary>
        public void Update()
        {
            Components.Update();

            if (!Loaded)
            {
                OnLoad?.Invoke(this, new EventArgs());
                Loaded = true;
            }
        }

        public override String ToString()
        {
            return Name;
        }

        private void Components_OnAdd(Object sender, SafeCollectionEventArgs<Type, IComponent> e)
        {
            Owner?.RegisterAdding(this, e.Value);
        }

        private void Components_OnRemove(Object sender, SafeCollectionEventArgs<Type, IComponent> e)
        {
            Owner?.RegisterRemoving(this, e.Value);
        }

    }
}
