using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance;
    [SerializeField] GameObject BuffPrefab;
    private void Awake()
    {
        Instance = this;
    }

    public void AddPBuff(BuffType bType, BuffDurationType bDurType, float val, float dur, float durNum)
    {
        PlayerBuff playerBuf= GameManager.Instance.player.pBuff;
        GameObject obj = Instantiate(BuffPrefab, GameManager.Instance.player.pBuff.BuffParent.transform);
        Buff buf = obj.GetComponent<Buff>();
        buf.Init(bType, bDurType, val, dur, durNum);
        playerBuf.AddBuff(buf);
    }
}
