public var SplatterMellon:Transform;
public var SplatterCitrus:Transform;
public var SplatterApple:Transform;
public var MelonSlice:Transform;
public var CitrusSlice:Transform;
public var AppleSlice:Transform;
public var clone:Transform;
public var splatSound : AudioClip; 
public var score: UnityEngine.UI.Text;
public var lifes: UnityEngine.UI.Text;
public var time: UnityEngine.UI.Text;
public var explosion:AudioSource;

static var scoreTime1:int;
static var scoreTime2:int;
static var scoreTime3:int;
static var scoreTime4:int;
static var scoreTime5:int;

static var scoreLifes1:int;  
static var scoreLifes2:int;
static var scoreLifes3:int;
static var scoreLifes4:int;
static var scoreLifes5:int;

static var lastScoreTime:int;
static var lastScoreLifes:int;

static var lastScore:int;
static var scoreLifes:int;
//Se muestra en la pantalla del GameOver

private var csScript : BodySourceView; 
private var csScriptVariablesGlobales : VariablesGlobales; 

function OnCollisionEnter (col:Collision)
	{
	Debug.Log("katana colision");

	var SliceHit : GameObject;
		SliceHit = col.gameObject;

		if(SliceHit.tag == "Fruit" || SliceHit.tag == "Bomb")
		{
		var scr;
		var lfe;
		var tm;
			if(SliceHit.tag == "Fruit")
			{
				scr = int.Parse(score.text) + 100;

				if(csScript.opcionMenu == 1)//con tiempo
				{
					tm = int.Parse(time.text) +10;
					time.text = tm.ToString();
		    	}
			}
			else
			{				
				//f((int.Parse(score.text) - 200) >= 0)
				  scr = int.Parse(score.text);
			if(csScript.opcionMenu == 0)//con vidas
			{
					lfe = int.Parse(lifes.text) -1;
					lifes.text = lfe.ToString();

					if(int.Parse(lifes.text) == 0 )
					{
						scoreLifes = scr;
						lastScore = scr;
						PlayerPrefs.SetInt("lastScoreLifes",scoreLifes);
						PlayerPrefs.SetInt("LastScore",lastScore);
						csScriptVariablesGlobales.volverAlMenu = true;
				  		Application.LoadLevel("GameOver");					
				  	}
			}

			if(csScript.opcionMenu == 1)//con tiempo
			{

				tm = int.Parse(time.text) -10;
				time.text = tm.ToString();
	
				if(int.Parse(time.text)<=0)
				{
						scoreTime = scr;
						lastScore = scr;
		
					PlayerPrefs.SetInt("lastScoreTime",scoreTime);
					PlayerPrefs.SetInt("LastScore",lastScore);
					csScriptVariablesGlobales.volverAlMenu = true;
					Application.LoadLevel("GameOver");
				}
			}

			}
			if(scr!=null)
			score.text = scr.ToString();

			GetComponent.<AudioSource>().PlayOneShot(splatSound);
								//get the speed and rotation and than destroy the fruit
			var VelocityF = SliceHit.transform.GetComponent.<Rigidbody>().velocity;
			var AngularVelocityF = SliceHit.transform.GetComponent.<Rigidbody>().angularVelocity;
			var pMagnitude = 4;
			AngularVelocityF = Vector3(AngularVelocityF.x*pMagnitude,AngularVelocityF.y*pMagnitude,AngularVelocityF.z*pMagnitude);
			
			if(SliceHit.name == 'Citrus(Clone)') {
				slice1 = Instantiate(CitrusSlice, SliceHit.transform.position, Quaternion.identity); 
				slice2 = Instantiate(CitrusSlice, SliceHit.transform.position, Quaternion.Euler(0,180,0)); 
				splat = Instantiate(SplatterCitrus, SliceHit.transform.position+Vector3(0,0,1), Quaternion.Euler(0,180,Random.Range(0, 360)));
			}
	
			if(SliceHit.name == 'Melon(Clone)') {
			
				slice1 = Instantiate(MelonSlice, SliceHit.transform.position, Quaternion.identity); 
				slice2 = Instantiate(MelonSlice, SliceHit.transform.position, Quaternion.Euler(0,180,0)); 
				splat = Instantiate(SplatterMellon, SliceHit.transform.position+Vector3(0,0,1), Quaternion.Euler(0,180,Random.Range(0, 360)));
			}
			
			if(SliceHit.name == 'Apple(Clone)') {
				slice1 = Instantiate(AppleSlice, SliceHit.transform.position, Quaternion.identity); 
				slice2 = Instantiate(AppleSlice, SliceHit.transform.position, Quaternion.Euler(0,180,0)); 
				splat = Instantiate(SplatterApple, SliceHit.transform.position+Vector3(0,0,1), Quaternion.Euler(0,180,Random.Range(0, 360)));
			}


			if(SliceHit.name == 'Bomb(Clone)') {
				var exp = SliceHit.GetComponent.<ParticleSystem>();
				exp.Play();
				explosion.Play();	
				Destroy(SliceHit,0.8f);
				return;
			}

			if(slice1 != null && slice2 !=null   && splat != null &&  SliceHit !=null )
			{
				slice1.GetComponent.<Rigidbody>().velocity = VelocityF;
				slice1.GetComponent.<Rigidbody>().angularVelocity = AngularVelocityF;
				//add force so it wont stay with slice 2
				slice1.GetComponent.<Rigidbody>().AddForce (Vector3(-100,0,0));
				slice2.GetComponent.<Rigidbody>().velocity = VelocityF;
				slice2.GetComponent.<Rigidbody>().angularVelocity = AngularVelocityF;
				//add force so it wont stay with slice 1
				slice2.GetComponent.<Rigidbody>().AddForce (Vector3(100,0,0));		
							
				Destroy (slice2.gameObject, 3);
				Destroy (slice1.gameObject, 3);
				Destroy (splat.gameObject, 3);
				Destroy (SliceHit);
			}
		}
	}

	function Start () {
	 csScript = this.GetComponent("BodySourceView");
	 csScriptVariablesGlobales = this.GetComponent("VariablesGlobales");

	 if(csScript.opcionMenu == 1)//con tiempo
	 {
		 //lifes.enabled = false;
		 //lifes.GetComponent.<UnityEngine.UI.Text>().enabled = false;
		 //lifes.GetComponent.<UnityEngine.UI.Text>().text = "-";
	     lifes.text ="-";
	     Debug.Log("Con Tiempo");
	     CountDown();
		 
	 }
	   if(csScript.opcionMenu == 0)//con vidas
	   {
		 // time.enabled = false;
		 // time.GetComponent.<UnityEngine.UI.Text>().enabled = false;
		// time.GetComponent.<UnityEngine.UI.Text>().text = "-";
	     time.text = "-";
		 //time.text ="CON VIDAS";
		 //  Debug.Log("Con Vidas");
	   }
	}
		
	function Update () {
	}

	function CountDown()
	{
		var curTime = int.Parse(time.text)-1;
	    time.text = curTime.ToString ();
			if(curTime > 0) {
				Invoke("CountDown",1);
		}

	}


 