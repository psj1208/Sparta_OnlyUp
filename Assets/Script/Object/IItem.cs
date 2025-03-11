using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IItem : MonoBehaviour
{
    public Object ItemInfo;
    
    public virtual void OnInteraction()
    {
        ItemAction();
    }

    public virtual void ItemAction()
    {

    }
}
