using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    public GameObject PickUpText;
    public GameObject miniMap;


    void Start()
    {
        PickUpText.SetActive(false);
    }

    public void OnShowInventory()
    {
        if (menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false; 
            miniMap.SetActive(true);          
        }
        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
            miniMap.SetActive(false);  
        }
    }


    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false && itemSlot[i].name == name || itemSlot[i].quantity ==0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, sprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, sprite, itemDescription);
                }

                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }


    public void ShowPickUpItem()
    {
        StartCoroutine(HideText());
    }


    IEnumerator HideText()
    {
        PickUpText.SetActive(true);
        yield return new WaitForSeconds(2);
        PickUpText.SetActive(false);

    }


}
