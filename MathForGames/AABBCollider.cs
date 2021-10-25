using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Left
        {
            get
            {
                return Height;
            }
        }

        public float Right
        {
            get
            {
                return Height;
            }
        }
        public float Top
        {
            get
            {
                return Width;
            }
        }
        public float Bottom
        {
            get
            {
                return Width;
            }
        }
    }
}
