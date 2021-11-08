﻿using System;
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

        
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public Vector3 Forward
        {
            get { return new Vector3(_rotation.M02, _rotation.M12, _rotation.M22); }
        }

        //public Sprite Sprite
        //{
        //    get { return _sprite; }
        //    set { _sprite = value; }
        //}

        public Vector3 LocalPosition
        {
            get { return new Vector3(_translation.M03, _translation.M13, _translation.M23); }
            set
            {
                SetTranslation(value.x, value.y, value.z);
            }
        }

        /// <summary>
        /// The position of this actor in the world 
        /// </summary>
        public Vector3 WorldPosition
        {
            //Return the global transform's T column
            get { return new Vector3(_globalTransform.M03, _globalTransform.M13, _globalTransform.M23); }
            set
            {
                //If the actor has a parent...
                if (Parent != null)
                {
                    //...convert the world cooridinates into local cooridinates and translate the actor
                    float xOffset = (value.x - Parent.WorldPosition.x) / new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    float yOffset = (value.y - Parent.WorldPosition.y) / new Vector3(GlobalTransform.M01, GlobalTransform.M11, GlobalTransform.M21).Magnitude;
                    float zOffset = (value.z - Parent.WorldPosition.z) / new Vector3(GlobalTransform.M02, GlobalTransform.M12, GlobalTransform.M22).Magnitude;
                    SetTranslation(xOffset, yOffset, zOffset);
                }
                //If this actor doesn't have a parent...
                else
                    //...set local position to be the given value
                    LocalPosition = value;
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

        
        public Actor( float x, float y, string name = "Arthurd", Shape shape = Shape.CUBE) :
            this( new Vector3 { x = x, y = y }, name, shape) {}

        public Actor(Vector3 position, string name = "Arthurd", Shape shape = Shape.CUBE)
        {
            LocalPosition = position;
            _name = name;
            _shape = shape;

           
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
            UpdateTransforms();
            Console.WriteLine(_name = "Current Position: " + WorldPosition.x + ", " + WorldPosition.z);
            
        }

        public virtual void Draw()
        {
            System.Numerics.Vector3 startPos = new System.Numerics.Vector3(WorldPosition.x, WorldPosition.y, WorldPosition.z);
            System.Numerics.Vector3 endPos = new System.Numerics.Vector3(WorldPosition.x + Forward.x * 5, WorldPosition.y * Forward.y * 5, WorldPosition.z + Forward.z * 5);

            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosition.x, WorldPosition.y, WorldPosition.z);
            switch (_shape)
            {
                case Shape.CUBE:
                    Raylib.DrawCube(position, Size.x, Size.y, Size.z, ShapeColor);
                    break;
                case Shape.SPHERE:
                    Raylib.DrawSphere(position, Size.x, ShapeColor);
                        break;

            }
            Raylib.DrawLine3D(startPos, endPos, Color.GREEN);
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
        /// <param name="translationz">the new z position</param>
        public void SetTranslation(float translationx, float translationy, float translationz)
        {
            _translation = Matrix4.CreateTranslation(translationx, translationy,  translationz);
        }

        /// <summary>
        /// Increases the position of the actor by the given values.
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY, float translationZ)
        {
            _translation *= Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radiansx, float radiansy, float radiansz)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansx);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansy);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansz);
            _rotation = rotationX * rotationY * rotationZ;

        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radiansx, float radiansy, float radiansz)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansx);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansy);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansz);
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
        /// Rotates the actyor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector3 position)
        {
            //Get the direction for the actor to look in
            Vector3 direction = (position - WorldPosition).Normalized;

            //Of the direction has a length of zero
            if (direction.Magnitude == 0)
                //Set it to be the default forward
                direction = new Vector3(0, 0, 1);

            //Create a vector that points direction upwards
            Vector3 allignAxis = new Vector3(0, 1, 0);

            //Creates two new Vectors that will be the new x and y axis
            Vector3 newYAxis = new Vector3(0, 1, 0);
            Vector3 newXAxis = new Vector3(1, 0, 0);

            //If the direction vector is parallel to the alignAxis vector
            if (Math.Abs(direction.y) > 0 && direction.x == 0 && direction.z == 0)
            {
                //Set the allignAxis vector to point to the right
                allignAxis = new Vector3(1, 0, 0);

                //Get the cross product of the drection and the right to find the Y axis
                newYAxis = Vector3.CrossProduct(direction, allignAxis);

                //Normalize the new Y axis to prevent the matrix from being scaled
                newYAxis.Normalize();

                //Get the cross product of the new y axis and the direction to find the new x axis
                newXAxis = Vector3.CrossProduct(newYAxis, direction);
                //Normalize the new x axis to prevent the matric from being scaled 
                newXAxis.Normalize();
            }
            //If the direction vector is not parallel
            else
            {
                //Set the align axis to point to the right 
                newXAxis = Vector3.CrossProduct(allignAxis, direction);

                //Normalize the newXaxis to prevent our matric from being scaled
                newXAxis.Normalize();

                //Get the cross product of the alignAxis and the direction vector
                newYAxis = Vector3.CrossProduct(direction, newXAxis);
                
                //Normalize the newYaxis to prevent the matrix from being scaled
                newYAxis.Normalize();
            }

            //Create a new matrix with the new axis.
            _rotation = new Matrix4(newXAxis.x, newYAxis.x, direction.x, 0,
                                    newXAxis.y, newYAxis.y, direction.y, 0,
                                    newXAxis.z, newYAxis.z, direction.z, 0,
                                    0, 0, 0, 1);

        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        //public void SetColor(Vector3 ColorValue)
        //{
        //    _color = new Color((int)ColorValue.x, (int)ColorValue.y, (int)ColorValue.z);
        //}
    }
}
