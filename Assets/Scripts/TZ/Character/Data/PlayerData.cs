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

        [SerializeField] SubDashData dashData;
        [SerializeField] GenericEventChannel<bool> onDashChangeConditionChannel;
        [SerializeField] EventChannel onDashChannel;

        public enum PlayerAttackChannelName { Base,Special,LongDistance};
        [SerializeField] List<SubAttackData> attackDataList;
        [SerializeField] List<GenericEventChannel<bool>> onAttckChangeConditionChannelList;
        [SerializeField] List<EventChannel> onAttackChannelList;

        protected override void AwakeEx()
        {
            dashData = GetSubData<SubDashData>();

        }
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
            dashData.onDashChangeCondition += (con) => onDashChangeConditionChannel.Invoke(con);

            for (int i = 0; i < onAttackChannelList.Count; i++) 
            {
                int j = i;
                onAttackChannelList[i].AddListener(() => Attack(j));
                attackDataList[i].onAttackChangeCondition += (con) => onAttckChangeConditionChannelList[j].Invoke(con);
            }
        }
        public override void ClearDependenceEx()
        {
            onMoveChannel.RemoveListener(Move);

            onDashChannel.RemoveListener(Dash);
            dashData.onDashChangeCondition = delegate { };

            for (int i = 0; i < onAttackChannelList.Count; i++)
            {
                onAttackChannelList[i].RemoveAllListener();
                attackDataList[i].onAttackChangeCondition = delegate { };
            }
        }
        void Move(Vector3 orient)
        {
            inputOrient.Value = orient;
        }
        void Dash()
        {
            dashData.onDash.Invoke();
        }
        void Attack(int index)
        {
            attackDataList[index].onAttack.Invoke();
        }
        
    }
}