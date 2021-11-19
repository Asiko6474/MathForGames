using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        public UIText Speech;
        private float _speed;
        private Vector2 _velocity;
        private Stopwatch _stopwatch = new Stopwatch();

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(float x, float y, float speed, string name = "Default", string path = "" ) 
            : base( x, y, name, path)
        {
            _speed = speed;
        }
        
        public override void Update(float deltaTime)
        {
            
            //Allows the player to move left and right, with A being left and D being right.
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            //Allows the player to move up and down, with W being up and S being down.
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            float growth = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_X));

            float Rotation = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
           
            //create a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            //if the left shit key is being pressed
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
            {
                // Set the player speed to 100
                Speed = 100;
            }

            //once the left shift button is released
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT_SHIFT))
            {
                // set the player speed to 200
                Speed = 200;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_X))
            {
                Scale(growth + deltaTime, growth + deltaTime);
            }

            if (Raylib.IsKeyReleased(KeyboardKey.KEY_X))
            {
                Scale(growth / deltaTime, growth / deltaTime);
            }
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Rotate(Rotation * deltaTime * 4);


            
            //Scale( growth + deltaTime,  growth * deltaTime);
            LocalPosition += Velocity;

            base.Update(deltaTime);
        }
        


        public override void Draw()
        {
            base.Draw();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
            Collider.Draw();
        }
    }
}
