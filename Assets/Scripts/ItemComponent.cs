using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private ItemType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Item item;
    public Item Item
    {
        get { return item; }
    }

    public bool isCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        item = GameManager.Instance.itemDataBase.GetItemOfID((int)type);
        spriteRenderer.sprite = item.Icon;
        GameManager.Instance.itemsContainer.Add(gameObject, this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy(GameObject gameObject)
    {
        MonoBehaviour.Destroy(gameObject);
    }
}

public enum ItemType
{
    DamagePotion =1, ArmorPotion = 2, ForcePotion = 3
}
