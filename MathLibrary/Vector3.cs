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
            get { return (float)Math.Sqrt(x * x + y * y + z * z); }
        }

        public Vector3 Normalized
        {
            get { 
                Vector3 value = this;
                return value.Normalize();
                }
        }

        public Vector3(float x1, float y1, float z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public Vector3 Normalize()
        {
            if (Magnitude == 0)
                return new Vector3();

            return this / Magnitude;
        }

        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.x * rhs.x) + (lhs.y * rhs.y) + (lhs.z * rhs.z);
        }

        public static float Distance(Vector3 lhs, Vector3 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { x = lhs.x - rhs.x, y = lhs.y - rhs.y, z = lhs.z - rhs.z };
        }
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { x = lhs.x + rhs.x, y = lhs.y + rhs.y, z = lhs.z + rhs.z };
        }
        public static Vector3 operator *(Vector3 lhs, float rhs)
        {
            return new Vector3 { x = lhs.x * rhs, y = lhs.y * rhs, z = lhs.z * rhs };
        }

        public static Vector3 operator /(Vector3 lhs, float rhs)
        {
            return new Vector3 { x = lhs.x / rhs, y = lhs.y / rhs, z = lhs.z / rhs };
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
        }
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }
    }
}
