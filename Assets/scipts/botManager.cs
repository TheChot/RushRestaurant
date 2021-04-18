using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botManager : MonoBehaviour
{
    // mock singleton
    #region 
    public static botManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject[] foodIcons;
    // Mood icons
    public GameObject callIcon;
    public GameObject happyIcon;
    public GameObject angryIcon;
    public GameObject waitIcon;
    // public GameObject 

    public float orderTime;
    float orderTimeReset;

    public botController[] bots;
    bool lookingForClient;

    public float botOrderTime = 15f;
    
    

    void Start() 
    {
        orderTimeReset = orderTime;
    }

    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
        
        if(orderTime > 0)
            orderTime -= Time.deltaTime;

        if(orderTime <= 0)
        {
            lookingForClient = true;
            int botNum = 0;

            while (lookingForClient)
            {
               int _botNum = Random.Range(0, bots.Length);

               if(!bots[_botNum].hasCalled)
               {
                   bots[_botNum].callWaiter();
                   lookingForClient = false;
                   orderTime = orderTimeReset;
                   break;
               }

               if(botNum == bots.Length)
               {
                   lookingForClient = false;
                   orderTime = orderTimeReset;
               }     

               botNum++;

            //    Debug.Log(botNum);

                // int _botNum = Random.Range(0, bots.Length);

                // for (int i = 0; i < bots.Length; i++)
                // {
                //     if(i != _botNum)
                    
                // }
                
            }
            


        }
    }
}
