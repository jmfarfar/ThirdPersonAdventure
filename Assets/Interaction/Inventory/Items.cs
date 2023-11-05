using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : MonoBehaviour
{

    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int itemIndex;
    private InventoryManager inventoryManager;
    [TextArea][SerializeField] string itemDescription;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            int  leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                ShowText();
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }


    private void ShowText()
    {
        inventoryManager.ShowPickUpItem();
    }
    


}

