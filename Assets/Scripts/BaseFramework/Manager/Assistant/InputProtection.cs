using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
    [Serializable]
    public class InputProtection
    {
        //The necessary condition for this action
        //Normally, action can only be done when condition=true and input
        //Advance: input before condition=true
        //Lag: input after condition=false
        public Action onAct = delegate { };

        [SerializeField] bool condition;
        [ReadOnly]
        [SerializeField] float lastConditionTrueTime;
        [ReadOnly]
        [SerializeField] bool hasInput;
        [ReadOnly]
        [SerializeField] float lastInputTime;

        [SerializeField] float advanceProtectionTime;
        [SerializeField] float lagProtectionTime;

        public InputProtection()
        {
            condition = true;
            lastConditionTrueTime = 0f;
            hasInput = false;
            lastInputTime = 0f;
        }
        public void Input()
        {
            hasInput = true;
            lastInputTime = Time.time;
            if (condition || Time.time - lastConditionTrueTime < lagProtectionTime)
            {
                hasInput = false;
                onAct.Invoke();
            }
        }
        public void ChangeCondition(bool condition)
        {
            if (this.condition == condition)
            {
                return;
            }
            this.condition = condition;

            if (condition)
            {
                if (hasInput && Time.time - lastInputTime < advanceProtectionTime)
                {
                    hasInput = false;
                    onAct.Invoke();
                }
            }
            else
            {
                lastConditionTrueTime = Time.time;
            }
        }
    }
    
}
