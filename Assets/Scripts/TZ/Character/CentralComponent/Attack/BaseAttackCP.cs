using BF.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZ.Character.Attack
{
	public abstract class BaseAttackCP : BaseComponent
	{
		public abstract void Attack();
		public abstract void CancelAttack();


    }
}