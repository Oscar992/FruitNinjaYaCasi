using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Scores : MonoBehaviour {

	// Use this for initialization

	public Text txtScoreTimes;
	public Text txtScoreLifes;

	void Start () {

		int []scoreTimes = new int[6];

		scoreTimes[0] =	PlayerPrefs.GetInt("scoreTime1");
		scoreTimes[1] =	PlayerPrefs.GetInt("scoreTime2");
		scoreTimes[2] =	PlayerPrefs.GetInt("scoreTime3");
		scoreTimes[3] =	PlayerPrefs.GetInt("scoreTime4");
		scoreTimes[4] =	PlayerPrefs.GetInt("scoreTime5");
		scoreTimes[5] = PlayerPrefs.GetInt("lastScoreTime");

		bubbleSort (scoreTimes);

		for (int i = 0; i < 5; i++) 
		{
			txtScoreTimes.text = txtScoreTimes.text + scoreTimes [i] + "\n";

			PlayerPrefs.SetInt("scoreTime1",scoreTimes[0]);
			PlayerPrefs.SetInt("scoreTime2",scoreTimes[1]);
			PlayerPrefs.SetInt("scoreTime3",scoreTimes[2]);
			PlayerPrefs.SetInt("scoreTime4",scoreTimes[3]);
			PlayerPrefs.SetInt("scoreTime5",scoreTimes[4]);
		}
			
		int []scoreLifes = new int[6];

		scoreLifes[0] =		PlayerPrefs.GetInt("scoreLifes1");
		scoreLifes[1] =		PlayerPrefs.GetInt("scoreLifes2");
		scoreLifes[2] =		PlayerPrefs.GetInt("scoreLifes3");
		scoreLifes[3] =  	PlayerPrefs.GetInt("scoreLifes4");
		scoreLifes[4] =		PlayerPrefs.GetInt("scoreLifes5");
		scoreLifes[5] =     PlayerPrefs.GetInt("lastScoreLifes"); 

		bubbleSort (scoreLifes);

		for (int i = 0; i < 5; i++) {
			
			txtScoreLifes.text = txtScoreLifes.text + scoreLifes [i] + " \n";

			PlayerPrefs.SetInt("scoreLifes1",scoreLifes[0]);
			PlayerPrefs.SetInt("scoreLifes2",scoreLifes[1]);
			PlayerPrefs.SetInt("scoreLifes3",scoreLifes[2]);
			PlayerPrefs.SetInt("scoreLifes4",scoreLifes[3]);
			PlayerPrefs.SetInt("scoreLifes5",scoreLifes[4]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void bubbleSort(int []ar)
	{
		for (int i = (ar.Length - 1); i >= 0; i--)
		{
			for (int j = 1; j <= i; j++) 
			{
				if (ar [j - 1] < ar [j]) 
				{
					int temp = ar [j-1];
					ar[j-1] = ar[j];
					ar[j] = temp;
				}
			}		
		}
	}

//	void shellsort(int []a , int n)
//	{
//		int x, i, j, inc, s;
//
//		for (s = 1; s < t; s++) {
//		
//			inc = h[s];
//
//			for (i = inc + 1; i < n; i++) {
//				x = a [i];
//				j = i - inc;
//				while (j > 0 && a[j] > x)
//				{
//					a [j + h] = a [j];
//					j = j - Helper;
//				}
//			}
//
//		}
//
//	}



}
