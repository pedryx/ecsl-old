using Microsoft.Xna.Framework;

namespace ECSL
{
    /// <summary>
    /// Represent a game system base class.
    /// </summary>
    public abstract class GameSystem
    {

        /// <summary>
        /// Game engine.
        /// </summary>
        public Engine Engine { get; private set; }

        /// <summary>
        /// Local entity pool.
        /// </summary>
        public EntityPool Pool { get; private set; }

        /// <summary>
        /// Create new instance of gmae state.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        /// <param name="pool">Local entity pool.</param>
        public GameSystem(Engine engine, EntityPool pool)
        {
            Engine = engine;
            Pool = pool;
        }

        /// <summary>
        /// Update current game system state.
        /// </summary>
        protected internal abstract void Update(GameTime gameTime);

    }
}
