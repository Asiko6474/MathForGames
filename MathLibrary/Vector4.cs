using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4(float x1, float y2, float z3, float w4)
        {
            X = x1;
            Y = y2;
            Z = z3;
            W = w4;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W); }
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it 
        /// </summary>
        public Vector4 Normalized
        {
            get
            {
                Vector4 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it 
        /// </summary>
        public Vector4 Normalize()
        {
            if (Magnitude == 0)
                return new Vector4();

            return this /= Magnitude;
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The dot product of the first vector on to the second</returns>
        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        public static Vector4 CrossProduct(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                               lhs.Z * rhs.X - lhs.X * rhs.Z,
                               lhs.X * rhs.Y - lhs.Y * rhs.X,
                               0);
        }

        /// <summary>
        /// Finds the distance from the first vector to the second
        /// </summary>
        /// <param name="lhs">The starting point </param>
        /// <param name="rhs">The ending point</param>
        /// <returns>Scalar representing the distances</returns>
        public static float Distance(Vector4 lhs, Vector4 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        /// <summary>
        /// Adds the X, Y, Z, or W values of the second vector to the first
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increase the 1st vector</param>
        /// <returns>The result of the vector additions</returns>
        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z, W = lhs.W + rhs.W };
        }

        /// <summary>
        /// Subracts X, Y, Z, or W values of the second vector from the first.
        /// </summary>
        /// <param name="lhs">The vector that is decreasing</param>
        /// <param name="rhs">The vector used to decrease the first vector</param>
        /// <returns></returns>
        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z, W = lhs.W - rhs.W };
        }

        /// <summary>
        /// Multiplies the vectors X, Y, Z, or W values by the scalar
        /// </summary>
        /// <param name="lhs">The vector that is being scaled </param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns></returns>
        public static Vector4 operator *(Vector4 lhs, float scalar)
        {
            return new Vector4 { X = lhs.X * scalar, Y = lhs.Y * scalar, Z = lhs.Z * scalar, W = lhs.W * scalar };
        }


        /// <summary>
        /// Divides X, Y, Z, or W values by a scalar
        /// </summary>
        /// <param name="lhs">The vector that is being scaled</param>
        /// <param name="scalar">The Value to scale the vector by</param>
        /// <returns>Result of the vector scaling</returns>
        public static Vector4 operator /(Vector4 lhs, float scalar)
        {
            return new Vector4 { X = lhs.X / scalar, Y = lhs.Y / scalar, Z = lhs.Z, W = lhs.W / scalar };
        }

        /// <summary>
        /// Compares the X, Y, Z, or W values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the comparison</param>
        /// <returns>True if the x values of both vectors </returns>
        public static bool operator ==(Vector4 lhs, Vector4 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z && lhs.W == rhs.W;
        }

       /// <summary>
       /// Contrasts the X, Y, Z, or W values of two vectors
       /// </summary>
       /// <param name="lhs">The left side of teh comparison</param>
       /// <param name="rhs">The right side of the comparison</param>
       /// <returns></returns>
        public static bool operator !=(Vector4 lhs, Vector4 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z || lhs.W != rhs.W;
        }
    }
}
