using BF.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZ.Character.Data
{
	public class SubAttackData : SubData
	{
		public Action onAttack = delegate { };
		public Action<bool> onAttackChangeCondition = delegate { };

		
    }
}