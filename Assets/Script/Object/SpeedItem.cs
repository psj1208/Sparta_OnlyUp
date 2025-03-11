using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : IItem
{
    public BuffType buffType;
    public BuffDurationType durationType;
    public float value;
    public float durationTime;
    public float durationNumber;
    public override void ItemAction()
    {
        BuffManager.Instance.AddPBuff(buffType, durationType, value, durationTime, durationNumber);
    }
}
