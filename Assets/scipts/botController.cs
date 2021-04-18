using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botController : MonoBehaviour
{
    public float orderTime;
    public Transform orderIcon;
    public bool hasOrdered;
    GameObject foodOrderedIcon;
    public int foodOrdered;
    bool canCount;
    public Transform foodPos;
    GameObject moodIcon;
    GameObject callWaiterIcon;
    GameObject waitIcon;
    public bool hasCalled;
    public bool hasServed;

    float waitIconTime;

    float botOrderTime;
    float orderTimeReset;

    bool cancelledOrder;

    public bool canServe = true;

    // Start is called before the first frame update
    void Start()
    {
        botOrderTime = botManager.instance.botOrderTime;
        orderTimeReset = botOrderTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;   

        if(canCount)        
        {
            orderTime -= Time.deltaTime;
        }

        if(orderTime <= 0)
        {
            // foodOrderedIcon.SetActive(false);
            hasOrdered = true;
        }

        if(hasCalled && canServe && !canCount)
        {
            botOrderTime -= Time.deltaTime;

        }

        if(!canCount && botOrderTime < 0 && canServe)
        {
            Destroy(callWaiterIcon);
            canServe = false;
            moodIcon = (GameObject)Instantiate(botManager.instance.angryIcon, orderIcon.position, orderIcon.rotation);
            scoreManager.instance.deductHappyness();
            scoreManager.instance.deductScore();
            soundManager.instance.playWrong();
        }

        
    }

    public void orderFood()
    {
        if(!canCount)
        {
            Destroy(callWaiterIcon);
            foodOrdered = Random.Range(0,botManager.instance.foodIcons.Length);
            foodOrderedIcon = (GameObject)Instantiate(botManager.instance.foodIcons[foodOrdered], orderIcon.position, orderIcon.rotation);
            waitIconTime = foodOrderedIcon.GetComponent<destroyOverTime>().destroyTime;            
            canCount = true;
            StartCoroutine(spawnWaitIcon());
        }
        
    }

    IEnumerator spawnWaitIcon()
    {
        yield return new WaitForSeconds(waitIconTime + 0.2f);
        waitIcon = (GameObject)Instantiate(botManager.instance.waitIcon, orderIcon.position, orderIcon.rotation);
    }

    public void foodChoice(bool _correct)
    {
        Destroy(waitIcon);
        if(_correct)
        {
            moodIcon = (GameObject)Instantiate(botManager.instance.happyIcon, orderIcon.position, orderIcon.rotation);
        } else 
        {
            moodIcon = (GameObject)Instantiate(botManager.instance.angryIcon, orderIcon.position, orderIcon.rotation);
        }
    }

    public void callWaiter()
    {
        soundManager.instance.playCustomerCalling();
        callWaiterIcon = (GameObject)Instantiate(botManager.instance.callIcon, orderIcon.position, orderIcon.rotation);
        hasCalled = true;
        

    }
}
