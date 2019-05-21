using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShitNeed))]
public class TowersonaNeeds : MonoBehaviour
{
    public enum Emotion { Fine = 0, Happy = 1, Hungry = 2, Missing = 3, Shit = 4 }
    public Emotion emotion = Emotion.Fine;

    public float HappinessLevel
    {
        get
        {
            float sum = hungerLevel + loveLevel + shitNeed.Level;
            return sum / AMOUNT_OF_NEEDS;
        }
    }

    [HideInInspector]
    public Slider happinessSlider;
    [HideInInspector]
    public GameObject overHappiness;

    [Header("Notification")]
    [SerializeField][Range(0, 1)]
    private float notificationThreshold = 0.3f;
    [SerializeField][Range(1, 2)]
    private float happinessCap = 1.3f;

    private float hungerDecayPerSecond = 0.05f;
    private float loveDecayPerSecond = 0.05f;
    private const int AMOUNT_OF_NEEDS = 3;  

    //Need levels
    private float hungerLevel;
    private float loveLevel;
    private ShitNeed shitNeed;
    private TowersonaStats stats;    

    private void Start()
    {
        shitNeed = GetComponent<ShitNeed>();
        stats = GetComponentInParent<TowersonaHOD>().towersona.stats;
    

        AssignStats();

        SetNeedLevel(NeedType.Hunger, 1);
        SetNeedLevel(NeedType.Shit, 1);
        SetNeedLevel(NeedType.Love, 1);
    }

    private void Update()
    {
        if (stats == null) return;
        DoNeedDecay();
        NeedType needToBeNotified = CheckIfShouldNotifyNeed();

        if (HappinessLevel <= 1f)
        {
            overHappiness.SetActive(false);
            happinessSlider.value = HappinessLevel;
        }
        else
        {
            overHappiness.SetActive(true);
        }

        ChooseEmotion();

    }

    private void AssignStats()
    {
        hungerDecayPerSecond = stats.hungerPerSecond;
        loveDecayPerSecond = stats.hapinessLossPerSecond;
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
        if (stats == null) return NeedType.None;

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
    
    public void ChooseEmotion()
    {
        NeedType notifiedNeed = CheckIfShouldNotifyNeed(); 

        if (notifiedNeed == NeedType.None) 
        {
            if (HappinessLevel > 1) emotion = Emotion.Happy;
            else emotion = Emotion.Fine;
        }
        else
        {
            if (notifiedNeed == NeedType.Hunger) emotion = Emotion.Hungry;
            else if (notifiedNeed == NeedType.Love) emotion = Emotion.Missing;
            else if (notifiedNeed == NeedType.Shit) emotion = Emotion.Shit;
        }       
    }

    public enum NeedType
    {
        Hunger, 
        Shit,
        Love,
        None
    }
}
