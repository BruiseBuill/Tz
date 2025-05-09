using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TZ.Character.Jump
{
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