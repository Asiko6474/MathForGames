using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    class Bullet : Actor
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

        public Bullet(float x, float y, string name = "Bullet", string path = "")
            : base(x, y, name, path)
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
            //When the left shift button is pressed, show the hitbox 
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
                Collider.Draw();

        }
        public override void OnCollision(Actor actor)
        {
            if (actor is Player)
            {
                RandomNumber();
            }
        }

        private void RandomNumber()
        {
            Random num = new Random();
            int spawn = num.Next(0, 10);
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

            if (spawn == 5)
            {
                SetTranslation(200, 100);
            }
            if (spawn == 6)
            {
                SetTranslation(750, 200);
            }
            if (spawn == 7)
            {
                SetTranslation(650, 375);
            }
            if (spawn == 8)
            {
                SetTranslation(750, 60);
            }
            if (spawn == 9)
            {
                SetTranslation(523, 230);
            }
            if (spawn == 10)
            {
                SetTranslation(50, 20);
            }
        }
    }
}

