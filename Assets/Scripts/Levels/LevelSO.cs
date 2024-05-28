using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelSO: ScriptableObject
{

	private int _completedLevelCounts;

	public int Value
    {
		get { return _completedLevelCounts; }
		set { _completedLevelCounts = value; }
	}

}
