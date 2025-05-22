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
        /// <summary>
        /// �˺�������Awake֮�����,��ֻ��Ҫ��ֵһ�ε��ֶθ�ֵ���ر���data����
        /// </summary>
        protected abstract void AfterAwake();
        /// <summary>�˺�������SetActive true֮ǰ����,��������������ϵ</summary>
        public abstract void Open();
        /// <summary>�˺�������SetActive true֮����ã���������Э�̵�</summary>
        public abstract void AfterOpen();
        /// <summary>�˺�������Data��Close�����ã�֮���������뵽PoolManager������ж��������ϵ</summary>
        public abstract void Close();
    }
}