using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParkourController : MonoBehaviour
{
    Player player;
    public ObstacleChecker obsCheck;
    bool playerInAction = false;
    public Animator animator;

    [Header("Parkour Action Area")]
    public List<ParkourAni> parkourAniList;
    ObstacleInfo hitData;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        obsCheck = GetComponent<ObstacleChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        hitData = obsCheck.CheckObstacle();
        if (hitData.hitFound)
            player.pMove.CanJump = false;
        else
            player.pMove.CanJump = true;
    }

    public void Parkour(InputAction.CallbackContext context)
    {
        if (context.started && !playerInAction)
        {
            Debug.Log("hi");
            var hitData = obsCheck.CheckObstacle();
            if (hitData.hitFound)
            {
                foreach (var action in parkourAniList)
                {
                    if (action.CheckIfAvailable(hitData, transform))
                    {
                        StartCoroutine(PerformParkourAction(action));
                        break;
                    }
                }
            }
        }
    }

    IEnumerator PerformParkourAction(ParkourAni action)
    {
        player.pMove.CanMove = false;
        playerInAction = true;
        

        animator.CrossFade(action.AnimationName, 0.05f);
        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);
        if(!animationState.IsName(action.AnimationName))
        {
            Debug.Log("Animation Name is InCorrect");
        }

        float duration = animationState.length;
        float elapsedTime = 0f;
        bool CanPass = true;
        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;

            if (action.LookAtObstacle)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRot, 600f * Time.deltaTime);
            }

            if (action.AllowTargetMatching && CanPass)
            {
                CanPass = false;
                CompareTarget(action);
            }
            yield return null;
        }

        playerInAction = false;
        player.rigid.useGravity = true;
        player.pMove.CanMove = true;
    }

    void OnAnimatorMove()
    {
        if (playerInAction)
        {
            player.rigid.useGravity = false;
            Vector3 rootMotion = animator.deltaPosition;
            player.rigid.MovePosition(player.rigid.position + rootMotion);
        }
    }

    void CompareTarget(ParkourAni action)
    {
        animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(new Vector3(1, 1, 1), 0), action.CompareStartTime, action.CompareEndTime);
    }
}
