using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZ.Character.Jump
{
    //虽然有跳跃，但是跳跃时不改变用于移动的碰撞盒的位置，防止卡模，即原本过不去的地方，跳跃之后仍然不会过去；用于受击判定碰撞盒的位置则会随人物模型发生移动
    public class JumpCP : BaseJumpCP
    {
        [SerializeField] float jumpSpeed;
        [SerializeField] float presentZSpeed;
        [SerializeField] float gravity;

        WaitForFixedUpdate fixedUpdate;

        protected override void AfterAwake()
        {
            base.AfterAwake();
            fixedUpdate= new WaitForFixedUpdate();
        }
        public override void Open()
        {

        }
        public override void AfterOpen()
        {
            
        }

        public override void Close()
        {
            
        }

        public override void Jump()
        {
            if (isOnGround)
            {
                StartCoroutine("Jumping");
            }
        }
        IEnumerator Jumping()
        {
            while (true)
            {
                presentZSpeed -= gravity * Time.deltaTime;

                
                yield return null;
                
            }
        }
    }
}