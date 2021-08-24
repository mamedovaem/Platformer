using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffReceiver : MonoBehaviour
{
    private List<Buff> buffs;
    public List<Buff> Buffs
    {
        get { return buffs; }
    }
    public Action OnBuffsChanged;
    [SerializeField] int buffLifeTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.buffReceiverContainer.Add(gameObject, this);
        buffs = new List<Buff>();
    }

    public void AddBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
        {
            buffs.Add(buff);
            StartCoroutine(BuffDuration(buff));
            
        }
        if (OnBuffsChanged != null)
            OnBuffsChanged();
    }

    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
            buffs.Remove(buff);

        if (OnBuffsChanged != null)
            OnBuffsChanged();
    }

    IEnumerator BuffDuration(Buff buff)
    {
        yield return new WaitForSeconds(buffLifeTime);
        
        if (buff!= null)
            RemoveBuff(buff);
        yield break;
    }
}
