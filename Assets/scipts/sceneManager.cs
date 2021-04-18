using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{

    public Slider xSlider;
    public Slider ySlider;
    public Text xText;
    public Text yText;
    public GameObject settingsMenu;
    public GameObject mainMenuMenu;

    public GameObject tutorialMenu;
    public GameObject[] tutSlides;
    public int currentSlide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xText.text = xSlider.value.ToString();
        yText.text = ySlider.value.ToString();
    }

    public void startGame()
    {
        soundManager.instance.playSelect();
        SceneManager.LoadScene(1);
    }

    public void openTutorialMenu()
    {
        tutorialMenu.SetActive(true);
        mainMenuMenu.SetActive(false);
        currentSlide = 0;
        tutSlides[currentSlide].SetActive(true);
        soundManager.instance.playSelect();
    }

    public void nextSlide()
    {
        
        tutSlides[currentSlide].SetActive(false);
        currentSlide++;
        if(currentSlide > (tutSlides.Length-1))
        {
            currentSlide--;
            tutSlides[currentSlide].SetActive(true);
            soundManager.instance.playIgnore();
            return;
        }
        tutSlides[currentSlide].SetActive(true);
        soundManager.instance.playSelect();
    }

    public void previousSlide()
    {
        tutSlides[currentSlide].SetActive(false);
        currentSlide--;
        if(currentSlide < 0)
        {
            currentSlide++;
            tutSlides[currentSlide].SetActive(true);
            soundManager.instance.playIgnore();
            return;
        }
        tutSlides[currentSlide].SetActive(true);
        soundManager.instance.playSelect();
    }

    public void closeTutSlides()
    {
        tutSlides[currentSlide].SetActive(false);
        tutorialMenu.SetActive(false);
        mainMenuMenu.SetActive(true);
        soundManager.instance.playIgnore();
    }

    public void setSensitivity()
    {
        PlayerPrefs.SetFloat("X Sensitivity", xSlider.value);
        PlayerPrefs.SetFloat("Y Sensitivity", ySlider.value);
        settingsMenu.SetActive(false);
        mainMenuMenu.SetActive(true);
        soundManager.instance.playSelect();
        
    }

    public void openSettings()
    {
        xSlider.value = PlayerPrefs.GetFloat("X Sensitivity", 100f);
        ySlider.value = PlayerPrefs.GetFloat("Y Sensitivity", 50f);
        
        settingsMenu.SetActive(true);
        mainMenuMenu.SetActive(false);
        
        soundManager.instance.playSelect();
    }

    public void closeSettings()
    {
        settingsMenu.SetActive(false);
        mainMenuMenu.SetActive(true);
        soundManager.instance.playIgnore();
    }

    public void openInsta()
    {
        Application.OpenURL("https://www.instagram.com/thechot99/");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
