using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public float Magnitude
        {
            get { return 0; ; }
            
                
            
        }

        public Vector3 Normalized
        {
            get { return new Vector3(); }
        }

        public Vector3(float x1, float y1, float z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public Vector3 Normalize()
        {
            return new Vector3();
        }

        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return 0;
        }

        public static float Distance(Vector3 lhs, Vector3 rhs)
        {
            return 0;
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3();
        }
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3();
        }
        public static Vector3 operator *(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3();
        }

        public static Vector3 operator /(Vector3 lhs, float rhs)
        {
            return new Vector3();
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return false;
        }
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return false;
        }
    }
}
