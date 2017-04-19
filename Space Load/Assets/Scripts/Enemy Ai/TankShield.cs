using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShield : MonoBehaviour {

    /// <summary>
    /// Just a quick script to handle the Tanks Shield
    /// </summary>
    /// 
    [Header("Gameplay")]
    public int shield;
    public AudioClip shieldHitSound;
    AudioSource audio;
    [Header("Visuals")]
    public GameObject enemyShieldCollisionEffect;

    // Use this for initialization
    void Start () {
        //Set Components
        shield = this.gameObject.GetComponentInParent<TankController>().shieldHealth;
        audio = GetComponentInParent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// This handles the Collision between the bullet objects or ant objects in general.
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Shield Collision with 
        if (collision.gameObject.tag == "PlayerBullet") {
            GameObject collisionPE = (GameObject)Instantiate(enemyShieldCollisionEffect,collision.gameObject.transform.position,collision.gameObject.transform.rotation);
            Destroy(collisionPE,1f); 
            shield--;
            audio.PlayOneShot(shieldHitSound);
            if (shield <= 0) {
                this.gameObject.SetActive(false);
             
            }
        }
    }
}
