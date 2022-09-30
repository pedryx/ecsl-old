using ECSL.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;

namespace ECSL.Systems
{
    /// <summary>
    /// Represent a mouse rotation system.
    /// </summary>
    public class MouseRotationSystem : GameSystem<Transform, MouseRotation>
    {

        /// <summary>
        /// Create new mouse rotaiton system.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="pool"></param>
        public MouseRotationSystem(Engine engine, EntityPool pool) 
            : base(engine, pool) { }

        protected override void Process(GameTime gameTime,
            Transform transform, MouseRotation mouseRotation)
        {
            transform.Rotation = Mouse.GetState().Position.ToVector2() 
                - mouseRotation.RotationPoint;
            transform.Rotation = Vector2.Normalize(transform.Rotation);
        }

    }
}
