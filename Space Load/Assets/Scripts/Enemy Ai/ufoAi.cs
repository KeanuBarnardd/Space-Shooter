using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufoAi : MonoBehaviour {

    /// <summary>
    /// MOVEMENT----
    /// 1) We need to Allow the UFO to access the WayPoints from the UFO Waypoints Object
    /// 2) Allow the UFO Object to locate the next Target Waypoint from its current position
    /// 3) Move Towards that Waypoint - and repeat until there are no more way points
    /// 
    /// SHOOTING----
    /// 1)We need to all the UFO to get bullets, create them at its position
    /// 2)Bullets need to move down
    /// </summary>

    //Variables 

    [Header("Movement - Shooting")]
    public float moveSpeed = 30f;
    [Space]
    public GameObject bullet;
    public float fireRate = 3f;
    [Header("GamePlay")]
    public int health = 2;
    [Header("Visuals")]
    public GameObject deathParticle;
    //Audio
    [Header("Audio")]
    public AudioClip laserShootSound;
    public AudioClip deathExplosion;
    AudioSource audio;

    //Back End
    private float fireRateRefresh;
    private int wavePointIndex = 0;
    private Transform target;

    // Use this for initialization
    void Start () {

        //Set Variables
        target = ufoWayPoints.points[0];
        fireRateRefresh = fireRate;

        //Set Components
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        #region Movement
        //Set the direction to move Towards
        Vector2 dir = target.position - transform.position;
        //Move in the direction of its direction
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        //Check if its position is less than its target position so we can call the next position
        if (Vector2.Distance(transform.position, target.position) <= 0.2f) {
            getNextWayPoint();
            return;
        }
        #endregion

        //UFO Shooting 
        fireRate -= Time.deltaTime;
        Shoot();     
    }

    /// <summary>
    /// Gets the Next available way Point
    /// </summary>
    public void getNextWayPoint() {
        if (wavePointIndex >= ufoWayPoints.points.Length - 1) {
            print("They have reached the end");
            Destroy(this.gameObject);
            return;
        }
        wavePointIndex++;
        target = ufoWayPoints.points[wavePointIndex];
    }

    public void Shoot() {
        if (fireRate <= 0)
        {
            GameObject bulletGo = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
            audio.PlayOneShot(laserShootSound);
            Destroy(bulletGo,3f);
            fireRate = Random.Range(1,fireRateRefresh);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            //Take Damage from the Players Bullets
            health--;
            if (health <= 0) {
                Death();
                Destroy(this.gameObject);
            }
        }
    }

    public void Death() {
        gameData.score++;
        //Create and then Destroy Death Particle Here
        GameObject deathParticleGo = (GameObject)Instantiate(deathParticle, transform.position, transform.rotation);
        deathParticleGo.GetComponent<AudioSource>().PlayOneShot(deathExplosion);
        Destroy(deathParticleGo, 2f);
    }
}
