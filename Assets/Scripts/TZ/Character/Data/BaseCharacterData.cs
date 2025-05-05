using BF.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tz.Character.Data
{
	public abstract class BaseCharacterData : BaseShareData
	{
        [Header("Model")]
        [SerializeField] public Transform model;

        [Header("Move")]
        public DataWithEventHop canMove;
        [SerializeField] public DataWithEvent<Vector3> inputOrient;
        [SerializeField] public DataWithEvent<Vector3> worldOrient;

        
        
    }
}