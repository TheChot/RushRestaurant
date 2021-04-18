using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    #region 
    public static scoreManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public int playerScore;
    public int totalScore;    
    int totalPenalties;
    public float happyPoints = 10f;
    public float angryPoints = 5f;
    public int pointsToAdd = 10;
    public int penalty = 5;
    float happyness = 50f;
    public float minHappyness = 0f;
    public float maxHappyness = 100f;

    public Transform happynessBar;
    public Text scoreText;
    public Text failText;

    // public Text happyPop;
    // public Text angryPop;
    // public Text scorePop;
    // public Text penaltyPop;

    public int playerHighScore;
    


    // Start is called before the first frame update
    void Start()
    {
        happynessBar.localScale = new Vector3((happyness/maxHappyness),1,1);
        playerHighScore = PlayerPrefs.GetInt("High Score", 0);

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playerScore.ToString();
        happynessBar.localScale = new Vector3((happyness/maxHappyness),1,1);
        
        if(happyness <= 0)
        {
            failText.text = "TOO MANY CUSTOMERS WERE UNHAPPY!";
            levelManager.instance.gameOver();
        }
    }

    public void addHappyness()
    {
        happyness += happyPoints;

        if(happyness > maxHappyness)
            happyness = minHappyness;
    }

    public void deductHappyness()
    {
        happyness -= angryPoints;

        if(happyness < minHappyness)
            happyness = minHappyness;
    }

    public void addScore()
    {
        playerScore += pointsToAdd;
        totalScore += pointsToAdd;
    }

    public void deductScore()
    {
        playerScore -= penalty;
    }
}
