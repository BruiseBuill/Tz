using System;
using UnityEngine;
using BF.Object;
using Tz.Character.Data;

namespace TZ.Character.Move
{
    public abstract class BaseMoveCP: BaseComponent
    {
        protected BaseCharacterData characterData;
        protected Vector3 faceOrient;
        

        public abstract void Move(Vector3 orient);
        public abstract void StopMove();
    }
}
