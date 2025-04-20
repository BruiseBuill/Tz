using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;

namespace TZ.ScriptableObject
{
    [CreateAssetMenu(menuName = "Tz/InputMode/ActMode_Fight",fileName = "ActMode_Fight")]
    public class ActMode_Fight : Input2ActMode
    {
        [SerializeField] KeyCode upCode;
        [SerializeField] KeyCode downCode;
        [SerializeField] KeyCode leftCode;
        [SerializeField] KeyCode rightCode;
        KeyCondition defaultKeyCondition = KeyCondition.Down;

        public override void SetActMode()
        {
            
        }
        public override void UnSetActMode()
        {
            
        }
    }
}
