using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundManager : MonoBehaviour {

	//Audio Mixer Snapshots
	public AudioMixerSnapshot unpaused;
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot fastStage;
	public AudioMixerSnapshot playerHurt;

	//Reference to the LevelManager
	public LevelManager levelManager;

	//Reference to Player's Health
	public PlayerHealth playerHealth;

	void Awake()
	{
		//keep effects at the default Snapshot at level's start
		DefaultMusicEffects ();
	}

	public void Pause()
	{
		//if game is paused
		if (Time.timeScale == 0) {
			paused.TransitionTo (.01f);
		} 
		//otherwise
		else 
		{
			checkStageStatus();
		}
		
	}

	//determines which AudioSnapshot to transition to based on 
	void checkStageStatus()
	{
		//Player really hurt
		if(levelManager.isPlayerHealthLow)
		{
			playerHurt.TransitionTo(.01f);
		}
		//fast stage music if nearing end of enemies
		else if (levelManager.isStageAlmostDone) 
		{
			fastStage.TransitionTo(.01f);
		}
        //default audioSnapShot
		else
		{
			unpaused.TransitionTo(.01f);
		}

	}

	//slows music when player is hurt
	public void PlayerHurtMusic()
	{
		playerHurt.TransitionTo(.4f);
	}

	//speeds up music enemies are almost gone
	public void FastStageMusic()
	{
		fastStage.TransitionTo(.4f);
	}

	public void DefaultMusicEffects()
	{
		unpaused.TransitionTo(.01f);
	}
}
