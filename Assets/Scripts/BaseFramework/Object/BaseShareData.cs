using BF.Utility;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


namespace BF.Object
{
	public abstract class BaseShareData : MonoBehaviour
	{
        public Action onCloseControl = delegate { };

        public DataWithEvent<bool> isAlive;
		public int _IdentityCode;
        Sequece<BaseComponent> componentList;
        Dictionary<Type, SubData> subDataDic;
        
        bool isInitialized;

        /// <summary>
        /// ����һЩֻ��Ҫ��ʼ���ڴ�һ�ε��ֶν��г�ʼ��
        /// </summary>
        protected void Awake()
		{
            if (!isInitialized)
            {
                isInitialized = true;
                componentList = new Sequece<BaseComponent>();

                subDataDic = new Dictionary<Type, SubData>();
                var subs = GetComponentsInChildren<SubData>();
                foreach (var sub in subs )
                {
                    subDataDic.Add(sub.GetType(), sub);
                }
                //AwakeEx();
            }
        }
        /// <summary>
        /// ����һЩֻ��Ҫ��ʼ���ڴ�һ�ε��ֶν��г�ʼ��,����subDataDic
        /// </summary>
        //protected abstract void AwakeEx();
        /// <summary>
        /// ��initializeʱ����
        /// </summary>
        public abstract void Initialize<T>(T para) where T : DataInit;

        public void Open()
		{
            SetLocalData();
            SetDataEventWhenOpen();
            AddDependence();
            OpenComponentList();
        }
        public void AfterOpen()
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                componentList[i].AfterOpen();
            }
            AfterOpenEx();
        }
        public void Close()
        {
            CloseComponentList();
            CloseLocalData();
            ClearDependence();
        }
        #region Open
        void SetLocalData()
        {
            _IdentityCode++;
            isAlive.ResetData(true);
        }
        /// <summary>
        /// ��openʱ����,��ÿһ�μ����Ҫ���¸�ֵ�����ݽ��г�ʼ��
        /// </summary>
        public abstract void SetDataEventWhenOpen();
        /// <summary>
        /// ��openʱ����,�����ڲ����ⲿ���¼�����
        /// </summary>
        public abstract void AddDependence();
        void OpenComponentList()
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                componentList[i].Open();
            }
        }
        #endregion
        protected abstract void AfterOpenEx();
        #region Close
        void CloseComponentList()
        {
            for (int i = componentList.Count; i >= 0; i--)
            {
                componentList[i].Close();
            }
        }
        void CloseLocalData()
        {
            
        }
        /// <summary>
        /// ��closeʱ����,����ڲ����ⲿ���¼�����
        /// </summary>
        public abstract void ClearDependence();
        #endregion
        #region Register
        public void Register(BaseComponent component, int priority)
        {
            if (!isInitialized)
            {
                Awake();
            }
            componentList.Add(component, priority);
        }
        public void Unregister(BaseComponent component)
        {
            componentList.Remove(component);
        }
        #endregion
        #region SubData
        public void AddSubData<T>(T subdata) where T : SubData
        {
            subDataDic.Add(typeof(T), subdata);
        }
        public void RemoveSubData<T>() where T : SubData
        {
            subDataDic.Remove(typeof(T));
        }
        public T GetSubData<T>() where T : SubData
        {
            return (T)subDataDic[typeof(T)];
        }
        #endregion
    }
}