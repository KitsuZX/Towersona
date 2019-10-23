using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LoveNeed), typeof(FoodNeed))]
public class TowersonaNeeds : MonoBehaviour
{
    [Header("Notification")]
    [SerializeField, Range(0, 1)]
    private float notificationThreshold = 0.3f;


    public enum Emotion { Fine = 0, Hungry = 2, Missing = 3, Asleep = 4 }
    public Emotion CurrentEmotion { get; private set; }

    public float HappinessLevel
    {
        get
        {
            //TODO: this
            return 1;
        }
    }


    private TowersonaStats stats;

    public LoveNeed LoveNeed { get; private set; }  //Temporarily public until eating is correctly implemented.
    public FoodNeed FoodNeed  { get; private set; } //Temporarily public until eating is correctly implemented.

    //This will probably die.
    private TowersonaHODAnimation towersonaAnimation;


    private void Update()
    {
        UpdateEmotion();
    }

    private void UpdateEmotion()
    {
        //TODO: Check for asleep.
        if (LoveNeed.CurrentLevel < FoodNeed.CurrentLevel)
        {
            if (LoveNeed.CurrentLevel < notificationThreshold)
            {
                CurrentEmotion = Emotion.Missing;
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

    #region Initialization
    private void Awake()
    {
        //Assign this with a public API instead of doing this? Seems error prone.
        Towersona towersona = GetComponentInParent<TowersonaHOD>().towersona;
        stats = towersona.stats;

        LoveNeed = GetComponent<LoveNeed>();
        FoodNeed = GetComponent<FoodNeed>();

        //This component will absolutetly be rewritten
        towersonaAnimation = GetComponent<TowersonaHODAnimation>();
    }

    private void Start()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        FoodNeed.SetStats(stats);
        FoodNeed.Reset();
        LoveNeed.SetStats(stats);
        LoveNeed.Reset();
    }
    #endregion
}
