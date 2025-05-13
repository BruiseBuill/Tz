using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF.Utility
{
    [Serializable]
    public class CoolDownTimer
    {
        public bool isCoolingDown;
        public bool isUsing;

        public float presentTime;
        public float coolDownTime;
        public float usingTime;

        public bool canCharge;
        [ShowIf("canCharge")]
        public int presentChargeCount;
        [ShowIf("canCharge")]
        public int maxChargeCount;
        [ShowIf("canCharge")]
        float invokeTime;

        public bool CanUse()
        {
            if (isCoolingDown || isUsing)
            {
                return false;
            }
            return true;
        }
        public void Use()
        {
#if UNITY_EDITOR
            if (!CanUse())
            {
                Debug.LogError("1");
            }
#endif
            if (canCharge)
            {
                UseChargedSkill();
            }
            else
            {
                UseNormalSkill();
            }
        }
        void UseChargedSkill()
        {
            if (usingTime > 0)
            {
                isUsing = true;
                invokeTime = 0;
            }

            presentChargeCount--;
            if (presentChargeCount == 0)
            {
                isCoolingDown = true;
            }
        }
        void UseNormalSkill()
        {
            if (usingTime > 0)
            {
                isUsing = true;
            }
            isCoolingDown = true;

            presentTime = 0;
        }
        public void Update(float duration)
        {
            if (canCharge)
            {
                UpdateChargedSkill(duration);
            }
            else
            {
                UpdateNormalSkill(duration);
            }
        }
        void UpdateChargedSkill(float duration)
        {
            if (isUsing)
            {
                invokeTime += duration;
                if (invokeTime > usingTime)
                {
                    isUsing = false;
                }
            }
            if (presentChargeCount < maxChargeCount)
            {
                presentTime += duration;
                if (presentTime > coolDownTime)
                {
                    presentTime -= coolDownTime;
                    presentChargeCount++;
                    isCoolingDown = false;
                    if (presentChargeCount == maxChargeCount)
                        presentTime = 0;
                }
            }
        }
        void UpdateNormalSkill(float duration)
        {
            if (isUsing)
            {
                presentTime += duration;
                if (presentTime > usingTime)
                {
                    isUsing = false;
                }
            }
            if (isCoolingDown)
            {
                presentTime += duration;
                if (presentTime > coolDownTime)
                {
                    presentTime = 0;
                    isCoolingDown = false;
                }
            }
        }
    }
}