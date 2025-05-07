using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF.Object
{
	public abstract class BaseComponent : MonoBehaviour
	{
        protected BaseShareData data;
        [Tooltip("LowerPriorityWillRunFirst")]
        [SerializeField] protected int priority;

        protected void Awake()
        {
            data = GetComponentInChildren<BaseShareData>();
            data.Register(this, priority);
            AfterAwake();
        }
        protected abstract void AfterAwake();
        /// <summary>此函数将在SetActive true之前调用,用来加载依赖关系</summary>
        public abstract void Open();
        /// <summary>此函数将在SetActive true之后调用，用来启动协程等</summary>
        public abstract void AfterOpen();
        /// <summary>此函数将由Data的Close来调用，之后物体会进入到PoolManager，用来卸载依赖关系</summary>
        public abstract void Close();
    }
}