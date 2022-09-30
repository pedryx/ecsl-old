using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace ECSL.Components
{
    /// <summary>
    /// Represent an apperance component.
    /// </summary>
    public class Apperance : IComponent
    {

        /// <summary>
        /// Name of the texture thats create apperance of an entity.
        /// </summary>
        public String Texture { get; set; }

        /// <summary>
        /// Relative apperance position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Source rectangle of a texture.
        /// </summary>
        public Rectangle? SourceRectangle { get; set; }

        /// <summary>
        /// Texture color modifier.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Rotation of a texture.
        /// </summary>
        public Vector2 Rotation { get; set; }

        /// <summary>
        /// Origin of a texture.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Scale of a texture.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Effects that are applied on a texture.
        /// </summary>
        public SpriteEffects SpriteEffects { get; set; }

        /// <summary>
        /// Layer where texture will be rendered.
        /// </summary>
        public Single LayerDepth { get; set; }

        public Apperance()
        {
            Texture = String.Empty;
            Color = Color.White;
            Scale = Vector2.One;
        }

    }
}
