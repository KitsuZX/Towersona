
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoveNeed), typeof(FoodNeed))]
public class TowersonaNeeds : MonoBehaviour
{
    //Sé que esto parece horrible... Pero hay varias mecánicas que afectan a todas las towersonas. Prometo no abusar de esta lista. -Aitor
    public static List<TowersonaNeeds> allExisting = new List<TowersonaNeeds>();

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

    public LoveNeed LoveNeed { get; private set; }
    public FoodNeed FoodNeed  { get; private set; }

    
    private TowersonaHODAnimation towersonaAnimation;   //This will probably die.


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
    }


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


    //Intialization
    private void Awake()
    {
        allExisting.Add(this);

        LoveNeed = GetComponent<LoveNeed>();
        FoodNeed = GetComponent<FoodNeed>();

        //This component will absolutetly be rewritten
        towersonaAnimation = GetComponent<TowersonaHODAnimation>();
    }

    private void OnDestroy()
    {
        allExisting.Remove(this);
    }

    public enum Emotion {
        Fine = 0,
        Hungry = 1,
        Missing = 2,
        Asleep = 3 }
}
