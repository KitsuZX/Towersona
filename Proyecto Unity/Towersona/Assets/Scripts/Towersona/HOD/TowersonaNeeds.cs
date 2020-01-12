using UnityEngine;

[RequireComponent(typeof(LoveNeed), typeof(FoodNeed), typeof(Sleeper))]
public class TowersonaNeeds : MonoBehaviour
{
    //Inspector
    [Header("Notification")]
    [SerializeField, Range(0, 1)]
    private float notificationThreshold = 0.3f;

    //Public properties
    public Emotion CurrentEmotion { get; private set; }
    public float HappinessLevel
    {
        get
        {
            //TODO: this
            return 1;
        }
    }

    public Sleeper Sleeper { get; private set; }
    public LoveNeed LoveNeed { get; private set; }
    public FoodNeed FoodNeed  { get; private set; }

    //Stat API
    public void SetStats(TowersonaStats stats)
    {
        LoveNeed.SetStats(stats);
        FoodNeed.SetStats(stats);
    }

    public void ResetNeeds()
    {
        LoveNeed.ResetNeed();
        FoodNeed.ResetNeed();
        Sleeper.WakeUp();
    }


    private void Update()
    {
        UpdateEmotion();
    }

    private void UpdateEmotion()
    {
        if (Sleeper.IsAsleep)
        {
            CurrentEmotion = Emotion.Asleep;
        }
        else if (LoveNeed.CurrentLevel < FoodNeed.CurrentLevel)
        {
            if (LoveNeed.CurrentLevel < notificationThreshold)
            {
                CurrentEmotion = Emotion.Lonely;
            }
        }
        else if (FoodNeed.CurrentLevel < notificationThreshold)
        {
            CurrentEmotion = Emotion.Hungry;
        }
        else
        {
            CurrentEmotion = Emotion.Fine;
        }
    }


    //Intialization
    private void Awake()
    {
        GlobalTowersonaNeedProvider.Register(this);

        LoveNeed = GetComponent<LoveNeed>();
        FoodNeed = GetComponent<FoodNeed>();
        Sleeper = GetComponent<Sleeper>();
    }

    private void OnDestroy()
    {
        GlobalTowersonaNeedProvider.Unregister(this);
    }


    public enum Emotion
    {
        Fine = 0,
        Hungry = 1,
        Lonely = 2,
        Asleep = 3
    }
}
