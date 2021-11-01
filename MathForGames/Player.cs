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
        private Vector2 _velocity;

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
            Speech.Text = "Player";
            Speech.LocalPosition = LocalPosition + new Vector2(10, -25);
            //Console.WriteLine("Player " + Position.x + ", " + Position.y);
            
            //Allows the player to move left and right, with A being left and D being right.
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            //Allows the player to move up and down, with W being up and S being down.
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //Allows the player to shoot up and down, with eight being up and two being down. The idea of this is to match the direction of shooting with the numpad.
            int BulletDirectionX = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_EIGHT)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_TWO));
            //Allows the player to shoot left and right, with four being left and six being down.
            int BulletDirectionY = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_FOUR)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_SIX));
            
            //create a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
            Forward = Velocity.Normalized;

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
