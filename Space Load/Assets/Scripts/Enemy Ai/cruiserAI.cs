using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cruiserAI : MonoBehaviour {

    #region Variables
    [Header("Gameplay")]
    public float moveSpeed = 30f;
    public int health = 5;
    [Space]
    public GameObject bullet;
    private float fireRate = 3f;
    [Header("Visuals")]
    public GameObject deathParticle;

    //BackEnd 
    private float fireRateRefresh;
    private int wavePointIndex = 0;
    private Transform target;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip deathSound;
    AudioSource audio;
    #endregion


    // Use this for initialization
    void Start () {

        //Get required Components
        audio = GetComponent<AudioSource>();

        //Set Variables  
        target = cruiserWaypoints.points[0];
        fireRateRefresh = fireRate;
        
	}
	
	// Update is called once per frame
	void Update () {
        #region Movement
        //Set the direction to move Towards
        Vector2 dir = target.position - transform.position;
        //Move in the direction of its direction
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        //Check if its position is less than its target position so we can call the next position
        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            getNextWayPoint();
            return;
        }
        #endregion

        //Cruiser Shooting 
        fireRate -= Time.deltaTime;
        Shoot();
    }

    public void getNextWayPoint()
    {
        if (wavePointIndex >= cruiserWaypoints.points.Length - 1)
        {
            print("They have reached the end");
            Destroy(this.gameObject);
            return;
        }
        wavePointIndex++;
        target = cruiserWaypoints.points[wavePointIndex];
    }

    public void Shoot() {
        if (fireRate <= 0) {
            StartCoroutine(ShootBullet());
            fireRate = fireRateRefresh;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health--;
            if (health <= 0) {
                Die();
            }
        }
    }

    public void Die() {
        GameObject deathPE = (GameObject)Instantiate(deathParticle, transform.position, transform.rotation);
        deathPE.GetComponent<AudioSource>().PlayOneShot(deathSound);
        Destroy(deathPE, 1f);
        gameData.score++;
        Destroy(this.gameObject);
        //Play death sound / destroy the particle after couple of seconds / destroy this game object / add score
    }

    IEnumerator ShootBullet() {
        for (int i = 0; i < 3; i++) {
            GameObject bulletGO = (GameObject)Instantiate(bullet, transform.position, transform.rotation);

            audio.PlayOneShot(shootSound);
            yield return new WaitForSeconds(0.3f);
        }
        
    }
}
