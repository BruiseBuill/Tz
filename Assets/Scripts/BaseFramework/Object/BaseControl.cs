using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF.Object
{
	public interface DataInit { }

	public abstract class BaseControl : BaseObject
	{
		protected BaseShareData data;
		public bool IsAlive
		{
			protected set => data.isAlive.Value = value;
            get => data.isAlive.Value;
		}
		protected virtual void Awake()
		{
			data = GetComponentInChildren<BaseShareData>();
		}

        /// <summary>
        /// �����ʼ������Ҫ�Ĳ����ṹ��
        /// </summary>
        public void Initialize<T>(T para) where T : DataInit
        {
            data.Initialize(para);
        }
        /// <summary>
        /// ͨ����Ҫinitialize֮����open��ֱ��open����ʹ��Ĭ�ϵĲ���
        /// </summary>
        public void Open()
		{
            data.Open();
            gameObject.SetActive(true);
			data.AfterOpen();
			data.onCloseControl += Close;
        }
        public override void Close()
        {
            data.onCloseControl -= Close;
            data.Close();
			PoolManager.Instance().Recycle(gameObject);
        }
    }
}