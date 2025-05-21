using BF.Utility;
using BF.Object;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TZ.Character.Data;
using UnityEngine;
using DG.Tweening;

namespace TZ.Character.Dash
{
    public class DashCP : BaseComponent
    {
        BaseCharacterData characterData;
        SubDashData dashData;

        [SerializeField] CoolDownTimer timer;

        protected override void AfterAwake()
        {
            characterData = data as BaseCharacterData;
            dashData = characterData.GetSubData<SubDashData>();

            timer.usingTime = dashData.dashDuration;
        }
        public override void Open()
        {
            dashData.onDash += Dash;
            timer.onTimerConditionChange += dashData.onDashChangeCondition;
        }
        public override void AfterOpen()
        {
            StartCoroutine("Updating");
        }
        public override void Close()
        {
            dashData.onDash -= Dash;
            timer.onTimerConditionChange -= dashData.onDashChangeCondition;
        }
        public void Dash()
        {
            //Lag Input
            if (!timer.CanUse())
            {
                return;
            }
            timer.Use();
            
            characterData.canMove.Value = false;

            var animationStart = 0f;
            var animationEnd = 1f;
            var dashOri = characterData.faceOrient;
            var dashTween = DOTween.To(() => animationStart, 
                (s) => 
                {
                    animationStart = s;
                    Debug.Log(s);
                    characterData.totalForce += dashData.dashCurve.Evaluate(s) * dashData.dashSpeed * dashOri;
                },
                animationEnd, dashData.dashDuration)
            .OnComplete(() =>
            {
                characterData.canMove.Value = true;
            });
        }
        IEnumerator Updating()
        {
            while (true)
            {
                yield return null;
                timer.Update(Time.deltaTime);
            }
        }
    }
}