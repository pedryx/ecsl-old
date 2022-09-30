using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace ECSL
{
    /// <summary>
    /// Represent a game state base class.
    /// </summary>
    public abstract class GameState
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
        /// Contains computing systems.
        /// </summary>
        protected HashSet<GameSystem> ComputeSystems { get; set; }

        /// <summary>
        /// Contains renderingSystems.
        /// </summary>
        protected HashSet<GameSystem> RenderSystems { get; set; }

        /// <summary>
        /// COntains factories.
        /// </summary>
        protected HashSet<Factory> Factories { get; set; }

        /// <summary>
        /// Represent a camera.
        /// </summary>
        public Camera Camera { get; private set; }

        /// <summary>
        /// Create new instance of game state.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        public GameState(Engine engine)
        {
            ComputeSystems = new HashSet<GameSystem>();
            RenderSystems = new HashSet<GameSystem>();
            Factories = new HashSet<Factory>();
            Pool = new EntityPool();
            Camera = new Camera(engine);

            Engine = engine;
        }

        /// <summary>
        /// Initialize game state.
        /// </summary>
        protected internal virtual void Initialize()
        {
            foreach (var factory in Factories)
            {
                factory.Craft();
            }
        }

        /// <summary>
        /// Update computing systems thats belongs to this game state.
        /// </summary>
        internal void Update(GameTime gameTime)
        {
            foreach (var system in ComputeSystems)
            {
                system.Update(gameTime);
            }
            Pool.Update();
        }

        /// <summary>
        /// Update render systems thats belongs to this game state.
        /// </summary>
        internal void Render(GameTime gameTime)
        {
            foreach (var system in RenderSystems)
            {
                system.Update(gameTime);
            }
        }

    }
}
