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

    void Awake () {
		if (instance != null) 
		{
			Destroy (this);
		}
		instance = this;
	}
	
	public void LoadScene(string name)
	{
		SceneManager.LoadScene (name, LoadSceneMode.Single);
	}
}
