using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurnOnOnStartBool : StateMachineBehaviour
{
    public string autoTurnedOnBool;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool(autoTurnedOnBool, true);
    }
}
