using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    [SerializeField] List<Buff> mBuffList;
    public GameObject BuffParent;

    // Start is called before the first frame update
    void Start()
    {
        mBuffList = new List<Buff>();
    }

    public void AddBuff(Buff input)
    {
        input.ApplyBuff();
        mBuffList.Add(input);
    }

    public void RemoveBuff(Buff input)
    {
        mBuffList.Remove(input);
        Destroy(input);
    }
}
