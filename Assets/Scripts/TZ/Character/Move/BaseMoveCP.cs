using System;
using UnityEngine;
using BF.Object;

namespace TZ.Character.Move
{
    public abstract class BaseMoveCP: BaseComponent
    {
        public abstract void Move(Vector3 orient);
        public abstract void StopMove();
    }
}
