using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public SoundManager soundManager;

	Canvas canvas;

	void Start()
	{
		canvas = GetComponent<Canvas>();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			canvas.enabled = !canvas.enabled;
			Pause ();
		} 

	}

	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		soundManager.Pause ();
	}
	

	//Main Quit-Exit
	public void Quit()
	{
#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
#else 
		Application.Quit();
#endif
	}
}
