using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTooltipUI : MonoBehaviour
{
    public GameObject tooltipContainer;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    public void SetTooltip (ItemData item)
    {
        tooltipContainer.SetActive(true);
        itemNameText.text = item.DisplayName;
        itemDescriptionText.text = item.Description;
    }

    public void DisableTooltip () 
    {
        tooltipContainer.SetActive(false);
    }
}
