using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{

    #region Variables
    //Variables
    public float moveSpeed = 100f;
    public bool playerBullet;
    [Header("Enemy Bullet Type")]
    public bool cruiser;
    public bool ufo;

    [Header("Player Bullet Effects")]
    public GameObject bulletDestroyEffect;

    [Header("Enemy Bullet Effects")]
    public GameObject greenBulletEffect;
    public GameObject redBulletEffect;

    [Header("Audio")]
    public AudioClip shieldHitSound;
    AudioSource audio;
    #endregion

    // Use this for initialization
    void Start()
    {
        //Apply the Components
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Handles the Bullet Movement
        if (playerBullet == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }

        //Garbage Collection 
        Destroy(this.gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player bullets Colliding with the Enemy
        if (collision.gameObject.tag == "Enemy")
        {
            if (playerBullet == true)
            {
                //Create Death Particle Effect
                GameObject deathPE = Instantiate(bulletDestroyEffect, transform.position, transform.rotation);
                Destroy(deathPE, 1f);
                Destroy(this.gameObject);
            }
        }
        //Bullets Colliding with the Player
        if (collision.gameObject.tag == "Player")
        {
            if (playerBullet == false)
            {
                //Take away health from the Player if the Player- then they die 
                gameData.health--;
                Destroy(this.gameObject);
            }
        }
        //Handles the Collision with the Bullets and the Shield
        if (collision.gameObject.tag == "PlayerShield")
        {
            if (playerBullet == false)
            {
                //UFO Bullet
                if (ufo == true && cruiser == false)
                {
                    GameObject ufodeathPE = Instantiate(redBulletEffect, transform.position, transform.rotation);
                    Destroy(ufodeathPE, 1f);
                    PlayerController.takeDamage();
                    FindObjectOfType<PlayerController>().GetComponent<AudioSource>().PlayOneShot(shieldHitSound);
                    Destroy(this.gameObject);
                }
                //Cruiser Bullet
                if (cruiser == true && ufo == false)
                {
                    GameObject cruiserPE = Instantiate(greenBulletEffect, transform.position, transform.rotation);
                    PlayerController.takeDamage();
                    FindObjectOfType<PlayerController>().GetComponent<AudioSource>().PlayOneShot(shieldHitSound);
                    Destroy(cruiserPE, 1f);
                    Destroy(this.gameObject);
                }

            }
        }
    }
}
