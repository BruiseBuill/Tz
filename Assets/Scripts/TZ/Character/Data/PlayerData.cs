using BF.EventChannel;
using System.Collections;
using System.Collections.Generic;
using TZ.Utility;
using UnityEngine;
using System;

namespace TZ.Character.Data
{
	public class PlayerData : BaseCharacterData
	{
		[SerializeField] GenericEventChannel<Vector3> onMoveChannel;
        [SerializeField] GenericEventChannel<bool> onDashChangeConditionChannel;
        [SerializeField] EventChannel onDashChannel;

        public override void Initialize<T>(T para)
        {
            model.position = Vector3.zero;
        }
        public override void SetDataEventWhenOpen()
        {
            inputOrient.ResetData(Vector3.zero);
        }
        public override void AddDependenceEx()
        {
            onMoveChannel.AddListener(Move);
            onDashChannel.AddListener(Dash);

            var dashData = GetSubData<SubDashData>();
            dashData.onDashChangeCondition += (con) => onDashChangeConditionChannel.Invoke(con);
        }
        public override void ClearDependenceEx()
        {
            onMoveChannel.RemoveListener(Move);
            onDashChannel.RemoveListener(Dash);

            var dashData = GetSubData<SubDashData>();
            dashData.onDashChangeCondition = delegate { };
        }
        private void Move(Vector3 orient)
        {
            inputOrient.Value = orient;
        }
        void Dash()
        {
            var dashData = GetSubData<SubDashData>();
            dashData.onDash.Invoke();
        }
        
    }
}