using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Tagger : Actor
    {
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

        public Tagger(char icon, float x, float y, float speed, Color color, string name = "Tagger")
            : base(icon, x, y, color, name)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            

            //create a vector that stores the move input
            Vector2 moveDirection = new Vector2(GetPlayerPosition.x, GetPlayerPosition.y);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Position += Velocity;
        }
        public Vector2 GetPlayerPosition
        {
            get { return Position; }
            set { Position = value; }
        }

        public virtual void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision Detected");
        }
    }
}
