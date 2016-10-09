using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {

	public Text score;

	// Use this for initialization
	void Start () {
		string pText = "Tu puntaje" + PlayerPrefs.GetInt ("LastScore");
		Debug.Log(pText);
		score.text = pText /*"Tu Puntaje "+ PlayerPrefs.GetInt ("LastScore")*/; 
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log("Tu puntaje" + PlayerPrefs.GetInt ("LastScore"));
	//	score.text = "Tu Puntaje "+ PlayerPrefs.GetInt ("LastScore"); 
	}
}
