using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour {

private float splashElapsedTime;
private bool isFirstTime;
	void Start(){
		splashElapsedTime = 0.0f;
		isFirstTime = true;
	}

	void Update(){
		splashElapsedTime += Time.deltaTime;
		Debug.Log(splashElapsedTime);
		if	(splashElapsedTime>=3.0 && isFirstTime){
			isFirstTime = false;
		SceneManager.LoadScene("Menu");
		}
	}




}
