using BF.Utility;
using BF.Object;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TZ.Character.Data;
using UnityEngine;

namespace TZ.Character.Dash
{
    public class DashCP : BaseComponent
    {
        BaseCharacterData characterData;
        SubDashData subDashData;

        [SerializeField] CoolDownTimer timer;
        [SerializeField] float dashSpeed;
        [SerializeField] float dashTime;

        [SerializeField] GameObject shadowPrefab;
        [SerializeField] int shadowCount;
        [SerializeField] float shadowLifeTime;

        protected override void AfterAwake()
        {
            characterData = data as BaseCharacterData;
            subDashData = characterData.GetSubData<SubDashData>();            
        }
        public override void Open()
        {
            subDashData.onDash += Dash;

        }
        public override void AfterOpen()
        {

        }
        public override void Close()
        {
            subDashData.onDash -= Dash;
        }
        public void Dash()
        {
            timer.Use();
            StartCoroutine("Updating");
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