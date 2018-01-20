using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts.Entity.Items;

public class InventorySlotEquippable : InventorySlot, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public new void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);


    }
}
