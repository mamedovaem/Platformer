using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.buffReceiverContainer.ContainsKey(collision.gameObject))
        {
            var receiver = GameManager.Instance.buffReceiverContainer[collision.gameObject];
            receiver.AddBuff(buff);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.buffReceiverContainer.ContainsKey(collision.gameObject))
        {
            var receiver = GameManager.Instance.buffReceiverContainer[collision.gameObject];
            receiver.RemoveBuff(buff);
        }
    }
}

[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonus; 
}

public enum BuffType: int
{
    Damage, Force, Armor
}