using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private Text[] itemLabels;

    [SerializeField] private Text curItemLabel;
    [SerializeField] private Button equipButtons;
    [SerializeField] private Button useButton;

    private string _curItem;

    public void Refresh()
    {
        List<string> itemList = Manager.Inventory.GetItemList();

        int itemLen = itemIcons.Length;

        for(int i = 0; i<=itemLen; i++)
        {
            if (i < itemList.Count)
            {
                // set active icons
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);
                //get itemName
                string item = itemList[i];
                   //load sprite for item and set sprite to sector
                Sprite sprite = Resources.Load<Sprite>("Icons/"+item);
                itemIcons[i].sprite = sprite;
                itemIcons[i].SetNativeSize();
                //add text for ector
                int count = Manager.Inventory.GetItemCound(item);
                string message = "x" + count;
                if (item == Manager.Inventory.equippedItem)
                {
                    message = "Equiped\n" + message;
                }
                itemLabels[i].text = message;

                //program interact
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) => {
                    OnItem(item);
                });

                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear(); //clear subscription
                trigger.triggers.Add(entry);//add subcriber
            }
            else
            {
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        if (!itemList.Contains(_curItem))
        {
            _curItem = null;
        }
        if (_curItem == null)
        {
            curItemLabel.gameObject.SetActive(false);
            equipButtons.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
        else
        {
            curItemLabel.gameObject.SetActive(true);
            equipButtons.gameObject.SetActive(true);
            if (_curItem=="health")
            {
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }
            curItemLabel.text = _curItem + ":";
        }

    }

    public void OnItem(string item)
    {
        _curItem = item;
        Refresh();
    }

    public void InEquip()
    {
        Manager.Inventory.EquipeItem(_curItem);
        Refresh();
    }

    public void OnUse()
    {
        Manager.Inventory.ConsumeItems(_curItem);
        if (_curItem == "health")
        {
            Manager.Player.ChangeHealth(25);
        }
        Refresh();
    }
}
