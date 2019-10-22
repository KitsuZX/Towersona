using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShitNeed))]
public class TowersonaNeeds : MonoBehaviour
{
    public enum Emotion { Fine = 0, Happy = 1, Hungry = 2, Missing = 3, Asleep = 4 }
    public Emotion currentEmotion = Emotion.Fine;

    public float HappinessLevel
    {
        get
        {
            //TODO: this
            return 1;
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

    private TowersonaStats stats;

    private LoveNeed loveNeed;
    private FoodNeed foodNeed;

    private TowersonaHODAnimation towersonaAnimation;

    #region Initialization
    private void Awake()
    {
        //Assign this with a public API instead of doing this? Seems error prone.
        Towersona towersona = GetComponentInParent<TowersonaHOD>().towersona;
        stats = towersona.stats;

        loveNeed = GetComponent<LoveNeed>();
        foodNeed = GetComponent<FoodNeed>();

        //This component will absolutetly be rewritten
        towersonaAnimation = GetComponent<TowersonaHODAnimation>();
    }

    private void Start()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        foodNeed.SetStats(stats);
        foodNeed.Reset();
        loveNeed.SetStats(stats);
        loveNeed.Reset();
    }
    #endregion


    private void Update()
    {
        if (stats == null) return;

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
			if (HappinessLevel > 1)
			{
				currentEmotion = Emotion.Happy;
			}
			else
			{
				currentEmotion = Emotion.Fine;
				towersonaAnimation.SetLoneliness(false);
			}
        }
        else
        {
			if (notifiedNeed == NeedType.Hunger) currentEmotion = Emotion.Hungry;
			else if (notifiedNeed == NeedType.Love)
			{
				currentEmotion = Emotion.Missing;
				towersonaAnimation.SetLoneliness(true);
			}
			else if (notifiedNeed == NeedType.Shit) currentEmotion = Emotion.Shit;
        }       
    }

    public enum NeedType
    {
        Hunger, 
        Love,
        None
    }
}
