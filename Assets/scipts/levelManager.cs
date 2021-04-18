using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    #region 
    public static levelManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public bool canControl = true;

    public GameObject allCrossHairs;
    public GameObject hud;

    public float levelTimer;


    public GameObject endScreen;
    public Text totalScoreText;
    public Text penaltiesText;
    public Text finalScoreText;
    public Text failText;
    public Text timerText;
    public Text highScoreText;

    public bool isPaused;
    
    botManager allBots;

    public GameObject pauseMenu;
    bool gamesOver;

    public GameObject inventoryMenu;
    public GameObject orderMenu;

    

    // Start is called before the first frame update
    void Start()
    {
        allBots = botManager.instance;
        levelTimer = ((float)allBots.bots.Length * allBots.botOrderTime) - 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            Time.timeScale = 0;
            
        } else 
        {
            
            Time.timeScale = 1;
        }

        if(!isPaused)
        {
            levelTimer -= Time.deltaTime;
            timerText.text = Mathf.Round(levelTimer).ToString();
            
        }

        if(levelTimer <= 0)
        {
            failText.text = "TIMES UP!";
            gameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           pauseLevel();
        }
    }

    public void activateCrosshairs()
    {
        allCrossHairs.SetActive(true);
    }

    public void deactivateCrosshairs()
    {
        allCrossHairs.SetActive(false);
    }

    public void activateHud()
    {
        hud.SetActive(true);
    }

    public void deactivateHud()
    {
        hud.SetActive(false);
    }

    public void gameOver()
    {
        int _finalScore = scoreManager.instance.playerScore;
        int _totalScore = scoreManager.instance.totalScore;
        int _penalties = _totalScore - _finalScore;
        endScreen.SetActive(true);
        finalScoreText.text = _finalScore <= 0 ? 0.ToString() : _finalScore.ToString();
        totalScoreText.text = _totalScore.ToString();
        penaltiesText.text = _penalties.ToString();
        canControl = false;
        isPaused = true;
        gamesOver = true;
        Cursor.lockState = CursorLockMode.None;
        allCrossHairs.SetActive(false);
        inventoryMenu.SetActive(false);
        orderMenu.SetActive(false);

        if(scoreManager.instance.playerHighScore < _finalScore)
        {
            PlayerPrefs.SetInt("High Score", _finalScore);
            highScoreText.text = _finalScore.ToString();
        } else 
        {
            highScoreText.text = scoreManager.instance.playerHighScore.ToString();
        }
        
        
    }

    

    public void restartLevel()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    
    public void quitLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void pauseLevel()
    {
        if(!gamesOver)
        {
            if(!isPaused)
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Cursor.lockState = CursorLockMode.None;
                
            } else 
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                
            }
        }
        
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
