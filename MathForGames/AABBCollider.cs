using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        /// <summary>
        /// The size of this collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The size of this collider on the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// The farthest left x position of this collider 
        /// </summary>
        public float Left
        {
            get
            {
                return Owner.LocalPosition.X - Width / 2;
            }
        }

        /// <summary>
        /// The farthest right x postion of this collider
        /// </summary>
        public float Right
        {
            get
            {
                return Owner.LocalPosition.X + Width / 2;
            }
        }

        /// <summary>
        /// The farthest y position upwards 
        /// </summary>
        public float Top
        {
            get
            {
                return Owner.LocalPosition.Y - Height / 2;
            }
        }

        /// <summary>
        /// The farthest y position downwards.
        /// </summary>
        public float Bottom
        {
            get
            {
                return Owner.LocalPosition.Y + Height / 2;
            }
        }

        /// <summary>
        /// Initializes the collider based on width, and height. This should come out as a box collider
        /// </summary>
        /// <param name="width">How wide the box will be</param>
        /// <param name="height">How tall the box with be</param>
        /// <param name="owner">The actor the collider will be assigned to</param>
        public AABBCollider(float width, float height, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Checks for a collision with any other box colliders.
        /// </summary>
        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Return false if this owner is checking for a collision against itself 
            if (other.Owner == Owner)
                return false;

            //Return true if there is an overlap between boxes
            if (other.Left <= Right && 
                other.Top <= Bottom && 
                Left <= other.Right && 
                Top <= other.Bottom)
            {
                return true;
            }
            //return false if there is no overlap
            return false;
        }

        public override void Draw()
        {
            Raylib.DrawRectangleLines((int)Left, (int)Top, (int)Width, (int)Height, Color.YELLOW);
        }

        /// <summary>
        /// Checks to see if there is a collision with a circdle collider
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }
    }
}
