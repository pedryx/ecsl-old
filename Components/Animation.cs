using System;

namespace ECSL.Components
{
    /// <summary>
    /// Represent an animation component.
    /// </summary>
    public class Animation : IComponent
    {

        /// <summary>
        /// Coor of X tile.
        /// </summary>
        public Int32 X { get; set; }

        /// <summary>
        /// Coor of Y tile.
        /// </summary>
        public Int32 Y { get; set; }

        /// <summary>
        /// Tile width.
        /// </summary>
        public Int32 Width { get; set; }

        /// <summary>
        /// Tile height.
        /// </summary>
        public Int32 Height { get; set; }

        /// <summary>
        /// Maximal allowed coor of X tile
        /// </summary>
        public Int32 MaxX { get; set; }

        /// <summary>
        /// Minimal allowed coor of X tile.
        /// </summary>
        public Int32 MinX { get; set; }

        /// <summary>
        /// Determine if animation is paused.
        /// </summary>
        public Boolean Paused { get; set; }

        /// <summary>
        /// Ellapsed time from last tile switching [ms].
        /// </summary>
        public Single Ellapsed { get; set; }

        /// <summary>
        /// Time that must ellapse before next switching [ms].
        /// </summary>
        public Single Speed { get; set; }

        /// <summary>
        /// Create new animation component.
        /// </summary>
        public Animation()
        {
            Width = 64;
            Height = 64;
            MaxX = 8;
            Speed = 100f;
        }

    }
}
