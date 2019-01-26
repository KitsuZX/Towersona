using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBoolOffOnExit : StateMachineBehaviour
{
    public string autoTurnedOffBool;

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool(autoTurnedOffBool, false);
    }
}
