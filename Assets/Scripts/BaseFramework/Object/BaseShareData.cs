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
        /// 对于一些只需要初始化内存一次的字段进行初始化
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
        /// 对于一些只需要初始化内存一次的字段进行初始化,包括subDataDic
        /// </summary>
        //protected abstract void AwakeEx();
        /// <summary>
        /// 在initialize时调用
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
        /// 在open时调用,对每一次激活都需要重新赋值的数据进行初始化
        /// </summary>
        public abstract void SetDataEventWhenOpen();
        /// <summary>
        /// 在open时调用,挂载内部和外部的事件依赖
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
        /// 在close时调用,清空内部和外部的事件依赖
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