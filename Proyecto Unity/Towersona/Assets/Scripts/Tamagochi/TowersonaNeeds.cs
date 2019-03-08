using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShitNeed))]
public class TowersonaNeeds : MonoBehaviour
{
    private const int AMOUNT_OF_NEEDS = 3;

    [SerializeField][Range(1, 2)]
    private float happinessCap = 1.3f;
    [Header("Decay")]
    [SerializeField]
    private float hungerDecayPerSecond = 0.1f;
    [SerializeField]
    private float loveDecayPerSecond = 0.1f;

    [HideInInspector]    
    public Slider happinessSlider;
    [HideInInspector]
    public GameObject overHappiness;

    [Header("Notification")]
    [SerializeField][Range(0, 1)]
    private float notificationThreshold = 0.3f;

    //Need levels
    private float hungerLevel;
    private float loveLevel;
    private ShitNeed shitNeed;

    public float HappinessLevel
    {
        get
        {
            float sum = hungerLevel + loveLevel + shitNeed.Level;
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
                if (setLevel >= 1) shitNeed.CleanAllShit();
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
                hungerLevel = Mathf.Min(hungerLevel, happinessCap);
                break;
            case NeedType.Love:
                loveLevel += changeAmount;
                loveLevel = Mathf.Min(loveLevel, happinessCap);
                break;
        }
    }


    #region Need Decay
    private void Update()
    {
        DoNeedDecay();
        NeedType needToBeNotified = CheckIfShouldNotifyNeed();

        if(HappinessLevel <= 1f)
        {
            overHappiness.SetActive(false);
            happinessSlider.value = HappinessLevel;
        }
        else
        {
            overHappiness.SetActive(true);
        }      

    }

    /// <summary>
    /// Lowers need fulfilment levels.
    /// </summary>
    private void DoNeedDecay()
    {
        hungerLevel -= hungerDecayPerSecond * Time.deltaTime;
        loveLevel -= loveDecayPerSecond * Time.deltaTime;

        hungerLevel = Mathf.Max(0, hungerLevel);
        loveLevel = Mathf.Max(0, loveLevel);
    }

    /// <summary>
    /// Returns the need that should be notified, if any.
    /// </summary>
    /// <returns></returns>
    public NeedType CheckIfShouldNotifyNeed()
    {
        NeedType notifiedNeed = NeedType.None;
        float lowest = happinessCap;

        if (hungerLevel < notificationThreshold)
        {
            notifiedNeed = NeedType.Hunger;
            lowest = hungerLevel;
        }
        if (shitNeed.Level < notificationThreshold && shitNeed.Level < lowest)
        {
            notifiedNeed = NeedType.Shit;
            lowest = shitNeed.Level;
        }
        if (loveLevel < notificationThreshold && loveLevel < lowest)
        {
            notifiedNeed = NeedType.Love;
            lowest = loveLevel;
        }

        return notifiedNeed;
    }  
    #endregion

    private void Awake()
    {
        shitNeed = GetComponent<ShitNeed>();

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
