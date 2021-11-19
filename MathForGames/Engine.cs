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
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
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
            Raylib.InitWindow(800, 450, "Pepsi for TV Game");
            Raylib.SetTargetFPS(60);

            Scene scene = new Scene();

            //Starts up the player with the following information: X position, Y position, speed, Name, Sprite.
            Player pepsiPlayer = new Player(100, 100, 150, "Player", "Images/PepsiSun.png");
            //Sets up the circle collider for the pepsi planet
            CircleCollider SunBoxCollider = new CircleCollider(10, pepsiPlayer);
            //sets up the mentos gun that will be attached to the player
            Bullet playerGun = new Bullet(0, 0, "planet", "images/gun.png");
            //sets up the mento's box collider. Note: this should be a box to fit the scale of the model better.
            AABBCollider PlanetBoxCollider = new AABBCollider(15, 50, playerGun);
            //X position, Y position, speed, Max ViewAngle, Max sight distance, Target, Name, Sprite.
            Tagger tagger = new Tagger(50, 200, 150, 800, 800, pepsiPlayer, "Actor", "Images/Enemy.png");
            //Collider but for circles
            CircleCollider enemyCollider = new CircleCollider(39, tagger);

            //Sets the size of the player
            pepsiPlayer.SetScale(75, 75);
            //sets the spawn point of the player
            pepsiPlayer.SetTranslation(400, 225);
            //Make the player displayed
            scene.AddActor(pepsiPlayer);
            //assign the player a collider
            pepsiPlayer.Collider = SunBoxCollider;

            //sets up the player's gun
            playerGun.SetScale(50, 50);
            playerGun.SetTranslation(400, 150);
            ////Sets the gun to be a child of the player
            //pepsiPlayer.AddChild(playerGun);
            //Adds in the gun for the player
            scene.AddActor(playerGun);
            //Sets the rotation of the gun, this rotation should never change by itself.
            playerGun.SetRotation(99);
            //Sets the collider for the gun. The player should have this to be able to use the gun as a melee weapon.
            playerGun.Collider = PlanetBoxCollider;

            //adds in the enemy
            scene.AddActor(tagger);
            //Sets the size of the enemy
            tagger.SetScale(75, 75);
            //Sets the collider to be part of the enemy
            tagger.Collider = enemyCollider;

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
