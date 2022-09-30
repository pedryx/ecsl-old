using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace ECSL.Systems
{
    /// <summary>
    /// Represent a game system.
    /// </summary>
    public abstract class GameSystem<T1> : GameSystem
        where T1 : IComponent
    {

        /// <summary>
        /// Contains entities.
        /// </summary>
        private readonly Dictionary<UInt32, Entity> entities; 

        /// <summary>
        /// Create new instance of game system.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        /// <param name="pool">Local entity pool.</param>
        public GameSystem(Engine engine, EntityPool pool)
            : base(engine, pool)
        {
            entities = new Dictionary<UInt32, Entity>();

            Pool.OnComponentAdd += Pool_OnComponentAdd;
            Pool.OnComponentRemove += Pool_OnComponentRemove;
        }

        protected internal override void Update(GameTime gameTime)
        {
            PreUpdate(gameTime);
            foreach (var entity in entities.Values)
            {
                Process(gameTime, (T1)entity.Components[typeof(T1)]);
            }
            PostUpdate(gameTime);
        }

        protected abstract void Process(GameTime gameTime, T1 component1);

        protected virtual void PreUpdate(GameTime gameTime) { }

        protected virtual void PostUpdate(GameTime gameTime) { }

        private void Pool_OnComponentAdd(Object sender, ComponentEventArgs e)
        {
            if (entities.ContainsValue(e.Entity))
                return;

            if (e.Component.GetType() == typeof(T1))
                entities.Add(e.Entity.Id, e.Entity);
        }

        private void Pool_OnComponentRemove(Object sender, ComponentEventArgs e)
        {
            if (!entities.ContainsValue(e.Entity))
                return;

            if (e.Component.GetType() == typeof(T1))
                entities.Remove(e.Entity.Id);
        }

    }
}
