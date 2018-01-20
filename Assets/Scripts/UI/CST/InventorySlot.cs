using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts.Entity.Items;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Item ItemRepresenting;
    public InventoryScreen Screen;
    public Image ItemImage;
    public int id;
    [HideInInspector]
    public Vector3 firstPostion;
    private Vector3 initialMousePosition;
    [HideInInspector]
    public bool CheckForDrag = false;

    public void Start()
    {
        firstPostion = new Vector3(transform.position.x, transform.position.y, -5);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.white;
        Screen.DraggingOver = this;
        if (Screen.Popup != null && !Screen.Dragging)
        {
            Destroy(Screen.Popup.gameObject);
            Screen.Popup = null;
        }
        if (ItemRepresenting && !Screen.Dragging)
        {
            GameObject go = Resources.Load("Items/UI/UIHoverPopup") as GameObject;
            ItemHoverPopup pop = (Instantiate(go) as GameObject).GetComponent<ItemHoverPopup>();
            pop.transform.SetParent(Screen.transform);
            pop.PopupText.text = ItemRepresenting.UIName;
            pop.PopupText.color = ItemRepresenting.GetTextColor();

            Screen.Popup = pop;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = new Color(.94f, .94f, .94f, 1);
        Screen.DraggingOver = null;
        if (Screen.Popup != null && !Screen.Dragging)
        {
            Destroy(Screen.Popup.gameObject);
            Screen.Popup = null;
        }
        if(Screen.Inspector.gameObject.activeSelf)
        {
            Screen.Inspector.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CheckForDrag = true;
        initialMousePosition = Input.mousePosition;
        Screen.CurrentlyDragging = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Screen.Dragging = false;
        ItemImage.transform.SetParent(transform);
        Screen.CurrentlyDragging = null;

        if (Screen.DraggingOver)
        {
            int pos = Screen.DraggingOver.id;

            if(pos == (byte)InventorySlots.Weapon)
            {
                if (ItemRepresenting.EquipType == ItemEquippableTypes.OneHanded)
                {
                    if(Screen.Slots[(byte)InventorySlots.Offhand].ItemRepresenting == null)
                        changeItem(pos);
                    else
                    {
                        //there is an offhand item!
                        Screen.Inventory.AddItemIfAble(Screen.Slots[(byte)InventorySlots.Offhand].ItemRepresenting);
                        Screen.Slots[(byte)InventorySlots.Offhand].ItemRepresenting = null;
                        Screen.Slots[(byte)InventorySlots.Offhand].ItemImage.sprite = null;
                        changeItem(pos);
                    }
                }                   
                else resetItem();
            }
            else if (pos == (byte)InventorySlots.Offhand)
            {
                if (ItemRepresenting.EquipType == ItemEquippableTypes.Offhand || ItemRepresenting.EquipType == ItemEquippableTypes.Spellbook)
                    changeItem(pos);
                else resetItem();
            }
            else if (pos == (byte)InventorySlots.Ranged)
            {
                if (ItemRepresenting.EquipType == ItemEquippableTypes.Ranged)
                    changeItem(pos);
                else resetItem();
            }
            else if (pos == (byte)InventorySlots.Head)
            {
                if (ItemRepresenting.EquipType == ItemEquippableTypes.Hat)
                    changeItem(pos);
                else resetItem();
            }
            else if (pos == (byte)InventorySlots.Chest)
            {
                if (ItemRepresenting.EquipType == ItemEquippableTypes.Chest)
                    changeItem(pos);
                else resetItem();
            }
            else if (pos == (byte)InventorySlots.Pants)
            {
                if (ItemRepresenting.EquipType == ItemEquippableTypes.Pants)
                    changeItem(pos);
                else resetItem();
            }
            else if (pos == (byte)InventorySlots.Shoes)
            {
                if (ItemRepresenting.EquipType == ItemEquippableTypes.Shoes)
                    changeItem(pos);
                else resetItem();
            }
            else
            {
                changeItem(pos);
            }
        }
        else
        {
            resetItem();
        }
    }

    private void changeItem(int pos)
    {
        if (Screen.DraggingOver.ItemRepresenting && ItemRepresenting)
        {
            Screen.DraggingOver.ItemRepresenting.InventoryPosition = ItemRepresenting.InventoryPosition;
        }
        Screen.DraggingOver.ItemImage.transform.position = Screen.DraggingOver.firstPostion;
        ItemImage.transform.position = firstPostion;
        if (ItemRepresenting)
        {
            ItemRepresenting.InventoryPosition = pos;
        }
        Screen.SetInventoryVisually(Screen.Inventory);
    }

    private void resetItem()
    {
        ItemImage.transform.position = firstPostion;
        Screen.SetInventoryVisually(Screen.Inventory);
    }

    public void Update()
    {
        if (Screen.Dragging && Screen.CurrentlyDragging == this)
        {
            ItemImage.transform.position = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && CheckForDrag)
        {
            if (Input.mousePosition != initialMousePosition)
            {
                if (ItemRepresenting)
                {
                    Screen.Dragging = true;
                    ItemImage.transform.SetParent(Screen.transform.parent);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CheckForDrag = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Screen.Inspector.gameObject.activeSelf)
        {
            Screen.Inspector.gameObject.SetActive(true);
        }

        Screen.Inspector.TitleText.text = ItemRepresenting.UIName;
        Screen.Inspector.TitleText.color = ItemRepresenting.GetTextColor();
        Screen.Inspector.LevelText.text = "Level " + ItemRepresenting.Stats.ItemLevel + " " + ItemRepresenting.ImpericalName;
        Screen.Inspector.RarityText.text = ItemRepresenting.Rarity.ToString();
        Screen.Inspector.TypeText.text = ItemPSTables.GetDescription(ItemRepresenting.EquipType);

        if (ItemRepresenting.EquipType == ItemEquippableTypes.OneHanded)
        {
            if (ItemRepresenting.Stats.Damage > ItemRepresenting.Stats.BaseDamage)
            {
                Screen.Inspector.StatAText.text = "Damage: " + ItemRepresenting.Stats.Damage + " (" + ItemRepresenting.Stats.BaseDamage + "<color=lime> + " + Round(ItemRepresenting.Stats.Damage - ItemRepresenting.Stats.BaseDamage).ToString() + "</color>)"; 
            }
            else if (ItemRepresenting.Stats.Damage < ItemRepresenting.Stats.BaseDamage)
            {
                Screen.Inspector.StatAText.text = "Damage: " + ItemRepresenting.Stats.Damage + " (" + ItemRepresenting.Stats.BaseDamage + "<color=red> - " + Round(ItemRepresenting.Stats.BaseDamage - ItemRepresenting.Stats.Damage).ToString() + "</color>)";
            }
            else
            {
                Screen.Inspector.StatAText.text = "Damage: " + ItemRepresenting.Stats.Damage;
            }
            if (ItemRepresenting.Stats.AttackSpeed > ItemRepresenting.Stats.BaseAttackSpeed)
            {
                Screen.Inspector.StatBText.text = "Speed: " + ItemRepresenting.Stats.AttackSpeed + " (" + ItemRepresenting.Stats.BaseAttackSpeed + "<color=lime> + " + Round(ItemRepresenting.Stats.AttackSpeed - ItemRepresenting.Stats.BaseAttackSpeed).ToString() + "</color>) per second";
            }
            else if (ItemRepresenting.Stats.AttackSpeed < ItemRepresenting.Stats.BaseAttackSpeed)
            {
                Screen.Inspector.StatBText.text = "Speed: " + ItemRepresenting.Stats.AttackSpeed + " (" + ItemRepresenting.Stats.BaseAttackSpeed + "<color=red> - " + Round(ItemRepresenting.Stats.BaseAttackSpeed - ItemRepresenting.Stats.AttackSpeed).ToString() + "</color>) per second";
            }
            else
            {
                Screen.Inspector.StatBText.text = "Speed: " + ItemRepresenting.Stats.AttackSpeed + " per second";
            }
        }
    }
    private float Round(float f)
    {
        return Mathf.Round(f * 100) / 100;
    }
}
