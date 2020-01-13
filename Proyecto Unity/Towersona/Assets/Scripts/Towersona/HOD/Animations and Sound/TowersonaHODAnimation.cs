using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

#pragma warning disable 649
[RequireComponent(typeof(TowersonaNeeds))]
public class TowersonaHODAnimation : MonoBehaviour
{
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator faceAnimator;

    private TowersonaNeeds needs;

    #region Cached hashes
    static int IS_ASLEEP_HASH = Animator.StringToHash("IsAsleep");
    static int IS_HUNGRY_HASH = Animator.StringToHash("IsHungry");
    static int IS_LONELY_HASH = Animator.StringToHash("IsLonely");
    static int CELEBRATE_HASH = Animator.StringToHash("Celebrate");
    static int EAT_HASH = Animator.StringToHash("Eat");
    #endregion

    private void Update()
    {
        //Update need parameters
        bool isHungry = needs.IsHungry;
        bool isLonely = needs.IsLonely;
        bool isAsleep = needs.Sleeper.IsAsleep;

        //El AnimatorController no lidia bien con estar hambriento y triste al mismo tiempo
        if (isHungry && isLonely)
        {
            TowersonaNeeds.Emotion notifiedEmotion = needs.CurrentEmotion;
            if (notifiedEmotion == TowersonaNeeds.Emotion.Hungry) isLonely = false;
            else if (notifiedEmotion == TowersonaNeeds.Emotion.Lonely) isHungry = false;
        }

        bodyAnimator.SetBool(IS_HUNGRY_HASH, isHungry);
        faceAnimator.SetBool(IS_HUNGRY_HASH, isHungry);
        
        bodyAnimator.SetBool(IS_LONELY_HASH, isLonely);
        faceAnimator.SetBool(IS_LONELY_HASH, isLonely);
        
        bodyAnimator.SetBool(IS_ASLEEP_HASH, isAsleep);
        faceAnimator.SetBool(IS_ASLEEP_HASH, isAsleep);
    }

    private void Celebrate()
    {
        bodyAnimator.SetTrigger(CELEBRATE_HASH);
    }

    private void Eat()
    {
        faceAnimator.SetTrigger(EAT_HASH);
    }


    private void Awake()
    {
        needs = GetComponent<TowersonaNeeds>();
    }

    private void Start()
    {
        if (GameManager.Instance)
        {
            //Se suscribe así para evitar estar suscrito dos veces.
            GameManager.Instance.OnWonGame -= Celebrate;
            GameManager.Instance.OnWonGame += Celebrate;
        }

        foreach (Feedable feedable in GetComponentsInChildren<Feedable>())
        {
            feedable.OnFed += (Food food) => Eat();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance) GameManager.Instance.OnWonGame -= Celebrate;
    }
}