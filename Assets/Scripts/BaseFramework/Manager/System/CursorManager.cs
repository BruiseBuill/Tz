using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF.Tool;

namespace BF
{
	public class CursorManager : Single<CursorManager>
	{
		[SerializeField] List<Input2ActMode> actModeList;
		[SerializeField] int lastActModeIndex = 0;

        private void Awake()
        {
			int initialActModeIndex = 0;
			LoadAct(initialActModeIndex);
        }
        void LoadAct(int index)
		{
			actModeList[lastActModeIndex].UnSetActMode();
			actModeList[index].SetActMode();
            lastActModeIndex = index;
        }
	}
}