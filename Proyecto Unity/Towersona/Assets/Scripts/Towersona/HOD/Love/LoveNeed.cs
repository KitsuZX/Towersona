using UnityEngine;

public class LoveNeed : MonoBehaviour
{
    public const float MAX_LEVEL = 1;

    public float CurrentLevel { get; private set; }
    //Maybe do this another way. They think of it as a happiness bonus, even though it's more of a decay rate multiplier. Talk about it.
    //Where should the buff be applied from? I don't know the callstack here.
    public float decayMultiplier = 1;

    private float decayPerSecond = 0.05f;
    private float loveGainPerSecondCaressed = 0.05f;


    public void SetStats(TowersonaStats stats)
    {
        loveGainPerSecondCaressed = stats.loveGainPerSecondCaressed;
        decayPerSecond = stats.loveDecayPerSecond;
    }

    public void ResetNeed()
    {
        CurrentLevel = MAX_LEVEL;
    }


    private void ReceiveLove(float timeCaressed)
    {
        CurrentLevel = Mathf.Min(MAX_LEVEL, CurrentLevel + timeCaressed * loveGainPerSecondCaressed);
    }

    private void Update()
    {
        float globalDecayMultiplier = NeedDecayRateManager.Instance ? NeedDecayRateManager.Instance.NeedDecayRateMultiplier : 1;
        float decayThisStep = decayPerSecond * Time.deltaTime * decayMultiplier * globalDecayMultiplier;
        CurrentLevel = Mathf.Max(0, CurrentLevel - decayThisStep);
    }

    private void Start()
    {
        Caressable[] caressables = GetComponentsInChildren<Caressable>();
        for (int i = 0; i < caressables.Length; i++)
        {
            caressables[i].OnCaressed += ReceiveLove;
        }

        if (caressables.Length == 0)
        {
            Debug.LogWarning("No se ha encontrado ningún componente Caressable en el HOD. " +
                "A no ser que se haya diseñado una Towersona que necesite amor pero no pueda ser acariciada, esto es un error.", this);
        }
    }
}