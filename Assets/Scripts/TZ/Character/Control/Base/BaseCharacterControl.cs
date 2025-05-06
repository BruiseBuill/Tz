using System;
using UnityEngine;
using BF.Object;
using TZ.Character.Data;

namespace TZ.Character.Control
{
    public abstract class BaseCharacterControl:BaseControl
    {
        protected BaseCharacterData characterData;

        protected override void Awake()
        {
            base.Awake();
            characterData = data as BaseCharacterData;
        }
    }
}
