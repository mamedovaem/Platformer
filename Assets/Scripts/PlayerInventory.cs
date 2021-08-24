using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    List<Item> items;
    public List<Item> Items
    {
        get { return items; }
    }
    public BuffReceiver buffReceiver;
    [SerializeField] private Text coinsText;

    
    public Text CoinsText
    {
         get { return coinsText; }

        set
        {
            coinsText = value;
        }
    }
    [SerializeField] private int moneyAmount;
    public int MoneyAmount
    {
        get { return moneyAmount; }
        set
        {
            if (value >= 0)
                moneyAmount = value;
        }
    }
    [SerializeField] private int coinCost = 5;
    public int CoinCost
    {
        get { return coinCost; }
        set { if (value > 0)
                coinCost = value;
        }
    }



    private void Awake()
    {
        Instance = this;
        
    }
    #region Singleton
    public static PlayerInventory Instance { get; set; }
    #endregion

    private void Start()
    {
        GameManager.Instance.inventory = this;
        coinsText.text = "0";
        items = new List<Item>();
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(collision.gameObject))

        {
            var coin = GameManager.Instance.coinContainer[collision.gameObject];
            if (coin.isCollected)
                return;
            coin.isCollected = true;
            PlayerInventory.Instance.MoneyAmount += PlayerInventory.Instance.CoinCost;
            PlayerInventory.Instance.CoinsText.text = PlayerInventory.Instance.MoneyAmount.ToString();
            
            coin.StartDestroy();
            Debug.Log("Amount of money: " + PlayerInventory.Instance.MoneyAmount);
        }

        if (GameManager.Instance.itemsContainer.ContainsKey(collision.gameObject))
        {
            var itemComponent = GameManager.Instance.itemsContainer[collision.gameObject];
            if (itemComponent.isCollected)
                return;
            itemComponent.isCollected = true;
            items.Add(itemComponent.Item);
            itemComponent.Destroy(collision.gameObject);

        }
    }
}
