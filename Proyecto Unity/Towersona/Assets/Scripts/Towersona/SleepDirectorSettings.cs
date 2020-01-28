using UnityEngine;

[CreateAssetMenu(menuName ="Level Balance/Sleep Director Settings", fileName ="New SleepDirectorSettings")]
public class SleepDirectorSettings : ScriptableObject
{
    public float Interval => Random.Range(minInterval, maxInterval);

    [SerializeField] float minInterval = 10;
    [SerializeField] float maxInterval = 30;

    private void OnValidate()
    {
        minInterval = Mathf.Max(minInterval, 0.1f);
        maxInterval = Mathf.Max(maxInterval, minInterval);
    }
}