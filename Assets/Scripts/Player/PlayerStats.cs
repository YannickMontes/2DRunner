using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	private int life;
	private bool vulnerable;

	// Use this for initialization
	void Start () 
	{
		life = 3;	
		vulnerable = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (life == 0) 
		{
			//End the game
		}
	}

	public void DecreaseLife()
	{
		if (vulnerable) 
		{
			life--;
			StartCoroutine (Fade ());
			vulnerable = false;
		}
	}

	public IEnumerator Fade() {
		for (int i = 0; i < 12; i++) {
			this.GetComponent<SpriteRenderer> ().enabled = !this.GetComponent<SpriteRenderer> ().enabled;
			yield return new WaitForSeconds (.05f);
		}
		vulnerable = true;
	}
}
