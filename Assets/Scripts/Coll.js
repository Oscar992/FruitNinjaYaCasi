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
public var explosion:AudioSource ;



function OnCollisionEnter (col:Collision)
	{
	Debug.Log("Collision");
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

			//	if(MODO TIEMPO)
			//	{
					tm = int.Parse(time.text) +10;
					time.text = tm.ToString();
			//	}

			}
			else
			{
				scr = int.Parse(score.text) - 200;

				if(false/*MODO VIDA*/)
			{
					lfe = int.Parse(lifes.text) -1;
					lifes.text = lfe.ToString();

					if(int.Parse(lifes.text) == 0 )
				  Application.LoadLevel("GameOver");					

				}

			//if(MODO TIEMPO)
			//{
					tm = int.Parse(time.text) -10;
					time.text = tm.ToString();

				//if(int.Parse(time.text)<=0)
				//Application.LoadLevel("GameOver");

			//}

			}

			score.text = scr.ToString();

			GetComponent.<AudioSource>().PlayOneShot(splatSound);
								//get the speed and rotation and than destroy the fruit
			var VelocityF = SliceHit.transform.GetComponent.<Rigidbody>().velocity;
			var AngularVelocityF = SliceHit.transform.GetComponent.<Rigidbody>().angularVelocity;
			
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

	function Start () {
	   CountDown();
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