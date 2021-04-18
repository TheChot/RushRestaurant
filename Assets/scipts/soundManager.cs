using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    #region 
    public static soundManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public AudioSource correctSound;
    public AudioSource wrongSound;
    public AudioSource foodReadySound;
    public AudioSource selectSound;
    public AudioSource ignoreSound;
    // public AudioSource serveSound;
    public AudioSource customerCallingSound;
    public AudioSource pickUpDropSound;
    public AudioSource themeSong;

    public bool muteSoundEffects;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCorrect()
    {
        if(muteSoundEffects)
            return;

        correctSound.Play(0);

    }

    public void playWrong()
    {
        if(muteSoundEffects)
            return;

        wrongSound.Play(0);
    }

    public void playFoodReady()
    {
        if(muteSoundEffects)
            return;
        
        foodReadySound.Play(0);
    }

    public void playSelect()
    {
        if(muteSoundEffects)
            return;

        selectSound.Play(0);
    }
    
    public void playIgnore()
    {
        if(muteSoundEffects)
            return;

        ignoreSound.Play(0);
    }

    // public void playServe()
    // {
    //     if(muteSoundEffects)
    //         return;

    //     serveSound.Play(0);
    // }

    public void playCustomerCalling()
    {
        if(muteSoundEffects)
            return;

        customerCallingSound.Play(0);
        

    }

    public void playpickDrop()
    {
        if(muteSoundEffects)
            return;

        pickUpDropSound.Play(0);        

    }

    // public void playIgnore()
    // {
    //     if(muteSoundEffects)
    //         return;

    //     ignoreSound.Play(0);        

    // }

    
}
