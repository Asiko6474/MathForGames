using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
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
                while (!Raylib.WindowShouldClose())
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
            //Create a window using raylib
            Raylib.InitWindow(800,450, "Math For Games");

            Scene scene = new Scene();
            
            Player player = new Player('@', 5, 5, 1,Color.RED, "Player");

            scene.AddActor(player);

            _currentSceneIndex = AddScene(scene);

            _scenes[_currentSceneIndex].Start();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update()
        {
            _scenes[_currentSceneIndex].Update();

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called everytime the game loops to the update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }
        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
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


        ///// <summary>
        ///// Gets the next key in the input stream
        ///// </summary>
        ///// <returns>The key that was pressed</returns>
        //public static ConsoleKey GetNextKey()
        //{
        //    //If there is no key being pressed....
        //    if (!Console.KeyAvailable)
        //        //...return
        //        return 0;

        //    //Return the current key being pressed4
        //    return Console.ReadKey(true).Key;
        //}


        /// <summary>
        /// Ends the game and closes the window. 
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
