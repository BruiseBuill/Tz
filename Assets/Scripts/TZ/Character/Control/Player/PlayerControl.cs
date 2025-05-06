using System;
using UnityEngine;
using BF.Object;
using TZ.Character.Data;

namespace TZ.Character.Control
{
    public class PlayerControl : BaseCharacterControl
    {
        PlayerData playerData;

        protected override void Awake()
        {
            base.Awake();
            playerData = data as PlayerData;
        }
    }
    public class PlayerInit : DataInit
    {

    }
}
