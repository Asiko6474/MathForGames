using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x1, float y1, float z1)
        {
            X = x1;
            Y = y1;
            Z = z1;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it 
        /// </summary>
        public Vector3 Normalized
        {
            get { 
                Vector3 value = this;
                return value.Normalize();
                }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one 
        /// </summary>
        /// <returns>The result of the normalization. returns an empty vector if the magnitude is zero</returns>
        public Vector3 Normalize()
        {
            if (Magnitude == 0)
                return new Vector3();

            return this / Magnitude;
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The dot product of the first vector on to the second</returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        /// <summary>
        /// Finds the distance from the first vector to the second
        /// </summary>
        /// <param name="lhs">The starting point </param>
        /// <param name="rhs">The ending point</param>
        /// <returns>Scalar representing the distances</returns>
        public static float Distance(Vector3 lhs, Vector3 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3( lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                                lhs.Z * rhs.X - lhs.X * rhs.Z,
                                lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        /// <summary>
        /// Subracts X, Y, or Z values of the second vector from the first.
        /// </summary>
        /// <param name="lhs">The vector that is decreasing</param>
        /// <param name="rhs">The vector used to decrease the first vector</param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z };
        }

        /// <summary>
        /// Adds the X, Y, and Z of the first vector to the second
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increase the 1st vector</param>
        /// <returns>The result of the vector additions</returns>
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z };
        }

        /// <summary>
        /// Multiplies the X, Y, and Z vectors of one vector to another
        /// </summary>
        /// <param name="lhs">The vector that is being scaled </param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns></returns>
        public static Vector3 operator *(float rhs, Vector3 lhs )
        {
            return new Vector3 { X = lhs.X * rhs, Y = lhs.Y * rhs, Z = lhs.Z * rhs };
        }

        /// <summary>
        /// Multiplies the X, Y, and Z vectors of one vector to another
        /// </summary>
        /// <param name="lhs">The vector that is being scaled </param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 lhs, float rhs )
        {
            return new Vector3 { X = lhs.X * rhs, Y = lhs.Y * rhs, Z = lhs.Z * rhs };
        }

        /// <summary>
        /// Divides X, Y, and Z vectors from one vector to another
        /// </summary>
        /// <param name="lhs">The vector that is being scaled</param>
        /// <param name="scalar">The Value to scale the vector by</param>
        /// <returns>Result of the vector scaling</returns>
        public static Vector3 operator /(Vector3 lhs, float rhs)
        {
            return new Vector3 { X = lhs.X / rhs, Y = lhs.Y / rhs, Z = lhs.Z / rhs };
        }

        /// <summary>
        /// Contrasts the X, Y, and Z vectors to each other
        /// </summary>
        /// <param name="lhs">The left side of the contrast</param>
        /// <param name="rhs">The right side of the contrast</param>
        /// <returns>True if the x values of both vectors </returns>
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }

        /// <summary>
        /// Compares the X, Y, and Z vectors to each other
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the comparison</param>
        /// <returns></returns>
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }
    }
}
