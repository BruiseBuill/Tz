using BF.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZ.Character.Data
{
	public abstract class BaseCharacterData : BaseShareData
	{
        [Header("Model")]
        [SerializeField] public Transform model;

        [Header("Move")]
        public DataWithEventHop canMove;
        public DataWithEvent<Vector3> inputOrient = new DataWithEvent<Vector3>();
        public DataWithEvent<Vector3> worldOrient= new DataWithEvent<Vector3>();

        
    }
}