using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;
using BF.EventChannel;

namespace TZ.ScriptableObject
{
    [CreateAssetMenu(menuName = "Tz/InputMode/ActMode_Fight",fileName = "ActMode_Fight")]
    public class ActMode_Fight_Win : Input2ActMode
    {
        #region Move
        [SerializeField] KeyCode upCode;
        [SerializeField] KeyCode downCode;
        [SerializeField] KeyCode leftCode;
        [SerializeField] KeyCode rightCode;
        [SerializeField] GenericEventChannel<Vector3> onMoveChannel;
        bool isUp;
        bool isDown;
        bool isLeft;
        bool isRight;
        bool IsUp
        {
            set
            {
                if (value != isUp)
                {
                    isUp = value;
                    ChangeMove();
                }
            }
        }
        bool IsDown
        {
            set
            {
                if (value != isDown)
                {
                    isDown = value;
                    ChangeMove();
                }
            }
        }
        bool IsLeft
        {
            set
            {
                if (value != isLeft)
                {
                    isLeft = value;
                    ChangeMove();
                }
            }
        }
        bool IsRight
        {
            set
            {
                if (value != isRight)
                {
                    isRight = value;
                    ChangeMove();
                }
            }
        }
        #endregion
        #region Dash
        [SerializeField] KeyCode dashCode;
        [SerializeField] GenericEventChannel<bool> onDashInputChannel;
        [SerializeField] InputProtection dashInput;
        [SerializeField] EventChannel onDashChannel;
        #endregion
        #region Attack
        #endregion

        

        public override void SetActMode()
        {
            InputManager.Instance().GetKeyEvent(upCode, KeyCondition.Down) += () => IsUp = true;
            InputManager.Instance().GetKeyEvent(upCode, KeyCondition.Up) += () => IsUp = false;
            InputManager.Instance().GetKeyEvent(downCode, KeyCondition.Down) += () => IsDown = true;    
            InputManager.Instance().GetKeyEvent(downCode, KeyCondition.Up) += () => IsDown = false;
            InputManager.Instance().GetKeyEvent(leftCode, KeyCondition.Down) += () => IsLeft = true;
            InputManager.Instance().GetKeyEvent(leftCode, KeyCondition.Up) += () => IsLeft = false;
            InputManager.Instance().GetKeyEvent(rightCode, KeyCondition.Down) += () => IsRight = true;
            InputManager.Instance().GetKeyEvent(rightCode, KeyCondition.Up) += () => IsRight = false;


            InputManager.Instance().GetKeyEvent(dashCode, KeyCondition.Down) += dashInput.Input;
            onDashInputChannel.AddListener(dashInput.ChangeCondition);
            dashInput.onAct += () => onDashChannel.Invoke();


        }
        public override void UnSetActMode()
        {
            InputManager.Instance().GetKeyEvent(upCode, KeyCondition.Down) = delegate { };
            InputManager.Instance().GetKeyEvent(upCode, KeyCondition.Up) = delegate { };
            InputManager.Instance().GetKeyEvent(downCode, KeyCondition.Down) = delegate { };
            InputManager.Instance().GetKeyEvent(downCode, KeyCondition.Up) = delegate { };
            InputManager.Instance().GetKeyEvent(leftCode, KeyCondition.Down) = delegate { };
            InputManager.Instance().GetKeyEvent(leftCode, KeyCondition.Up) = delegate { };
            InputManager.Instance().GetKeyEvent(rightCode, KeyCondition.Down) = delegate { };
            InputManager.Instance().GetKeyEvent(rightCode, KeyCondition.Up) = delegate { };
        }
        void ChangeMove()
        {
            Vector3 orient = Vector3.zero;
            orient += isUp ? Vector3.up : Vector3.zero;
            orient += isDown ? Vector3.down : Vector3.zero;
            orient += isLeft ? Vector3.left : Vector3.zero;
            orient += isRight ? Vector3.right : Vector3.zero;
            onMoveChannel.Invoke(orient.normalized);
        }
        
    }
}
