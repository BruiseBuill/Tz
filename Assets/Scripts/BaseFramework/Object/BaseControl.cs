using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF.Object
{
	public interface ControlInit { }

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
        /// 传入初始化所需要的参数结构体
        /// </summary>
        public abstract void Initialize<T>(T para) where T : ControlInit;
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