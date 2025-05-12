using BF.Object;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TZ.Character.Data;
using UnityEngine;

namespace TZ.Character.Dash
{
    public class CoolDownTimer
    {
        public bool isCoolingDown;
        public bool isUsing;

        public float presentTime;
        public float coolDownTime;
        public float usingTime;
        
        public bool canCharge;
        [ShowIf("canCharging")]
        public int presentChargeCount;
        [ShowIf("canCharging")]
        public int maxChargeCount;

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
            if (!(isCoolingDown||canCharge))
            {
                return;
            }

            presentTime += duration;
            if (presentTime < coolDownTime)
            {
                return;
            }

            if (canCharge)
            {
                presentChargeCount = Mathf.Min(presentChargeCount + 1, maxChargeCount);
                
            }
            else
            {

            }
        }
        void UpdateChargedSkill(float duration)
        {
            if (presentChargeCount < maxChargeCount)
            {

            }
        }
        void UpdaetNormalSkill()
        {

        }
    }
    public class DashCP : BaseComponent
    {
        BaseCharacterData characterData;

        [SerializeField] float dashSpeed;
        [SerializeField] float dashTime;
        
        [SerializeField] GameObject shadowPrefab;
        [SerializeField] int shadowCount;
        [SerializeField] float shadowLifeTime;

        protected override void AfterAwake()
        {
            characterData = data as BaseCharacterData;
        }
        public override void Open()
        {
            
        }
        public override void AfterOpen()
        {
            
        }

        public override void Close()
        {
            
        }
        public void Dash()
        {

        }
        

        
    }
}