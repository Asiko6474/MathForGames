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
        private Vector3 _velocity;
        private Actor _target;
        private float _maxSightDistance;
        public UIText SpeechText;
        private float _maxViewAngle;


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

        public Tagger(float x, float y, float speed, float maxSightDistance, float maxViewAngle,
            Actor target, string name = "Actor", Shape shape = Shape.CUBE)
            : base(x, y, name, shape)
        {
            _speed = speed;
            _target = target;
            _maxSightDistance = maxSightDistance;
            _maxViewAngle = maxViewAngle;
        }


        public override void Start()
        {
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            //Create a vector that stores the move input
            Vector3 moveDirection = (_target.LocalPosition - LocalPosition).Normalized;

            Velocity = moveDirection * Speed * deltaTime;

            if (GetTargetInSight())
                LocalPosition += Velocity;

            base.Update(deltaTime);
        }

        public bool GetTargetInSight()
        {
            Vector3 directionOfTarget = (_target.LocalPosition - LocalPosition).Normalized;
            float distanceToTarget = Vector3.Distance(_target.LocalPosition, LocalPosition);

            float dotProduct = Vector3.DotProduct(directionOfTarget, Forward);

            return MathF.Acos(dotProduct) < _maxViewAngle && distanceToTarget < _maxSightDistance;
        }

        public override void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision occured");
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}
