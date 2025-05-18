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
            timer.Use();
            
            characterData.canMove.Value = false;

            var aimPos = characterData.realPos.Value + characterData.faceOrient * dashData.dashSpeed * dashData.dashDuration;
            var dashTween = DOTween.To(() => characterData.realPos.Value, (pos) => characterData.realPos.Value = pos, aimPos, dashData.dashDuration)
            .SetEase(dashData.dashCurve)
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