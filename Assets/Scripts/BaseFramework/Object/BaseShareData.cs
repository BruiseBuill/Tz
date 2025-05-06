using BF.Utility;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace BF.Object
{
	public abstract class BaseShareData : MonoBehaviour
	{
        public Action onCloseControl = delegate { };

        public DataWithEvent<bool> isAlive;
		public int _IdentityCode;
        public Sequece<BaseComponent> componentList;
        
        bool isInitialized;

        /// <summary>
        /// ����һЩֻ��Ҫ��ʼ���ڴ�һ�ε��ֶν��г�ʼ��
        /// </summary>
        protected virtual void Awake()
		{
            isInitialized = true;
            componentList = new Sequece<BaseComponent>();
        }

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
            isAlive.onValueChange += (alive) =>
            {
                if (!alive)
                    Close();
            };
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
            isAlive.onValueChange = delegate { };
        }
        /// <summary>
        /// ��closeʱ����,����ڲ����ⲿ���¼�����
        /// </summary>
        public abstract void ClearDependence();
        #endregion

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
        #region Data
        [Serializable]
		public class DataWithEvent<T>
		{
			public UnityAction<T> onValueChange = delegate { };
			public UnityAction<T, T> onValueChange2Value = delegate { };
            [SerializeField] protected T data;

            /// <summary>
            /// ��ֵ��ᴥ���¼�
            /// </summary>
            public virtual T Value 
            {
                get => data;			
                set
				{
					onValueChange2Value.Invoke(data, value);
                    data = value;
					onValueChange.Invoke(data);
				}
			}
			public DataWithEvent()
			{
				onValueChange = delegate { };
                onValueChange2Value = delegate { };
            }
            /// <summary>
            /// ��data��ʼ�������ᴥ���¼���������open���е���
            /// </summary>
            public void ResetData(T data)
            {
                this.data = data;

            }
        }
		[Serializable]
		public class DataWithEventHop: DataWithEvent<bool> 
		{
            public override bool Value 
			{ 
				get => base.Value;
				set 
				{
					if (value != data) 
					{
                        data = value;
                        onValueChange.Invoke(data);
                    }
				}
			}
		}
        #endregion
        #region Data with Additive
        [Serializable]
		public abstract class DataWithVariableValue<T> : DataWithEvent<T> 
		{
            protected T additive;
            public T Additive
            {
                get => additive;
            }
			public virtual T FullValue
			{
				get;
			}
            
            public abstract void AddAdditive(T additive);
            /// <summary>
            /// ��additive��ʼ�������ᴥ���¼���������open���е���
            /// </summary>
            public void ResetAdditive(T additive)
            {
                this.additive = additive;
            }
        }
		[Serializable]
        public class DataWithVariableFloat : DataWithVariableValue<float>
		{
            public override float FullValue 
			{
				get => data + additive;
			}
            public override float Value
            {
                get => base.Value;
                set
                {
                    onValueChange2Value.Invoke(FullValue,Additive+ value); 
                    data = value;
                    onValueChange.Invoke(FullValue);
                }
            }
            public override void AddAdditive(float additive)
            {
				onValueChange2Value.Invoke(FullValue, FullValue + additive);
				this.additive += additive;
				onValueChange.Invoke(FullValue);
            }
        }
        [Serializable]
        public class DataWithVariableInt : DataWithVariableValue<int>
        {
            public override int FullValue
            {
                get => data + Additive;
            }
            public override int Value
            {
                get => base.Value;
                set
                {
                    onValueChange2Value.Invoke(FullValue, Additive + value);
                    data = value;
                    onValueChange.Invoke(FullValue);
                }
            }
            public override void AddAdditive(int additive)
            {
                onValueChange2Value.Invoke(FullValue, FullValue + additive);
                this.additive += additive;
                onValueChange.Invoke(FullValue);
            }
        }
        #endregion
    }
}