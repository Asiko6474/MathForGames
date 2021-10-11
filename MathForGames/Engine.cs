using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MathForGames
{
    class Engine
    {
        private static bool _shouldApplicationClose = false;
        private Scene[] _scenes = new Scene[0];
        private static int _currentSceneIndex;
        
        
        /// <summary>
        /// Called to bein the application.
        /// </summary>
        public void Run()
        {
            //Call start for the entire application.
            Start();

            //Loop until the application is told to close.
                while (!_shouldApplicationClose)
                {
                    Update();
                    Draw();
                    Thread.Sleep(150);
                }

            // call end for the entire application.
            End();  
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            Scene scene = new Scene();
            Actor actor = new Actor('@', new MathLibrary.Vector2 { x = 0, y = 0 });

            scene.AddActor(actor);

            _currentSceneIndex = AddScene(scene);

            _scenes[_currentSceneIndex].Start();
           
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update()
        {
            _scenes[_currentSceneIndex].Update();
        }

        /// <summary>
        /// Called everytime the game loops to the update visuals
        /// </summary>
        private void Draw()
        {
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();

        }
        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
        }

        /// <summary>
        /// Addsa a scene to thje engine's scene array
        /// </summary>
        /// <param name="scene">the scene that will be added to the scene array</param>
        /// <returns>the index where the new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Creats a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];
            //copies all values from the old array into the new array
            for (int i = 0; i < _scenes.Length; i ++)
            {
                tempArray[i] = _scenes[i];
            }
            //Set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //set the old array to be the new array
            _scenes = tempArray;
            //return the last array
            return _scenes.Length - 1;
        }
    }
}
