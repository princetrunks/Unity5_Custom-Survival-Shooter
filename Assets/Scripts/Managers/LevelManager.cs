using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	
	//Enemies
	public static int totalDefeatableEnemies;  //enemies left to win stage
	public static float enemySpeed;

	public EnemyManager enemyManager;

	public int maxEnemiesOnScreen      = 5;
	public int totalEnemiesToDefeat    = 30;
	public float speedOfEnemies        = 3;  

	//reference to soundManager
	public SoundManager soundManager;

	//types of stage status
	public bool isPlayerHealthLow;
	public bool isStageAlmostDone;
	
	void Awake ()
	{
		enemySpeed = speedOfEnemies;
		EnemyManager.totalEnemies = 0; //resets the enemy count for the start of the stage
		EnemyManager.maxEnemies   = maxEnemiesOnScreen;
		totalDefeatableEnemies    = totalEnemiesToDefeat;
		isPlayerHealthLow = false;
		isStageAlmostDone = false;
	}

	bool EnemiesAlmostGone()
	{
		return totalDefeatableEnemies < (totalEnemiesToDefeat * .10f);
	}

	void Update()
	{
		//if < 10% of remaining enemies are left, speed music and enemies.
		if(EnemiesAlmostGone() && !isStageAlmostDone)  
		{
			BGMusicSpeedUp();
			enemyManager.SpeedUp();
		}
	}
	
     public void BGMusicSpeedUp()
	{
		isStageAlmostDone = true;
		soundManager.FastStageMusic();
	}
	
}
