using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.SpidaLib.Util
{
    public static class VectorUtil
    {
        public static Vector4 UnitX { get { return new Vector4(1, 0, 0, 0); } }
        public static Vector4 UnitY { get { return new Vector4(0, 1, 0, 0); } }
        public static Vector4 UnitZ { get { return new Vector4(0, 0, 1, 0); } }
        public static Vector4 UnitW { get { return new Vector4(0, 0, 0, 1); } }
        
        /// <summary>
        /// Finds the Angle between two Vectors
        /// </summary>
        /// <returns>The angles.</returns>
        /// <param name="vectorU">Vector u.</param>
        /// <param name="vectorV">Vector v.</param>
        public static float VectorAngle(Vector2 vectorU, Vector2 vectorV)
        {
            float vectors = Vector2.Dot(vectorU, vectorV);
            float magnitude = vectorU.magnitude * vectorV.magnitude;
            float normal = vectors / magnitude;
            float angleInRadian = (float)Mathf.Acos(normal);
            float convertToDegrees = Mathf.Rad2Deg * angleInRadian;
            return convertToDegrees;
        }
        public static float VectorAngle(Vector3 vectorU, Vector3 vectorV)
        {
            float vectors = Vector3.Dot(vectorU, vectorV);
            float magnitude = vectorU.magnitude * vectorV.magnitude;
            float normal = vectors / magnitude;
            float angleInRadian = (float)Mathf.Acos(normal);
            float convertToDegrees = Mathf.Rad2Deg * angleInRadian;
            return convertToDegrees;
        }
        public static float VectorAngle(Vector4 vectorU, Vector4 vectorV)
        {
            float vectors = Vector4.Dot(vectorU, vectorV);
            float magnitude = vectorU.magnitude * vectorV.magnitude;
            float normal = vectors / magnitude;
            float angleInRadian = (float)Mathf.Acos(normal);
            float convertToDegrees = Mathf.Rad2Deg * angleInRadian;
            return convertToDegrees;
        }
    }
}
