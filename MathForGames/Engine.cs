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
        private Camera3D _camera = new Camera3D();
        
        
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

        private void InitializeCamera()
        {
            _camera.position = new System.Numerics.Vector3(0, 10, 10); // camera position
            _camera.target = new System.Numerics.Vector3(0, 0, 0); // camera focousing point
            _camera.up = new System.Numerics.Vector3(0, 1, 0); // camera up vecotr (rotation towards target)
            _camera.fovy = 45; // Camera field of view Y
            _camera.projection = CameraProjection.CAMERA_PERSPECTIVE; //Camera mode type 
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

            InitializeCamera();


            Scene scene = new Scene();

            Player sun = new Player(0, 0, 50, "pepsiSun", Shape.SPHERE);
            sun.SetScale(1, 1, 1);
            CircleCollider PlayerCollider = new CircleCollider(1, sun);
            sun.Collider = PlayerCollider;
            scene.AddActor(sun);
            sun.LookAt(new Vector3(1, 0, 0));
            

            
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
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.DARKGRAY);
            Raylib.DrawGrid(50, 1);

            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

            Raylib.EndMode3D();
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
