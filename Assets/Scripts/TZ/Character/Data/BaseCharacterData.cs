using BF.Object;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TZ.Utility;
using UnityEditor.PackageManager;
using UnityEngine;

namespace TZ.Character.Data
{
	public abstract class BaseCharacterData : BaseShareData
	{
        [Header("Model")]
        public Transform model;
        public Rigidbody2D rb;

        [Tooltip("正交3d视角当中的真实坐标，非等距视角")]
        [ReadOnly]
        [SerializeField] Vector3 realPos;
        public Vector3 RealPos
        {
            get => realPos;
            protected set => realPos = value;
        }
        public Vector3 totalForce;

        public DataWithEventHop canMove;
        public DataWithEvent<Vector3> inputOrient = new DataWithEvent<Vector3>();
        public Vector3 faceOrient;

        WaitForEndOfFrame wait_EndFrame;

        protected override void AfterOpenEx()
        {
            StartCoroutine("UpdatingRbPos");
        }
        public override void AddDependence()
        {
            inputOrient.onValueChange += (orient) =>
            {
                if (orient != Vector3.zero && canMove.Value)
                    faceOrient = orient;
            };
            canMove.onValueChange += (canMove) =>
            {
                if (canMove && inputOrient.Value != Vector3.zero) 
                    faceOrient = inputOrient.Value;
            };
            AddDependenceEx();
        }
        public override void ClearDependence()
        {
            ClearDependenceEx();
            inputOrient.onValueChange = delegate { };
            canMove.onValueChange = delegate { };
        }
        public abstract void AddDependenceEx();
        public abstract void ClearDependenceEx();

        IEnumerator UpdatingRbPos()
        {
            while (true)
            {
                yield return wait_EndFrame;
                rb.velocity = totalForce.ToIsoVector() / rb.mass;
                model.position += totalForce.ToIsoVector().z / rb.mass * Vector3.forward;
                totalForce = Vector3.zero;
                realPos = new Vector3(rb.position.x, rb.position.y, model.position.z).ToRealVector();
                yield return null;
            }
        }
    }
}