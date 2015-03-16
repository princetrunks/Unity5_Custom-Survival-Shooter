using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	//Class vars
	public static int maxEnemies;              //max enemies allowed simultaneously on screen
	public static int totalEnemies;            //total enemies currently on screen

	public LevelManager levelManager;
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start ()
    {

		//'InvokeRepeating' means no timer needed for repeating function
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
		//No spawning if player is Dead
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

		//prevents infinite spawning & keeps only enough enemies as the LevelManager dictates
		if (totalEnemies < maxEnemies && totalEnemies < LevelManager.totalDefeatableEnemies)
		{

			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			
			Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

			totalEnemies++;
			//Debug.Log("Total Enemies:" + totalEnemies.ToString());

		}

    }

	//speeds up the movement of the enemy toward the player.
	public void SpeedUp()
	{
		LevelManager.enemySpeed = 8;
	}
	
}
