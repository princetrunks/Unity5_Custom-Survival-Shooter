using UnityEngine;
using System.Collections;

public class HealthMeterManager : MonoBehaviour {

	public LevelManager levelManager;
	public PlayerHealth playerHealth;

	Animator anim;
	bool isLowHealthAnimInitiated;
	bool isMediumHealthAnimInitiated;

	void Awake ()
	{
		anim = GetComponent <Animator> ();

		isLowHealthAnimInitiated    = false;
		isMediumHealthAnimInitiated = false;

	}

	void Update ()
	{
		if(levelManager.isPlayerHealthLow && !isLowHealthAnimInitiated)
		{
			isLowHealthAnimInitiated = true;

			// do animation of red health meter
			anim.SetTrigger ("LowHealth");

		}
		else if(playerHealth.currentHealth < 70 && !isMediumHealthAnimInitiated)
		{
			isMediumHealthAnimInitiated = true;

			// do animation of yellow health meter
		    anim.SetTrigger ("MediumHealth");
		}

	}


}
