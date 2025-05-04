using BF.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tz.Character.Data
{
	public abstract class BaseCharacterData : BaseShareData
	{
        [SerializeField] public DataWithEvent<Vector3> inputOrient;
        [SerializeField] public DataWithEvent<Vector3> worldOrient;
        [SerializeField] float walkSpeed;
    }
}