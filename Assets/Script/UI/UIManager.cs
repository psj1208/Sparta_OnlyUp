using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Condition Health;
    [SerializeField] private TextMeshProUGUI interactTxt;
    public bool InteractTxt { set { if (interactTxt.gameObject.activeSelf == value) return; 
            interactTxt.gameObject.SetActive(value); } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InteractTxt = false;
        interactTxt.text = "";
    }

    public void ChangeInteract(Object obj)
    {
        interactTxt.text = obj.name + "\n" + obj.description;
    }
}
