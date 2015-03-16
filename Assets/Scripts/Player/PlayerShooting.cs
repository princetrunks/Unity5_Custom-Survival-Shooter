using UnityEngine;

public enum GunType
{
	MachineGun, 
	Pistol
};

public class PlayerShooting : MonoBehaviour
{

	public static GunType typeOfGun;
	
    public int damagePerShot;
    public float timeBetweenBullets;
    public float range = 100f;

	int machineGunDamage  = 20;
	int pistolDamage      = 45;

	float machineGunReloadTime = 0.15f;
	float pistolReloadTime     = 1.20f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;

    AudioSource[] gunSounds;
    AudioSource machineGunAudio;
	AudioSource pistolAudio;

    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
		//gun defaults to the fast MachineGun
		typeOfGun = GunType.MachineGun;
		timeBetweenBullets = machineGunReloadTime;
		damagePerShot = machineGunDamage;

        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();

        gunSounds = GetComponents<AudioSource> ();
		machineGunAudio = gunSounds [0];
		pistolAudio     = gunSounds [1];

        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            //Shoot ();
			damagePerShot = machineGunDamage;
			ShootMachineGun ();
			timeBetweenBullets = machineGunReloadTime;
        }

		if((Input.GetButton ("Fire2") || Input.GetKeyDown("space")) && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			damagePerShot = pistolDamage;
			ShootPistol ();
			timeBetweenBullets = pistolReloadTime;
		}

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

	void ShootMachineGun()
	{
		typeOfGun = GunType.MachineGun;
		machineGunAudio.Play ();
		Shoot ();
	}

	void ShootPistol()
	{
		typeOfGun = GunType.Pistol;
		pistolAudio.Play ();
		Shoot ();
	}

    void Shoot ()
    {
        timer = 0f;

        //gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point, typeOfGun);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
