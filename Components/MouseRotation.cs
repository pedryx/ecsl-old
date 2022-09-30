using Microsoft.Xna.Framework;

namespace ECSL.Components
{
    /// <summary>
    /// Represent a mouse rotation component.
    /// </summary>
    public class MouseRotation : IComponent
    {

        /// <summary>
        /// Center point of rotation.
        /// </summary>
        public Vector2 RotationPoint { get; set; }

        public MouseRotation()
        {
            RotationPoint = new Vector2(960, 540);
        }

    }

}
