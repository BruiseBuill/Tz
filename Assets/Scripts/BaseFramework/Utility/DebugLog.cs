using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
	public class DebugLog : Single<DebugLog>
	{
		[SerializeField] bool isDebugA;

		public void LogA(object message)
		{
			if (isDebugA)
				Debug.Log(message);
		}
	}
}