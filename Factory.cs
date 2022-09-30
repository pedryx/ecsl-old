namespace ECSL
{
    /// <summary>
    /// Represent a factory for building up scene.
    /// </summary>
    public abstract class Factory
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
        /// Create new factory.
        /// </summary>
        /// <param name="engine">Game engine.</param>
        /// <param name="pool">Local netity pool.</param>
        public Factory(Engine engine, EntityPool pool)
        {
            Engine = engine;
            Pool = pool;
        }

        /// <summary>
        /// Craft entities.
        /// </summary>
        protected internal abstract void Craft();

    }
}
