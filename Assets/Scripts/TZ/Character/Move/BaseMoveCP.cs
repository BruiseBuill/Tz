using System;
using UnityEngine;
using BF.Object;
using TZ.Character.Data;

namespace TZ.Character.Move
{
    public abstract class BaseMoveCP: BaseComponent
    {
        protected BaseCharacterData characterData;
        protected Vector3 faceOrient;

        protected override void AfterAwake()
        {
            characterData = data as BaseCharacterData;
        }
        public abstract void Move(Vector3 orient);
        public abstract void StopMove();
    }
}
