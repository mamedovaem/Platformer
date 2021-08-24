using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image icon;
    private  Item item;
    public Action OnCellUpdate;
    // Start is called before the first frame update
    private void Awake()
    {
        icon.sprite = null;
    }
    public void Init(Item item)
    {
        this.item = item;
        if (item == null)
            icon.sprite = null;
        else
        icon.sprite = item.Icon;

    }
    
    public void OnClickCell()
    {
        if (item == null)
            return;
        GameManager.Instance.inventory.Items.Remove(item);
        if (item.ItemBuffType == BuffType.Armor)
        {
            GameManager.Instance.player.Health.SetHealth((int)item.Value); 
        }
        else
        {
            Buff buff = new Buff
            {
                type = item.ItemBuffType,
                additiveBonus = item.Value
            };
            GameManager.Instance.inventory.buffReceiver.AddBuff(buff);
        }
        if (OnCellUpdate!= null)
        OnCellUpdate();
    }
}
