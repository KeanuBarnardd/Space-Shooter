using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /// <summary>
    /// This will handle the Players movement , Shooting and active interactions between 
    /// the player, and the game world.
    /// </summary>

    [Header("Attachments")]
    [Tooltip("Attach the Players bullet to shoot here")]
    public GameObject bullet;
    public GameObject spiralMissle;
    public GameObject muzzleFlash;
    public Transform muzzlePoint;
    [Header("Shields")]
    public GameObject playerShield;
    public GameObject coreShield;
    public GameObject outsideShield;


    //Movement
    [Header("Handlers")]
    public float moveSpeed = 50f;
    private Vector2 originalPos;
    [Space]
    public float shootcd = 1f;
    private float shootRefresh;
    public float muzzleShotOffset = 50;
    [Header("Audio")]
    AudioSource audio;
    public AudioClip laserBeamSound;
    public AudioClip shieldCollisionSound;


	// Use this for initialization
	void Start () {
        //Set Variables
        shootRefresh = shootcd;
        //Get Components and References
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        //Handles the Players Movement
        PlayerControl();

        print("<color=red>Player Shield is : </color> " + gameData.sheildHealth);
        //Handles the Player Shield Controls
        PlayerShield();
    }

    public void ShootBullet()
    {
        shootcd -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (shootcd <= 0)
            {
                MuzzleFlash();

                //Create the Bullet Object
                GameObject bulletGo = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
                audio.PlayOneShot(laserBeamSound);
                //Garbage Collect
                Destroy(bulletGo, 1.23f);
                //Reset Shoot Cool down
                shootcd = shootRefresh;
            }
        }

        //Handles the Spiral Bullets
        if (Input.GetKey(KeyCode.E)) {
            if (shootcd <= 0) {
                MuzzleFlash();

                GameObject spiralMissleGO = (GameObject)Instantiate(spiralMissle, muzzlePoint.transform.position, transform.rotation);
                audio.PlayOneShot(laserBeamSound);
                shootcd = shootRefresh;

            }
        }
    }

    public void PlayerControl() {
        //Move the Player Left
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow))) {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        }
        //Move the Player Right
        if (Input.GetKey(KeyCode.D)|| (Input.GetKey(KeyCode.RightArrow))) {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        }

        //Allow the Player to shoot if controls are active
        ShootBullet();
    }

    public void PlayerShield() {
        //Handles the Smallest Shield
        if (gameData.sheildHealth <= 0)
        {
            playerShield.SetActive(false);
        }
        else {
            playerShield.SetActive(true);
        }
        //Handles the Inner Shield
        if (gameData.sheildHealth < 3)
        {
            coreShield.SetActive(false);
        }
        else {
            coreShield.SetActive(true);
        }
        //Handles the Outer Sheild
        if (gameData.sheildHealth <= 4)
        {
            outsideShield.SetActive(false);
        }
        else {
            outsideShield.SetActive(true);
        }

        if (gameData.sheildHealth <= 0) {
            RechargeShield();
        }
    }
    /// <summary>
    /// This will Handle the Ship Taking Damage from the Enemy Bullets - Will be accesed on 
    /// the Collision events on the BasicBullet Script
    /// </summary>
    public  static void takeDamage()
    {
        //Play Damage Taken Sound 
        //If the Shield is up then it can take Damage
        if (gameData.sheildHealth > 0)
        {
            gameData.sheildHealth--;
        }
        //If the Shield is down then the Player will take the Damage
        if (gameData.sheildHealth <= 0)
        {
            gameData.health--;
        }
    }

    public void RechargeShield() {
        print("<color=blue>Shields Recharging</color>");
    }

    public void MuzzleFlash() {
        //Create the Muzzle Flash Effect 
        GameObject muzzleGo = (GameObject)Instantiate(muzzleFlash, muzzlePoint.position, muzzlePoint.rotation);
        Destroy(muzzleGo, 0.03f);
    }
}
