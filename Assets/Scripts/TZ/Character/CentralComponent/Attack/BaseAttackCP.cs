using BF.Object;
using System.Collections;
using System.Collections.Generic;
using TZ.Character.Data;
using UnityEngine;

namespace TZ.Character.Attack
{
	public abstract class BaseAttackCP : BaseComponent
	{
		protected SubAttackData attackData;

        protected override void AfterAwake()
        {
            attackData = data.GetSubData<SubAttackData>();
        }
        public abstract void Attack();
		public abstract void CancelAttack();
    }
	
}