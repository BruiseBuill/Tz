using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZ.Character.Move
{
 	public class WalkMoveCP : BaseMoveCP
	{
        [SerializeField] protected Vector3 moveOrient;
        [SerializeField] protected float accelerateTime;
        [SerializeField] float walkSpeed;
        Tween tween;
 
        public override void Open()
        {
            characterData.inputOrient.onValueChange += Move;
            characterData.canMove.onValueChange += (canMove) =>
            {
                if (canMove)
                {
                    StartCoroutine("Moving");
                }
                else
                {
                    StopCoroutine("Moving");
                }
            };
        }
        public override void AfterOpen()
        {
            StartCoroutine("Moving");
        }
        public override void Close()
        {
            characterData.inputOrient.onValueChange -= Move;
        }
        public override void Move(Vector3 orient)
        {
            if (orient == Vector3.zero)
            {
                StopMove();
            }
            else
            {
                tween.Kill();
                tween = DOTween.To(() => moveOrient, (a) => moveOrient = a, characterData.inputOrient.Value, accelerateTime);
            }
        }
        public override void StopMove()
        {
            tween.Kill();
            tween = DOTween.To(() => moveOrient, (a) => moveOrient = a, Vector3.zero, accelerateTime);
        }
        IEnumerator Moving()
        {
            while (characterData.canMove.Value)
            {
                characterData.realPos.Value += moveOrient * walkSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}
