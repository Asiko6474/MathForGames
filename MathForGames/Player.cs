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

        public Player(char icon, float x, float y, float speed,Color color, string name = "Default" ) 
            : base(icon, x, y,color, name)
        {
            _speed = speed;
        }
        
        public override void Update(float deltaTime)
        {
            Speech.Text = "Player";
            Speech.Position = Position + new Vector2(0, -5);
            //Console.WriteLine("Player " + Position.x + ", " + Position.y);
            
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            int BulletDirectionX = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_EIGHT)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_TWO));
            int BulletDirectionY = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_FOUR)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_SIX));

            //create a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Position += Velocity; 
        }
        

        public virtual void OnCollision(Actor actor)
        {
            //if (actor is Tagger)
            //    Engine.CloseApplication();
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}
