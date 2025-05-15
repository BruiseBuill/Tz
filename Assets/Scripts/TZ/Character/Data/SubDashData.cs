using BF.Object;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TZ.Utility;
using UnityEngine;

namespace TZ.Character.Data
{
    public class SubDashData:SubData
    {
        public Action onDash = delegate { };
        public Action<bool> onDashChangeCondition = delegate { };

        public float dashSpeed;
        public AnimationCurve dashCurve;
        public float dashDuration;
    }
}
