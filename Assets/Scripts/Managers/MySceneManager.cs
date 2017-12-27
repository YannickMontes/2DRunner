using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	private static MySceneManager instance;
	private AssetBundle assetBundle;
	private string[] scenePaths;

	public static MySceneManager getInstance(){
		if (instance == null) {
			instance = new MySceneManager ();
		}
		return instance;
	}

	// Use this for initialization
	void Start () {
		if (instance != null) 
		{
			Destroy (this);
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene (name, LoadSceneMode.Single);
	}
}
