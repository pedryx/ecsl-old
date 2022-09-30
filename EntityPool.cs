using ECSL.Structures;

using System;

namespace ECSL
{
    /// <summary>
    /// Represent a pool of entities.
    /// </summary>
    public class EntityPool
    {

        /// <summary>
        /// Id that will be assigned to the next entity.
        /// </summary>
        private UInt32 nextId;

        /// <summary>
        /// Contains entities that belongs to this pool.
        /// </summary>
        public SafeCollection<UInt32, Entity> Entities { get; private set; }

        /// <summary>
        /// Occur when component is added.
        /// </summary>
        public event ComponentEventHandler OnComponentAdd;
        /// <summary>
        /// Occur when component is removed.
        /// </summary>
        public event ComponentEventHandler OnComponentRemove;

        /// <summary>
        /// Create new instance of entity pool.
        /// </summary>
        public EntityPool()
        {
            Entities = new SafeCollection<UInt32, Entity>();
            nextId = 1;

            Entities.OnAdd += Entities_OnAdd;
            Entities.OnRemove += Entities_OnRemove;
        }

        /// <summary>
        /// Register adding of component.
        /// </summary>
        /// <param name="component">Component that has been added.</param>
        internal void RegisterAdding(Entity entity, IComponent component)
        {
            OnComponentAdd?.Invoke(this, new ComponentEventArgs(entity, component));
        }

        /// <summary>
        /// Registyer removing of component.
        /// </summary>
        /// <param name="component">Component that has been removed.</param>
        internal void RegisterRemoving(Entity entity, IComponent component)
        {
            OnComponentRemove?.Invoke(this, new ComponentEventArgs(entity, component));
        }

        /// <summary>
        /// Get new id from entity pool.
        /// </summary>
        internal UInt32 GetNewId()
        {
            return nextId++;
        }

        /// <summary>
        /// Update current entity pool state.
        /// </summary>
        public void Update()
        {
            Entities.Update();
            foreach (Entity entity in Entities)
            {
                entity.Update();
            }
        }

        private void Entities_OnAdd(Object sender, SafeCollectionEventArgs<UInt32, Entity> e)
        {
            foreach (IComponent component in e.Value.Components)
            {
                OnComponentAdd?.Invoke(this, new ComponentEventArgs(e.Value, component));
            }
        }

        private void Entities_OnRemove(Object sender, SafeCollectionEventArgs<UInt32, Entity> e)
        {
            e.Value.Components.Clear();
            e.Value.Update();
        }

    }
}
