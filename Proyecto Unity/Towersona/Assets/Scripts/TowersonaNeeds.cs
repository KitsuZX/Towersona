using NaughtyAttributes;
using UnityEngine;

public class TowersonaNeeds : MonoBehaviour
{
    private const int AMOUNT_OF_NEEDS = 3;

    [SerializeField][Range(1, 2)]
    private float happinessCap;
    [Header("Decay")]
    [SerializeField]
    private float hungerDecayPerSecond = 0.1f;
    [SerializeField]
    private float loveDecayPerSecond = 0.1f;
    [SerializeField]
    private float shitDecayPerSecond = 0.1f;

    [Header("Notification")]
    [SerializeField][Range(0, 1)]
    private float notificationThreshold = 0.3f;

    //Need levels
    private float hungerLevel;
    private float loveLevel;
    private float shitLevel;


    public float HappinessLevel
    {
        get
        {
            float sum = hungerLevel + loveLevel + shitLevel;
            return sum / AMOUNT_OF_NEEDS;
        }
    }


    /// <summary>
    /// 1 means fulfilled, 0 means unfulfilled
    /// </summary>
    public void SetNeedLevel(NeedType needType, float setLevel)
    {
        setLevel = Mathf.Clamp(setLevel, 0, happinessCap);

        switch (needType)
        {
            case NeedType.Hunger:
                hungerLevel = setLevel;
                break;
            case NeedType.Shit:
                shitLevel = setLevel;
                break;
            case NeedType.Love:
                loveLevel = setLevel;
                break;
        }
    }

    /// <summary>
    /// Use this to increment/decrement a need.
    /// </summary>
    public void ChangeNeedLevel(NeedType needType, float changeAmount)
    {
        switch (needType)
        {
            case NeedType.Hunger:
                hungerLevel += changeAmount;
                break;
            case NeedType.Shit:
                shitLevel += changeAmount;
                break;
            case NeedType.Love:
                loveLevel += changeAmount;
                break;
        }
    }


    #region Need Decay
    private void Update()
    {
        DoNeedDecay();
        NeedType needToBeNotified = CheckIfShouldNotifyNeed();
        if (needToBeNotified != NeedType.None) NotifyNeed(needToBeNotified);
    }

    /// <summary>
    /// Lowers need fulfilment levels.
    /// </summary>
    private void DoNeedDecay()
    {
        hungerLevel -= hungerDecayPerSecond * Time.deltaTime;
        shitLevel -= shitDecayPerSecond * Time.deltaTime;
        loveLevel -= loveDecayPerSecond * Time.deltaTime;

        hungerLevel = Mathf.Max(0, hungerLevel);
        shitLevel = Mathf.Max(0, shitLevel);
        loveLevel = Mathf.Max(0, loveLevel);
    }

    /// <summary>
    /// Returns the need that should be notified, if any.
    /// </summary>
    /// <returns></returns>
    private NeedType CheckIfShouldNotifyNeed()
    {
        NeedType notifiedNeed = NeedType.None;
        float lowest = happinessCap;

        if (hungerLevel < notificationThreshold)
        {
            notifiedNeed = NeedType.Hunger;
            lowest = hungerLevel;
        }
        if (shitLevel < notificationThreshold && shitLevel < lowest)
        {
            notifiedNeed = NeedType.Shit;
            lowest = shitLevel;
        }
        if (loveLevel < notificationThreshold && loveLevel < lowest)
        {
            notifiedNeed = NeedType.Love;
            lowest = loveLevel;
        }

        return notifiedNeed;
    }

    private void NotifyNeed(NeedType needToBeNotified)
    {
        //TODO: bring up a graphic in 2d representation
        string tmpMessage = "";

        switch (needToBeNotified)
        {
            case NeedType.Hunger:
                tmpMessage = "I'm hungry!";
                break;
            case NeedType.Shit:
                tmpMessage = "I may have just done a big shit.";
                break;
            case NeedType.Love:
                tmpMessage = "Love me, human.";
                break;
        }

        Debug.Log(tmpMessage);
    }
    #endregion

    private void Awake()
    {
        SetNeedLevel(NeedType.Hunger, 1);
        SetNeedLevel(NeedType.Shit, 1);
        SetNeedLevel(NeedType.Love, 1);
    }

    public enum NeedType
    {
        Hunger, 
        Shit,
        Love,
        None
    }

    
}
