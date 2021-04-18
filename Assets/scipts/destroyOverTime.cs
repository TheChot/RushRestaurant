using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOverTime : MonoBehaviour
{
    public float destroyTime;
    
    void Update()
    {
        if(levelManager.instance.isPaused)
            return;
            
        destroyTime -= Time.deltaTime;

        if(destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
