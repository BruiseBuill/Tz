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
        [SerializeField] public Transform model;
        [Tooltip("正交3d视角当中的真实坐标，非等距视角")]
        [ReadOnly]
        [SerializeField] public DataWithEvent<Vector3> realPos;

        [Header("Move")]
        public DataWithEventHop canMove;
        public DataWithEvent<Vector3> inputOrient = new DataWithEvent<Vector3>();

        

        public override void AddDependence()
        {
            realPos.onValueChange += (pos) => model.position = realPos.Value.ToIsoVector();
            AddDependenceEx();
        }
        public override void ClearDependence()
        {
            ClearDependenceEx();
            realPos.onValueChange = delegate { };
        }
        public abstract void AddDependenceEx();
        public abstract void ClearDependenceEx();

    }
}