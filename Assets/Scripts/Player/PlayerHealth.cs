﻿using UnityEngine;
using UnityEngine.UI;    //needed as of 4.6 to use newer UI components
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public LevelManager levelManager;
	public SoundManager soundManager;


    Animator anim;
    AudioSource playerAudio;
	PlayerMovement playerMovement;    //reference to another custom script
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();

		//InChildren since the script is in a compenent child of player
        playerShooting = GetComponentInChildren <PlayerShooting> (); 

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour; 
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)        //public function that can be called by other components/scripts
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

		//dead
        if (currentHealth <= 0 && !isDead) {
			Death ();
		}
		// low health
		else if(currentHealth <= 15)
		{
			levelManager.isPlayerHealthLow = true;
			soundManager.PlayerHurtMusic();
		}
	
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;    //disables playerMovement script
        playerShooting.enabled = false;
    }


    /*
    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
    */
}