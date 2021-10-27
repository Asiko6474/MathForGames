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
            _transform = _translation * _rotation * _scale;
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

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">The value used to scale the matrix in the x axis.</param>
        /// <param name="y">The value used to scale the matrix in the y axis.</param>
        public void SetTranslation(float x, float y)
        {

        }

        /// <summary>
        /// Increases the position of the actor by the given values.
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY)
        {

        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radians)
        {

        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radians)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetScale(float x, float y)
        {
            _transform.M00 = x;
            _transform.M11 = y;
        }

        /// <summary>
        /// Scales the actor by the given amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Scale(float x, float y)
        {

        }
    }
}
