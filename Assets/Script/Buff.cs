using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDeclaration;

public class Buff : MonoBehaviour
{
    public BuffType buffType;
    public BuffDurationType durationType;
    public float value;
    public float durationTime;
    public float durationNumber;
    Coroutine curCoroutine;

    public void Init(BuffType bType,BuffDurationType bDurType, float val, float dur, float durNum)
    {
        buffType = bType;
        durationType = bDurType;
        value = val;
        durationTime = dur;
        durationNumber = durNum;
    }
    public void ApplyBuff()
    {
        curCoroutine = StartCoroutine(buffApply());
    }

    public void DestroyBuff()
    {
        if (durationType == BuffDurationType.time)
        {
            Player p = GameManager.Instance.player;
            StopCoroutine(curCoroutine);
            p.pMove.MoveSpeed -= value;
            p.pBuff.RemoveBuff(this);
        }
        Destroy(gameObject);
    }

    IEnumerator buffApply()
    {
        if(durationType == BuffDurationType.time)
        {
            if(buffType == BuffType.speed)
            {
                Player p = GameManager.Instance.player;
                p.pMove.PlusSpeed(value);
                yield return new WaitForSeconds(durationTime);
                DestroyBuff();
            }
        }
        yield return null;
    }
}