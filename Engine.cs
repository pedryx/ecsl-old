using ECSL.IO;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.IO;

namespace ECSL
{
    /// <summary>
    /// Represent a game engine.
    /// </summary>
    public partial class Engine : Game
    {

        /// <summary>
        /// Type finder for always needed types.
        /// </summary>
        private readonly TypeFinder typeFinder;
        /// <summary>
        /// Contains game states.
        /// </summary>
        private readonly Stack<GameState> states;
        /// <summary>
        /// Type of state that will be loaded to state stack firstly.
        /// </summary>
        private Type initialState;

        /// <summary>
        /// Manager of graphical settings.
        /// </summary>
        public GraphicsDeviceManager Graphics { get; private set; }

        /// <summary>
        /// Global IO manager for sprites.
        /// </summary>
        public SpritesIOManager Sprites { get; private set; }

        /// <summary>
        /// Global IO manager for entities.
        /// </summary>
        public EntitiesIOManager Entities { get; private set; }

        /// <summary>
        /// Create new instance of engine.
        /// </summary>
        public Engine()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                IsFullScreen = false,
            };
            typeFinder = new TypeFinder();
            states = new Stack<GameState>();
            Sprites = new SpritesIOManager(this);
            Entities = new EntitiesIOManager();
        }

        #region game loop

        protected override void Initialize()
        {
            IsMouseVisible = true;
            typeFinder.FindTypes(this);
            #region logs
            Console.WriteLine("Game has been initialized!");
            Console.WriteLine($"-Initial State: {initialState.Name}");
            Console.WriteLine($"-Resolution: {Graphics.PreferredBackBufferWidth}x{Graphics.PreferredBackBufferHeight}");
            Console.WriteLine($"-Fullscreen: {Graphics.IsFullScreen}");
            Console.WriteLine($"-Mouse Visible: {IsMouseVisible}");
            Console.WriteLine($"-Game States Count: {typeFinder.GameStates.Count}");
            Console.WriteLine();
            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Sprites.LoadAll();
            Entities.LoadAll();
            #region game states initialization
            foreach (var state in typeFinder.GameStates.Values)
            {
                state.Initialize();
            }
            states.Push(typeFinder.GameStates[initialState]);
            #endregion

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            states.Peek().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MonoGameOrange);
            states.Peek().Render(gameTime);

            base.Draw(gameTime);
        }

        #endregion
        #region game states

        /// <summary>
        /// Set specific state that will be loaded to state stack firstly.
        /// </summary>
        /// <typeparam name="T">Type of specific state.</typeparam>
        public void SetInitialState<T>()
            where T : GameState
        {
            initialState = typeof(T);
        }

        /// <summary>
        /// Switch current active state to specific one.
        /// </summary>
        /// <typeparam name="T">Type specific state.</typeparam>
        public void SwitchState<T>()
        {
            states.Pop();
            states.Push(typeFinder.GameStates[typeof(T)]);

            Console.WriteLine($"Current game state switched to {typeof(T).Name}!");
        }

        /// <summary>
        /// Put specific state at the top of state stack.
        /// </summary>
        /// <typeparam name="T">Specific state.</typeparam>
        public void PushState<T>()
        {
            states.Push(typeFinder.GameStates[typeof(T)]);

            Console.WriteLine($"Current game state switched to {typeof(T).Name}!");
        }

        /// <summary>
        /// Pop one state from state stack.
        /// </summary>
        public void PopState()
        {
            if (states.Count == 1)
                throw new Exception("Cannot pop state when state is the last state in the state stack!");

            states.Pop();

            Console.WriteLine($"Current game state switched to {states.Peek().GetType().Name}!");
        }

        #endregion

    }
}
