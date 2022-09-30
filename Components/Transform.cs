using Microsoft.Xna.Framework;

using System;

namespace ECSL.Components
{
    /// <summary>
    /// Represent a transform component.
    /// </summary>
    public class Transform : IComponent
    {

        /// <summary>
        /// Entity position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Entity rotation.
        /// </summary>
        public Vector2 Rotation { get; set; }

        /// <summary>
        /// Entity scale.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Create new instance of transform.
        /// </summary>
        public Transform()
        {
            Scale = Vector2.One;
        }

    }
}
