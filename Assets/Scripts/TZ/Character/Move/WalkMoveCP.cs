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
            characterData.worldOrient.onValueChange += Move;
        }
        public override void AfterOpen()
        {
            StartCoroutine("Moving");
        }
        public override void Close()
        {
            characterData.worldOrient.onValueChange -= Move;
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
                tween = DOTween.To(() => moveOrient, (a) => moveOrient = a, characterData.worldOrient.Value, accelerateTime);
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
                characterData.model.position += moveOrient * walkSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}
