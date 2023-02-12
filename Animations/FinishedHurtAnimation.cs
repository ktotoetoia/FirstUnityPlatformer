using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedHurtAnimation : StateMachineBehaviour
{
    string isHurted = "IsHurted";
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isHurted, false);
    }
}
