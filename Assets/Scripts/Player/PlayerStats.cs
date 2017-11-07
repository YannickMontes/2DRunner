using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	private int life;
	private bool vulnerable;
	private Animator animator;

	// Use this for initialization
	void Start () 
	{
		life = 3;	
		vulnerable = true;
		this.animator = this.GetComponent<Animator> ();
		this.animator.SetInteger ("Life", life);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (life == 0) 
		{
			Debug.Log ("PLus de vie");
			this.GetComponent<PlayerController> ().enabled = false;
		}
	}

	public void DecreaseLife()
	{
		if (vulnerable) 
		{
			life--;
			StartCoroutine (Fade ());
			vulnerable = false;
			this.animator.SetInteger ("Life", life);
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
