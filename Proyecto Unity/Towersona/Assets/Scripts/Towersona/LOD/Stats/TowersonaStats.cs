using UnityEngine;

public abstract class TowersonaStats : ScriptableObject
{
    [Header("Money")]
    public int buyCost;
    public int sellCost;
    [Header("Towersonsa stats")]
    [Tooltip("Si esta towersona ataca a los enemigos voladores")]
    public bool attacksFliers;

    [Header("Tamagochi stats")]
    [Tooltip("El tamaño del estómago de la towersona")]
    public float maxFood = 1;
    [Tooltip("Hambre que pierde por segundo la towersona")]
    public float foodDecayPerSecond = 0.0f;
    [Tooltip("Amor que pierde por segundo la towersona")]
    public float loveDecayPerSecond = 0.05f;
    [Tooltip("Amor que gana la towersona al ser acariciada durante un segundo.")]
    public float loveGainPerSecondCaressed = 0.25f;
    [Tooltip("Si imaginas escoger quién duerme como una lotería, este es el número de boletos de la towersona.")]
    public float sleepChance = 1;

    [Header("Damage")]
	public Vector2 bulletDamage = Vector2.zero;
	public Vector2 attackSpeed = Vector2.zero;
	public Vector2 range = Vector2.zero;
	public Vector2 bulletSpeed = Vector2.zero;

    private void OnValidate()
    {
        sleepChance = Mathf.Max(sleepChance, 0);
    }
}
