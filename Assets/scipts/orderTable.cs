using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orderTable : MonoBehaviour
{
    #region 
    public static orderTable instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject[] foodItem;
    public Transform[] foodPositions;
    public bool[] positionBool;
    int[] foodOrdered;
    public GameObject[] foodIcons;
    public bool[] orderBools;
    public Transform[] orderPt;
    int orderNum;
    int totalOrder;

    public float orderTime;
    float orderTimeCount;
    public Transform orderBar;
    public GameObject orderMenu;
    bool isCounting;
    int orderCount;
    public int tableSpaces = 0;


    void Start()
    {
        // tableSpaces = foodPositions.Length();
        foodOrdered = new int[foodPositions.Length];

    }

    public void showOrderMenu()
    {
        orderMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        levelManager.instance.canControl = false;
        levelManager.instance.deactivateHud();

    }

    public void closeOrderMenu()
    {
        orderMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        levelManager.instance.canControl = true;
        levelManager.instance.activateHud();

    }

    void Update()
    {
        if (levelManager.instance.isPaused)
            return;

        if (orderCount > 0)
        {
            isCounting = true;
        }
        else
        {
            isCounting = false;
            orderBar.localScale = new Vector3(0, 1, 1);
            orderTimeCount = 0;
        }

        if (isCounting && orderTimeCount <= orderTime)
        {
            orderTimeCount += Time.deltaTime;
            orderBar.localScale += new Vector3((1 / orderTime) * Time.deltaTime, 0, 0);
        }

        if (orderTimeCount >= orderTime)
        {
            makeOrder();
            orderTimeCount = 0;


            // isCounting = false;
            orderBar.localScale = new Vector3(0, 1, 1);


        }
    }

    public void checkChild(int _orderNum)
    {
        // Debug.Log(orderNum);
        // for (int i = 0; i < positionBool.Length; i++)
        // {
        //     if(positionBool[i] == false)
        //     {
        //         break;
        //     }
        //     if(i >= (positionBool.Length - 1))
        //     {
        //         Debug.Log("table full");
        //         return;
        //     }
        // }
        if (tableSpaces >= foodPositions.Length || totalOrder >= foodPositions.Length)
        {
            // Debug.Log("table full");
            soundManager.instance.playIgnore();
            return;
        }
        else
        {
            tableSpaces++;
        }

        for (int i = 0; i < orderBools.Length; i++)
        {

            if (orderBools[i] == false)
            {
                // pos = i;
                soundManager.instance.playSelect();
                Instantiate(foodIcons[_orderNum], orderPt[i], false);
                orderCount++;
                totalOrder++;
                orderBools[i] = true;
                foodOrdered[i] = _orderNum;

                if (orderNum == 0)
                {
                    orderNum++;
                }
                // hasSpace = true;
                break;
            }
        }
    }

    public void makeOrder()
    {
        bool resetOrder = false;


        for (int i = 0; i < positionBool.Length; i++)
        {
            if (positionBool[i] == false)
            {
                soundManager.instance.playFoodReady();
                GameObject _foodItem = (GameObject)Instantiate(foodItem[foodOrdered[orderNum - 1]], foodPositions[i].position, foodPositions[i].rotation);
                _foodItem.GetComponent<foodController>().itemPos = i;
                positionBool[i] = true;
                break;
            }

        }

        orderCount--;
        orderBools[orderNum - 1] = false;
        Destroy(orderPt[orderNum - 1].GetChild(0).gameObject);
        // on = 1 oc = 2
        // on = 2
        if (orderNum >= totalOrder)
        {
            orderNum = 1;
            totalOrder = orderCount;
            // Debug.Log("i run");
            resetOrder = true;

        }

        if (!resetOrder)
            orderNum++;

        resetOrder = false;

    }
}
