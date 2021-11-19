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
        private Actor _target;
        private float _maxSightDistance;
        public UIText SpeechText;
        private float _maxViewAngle;


        //Set the speed for the tagger
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        // Sets the velocity value
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        /// Initializes the 
        /// </summary>
        /// <param name="x">the starting X position</param>
        /// <param name="y">the starting Y position</param>
        /// <param name="speed">the speed of the tagger</param>
        /// <param name="maxViewAngle">The angle required for the tagger to see the player</param>
        /// <param name="maxSightDistance">the max amount of distance the enemy can see the tagger</param>
        /// <param name="target">what the tagger is after</param>
        /// <param name="name">The name of the tagger</param>
        /// <param name="path">the file location of the tagger's sprite</param>
        public Tagger(float x, float y, float speed, float maxViewAngle, float maxSightDistance, Actor target, string name = "Tagger", string path = "")
           : base(x, y, name, path)
        {
            _target = target;
            _speed = speed;
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
            Vector2 moveDirection = (_target.LocalPosition - LocalPosition).Normalized;

            Velocity = moveDirection * Speed * deltaTime;
            
            if (GetTargetInSight())
                LocalPosition += Velocity;

            base.Update(deltaTime);
        }

        /// <summary>
        /// this function allows the tagger to see the target.
        /// </summary>
        /// <returns></returns>
        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.LocalPosition - LocalPosition).Normalized;
            float distanceToTarget = Vector2.Distance(_target.LocalPosition, LocalPosition);

            float dotProduct = Vector2.DotProduct(directionOfTarget, Forward);

            return MathF.Acos(dotProduct) < _maxViewAngle && distanceToTarget < _maxSightDistance;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Bullet)
            {
                Random num = new Random();
                int spawn = num.Next(0, 4);
                if (spawn == 0)
                {
                    SetTranslation(25, 50);
                }
                if (spawn == 1)
                {
                    SetTranslation(700, 200);
                }
                if (spawn == 2)
                {
                    SetTranslation(400, 200);
                }
                if (spawn == 3)
                {
                    SetTranslation(500, 250);
                }
                if (spawn == 4)
                {
                    SetTranslation(250, 400);
                }
            }
            else
            {

                Engine.CloseApplication();
            }
        }

        public override void Draw()
        {
            base.Draw();

            //When the left shift button is pressed, show the hitbox 
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
                Collider.Draw();
        }
    }
}
