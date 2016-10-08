using UnityEngine;
using System.Collections;

public class MainRender : MonoBehaviour {
	
	public GameObject centerOfView;

	public GameObject renderCubeDerecha;
	public GameObject renderCubeTrasera;
	public GameObject renderCubeFrontal;
	public GameObject renderCubeIzquierda;

	// Use this for initialization
	void Start () {
	    //CountDown ();
		//	Screen.fullScreen = false;
		//renderCubeDerecha.transform.s	cale = new vector3 (3,3,0.1);
	}
	// Update is called once per frame
	void Update () {
		
		Debug.Log("xczxczx");

	float fixCamera = 2.88f;
		//renderCubeDerecha.transform.position = new Vector3 (2.0f,0f,0f);
		renderCubeDerecha.transform.position = new Vector3 (centerOfView.transform.position.x+fixCamera,centerOfView.transform.position.y,centerOfView.transform.position.z);
		renderCubeDerecha.transform.localScale = new Vector3 (fixCamera ,fixCamera ,0.1f);

		renderCubeTrasera.transform.position = new Vector3 (centerOfView.transform.position.x,centerOfView.transform.position.y-fixCamera,centerOfView.transform.position.z);
		renderCubeTrasera.transform.localScale = new Vector3 (fixCamera ,fixCamera ,0.1f);

		renderCubeFrontal.transform.position = new Vector3 (centerOfView.transform.position.x,centerOfView.transform.position.y+fixCamera,centerOfView.transform.position.z);
		renderCubeFrontal.transform.localScale = new Vector3 (fixCamera ,fixCamera ,0.1f);

		renderCubeIzquierda.transform.position = new Vector3 (centerOfView.transform.position.x-fixCamera,centerOfView.transform.position.y,centerOfView.transform.position.z);
		renderCubeIzquierda.transform.localScale = new Vector3 (fixCamera ,fixCamera ,0.1f);
		
	}
}
