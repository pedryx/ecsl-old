using ECSL.Components;

using Microsoft.Xna.Framework;

//todo: fix bug with invalid transform when camera doesnt follow any target
namespace ECSL
{
    /// <summary>
    /// Represent a camera.
    /// </summary>
    public class Camera
    {

        /// <summary>
        /// Target thjat will camera follow.
        /// </summary>
        public Transform Target { get; set; }

        /// <summary>
        /// Camera origin.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Create new camera.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        public Camera(Engine engine)
        {
            Origin = new Vector2()
            {
                X = engine.Graphics.PreferredBackBufferWidth / 2,
                Y = engine.Graphics.PreferredBackBufferHeight / 2,
            };
        }

        /// <summary>
        /// Calculate world transform matrix.
        /// </summary>
        public Matrix GetTransform()
        {
            var matrix = Matrix.Identity * Matrix.CreateTranslation(Origin.X, Origin.Y, 0);

            if (Target != null)
                matrix *= Matrix.CreateTranslation(-Target.Position.X, -Target.Position.Y, 0);

            return matrix;
        }

    }
}
