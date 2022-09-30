using System;

namespace ECSL
{

    /// <summary>
    /// Represent a handler for component events.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void ComponentEventHandler(Object sender, ComponentEventArgs e);

    /// <summary>
    /// Represent an arguments for component events.
    /// </summary>
    public class ComponentEventArgs : EventArgs
    {

        /// <summary>
        /// Component.
        /// </summary>
        public IComponent Component { get; private set; }

        /// <summary>
        /// Component owner.
        /// </summary>
        public Entity Entity { get; private set; }

        /// <summary>
        /// Create instance of an arguments for component events.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entity">Component owner.</param>
        public ComponentEventArgs(Entity entity, IComponent component)
        {
            Component = component;
            Entity = entity;
        }

    }
}
