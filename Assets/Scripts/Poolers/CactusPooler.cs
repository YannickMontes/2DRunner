using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPooler : AbstractObjectPooler {

	public static CactusPooler current;

	void Awake()
	{
		current = this;
	}
}
