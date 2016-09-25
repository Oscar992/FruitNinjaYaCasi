using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {

	public Text score;

	// Use this for initialization
	void Start () {
		score.text = "Tu Puntaje "+ PlayerPrefs.GetInt ("LastScore").ToString (); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
