using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorIKHands : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] Transform leftHandObj = null;
    [SerializeField] Transform attachLeft = null;
    [SerializeField] bool canBeUsed = false;
    [SerializeField, Range(0, 1)] float leftHandPositionWeight = 0;
    [SerializeField, Range(0, 1)] float leftHandRotationWeight = 0;

    private void OnAnimatorIK(int layerIndex)
    {
        if (canBeUsed)
        {
            if (leftHandObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotationWeight);

                if (attachLeft != null)
                {
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, attachLeft.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, attachLeft.rotation);
                }
            }
        }
    }
}
