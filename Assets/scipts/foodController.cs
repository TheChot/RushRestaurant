using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodController : MonoBehaviour
{
    public int itemPos;
    orderTable table;
    playerInventory inventory;
    public GameObject foodImage;
    public bool served;
    // bool pickedU
    public Transform iconPos;
    public GameObject foodIcon;

    void Awake()
    {
        table = orderTable.instance;
        inventory = GameObject.Find("player").GetComponent<playerInventory>();
    }
    void Start()
    {

        if (!served)
        {
            GameObject _foodIcon = Instantiate(foodIcon, iconPos.position, iconPos.rotation);
            _foodIcon.transform.SetParent(iconPos);
        }
    }



    public void pickup()
    {
        table.positionBool[itemPos] = false;
        table.tableSpaces--;
        inventory.addToInventory();
        gameObject.SetActive(false);
    }
}
