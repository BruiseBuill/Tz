using BF.Object;
using System.Collections;
using System.Collections.Generic;
using TZ.Character.Data;
using UnityEngine;

namespace TZ.Character.Jump
{
	public abstract class BaseJumpCP : BaseComponent
	{
		protected BaseCharacterData characterData;
        [Header("Jump/Fly")]
        protected bool isOnGround;

        protected override void AfterAwake()
        {
            characterData = data as BaseCharacterData;
        }
        public abstract void Jump();
	}
}