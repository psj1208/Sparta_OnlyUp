using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [SerializeField] private Image filled;

    public void SetFilled(float value)
    {
        filled.fillAmount = Mathf.Clamp01(value);
    }
}
