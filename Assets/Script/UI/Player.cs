using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement pMove;
    public PlayerCodition pCond;
    public PlayerBuff pBuff;
    public ParkourController pParkCont;
    public Rigidbody rigid;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float rayDistance;

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
        BuffManager.Instance.AddPBuff(BuffType.speed, BuffDurationType.time, 2, 5, 5);
    }
    private void Update()
    {
        if (Physics.Raycast(pMove.cameraContainer.position, pMove.cameraContainer.forward, out RaycastHit hit, rayDistance, layer)) 
        {
            UIManager.Instance.InteractTxt = true;
        }
        else
        {
            UIManager.Instance.InteractTxt = false;
        }
    }
}
