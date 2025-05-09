using BF.EventChannel;
using System.Collections;
using System.Collections.Generic;
using TZ.Utility;
using UnityEngine;

namespace TZ.Character.Data
{
	public class PlayerData : BaseCharacterData
	{
		[SerializeField] GenericEventChannel<Vector3> onMoveChannel;

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
        }
        public override void ClearDependenceEx()
        {
            onMoveChannel.RemoveListener(Move);
        }
        private void Move(Vector3 orient)
        {
            inputOrient.Value = orient;
        }

        
    }
}