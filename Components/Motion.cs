using System;

namespace ECSL.Components
{
    /// <summary>
    /// Represent a motion component.
    /// </summary>
    public class Motion : IComponent
    {

        /// <summary>
        /// Motion speed [pixels/s].
        /// </summary>
        public Single Speed { get; set; }

    }
}
