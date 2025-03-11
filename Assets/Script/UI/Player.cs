using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerMovement pMove;
    public PlayerCodition pCond;
    public PlayerBuff pBuff;
    public ParkourController pParkCont;
    public Rigidbody rigid;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float rayDistance;
    [SerializeField] private GameObject curInteractItem;

    private void Awake()
    {
        GameManager.Instance.player = this;
    }
    private void Start()
    {
        pMove = GetComponent<PlayerMovement>();
        pCond = GetComponent<PlayerCodition>();
        pBuff = GetComponent<PlayerBuff>();
        pParkCont = GetComponent<ParkourController>();
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Physics.Raycast(pMove.cameraContainer.position, pMove.cameraContainer.forward, out RaycastHit hit, rayDistance, layer)) 
        {
            if (hit.collider.gameObject.TryGetComponent<IItem>(out IItem output))
            {
                UIManager.Instance.InteractTxt = true;
                UIManager.Instance.ChangeInteract(output.ItemInfo);
            }
            curInteractItem = hit.collider.gameObject;
        }
        else
        {
            UIManager.Instance.InteractTxt = false;
            curInteractItem = null;
        }
    }

    public void OnInteractContext(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (curInteractItem != null)
            {
                Debug.Log("Å‰µæ ½Ãµµ");
                curInteractItem.GetComponent<IItem>().OnInteraction();
            }
        }
    }
}
