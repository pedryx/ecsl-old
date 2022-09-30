using System;
using System.Collections.Generic;

namespace ECSL
{
    public partial class Engine
    {

        /// <summary>
        /// Represent a finder of always needed types.
        /// Its find specific types and create instance of them,
        /// game engine always need to have acces to instances if these types.
        /// </summary>
        private class TypeFinder
        {

            /// <summary>
            /// Instances of game states.
            /// </summary>
            public Dictionary<Type, GameState> GameStates { get; private set; }

            /// <summary>
            /// Create new instance of type finder.
            /// </summary>
            public TypeFinder()
            {
                GameStates = new Dictionary<Type, GameState>();
            }

            /// <summary>
            /// Find types that are always needed by engine.
            /// </summary>
            /// <param name="engine">Game engine</param>
            public void FindTypes(Engine engine)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                foreach (var assembly in assemblies)
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.IsSubclassOf(typeof(GameState)))
                            GameStates.Add(type, (GameState)Activator.CreateInstance(type, engine));
                    }
                }
            }

        }

    }
}
