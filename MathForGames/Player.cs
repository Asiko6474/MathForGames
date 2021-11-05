using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        public UIText Speech;
        private float _speed;
        private Vector3 _velocity;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(float x, float y, float speed, string name = "Default", Shape shape = Shape.CUBE ) 
            : base( x, y, name, shape)
        {
            _speed = speed;
        }
        
        public override void Update(float deltaTime)
        {
            //Speech.Text = "Player";
            //Speech.LocalPosition = LocalPosition + new Vector2(10, -25);
            //Console.WriteLine("Player " + Position.x + ", " + Position.y);
            
            //Allows the player to move left and right, with A being left and D being right.
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            //Allows the player to move up and down, with W being up and S being down.
            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            
            //create a vector that stores the move input
            Vector3 moveDirection = new Vector3(xDirection, 0 , zDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            LocalPosition += Velocity;

            base.Update(deltaTime);
        }
        

        public virtual void OnCollision(Actor actor)
        {
            //if (actor is Tagger)
            //    Engine.CloseApplication();
        }

        public override void Draw()
        {
            base.Draw();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
            Collider.Draw();
        }
    }
}
