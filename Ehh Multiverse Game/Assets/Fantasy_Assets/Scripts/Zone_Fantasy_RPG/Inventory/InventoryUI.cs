using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySlotUI[] uiSlots;
    public ItemTooltipUI TooltipUI;

    public void UpdateUI (ItemSlot[] items)
    {
        for(int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].SetItemSlot(items[i]);
        }
    }
}
