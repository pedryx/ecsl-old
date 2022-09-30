using ECSL.Components;

using Microsoft.Xna.Framework;

using System;

namespace ECSL.Systems
{
    /// <summary>
    /// Represent an animation system.
    /// </summary>
    public class AnimationSystem : GameSystem<Apperance, Animation>
    {

        /// <summary>
        /// Create new animation system.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        /// <param name="pool">Local entity pool.</param>
        public AnimationSystem(Engine engine, EntityPool pool) 
            : base(engine, pool) { }

        protected override void Process(GameTime gameTime, Apperance apperance, Animation animation)
        {
            if (!animation.Paused)
            {
                animation.Ellapsed += (Single)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (animation.Ellapsed >= animation.Speed)
                {
                    animation.Ellapsed -= animation.Speed;
                    animation.X++;
                    if (animation.X > animation.MaxX)
                        animation.X = animation.MinX;
                }
            }

            apperance.SourceRectangle = new Rectangle(animation.Width * animation.X, animation.Height * animation.Y,
                animation.Width, animation.Height);
        }

    }
}
