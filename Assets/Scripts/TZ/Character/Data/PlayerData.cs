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
            Debug.Log(inputOrient == null);
            inputOrient.ResetData(Vector3.zero);
            worldOrient.ResetData(Vector3.zero);
        }
        public override void AddDependence()
        {
            onMoveChannel.AddListener(Move);
            inputOrient.onValueChange += (inputOrient) => worldOrient.Value = inputOrient.ToIsoVector();
        }
        public override void ClearDependence()
        {
            onMoveChannel.RemoveListener(Move);
            inputOrient.onValueChange = delegate { };
            worldOrient.onValueChange = delegate { };
        }



        private void Move(Vector3 orient)
        {
            inputOrient.Value = orient;
            Debug.Log(3);
        }
    }
}