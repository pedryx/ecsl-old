using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace ECSL.IO
{
    /// <summary>
    /// Represent an IO manager for sprites.
    /// </summary>
    public class SpritesIOManager : IOManager<Sprite>
    {

        /// <summary>
        /// Game engine.
        /// </summary>
        private Engine engine;

        /// <summary>
        /// Create new sprite IO manager.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        public SpritesIOManager(Engine engine) 
            : base("png", "Textures")
        {
            this.engine = engine;
        }

        /// <summary>
        /// Get error sprite.
        /// </summary>
        /// <returns>Error sprite.</returns>
        public override Sprite GetErrorItem()
        {
            return Items["error"];
        }

        /// <summary>
        /// Get sprite with specific name.
        /// </summary>
        /// <param name="name">Specific name of sprite to get.</param>
        /// <returns>Sprite with specific name.</returns>
        public override Sprite GetItem(String name)
        {
            return Items[name];
        }

        /// <summary>
        /// Load sprite from IO file located at specific path and assign specific name to it.
        /// </summary>
        /// <param name="name">Specific name that will be assigned to speite after load.</param>
        /// <param name="stream">Specific path where IO file with texture is located.</param>
        /// <returns>Loaded sprite with assigned name.</returns>
        public override Sprite Load(String name, String path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                Sprite sprite = Sprite.FromStream(engine.GraphicsDevice, stream);
                sprite.Name = name;

                return sprite;
            }
        }

        /// <summary>
        /// Save specific sprite at specific path.
        /// </summary>
        /// <param name="item">Specific sprite to save.</param>
        /// <param name="path">Location where specificc sprite will be saved.</param>
        public override void Save(Sprite item, String path)
        {
            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                item.SaveAsPng(stream, item.Width, item.Height);
            }
        }
    }
}
