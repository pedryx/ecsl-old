using ECSL.Components;

using Microsoft.Xna.Framework;

using System;

namespace ECSL.Systems
{
    /// <summary>
    /// Represent a motion system.
    /// </summary>
    public class MotionSystem : GameSystem<Transform, Motion>
    {

        /// <summary>
        /// Create new motion system.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        /// <param name="pool">Local entity pool.</param>
        public MotionSystem(Engine engine, EntityPool pool) 
            : base(engine, pool) { }

        protected override void Process(GameTime gameTime, Transform transform, Motion motion)
        {
            transform.Position += new Vector2()
            {
                X = transform.Rotation.X * motion.Speed 
                * (Single)gameTime.ElapsedGameTime.TotalSeconds,
                Y = transform.Rotation.Y * motion.Speed 
                * (Single)gameTime.ElapsedGameTime.TotalSeconds,
            };
        }

    }
}
