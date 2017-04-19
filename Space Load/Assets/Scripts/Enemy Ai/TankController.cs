using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

    //Variables
    [Header("GamePlay")]
    public float moveSpeed = 2f;
    public int health = 2;
    public  int shieldHealth = 10;
    [Header("Attachments")]
    public Transform shield;

	// Use this for initialization
	void Start () {
        //Set the Components
        shield = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet") {
            //Take Damage from the Players Bullet
            health--;
            if (health <= 0) {
                Death();
                Destroy(this.gameObject);
            }   
        }
    }

    public void Death() {
        gameData.score++;
        //Create the Death Particle Here for the Tank Ship and Play the Audio Clip
    }
}
