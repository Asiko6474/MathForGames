using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    public enum Shape
    {
        CUBE,
        SPHERE
    }
    class Actor
    {
        
        private string _name;
        private bool _started;
        private Vector2 _forward = new Vector2(1,0);
        public Collider _collider;
        private Matrix3 _globalTransform = Matrix3.Identity;
        private Matrix3 _localTransform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private Shape _shape;
        private Sprite _sprite;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Color _color;

        public Color ShapeColor
        {
            get { return _color; }
        }
        /// <summary>
        /// True if the start function has been called for this actor.
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        /// <summary>
        /// Sets the collider for the actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        /// <summary>
        /// Sets the movement of the actor
        /// </summary>
        public Vector2 Forward
        {
            get { return new Vector2(_rotation.M00, _rotation.M10); }
        }

        /// <summary>
        /// Sets the sprite
        /// </summary>
        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        /// <summary>
        /// position around the specific point
        /// </summary>
        public Vector2 LocalPosition
        {
            get { return new Vector2(_translation.M02, _translation.M12); }
            set
            {
                SetTranslation(value.X, value.Y);
            }
        }

        /// <summary>
        /// The position of this actor in the world 
        /// </summary>
        public Vector2 WorldPosition
        {
            //Return the global transform's T column
            get { return new Vector2(_globalTransform.M02, _globalTransform.M12); }
            set
            {
                //If the actor has a parent...
                if (Parent != null)
                {
                    //...convert the world cooridinates into local cooridinates and translate the actor
                    float xOffset = (value.X - Parent.WorldPosition.X) / new Vector2(GlobalTransform.M00, GlobalTransform.M10).Magnitude;
                    float yOffset = (value.Y - Parent.WorldPosition.Y) / new Vector2(GlobalTransform.M10, GlobalTransform.M11).Magnitude;
                    
                    SetTranslation(xOffset, yOffset);
                }
                //If this actor doesn't have a parent...
                else
                    //...set local position to be the given value
                    LocalPosition = value;
            }
        }

        /// <summary>
        /// Moves around the world position 
        /// </summary>
        public Matrix3 GlobalTransform
        {
            get { return _globalTransform; }
            set
            {
                _globalTransform = value;
            }
        }

        /// <summary>
        /// Moves around the local position
        /// </summary>
        public Matrix3 LocalTransform
        {
            get { return _localTransform; }
            set { _localTransform = value; }
        }

        /// <summary>
        /// Sets the parent for a child
        /// </summary>
        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Sets the children for the parent
        /// </summary>
        public Actor[] Children
        {
            get { return _children; }
        }
        /// <summary>
        /// edits the size of an actor.
        /// </summary>
            public Vector2 Size
        {
            get 
            {
                float xScale = new Vector2(_scale.M00, _scale.M10).Magnitude;
                float yScale = new Vector2(_scale.M01, _scale.M11).Magnitude;
                return new Vector2(xScale, yScale);  
            }
            set { SetScale(value.X, value.Y); }
        }

        /// <summary>
        /// Initializes the actor's values
        /// </summary>
        /// <param name="x">The starting X position</param>
        /// <param name="y">The startomg Y position</param>
        /// <param name="name">The name of the actor (Arthurd by default)</param>
        /// <param name="path">The file location the actor will find their sprite in.</param>
        public Actor( float x, float y, string name = "Arthurd", string path = "") :
            this( new Vector2 { X = x, Y = y }, name, path) {}

        public Actor(Vector2 position, string name = "Arthurd", string path = "")
        {
            LocalPosition = position;
            _name = name;

            //If the path is not set to nothing
            if (path != "")
                //The sprite will follow the path 
                _sprite = new Sprite(path);

        }

        /// <summary>
        /// Updates all actor's transformations.
        /// </summary>
        public void UpdateTransforms()
        {
            
            _localTransform = _translation * _rotation * _scale;

            //If the parent is set to something
            if (Parent != null)
            {
                //Base the local transformation on the parent's global transformation.
                GlobalTransform = Parent.GlobalTransform * LocalTransform;
            }
            else
                GlobalTransform = LocalTransform;
        }

        /// <summary>
        /// Adds a child for a parent
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(Actor child)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_children.Length + 1];

            //Copy all values from the original array into the temp array
            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }
            //Add the new Actor to the end of the new Array
            tempArray[_children.Length] = child;

            //set the old array to be the new aray
            _children = tempArray;

            //Set the parent of the actor
            child.Parent = this;
        }

        /// <summary>
        /// Removes the child from a parent
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public bool RemoveChild(Actor child)
        {
            //create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_children.Length - 1];

            //Copy all values except the actor we don't want into the new array
            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                //if the actor taht the loop is on is not the one to remove...
                if (_children[i] != child)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _children[j];
                    j++;
                }
                //otherwise if this actor is the one to remove...
                else
                    //...set actorRemoved to be true
                    actorRemoved = true;
            }
            //if the actor removal was successful...
            if (actorRemoved == true)
                //...set the old array to be the new array
                _children = tempArray;

            //Set the parent to be nothing
            child.Parent = null;

            return actorRemoved;
         
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
            UpdateTransforms();
            Console.WriteLine(_name = "Current Position: " + WorldPosition.X + ", " + WorldPosition.Y);
            
        }

        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(GlobalTransform);

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
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationx">the new x position</param>
        /// <param name="translationy">the new y position</param>
        public void SetTranslation(float translationx, float translationy)
        {
            _translation = Matrix3.CreateTranslation(translationx, translationy);
        }

        /// <summary>
        /// Increases the position of the actor by the given values.
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY)
        {
            _translation *= Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);

        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Scales the actor by the given amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Scale(float x, float y)
        {
            _scale *= Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Rotates the actyor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector2 position)
        {
            //Find the directyion that the actor should look in
            Vector2 direction = (position - LocalPosition).Normalized;

            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(direction, Forward);

            if (dotProd > 1)
                dotProd = 1;

            float angle = (float)Math.Acos(dotProd);

            //Find a perindicular vector to the direction
            Vector2 perpDirection = new Vector2(direction.Y, -direction.X);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perpDirection, Forward);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= perpDot / Math.Abs(perpDot);

            Rotate(angle);

        }
    }
}
