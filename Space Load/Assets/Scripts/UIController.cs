using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [Header("Text Object")]
    public Text scoreText;
    public Text shieldHealthText;
    public Text HealthText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Updates the Text of the UI components
        scoreText.text = gameData.score.ToString();
        shieldHealthText.text = gameData.sheildHealth.ToString();
        HealthText.text = gameData.health.ToString();

        //Tidy up of Shield
        if (gameData.sheildHealth <= 0) {
            shieldHealthText.text = "";
        }
    }
}
