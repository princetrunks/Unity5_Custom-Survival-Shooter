using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyStatManager : MonoBehaviour { 

	Text text;
		
	void Awake ()
	{
		text = GetComponent <Text> ();
	}
		
		
	void Update ()
	{
		text.text = "Enemies: " + LevelManager.totalDefeatableEnemies;
	}
}
