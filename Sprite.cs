using ECSL.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.IO;

namespace ECSL
{
    /// <summary>
    /// Represent a sprite.
    /// </summary>
    public class Sprite : Texture2D, IIdentity
    {

        /// <summary>
        /// Create new instacne of a sprite.
        /// </summary>
        public Sprite(GraphicsDevice graphicsDevice, Int32 width, Int32 height) 
            : base(graphicsDevice, width, height) { }

        /// <summary>
        /// Load sprite from stream.
        /// </summary>
        public new static Sprite FromStream(GraphicsDevice graphicsDevice, Stream stream)
        {
            Texture2D texture = Texture2D.FromStream(graphicsDevice, stream);
            Sprite sprite = new Sprite(graphicsDevice, texture.Width, texture.Height);

            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData(data);
            sprite.SetData(data);

            return sprite;
        }

    }
}
