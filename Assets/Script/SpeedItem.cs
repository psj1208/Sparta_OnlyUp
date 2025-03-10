using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : IItem
{
    public override void ItemAction()
    {
        StartCoroutine(Iaction());
    }

    IEnumerator Iaction()
    {
        Player p = GameManager.Instance.player;
        p.pMove.MoveSpeed += 2f;
        yield return new WaitForSeconds(5.0f);
        p.pMove.MoveSpeed -= 2f;
        yield return null;
    }
}
