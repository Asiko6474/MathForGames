using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private Scene[] _scenes = new Scene[0];
        private static int _currentSceneIndex;
        private Stopwatch _stopwatch = new Stopwatch();
        
        
        /// <summary>
        /// Called to bein the application.
        /// </summary>
        public void Run()
        {
            //Call start for the entire application.
            Start();
            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;
            //Loop until the application is told to close.
                while (!Raylib.WindowShouldClose())
                {
                //Get how much time has passed since the application started
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time 
                deltaTime = currentTime - lastTime;
                //Update the application
                    Update(deltaTime);
                //Draw all items
                    Draw();
                
                //Set the last time recorded to be the current time
                lastTime = currentTime;
                }

            // call end for the entire application.
            End();  
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            _stopwatch.Start();
            //Create a window using raylib
            Raylib.InitWindow(800,450, "Math For Games");
            Raylib.SetTargetFPS(60);

            Scene scene = new Scene();

            //X position, Y position, speed, Max ViewAngle, Max sight distance, Color, Name
            Player player = new Player('@', 5, 5, 150, Color.RED, "Player");
            CircleCollider PlayerCollider = new CircleCollider(15, player);
            player.Collider = PlayerCollider;

            //X position, Y position, speed, Max ViewAngle, Max sight distance, Color, Target, Name
            Tagger tagger = new Tagger('&', 50, 200, 100, 100, 100, Color.BLUE, player, "Actor");
            CircleCollider enemyCollider = new CircleCollider(15, tagger);
            tagger.Collider = enemyCollider;
            Tagger tagger2 = new Tagger('&', 40, 100, 100, 100, 100, Color.BLUE, player, "Actor");
            
            Tagger tagger3 = new Tagger('&', 200, 50, 100, 100, 100, Color.BLUE, player, "Actor");
            

            // x position, y position, name, color, width, height, fontsize, text
            UIText taggerText = new UIText(50, 20, "test", Color.DARKGREEN, 70, 70, 15, "I should not be able to see this text");
            UIText playerText = new UIText(50, 20, "test", Color.DARKGREEN, 70, 70, 15, "I should not be able to see this text");
            tagger.Speech = taggerText;
            player.Speech = playerText;

            scene.AddUIElement(playerText);
            scene.AddUIElement(taggerText);
            scene.AddActor(tagger);
            //scene.AddActor(tagger2);
            //scene.AddActor(tagger3);
            scene.AddActor(player);
            _currentSceneIndex = AddScene(scene);
            _scenes[_currentSceneIndex].Start();
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update(float deltaTime)
        {
            _scenes[_currentSceneIndex].Update(deltaTime);

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
            _scenes[_currentSceneIndex].DrawUI();

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
