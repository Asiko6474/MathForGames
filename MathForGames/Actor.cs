﻿using System;
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
        private Vector2 _forward = new Vector2(1,0);
        public Collider _collider;
        private Matrix3 _globalTransform = Matrix3.Identity;
        private Matrix3 _localTransform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private Sprite _sprite;
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

        public Vector2 Forward
        {
            get { return new Vector2(_rotation.M00, _rotation.M10); }
            set {
                Vector2 point = value.Normalized + LocalPosition;
                LookAt(point);
            }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.M02, _localTransform.M12);  }
            set
            {
                SetTranslation(value.x, value.y);
            }
        }

        public Vector2 WorldPosition
        {
            get; set;
        }

        public Matrix3 GlobalTransform
        {
            get; set;
        }

        public Matrix3 LocalTransform
        {
            get; set;
        }

        public Actor Parent
        {
            get; set;
        }

        public Actor[] Children
        {
            get; set;
        }

            public Vector2 Size
        {
            get { return new Vector2(_scale.M00, _scale.M11);  }
            set { SetScale(value.x, value.y); }
        }

        //X position, Y position, Name, sprite
        public Actor( float x, float y, string name = "Arthurd", string path = "") :
            this( new Vector2 { x = x, y = y }, name, path) {}

        public Actor(Vector2 position, string name = "Arthurd", string path = "")
        {
            LocalPosition = position;
            _name = name;

            if (path != "")
                _sprite = new Sprite(path);
        }

        public void UpdateTransforms()
        {

        }

        public void AddChild(Actor child)
        {

        }

        public bool RemoveChild(Actor child)
        {
            return true;
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
            //Console.WriteLine(_name = "Jake " + Position.x + ", " + Position.y);
           
        }

        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(_localTransform);
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
        /// Rotates the actyor to face teh given position
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
            Vector2 perpDirection = new Vector2(direction.y, -direction.x);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perpDirection, Forward);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }
    }
}
