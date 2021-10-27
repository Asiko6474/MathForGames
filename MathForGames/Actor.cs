using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    class Actor
    {
        
        private string _name;
        private Vector2 _position;
        private bool _started;
        private Vector2 _forward = new Vector2(1,0);
        public Collider _collider;
        private Matrix3 _transform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private Sprite _sprite;


        /// <summary>
        /// True if the start function has been called for this actor.
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public Vector2 Forward
        {
            get { return _forward; }
            set { _forward = value; }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(_transform.M02, _transform.M12);  }
            set { _transform.M02 = value.x; _transform.M12 = value.y;  }
        }


        //X position, Y position, Name, sprite
        public Actor( float x, float y, string name = "Arthurd", string path = "") :
            this( new Vector2 { x = x, y = y }, name, path) {}

        public Actor(Vector2 position, string name = "Arthurd", string path = "")
        {
            Position = position;
            _name = name;

            if (path != "")
                _sprite = new Sprite(path);
        }

        public virtual void OnCollision(Actor actor)
        {
            //Console.WriteLine("collision detected");
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {

            //Console.WriteLine(_name = "Jake " + Position.x + ", " + Position.y);
           
        }

        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(_transform);
        }

        public virtual void End()
        {

        }

        /// <summary>
        /// Checks if this actor collided with another actor
        /// </summary>
        /// <param name="other">The actor to check for a collision against</param>
        /// <returns>True if the distance between the actors is less than the radii of the two combined</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Return flse if either actor doesn't have a collider attached
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }

        public void SetScale(float x, float y)
        {
            _transform.M00 = x;
            _transform.M11 = y;
        }
    }
}
