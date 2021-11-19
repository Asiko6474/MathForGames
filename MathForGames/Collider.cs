using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    enum ColliderType
    {
        CIRCLE,
        AABB
    }

    abstract class Collider
    {
        private Actor _owner;
        private ColliderType _colliderType;

        //Sets the owner for the collider
        public Actor Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        //sets the type of collider. AABB being a square/box collider, or a circle collider
        public ColliderType ColliderType
        {
            get { return _colliderType; }
        }

        /// <summary>
        /// Initializes any collider
        /// </summary>
        /// <param name="owner">The actor the collider is assigned to</param>
        /// <param name="colliderType">The type of collider the collider would be.</param>
        public Collider(Actor owner, ColliderType colliderType)
        {
            _owner = owner;
            _colliderType = colliderType;

            //Reminder: Box colliders would be in the class AABBCollider while the circle colliders would be in the CircleCollider class
        }

        /// <summary>
        /// Checks for the collision of both types of colliders
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CheckCollision(Actor other)
        {
            //If the collider type is a circle
            if (other.Collider.ColliderType == ColliderType.CIRCLE)
                //Use the circle collider function
                return CheckCollisionCircle((CircleCollider)other.Collider);
            //If the collider type is a box
            else if (other.Collider.ColliderType == ColliderType.AABB)
                //Use the box collider function
                return CheckCollisionAABB((AABBCollider)other.Collider);
            //if there is no collision going on, return false
            return false;
        }

        public virtual bool CheckCollisionCircle(CircleCollider other) { return false; }

        public virtual bool CheckCollisionAABB(AABBCollider other) { return false; }

        public virtual void Draw() 
        {
            
        }
    }
}
