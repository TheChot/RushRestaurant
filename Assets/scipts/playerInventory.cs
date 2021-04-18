using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public Transform camPosition;
    public float raycastDistance = 10f;

    public Transform[] inventorySlots;
    public bool[] positionBool;
    GameObject pickupImage;
    int inventorySpace;
    public GameObject closeInvBtn;
    Transform npcPlate;

    public GameObject[] foods;
    botController bC;

    // Crosshairs 
    public GameObject normalCrosshair;
    public GameObject orderCrosshair;
    public GameObject takeOrderCrosshair;
    public GameObject serveClientCrosshair;
    public GameObject pickFoodCrosshair;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (levelManager.instance.isPaused)
            return;

        checkLayer();

    }

    void checkLayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(camPosition.position, camPosition.TransformDirection(Vector3.forward), out hit, raycastDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("order table"))
            {
                switchCrossHairs(1);

                if (Input.GetMouseButtonDown(0))
                {
                    hit.transform.gameObject.GetComponent<orderTable>().showOrderMenu();

                }

            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("food"))
            {
                foodController _foodCon = hit.transform.gameObject.GetComponent<foodController>();
                if (!_foodCon.served)
                {
                    switchCrossHairs(2);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (inventorySpace < inventorySlots.Length)
                        {
                            pickupImage = (GameObject)hit.transform.gameObject.GetComponent<foodController>().foodImage;
                            hit.transform.gameObject.GetComponent<foodController>().pickup();
                            inventorySpace++;
                        }
                        else
                        {
                            soundManager.instance.playIgnore();
                            // Debug.Log("nventory full");
                        }
                        // Assign position number to pickup image here
                        // So that inventory space can be cleared when used
                    }
                }

            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("customer"))
            {
                bC = hit.transform.gameObject.GetComponent<botController>();

                if (bC.canServe)
                {
                    if (!bC.hasOrdered && bC.hasCalled)
                    {
                        switchCrossHairs(3);
                        if (Input.GetMouseButtonDown(0))
                        {
                            bC.orderFood();
                        }
                    }
                    else if (!bC.hasServed && bC.hasOrdered)
                    {
                        switchCrossHairs(4);
                        if (Input.GetMouseButtonDown(0))
                        {
                            openInventory();
                            npcPlate = bC.foodPos;
                        }
                    }
                }

            }
            else
            {
                // writee a switch case function
                // that disables the inactive crosshairs
                switchCrossHairs(0);
            }
            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

        }
        else
        {
            switchCrossHairs(0);
        }
    }

    public void addToInventory()
    {
        // int pos = 0;
        bool hasSpace = false;

        for (int i = 0; i < positionBool.Length; i++)
        {
            if (positionBool[i] == false)
            {
                // pos = i;
                anim.SetTrigger("interact");
                soundManager.instance.playpickDrop();
                Instantiate(pickupImage, inventorySlots[i], false);
                positionBool[i] = true;
                hasSpace = true;
                break;
            }
        }

        if (!hasSpace)
        {

            Debug.Log("Inventory is Full");
        }
    }


    public void feedNpc(int _inventoryNum)
    {
        if (inventorySlots[_inventoryNum].childCount == 0)
        {
            soundManager.instance.playIgnore();
            return;
        }
        soundManager.instance.playpickDrop();
        anim.SetTrigger("interact");
        positionBool[_inventoryNum] = false;
        int _foodNo = inventorySlots[_inventoryNum].GetChild(0).gameObject.GetComponent<foodIcon>().foodNo;
        GameObject _foodItem = Instantiate(foods[_foodNo], npcPlate.position, npcPlate.rotation);
        _foodItem.GetComponent<foodController>().served = true;
        inventorySpace--;
        bC.hasServed = true;
        if (_foodNo == bC.foodOrdered)
        {
            // Debug.Log("Right Food");
            bC.foodChoice(true);
            scoreManager.instance.addHappyness();
            scoreManager.instance.addScore();
            soundManager.instance.playCorrect();
        }
        else
        {

            // Debug.Log("Wrong Food");
            bC.foodChoice(false);
            scoreManager.instance.deductHappyness();
            scoreManager.instance.deductScore();
            soundManager.instance.playWrong();
        }
        Destroy(inventorySlots[_inventoryNum].GetChild(0).gameObject);
    }

    void openInventory()
    {
        // orderMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        levelManager.instance.canControl = false;
        levelManager.instance.deactivateCrosshairs();
        closeInvBtn.SetActive(true);
        soundManager.instance.playSelect();

    }

    public void closeInventory()
    {
        // orderMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        levelManager.instance.canControl = true;
        levelManager.instance.activateCrosshairs();
        closeInvBtn.SetActive(false);
        soundManager.instance.playIgnore();

    }

    void switchCrossHairs(int _id)
    {
        switch (_id)
        {
            case 0:
                normalCrosshair.SetActive(true);
                orderCrosshair.SetActive(false);
                takeOrderCrosshair.SetActive(false);
                serveClientCrosshair.SetActive(false);
                pickFoodCrosshair.SetActive(false);
                break;
            case 1:
                normalCrosshair.SetActive(false);
                orderCrosshair.SetActive(true);
                takeOrderCrosshair.SetActive(false);
                serveClientCrosshair.SetActive(false);
                pickFoodCrosshair.SetActive(false);
                break;
            case 2:
                normalCrosshair.SetActive(false);
                orderCrosshair.SetActive(false);
                takeOrderCrosshair.SetActive(false);
                serveClientCrosshair.SetActive(false);
                pickFoodCrosshair.SetActive(true);
                break;
            case 3:
                normalCrosshair.SetActive(false);
                orderCrosshair.SetActive(false);
                takeOrderCrosshair.SetActive(true);
                serveClientCrosshair.SetActive(false);
                pickFoodCrosshair.SetActive(false);
                break;
            case 4:
                normalCrosshair.SetActive(false);
                orderCrosshair.SetActive(false);
                takeOrderCrosshair.SetActive(false);
                serveClientCrosshair.SetActive(true);
                pickFoodCrosshair.SetActive(false);
                break;
            default:
                normalCrosshair.SetActive(true);
                orderCrosshair.SetActive(false);
                takeOrderCrosshair.SetActive(false);
                serveClientCrosshair.SetActive(false);
                pickFoodCrosshair.SetActive(false);
                break;
        }

    }
}
