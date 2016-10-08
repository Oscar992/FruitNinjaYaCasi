using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public UnityEngine.UI.Text score;
	public UnityEngine.UI.Text GameTimer;
	public GameObject Katana;
	private Vector3 endpoint; 
	public GameObject rendererCamera;
	public GameObject centerOfView;

	public GameObject cameraDerecha;
	public GameObject cameraTrasera;
	public GameObject cameraFrontal;
	public GameObject cameraIzquierda;

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
	void CountDown()
	{
		int curTime = int.Parse(GameTimer.text)-1;
		GameTimer.text = curTime.ToString ();
		if(curTime > 0) 
			Invoke("CountDown",1);
	}
	// Update is called once per frame
	void Update () {
		
		

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
		
		float distanceBetweenCameras = 30;
		cameraDerecha.transform.position = new Vector3 (-distanceBetweenCameras,0.0f,0.0f);
		cameraTrasera.transform.position = new Vector3 (0.0f,0.0f,-distanceBetweenCameras);
		cameraFrontal.transform.position = new Vector3 (0.0f,0.0f,distanceBetweenCameras);
		cameraIzquierda.transform.position = new Vector3 (distanceBetweenCameras,0.0f,0.0f);

		

		if(Input.GetMouseButton(0)) 
		{
			
			Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
			endpoint = ray.GetPoint(15); 
			Debug.DrawRay(ray.origin, ray.direction * 15, Color.yellow);
			Katana.transform.position = Vector3.Lerp(Katana.transform.position,new Vector3(endpoint.x,endpoint.y,-1),Time.deltaTime*15);
		}
		if (Input.GetKey ("escape")) {
			Application.Quit();
		}
	}
}
