using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDeclaration;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
