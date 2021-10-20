using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    
    class Tagger : Actor
    {
        private Actor _target;
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

        public Tagger(char icon, float x, float y, float speed, Color color, Actor target, string name = "Tagger")
            : base(icon, x, y, color, name)
        {
            _target = target;
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            //create a vector that stores the move input
            Vector2 moveDirection = (_target.Position - Position).Normalized;

            Velocity = moveDirection * Speed * deltaTime;

            if (GetTargetInsight())
                Position += Velocity;

            base.Update(deltaTime);
        }

        public bool GetTargetInsight()
        {
            Vector2 directionOfTarget = (_target.Position - Position).Normalized;

            return Vector2.DotProduct(directionOfTarget, Forward) > 0 && Vector2.DotProduct(directionOfTarget, Forward) < 25;
        }



        public virtual void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision Detected");
        }
    }
}
