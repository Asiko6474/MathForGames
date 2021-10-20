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
        public float _maxViewAngle;
        public float _maxSightDistance;
      

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

        public Tagger(char icon, float x, float y, float speed, float maxViewAngle, float maxSightDistance, Color color, Actor target, string name = "Tagger")
            : base(icon, x, y, color, name)
        {
            _target = target;
            _speed = speed;
            _maxSightDistance = maxSightDistance;
            _maxViewAngle = maxViewAngle;
        }

        public override void Update(float deltaTime)
        {
            //create a vector that stores the move input
            Vector2 moveDirection = ( Position - _target.Position).Normalized;

            Velocity = moveDirection * Speed * deltaTime;

            if (GetTargetInsight())
                Position += Velocity;

            base.Update(deltaTime);
        }

        public bool GetTargetInsight()
        {
            Vector2 directionOfTarget = ( _target.Position - Position).Normalized;
            float distanceToTarget = Vector2.Distance(_target.Position, Position);

            float dotProduct = Vector2.DotProduct(directionOfTarget, Forward);

            return Math.Acos(dotProduct) < _maxViewAngle && distanceToTarget < _maxSightDistance;
        }



        public virtual void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision Detected");
        }
    }
}
