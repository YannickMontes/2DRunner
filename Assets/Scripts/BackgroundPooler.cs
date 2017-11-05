using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPooler : AbstractObjectPooler {

	public static BackgroundPooler current;

	// Use this for initialization
	void Awake () {
		BackgroundPooler.current = this;
	}
}
