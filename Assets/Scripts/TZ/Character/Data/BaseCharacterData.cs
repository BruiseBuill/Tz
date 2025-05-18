using BF.Object;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TZ.Utility;
using UnityEngine;

namespace TZ.Character.Data
{
	public abstract class BaseCharacterData : BaseShareData
	{
        [Header("Model")]
        public Transform model;
        public Rigidbody2D rb;

        [Tooltip("正交3d视角当中的真实坐标，非等距视角")]
        [ReadOnly]
        public DataWithEvent<Vector3> realPos;

        public DataWithEventHop canMove;
        public DataWithEvent<Vector3> inputOrient = new DataWithEvent<Vector3>();
        public Vector3 faceOrient;

        public override void AddDependence()
        {
            realPos.onValueChange += (pos) =>
            {
                var p = pos.ToIsoVector();
                rb.position = p;
                realPos.ResetData(new Vector3(rb.position.x, rb.position.y, p.z).ToRealVector())
            };
            inputOrient.onValueChange += (orient) =>
            {
                if (orient != Vector3.zero)
                    faceOrient = orient;
            };
            AddDependenceEx();
        }
        public override void ClearDependence()
        {
            ClearDependenceEx();
            realPos.onValueChange = delegate { };
            inputOrient.onValueChange = delegate { };
        }
        public abstract void AddDependenceEx();
        public abstract void ClearDependenceEx();

    }
}