using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour Menu/Create New Parkour Ani")]
public class ParkourAni : ScriptableObject
{
    [SerializeField] string animationName;
    [SerializeField] float minimum;
    [SerializeField] float maximum;

    [Header("Rotating Player towards")]
    [SerializeField] bool lookAtObstacle;

    public Quaternion RequiredRot { get; set; }

    [Header("Target Matching")]
    [SerializeField] bool allowTargetMatching = true;
    [SerializeField] AvatarTarget compareBodyPart;
    [SerializeField] float compareStartTime;
    [SerializeField] float compareEndTime;

    public Vector3 ComparePosition { get; set; }
    public bool CheckIfAvailable(ObstacleInfo hitInfo, Transform player)
    {
        float checkHeight = hitInfo.hitInfo.point.y - player.transform.position.y;

        if (checkHeight < minimum || checkHeight > maximum)
        {
            return false;
        }
        if(lookAtObstacle)
        {
            RequiredRot = Quaternion.LookRotation(-hitInfo.hitInfo.normal);
        }
        if(allowTargetMatching)
        {
            ComparePosition = hitInfo.heightInfo.point;
        }
        return true;
    }

    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;
    public bool AllowTargetMatching => allowTargetMatching;
    public AvatarTarget CompareBodyPart => compareBodyPart;
    public float CompareStartTime => compareStartTime;
    public float CompareEndTime => compareEndTime;
}
