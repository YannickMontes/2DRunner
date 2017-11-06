using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusCollider : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ().DecreaseLife ();
	}
}
