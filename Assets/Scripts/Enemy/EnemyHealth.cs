using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth  = 100;
    public int currentHealth;
    public float sinkSpeed     = 2.5f;
	public float kickBackSpeed = 60;
    public int scoreValue      = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
	
	
    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
	
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); //moves transform at sinkSpeed per second
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint, GunType gunType)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

		if (gunType == GunType.Pistol)
		{
			KickBack();
		}

        if(currentHealth <= 0)
        {
            Death ();
        }
    }

	//knocks the enemy back when shot
	void KickBack()
	{
//		transform.TransformDirection (Vector3.back * kickBackSpeed * Time.deltaTime);
		transform.Translate (Vector3.back * kickBackSpeed * Time.deltaTime);
	}


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;  //so player can move through enemy after their death

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();

		//removes total enemies from the global counter
		EnemyManager.totalEnemies--;

		//and from the stage's enemies to defeat
		if (LevelManager.totalDefeatableEnemies > 0)
		{
			LevelManager.totalDefeatableEnemies--;
		}
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true; 
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
