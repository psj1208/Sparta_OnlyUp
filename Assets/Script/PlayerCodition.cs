using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCodition : MonoBehaviour
{
    [Header("ü��")]
    public float curHp;
    public float maxHp;
    [Header("���׹̳�")]
    public float curStamina;
    public float maxStamina;

    void Start()
    {
        curHp = maxHp;
        curStamina = maxStamina;
    }

    void Update()
    {
        curHp -= Time.deltaTime;
        UIManager.Instance.Health.SetFilled(curHp / maxHp);
    }

    public void Damage(float value)
    {
        if (curHp < 0)
            return;
        curHp -= value;
        curHp = Mathf.Clamp(curHp, 0, maxHp);
    }
}
