using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kinect = Windows.Kinect;
using Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
	protected float yScale = 0;
	public float scale;
	public float smoothMovement;
	protected CameraSpacePoint pos;
    public Material BoneMaterial;
    public GameObject BodySourceManager;
	public GameObject HandCursor;  
	private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    
	public Canvas canvas;
    private Button[] buttons;
	private Image[] images;
	//bool isMenu 

	public static int opcionMenu;	


    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };


	void Start(){
	
		images = canvas.GetComponentsInChildren<Image> ();
		buttons = canvas.GetComponentsInChildren<Button> ();

	}
    
    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                	_Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
				//---

				Kinect.Joint izquierda = body.Joints[JointType.HandLeft];
				Kinect.Joint derecha = body.Joints[JointType.HandRight];
				var positionizq = izquierda.Position;
				var positionder = derecha.Position;

				CameraSpacePoint cameraPoint2 = derecha.Position;

				pos = derecha.Position;
				HandCursor.transform.localPosition = Vector3.Lerp(HandCursor.transform.localPosition,new Vector3(pos.X*scale, pos.Y*scale-yScale, this.HandCursor.transform.localPosition.z)-HandCursor.transform.localPosition,Time.deltaTime*smoothMovement);

				//---
				//if (body.HandRightState == HandState.Closed) 
				//{
				//	Debug.Log ("CLICK!!!");
				//}

				//int cont = 0;
				foreach (Button button in buttons) 
				{   
					
					float px = button.GetComponent<RectTransform>().localPosition.x;
					float py = button.GetComponent<RectTransform>().localPosition.y;
					float pWidth = button.GetComponent<RectTransform>().rect.width;
					float pHeight = button.GetComponent<RectTransform>().rect.height;

//					Debug.Log ("Button " + cont);
//					Debug.Log ("px " + px);
//					Debug.Log ("py " + py);
//					Debug.Log ("pWidth " + pWidth);
//					Debug.Log ("pHeight " + pHeight);
//					Debug.Log ("Mouse x: " + this.HandCursor.transform.localPosition.x);
//					Debug.Log ("Mouse y: " + this.HandCursor.transform.localPosition.y);
//					cont++;

					if(this.HandCursor.transform.localPosition.x  >= (px - pWidth/2) &&
					   this.HandCursor.transform.localPosition.x  <= (px + pWidth/2) &&
					   this.HandCursor.transform.localPosition.y  >= (py - pHeight/2)&&
					   this.HandCursor.transform.localPosition.y  <= (py + pHeight/2))

					if (body.HandRightState == HandState.Closed)
					{
						if( button == buttons[0] )//Con Vidas
						{
							
							opcionMenu = 0;
							if (VariablesGlobales.volverAlMenu) {
								SceneManager.LoadScene("Menu");
								return;
							}	
							SceneManager.LoadScene("GameFN");		

						
						}
						else if (button == buttons[1])//Con tiempo
						{
							opcionMenu = 1;
							SceneManager.LoadScene("GameFN");	
							//Application.LoadLevel("Game");
						

						}else if(button == buttons[2]) //Scores
						{
							opcionMenu = 2;
							SceneManager.LoadScene("Score");	
							VariablesGlobales.volverAlMenu = true;
							//Application.LoadLevel("Score");
						}
					}
				}
			}
		}
	}
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            //LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            //lr.SetVertexCount(2);
           // lr.material = BoneMaterial;
           // lr.SetWidth(0.05f, 0.05f);
            
            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.FindChild(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
          //  LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
             //   lr.SetPosition(0, jointObj.localPosition);
             //   lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
              //  lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                //lr.enabled = false;
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
