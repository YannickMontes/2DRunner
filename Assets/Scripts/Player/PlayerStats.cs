using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	private int life;
	private bool vulnerable;
	private Animator animator;

	private List<GameObject> lifeUI;

	// Use this for initialization
	void Start () 
	{
		life = 3;	
		vulnerable = true;
		this.animator = this.GetComponent<Animator> ();
		this.animator.SetInteger ("Life", life);
		lifeUI = new List<GameObject> ();
		for (int i = 1; i <= 3; i++) {
			lifeUI.Add (GameObject.Find ("UILife" + i));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (life == 0) 
		{
            FindObjectOfType<BoardManager>().GameOver();
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
			lifeUI [life].SetActive (false);
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
