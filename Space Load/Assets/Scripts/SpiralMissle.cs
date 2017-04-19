using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMissle : MonoBehaviour {


    //Attachments   
    public GameObject deathEffect;

    //Variables
    public float MoveSpeed = 5.0f;

    public float frequency = 20.0f;

    public float magnitude = 0.5f;

    private Vector3 axis;

    private Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = transform.position;
        Destroy(this.gameObject,2f);
        //Set Random Values
        MoveSpeed = Random.Range(4.0f, 6.0f);
        frequency = Random.Range(15.0f, 20.0f);
        //magnitude = Random.Range(0.4f,1f);
        axis = transform.right;
        
	}
	
	// Update is called once per frame
	void Update () {
        pos += transform.up * Time.deltaTime * MoveSpeed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {

            GameObject deathPE = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(deathPE, 1f);
            Destroy(this.gameObject);
        }
    }
}
