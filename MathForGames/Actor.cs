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
        private bool _started;
        private Vector3 _forward = new Vector3(0,0,1);
        public Collider _collider;
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _localTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;
        private Shape _shape;
        private Actor[] _children = new Actor[0];
        private Actor _parent;


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

        public Vector3 Forward
        {
            get { return new Vector3(_rotation.M02, _rotation.M12, _rotation.M22); }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Vector2 LocalPosition
        {
            get { return new Vector2(LocalTransform.M02, LocalTransform.M12);  }
            set
            {
                SetTranslation(value.x, value.y);
            }
        }

        /// <summary>
        /// The position of this actor in the world 
        /// </summary>
        public Vector2 WorldPosition
        {
            //Return the global transform's T column
            get { return new Vector2(GlobalTransform.M02, GlobalTransform.M12); }
            set
            {
                //If the actor has a parent...
                if (Parent != null)
                {
                    //...convert the world cooridinates into local cooridinates and translate the actor
                    float xOffset =(value.x - Parent.WorldPosition.x) / new Vector2(_globalTransform.M00, _globalTransform.M10).Magnitude;
                    float yOffset = (value.y - Parent.WorldPosition.y) / new Vector2(_globalTransform.M10, _globalTransform.M11).Magnitude;
                    SetTranslation(xOffset, yOffset);
                }
                //If this actor doesn't have a parent
                else
                {
                    //set local position to be the given value
                    LocalPosition = value;
                }
            }
        }

        public Matrix4 GlobalTransform
        {
            get { return _globalTransform; }
            set
            {
                _globalTransform = value;
            }
        }

        public Matrix4 LocalTransform
        {
            get { return _localTransform; }
            set { _localTransform = value; }
        }

        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public Actor[] Children
        {
            get { return _children; }
        }

            public Vector3 Size
        {
            get 
            {
                float xScale = new Vector3(_scale.M00, _scale.M10, _scale.M20).Magnitude;
                float yScale = new Vector3(_scale.M01, _scale.M11, _scale.M21).Magnitude;
                float zScale = new Vector3(_scale.M02, _scale.M12, _scale.M22).Magnitude;
                return new Vector3(xScale, yScale, zScale);  
            }
            set { SetScale(value.x, value.y, value.z); }
        }

        //X position, Y position, Name, sprite
        public Actor( float x, float y, string name = "Arthurd", Shape shape = Shape.CUBE) :
            this( new Vector3 { x = x, y = y }, name, path) {}

        public Actor(Vector3 position, string name = "Arthurd", Shape shape = Shape.CUBE)
        {
            LocalPosition = position;
            _name = name;

           
        }

        public void UpdateTransforms()
        {
            _localTransform = _translation * _rotation * _scale;
            if (Parent != null)
            {
                GlobalTransform = Parent.GlobalTransform * LocalTransform;
            }
            else
                GlobalTransform = LocalTransform;
        }

        public void AddChild(Actor child)
        {
            //Creat a temp array larger than the original
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
            _localTransform = _translation * _rotation * _scale;
            //Console.WriteLine(_name = "Jake " + WorldPosition.x + ", " + WorldPosition.y);
            UpdateTransforms();
        }

        public virtual void Draw()
        {
            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosition.x, WorldPosition.y, WorldPosition.z);
            switch (_shape)
            {
                case Shape.CUBE:
                    float SizeX = new Vector3(_scale.M00, _scale.M10, _scale.M20).Magnitude;
                    float SizeY = new Vector3(_scale.M01, _scale.M11, _scale.M21).Magnitude;
                    float SizeZ = new Vector3(_scale.M02, _scale.M12, _scale.M22).Magnitude;
                    Raylib.DrawCube(position, SizeX, SizeY, SizeZ, Color.BLUE);
                    break;
                case Shape.SPHERE:
                    SizeX = 0;
                    Raylib.DrawSphere(position.SizeX, Color.BLUE);
                        break;

            }
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
        public void SetTranslation(float translationx, float translationy, float translationz)
        {
            _translation = Matrix4.CreateTranslation(translationx, translationy,  translationz);
        }

        /// <summary>
        /// Increases the position of the actor by the given values.
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY, float translationY)
        {
            _translation *= Matrix4.CreateTranslation(translationX, translationY, float translationY);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radiansx, float radiansy, float radiansz)
        {
            Matrix4 rotationX = Matrix4.CreateRotation(radiansx);
            Matrix4 rotationY = Matrix4.CreateRotation(radiansy);
            Matrix4 rotationZ = Matrix4.CreateRotation(radiansz);
            _rotation = rotationX * rotationY * rotationZ;

        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radiansx, float radiansy, float radiansz)
        {
            Matrix4 rotationX = Matrix4.CreateRotation(radiansx);
            Matrix4 rotationY = Matrix4.CreateRotation(radiansy);
            Matrix4 rotationZ = Matrix4.CreateRotation(radiansz);
            _rotation *= rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Scales the actor by the given amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Scale(float x, float y, float z)
        {
            _scale *= Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Rotates the actyor to face teh given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector3 position)
        {
            //Find the directyion that the actor should look in
            Vector2 direction = (position - LocalPosition).Normalized;

            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(direction, Forward);

            if (dotProd > 1)
                dotProd = 1;

            float angle = (float)Math.Acos(dotProd);

            //Find a perindicular vector to the direction
            Vector2 perpDirection = new Vector3(direction.y, -direction.x);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perpDirection, Forward);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }
    }
}
