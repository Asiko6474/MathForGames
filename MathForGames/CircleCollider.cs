﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;
namespace MathForGames
{
    class CircleCollider : Collider
    {
        private float _collisionRadius;
        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        public CircleCollider(float collisionRadius, Actor owner) :base (owner, ColliderType.CIRCLE)
        {
            _collisionRadius = collisionRadius;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            
            if (other.Owner == Owner)
                return false;

            
            float distance = Vector2.Distance(other.Owner.LocalPosition, Owner.LocalPosition);
            float combinedRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combinedRadii;
        }


        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Return false if this collider is checking collision against itself
            if (other.Owner == Owner)
                return false;

            //Get the direction from this collider to the AABB
            Vector2 direction = Owner.LocalPosition - other.Owner.LocalPosition;

            //clamp the direction from this vector to get the closest point to the circle
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);
            direction.Y = Math.Clamp(direction.Y, -other.Height / 2, other.Height / 2);

            //Add the direction vector to the AABB center to get the closest point to the circle
            Vector2 closestPoint = other.Owner.LocalPosition + direction;

            //Find the distance from the circle's center to the closest point
            float distanceFromClosestPoint = Vector2.Distance(Owner.LocalPosition, closestPoint);

            //return whether or not the distance is less than the circle's radius
            return distanceFromClosestPoint <= CollisionRadius;
        }

        public override void Draw()
        {
            base.Draw();
            Raylib.DrawCircleLines((int)Owner.LocalPosition.X, (int)Owner.LocalPosition.Y, CollisionRadius, Color.PURPLE);
        }
    }
}
