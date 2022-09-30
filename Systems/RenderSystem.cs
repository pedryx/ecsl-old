using ECSL.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace ECSL.Systems
{
    /// <summary>
    /// Represent a system for rendering.
    /// </summary>
    public class RenderSystem : GameSystem<Transform, Apperance>
    {

        /// <summary>
        /// Local sprite batch.
        /// </summary>
        private readonly SpriteBatch spriteBatch;

        /// <summary>
        /// Local camera.
        /// </summary>
        private readonly Camera camera;

        /// <summary>
        /// Create new instance of render system.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        /// <param name="pool">Local entity pool.</param>
        public RenderSystem(Engine engine, EntityPool pool, Camera camera)
            : base(engine, pool)
        {
            this.camera = camera;
            spriteBatch = new SpriteBatch(engine.GraphicsDevice);
        }

        protected override void PreUpdate(GameTime gameTime)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack,
                transformMatrix: camera.GetTransform());
        }

        protected override void Process(GameTime gameTime, Transform transform, Apperance apperance)
        {
            spriteBatch.Draw(Engine.Sprites[apperance.Texture],
                transform.Position + apperance.Position, apperance.SourceRectangle,
                apperance.Color, ToAngle(transform.Rotation) + ToAngle(apperance.Rotation), apperance.Origin,
                transform.Scale * apperance.Scale, apperance.SpriteEffects, apperance.LayerDepth);
        }

        protected override void PostUpdate(GameTime gameTime)
        {
            spriteBatch.End();
        }

        private Single ToAngle(Vector2 vector)
        {
            return (Single)Math.Atan2(vector.Y, vector.X);
        }

    }
}
