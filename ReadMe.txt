This is a custom build of the Unity tutorial game, NightMares.

The game was build from scratch to the online tutorial videos (http://unity3d.com/learn/tutorials/projects/survival-shooter) with the completed assets never used & removed (except for some of the PauseManager’s code)

These are the following edits/additions that I made in this particular build that should show how I can not only expand on the tutorial’s initial gameplay, but also use the new tools recently introduced in Unity 5.0…

General Code and GamePlay
	•	Created a LevelManager script to have overall game manager functionality
	•	LevelManager has public vars that set the number of enemies that can be on the screen at the same time and the total enemies for the stage
	•	Edited the GameOver HUDCanvas to a red tint
	•	Added a way for the player to Win; defeating all enemies
	•	Added a Win screen and animation to the HUDCanvus
	•	Gave the player two gun types; the default machine gun from the tutorial and a pistol; no new mesh / visual effects made for the different guns at the moment but a second gun audio source is added in script with an audio source array as well as a GunType enum type so that the type of gun being fired can be logged for future use. Fire1 is the machine gun, Fire2 is the pistol.  The pistol has a slower reload time but has greater damage.
	•	HealthBar changed to be green when full or close to full, yellow if < 70 and flashing red when health is low.
Enemies:
	•	No more infinite spawning; a max number of enemies that can be on screen at the same time to prevent a memory leak/lag/crash.
	•	Created a total number of enemies to defeat to win the stage
	•	If nearing the last of the enemies, added code to the EnemyManager that would keep only that number of enemies live on the screen. In other words, if there’s only 2 enemies left, there won’t be 3 or more enemies on the screen.
	•	Added Enemy text manager  script and HUD text that is controlled by that script for the enemy countdown
	•	Enemies will speed up at the player if only a few are left to beat the stage.
	•	Enemies shot by pistol get knocked back a bit.
	•	
Audio:

	•	Added new Unity 5 AudioMixer component; separated the music and the sound effects
	•	Created a number of AudioMixer Snapshots based on game states; Paused creates a lowpass effect and UnPaused is the default sounds, akin to the online tutorial on using Snapshots. LowHealth slows the pitch/tempo of the music when the Player is low on Health.  FastStage speeds up the music pitch/tempo as the enemies near defeat.
	•	Created a SoundManager script to control music and sound effects in conjunction with the LevelManager script and the various AudioMixer Snapshots

Lighting:
	•	using the new Unity 5 GI baked lighting in conjunction with a nighttime skybox from the FantasySkybox download
	•	edited the Moonlight directional lighting to help edit ver 4 > 5 conversion errors*


———
*Known Bugs
	•	The conversion from Unity 4.x to 5.0 did seem to cause some lighting errors in this project  which I would assume stems from Unity 5.0 now using global baked lighting.  The Environment prefab (only in the Unity editor, not in a PC/MACOSX compiled build) will oddly sometimes either lose it’s GI lighting or in some cases not have any lighting at.  The “fix” is to toggle the Environment as a Static component off and then back on.  Could be something to do with the lightmaps Unity created with the Environment prefab that with Unity 5’s new lighting, are being broken by the new functionality.
	•	In the WebGL build, shadows don’t show and some of the Player’s mesh is warped.  The later could be from edits

Disclaimer:

The assets/meshes of this project were built by Unity and I hold no ownership to them.

Anyone is free to download and use these as per Unity’s disclaimers on the tutorial projects.

Anyone is free to use my customs scripts, components and prefabs in their project if you so wish.

Here’s to making more awesome games :-)

-Chuck Gaffney
