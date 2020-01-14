using UnityEngine;
using NaughtyAttributes;

#pragma warning disable 649
[RequireComponent(typeof(TowersonaNeeds), typeof(LookAtFood))]
public class HODAnimatorParameters : MonoBehaviour
{
    [SerializeField, Required] Animator bodyAnimator;
    [SerializeField, Required] Animator faceAnimator;

    private TowersonaNeeds needs;
    private Caressable[] caressables;
    private LookAtFood lookAtFood;

    #region Cached hashes
    static int IS_ASLEEP_HASH = Animator.StringToHash("IsAsleep");
    static int IS_HUNGRY_HASH = Animator.StringToHash("IsHungry");
    static int IS_LONELY_HASH = Animator.StringToHash("IsLonely");
    static int IS_BEING_CARESSED_HASH = Animator.StringToHash("IsBeingCaressed");
    static int IS_LOOKING_AT_FOOD_HASH = Animator.StringToHash("IsLookingAtFood");
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

        //Caress parameter
        bool isBeingCaressed = false;
        for (int i = 0; i < caressables.Length && !isBeingCaressed; i++)
        {
            if (caressables[i].IsBeingCaressed) isBeingCaressed = true;
        }
        faceAnimator.SetBool(IS_BEING_CARESSED_HASH, isBeingCaressed);

        //Look at food parameter
        faceAnimator.SetBool(IS_LOOKING_AT_FOOD_HASH, lookAtFood.IsLookingAtFood);
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
        lookAtFood = GetComponent<LookAtFood>();
        caressables = GetComponentsInChildren<Caressable>();
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