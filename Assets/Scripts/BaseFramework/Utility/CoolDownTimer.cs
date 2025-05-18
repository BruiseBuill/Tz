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
        public Action<bool> onTimerConditionChange = delegate { };

        protected bool IsCoolingDown
        {
            get => isCoolingDown;
            set
            {
                if (isCoolingDown == value)
                    return;
                isCoolingDown = value;
                onTimerConditionChange.Invoke(CanUse());
            }
        }
        protected bool IsUsing
        {
            get => isUsing;
            set
            {
                if (isUsing == value)
                    return;
                isUsing = value;
                onTimerConditionChange.Invoke(CanUse());
            }
        }
        [SerializeField] protected bool isCoolingDown;
        [SerializeField] protected bool isUsing;

        [SerializeField] protected float presentTime;
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
            if (IsCoolingDown || IsUsing)
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
                IsUsing = true;
                invokeTime = 0;
            }

            presentChargeCount--;
            if (presentChargeCount == 0)
            {
                IsCoolingDown = true;
            }
        }
        void UseNormalSkill()
        {
            if (usingTime > 0)
            {
                IsUsing = true;
            }
            IsCoolingDown = true;

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
            if (IsUsing)
            {
                invokeTime += duration;
                if (invokeTime > usingTime)
                {
                    IsUsing = false;
                }
            }
            if (presentChargeCount < maxChargeCount)
            {
                presentTime += duration;
                if (presentTime > coolDownTime)
                {
                    presentTime -= coolDownTime;
                    presentChargeCount++;
                    IsCoolingDown = false;
                    if (presentChargeCount == maxChargeCount)
                        presentTime = 0;
                }
            }
        }
        void UpdateNormalSkill(float duration)
        {
            if (IsUsing)
            {
                presentTime += duration;
                if (presentTime > usingTime)
                {
                    IsUsing = false;
                }
            }
            if (IsCoolingDown)
            {
                presentTime += duration;
                if (presentTime > coolDownTime)
                {
                    presentTime = 0;
                    IsCoolingDown = false;
                }
            }
        }
    }
}