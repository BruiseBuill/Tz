using BF.EventChannel;
using System.Collections;
using System.Collections.Generic;
using TZ.Utility;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Tz.Character.Data
{
	public class PlayerData : BaseCharacterData
	{
		[SerializeField] GenericEventChannel<Vector3> onMoveChannel;

        public override void AfterAwake()
        {
            
        }
        public override void SetDataEventWhenOpen()
        {
            inputOrient.ResetData(Vector3.zero);
            worldOrient.ResetData(Vector3.zero);

            onMoveChannel.AddListener(Move);
            inputOrient.onValueChange += (inputOrient) => worldOrient.Value = inputOrient.ToIsoVector();
        }
        public override void CloseDataEvent()
        {
            onMoveChannel.RemoveListener(Move);
            inputOrient.onValueChange = delegate { };
            worldOrient.onValueChange = delegate { };
        }
        private void Move(Vector3 orient)
        {
            inputOrient.Value = orient;
        }
    }
}