using System;
using UnityEngine;

namespace TZ.Utility
{
    public static class IsoVectorConverter
    {
        static float sin30 = Mathf.Sin(Mathf.PI / 6);
        static float cos30 = Mathf.Cos(Mathf.PI / 6);
        public static Vector3 ToIsoVector(this Vector3 realVector)
        {
            return new Vector3(realVector.x, realVector.y * cos30 + realVector.z * sin30, -realVector.y * sin30 + realVector.z * cos30);
        }
        public static Vector3 ToRealVector(this Vector3 isoVector)
        {
            return new Vector3(isoVector.x, isoVector.y * cos30 - isoVector.z * sin30, isoVector.y * sin30 + isoVector.z * cos30);
        }
    }
}
