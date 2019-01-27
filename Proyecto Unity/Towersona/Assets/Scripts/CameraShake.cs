using UnityEngine;

/// <summary>
/// Simple, fire and forget Perlin noise based shaking. Can be applied to any transform, not only cameras. 
/// </summary>
public class CameraShake : MonoBehaviour
{
    //Made as a singleton for convenience, but it doesn't need to be.
    public static CameraShake Instance
    {
        get;
        private set;
    }

    //Entirely arbitrary constant. Needed so values for offset and rotation values aren't the same.
    private const float ROTATION_SEED_OFFSET = 100;

    #region Inspector
    [Header("Offset")]
    [SerializeField]
    [Tooltip("Enable or disable offset in each axis separately.")]
    private ShakeDimensions offsetDirections = new ShakeDimensions(true, true, false);
    [SerializeField]
    private float maxOffset = 3.0f;

    [Header("Rotation")]
    [SerializeField]
    [Tooltip("Enable or disable rotation in each axis separately.")]
    private ShakeDimensions rotationDirections = new ShakeDimensions(false, false, true);
    [SerializeField]
    private float maxAngle = 15.0f;

    [Header("Shake parameters")]
    [SerializeField]
    [Tooltip("Frequency at which to sample the Perlin noise,")]
    private float frequency = 8.0f;
    [SerializeField]
    [Range(0, 5)]
    private float shakeScale = 1.0f;

    [Header("Trauma")]
    [SerializeField]
    [Range(0, 10)]
    [Tooltip("How fast the trauma decreases, if at all.")]
    private float traumaDecreaseSpeed = 1.5f;
    [SerializeField]
    [Tooltip("If true, added trauma is reduced proportionally to the current amount of trauma. Useful to avoid it getting out of control.")]
    private bool doNegativeFeedbackLoop = false;
    #endregion

    //Reference to the transform that will be shaken.
    private new Transform transform;

    //Trauma counter. Clamped between 0 and 1.
    private float trauma = 0.0f;

    /// <summary>
    /// Increase the trauma of the shaking.
    /// </summary>
    /// <param name="addedTrauma"></param>
    public void AddTrauma(float addedTrauma)
    {
        if (doNegativeFeedbackLoop) addedTrauma *= 1 - trauma;

        trauma += addedTrauma;
        trauma = Mathf.Clamp01(trauma);
    }


    private void Update()
    {
        //Compute shake amount. Doing it exponentially helps sell the impact. 
        //Done like this because it's technically more performant than Mathf.Pow(x, y)
        float shake = trauma * trauma * shakeScale;

        float offsetX, offsetY, offsetZ, angleX, angleY, angleZ;
        offsetX = offsetY = offsetZ = angleX = angleY = angleZ = 0;

        //RNG
        float seed = Time.time * frequency;
        if (offsetDirections.x) offsetX = maxOffset * shake * ((Mathf.PerlinNoise(seed, 0.0f) - 0.5f) * 2);
        if (offsetDirections.y) offsetY = maxOffset * shake * ((Mathf.PerlinNoise(0.0f, seed) - 0.5f) * 2);
        if (offsetDirections.z) offsetZ = maxOffset * shake * ((Mathf.PerlinNoise(seed, seed) - 0.5f) * 2);

        float offsetSeed = (Time.time + ROTATION_SEED_OFFSET) * frequency;
        if (rotationDirections.x) angleX = maxAngle * shake * ((Mathf.PerlinNoise(offsetSeed, 0.0f) - 0.5f) * 2);
        if (rotationDirections.y) angleY = maxAngle * shake * ((Mathf.PerlinNoise(0.0f, offsetSeed) - 0.5f) * 2);
        if (rotationDirections.z) angleZ = maxAngle * shake * ((Mathf.PerlinNoise(offsetSeed, offsetSeed) - 0.5f) * 2);

        //Apply the shaking
        transform.localPosition = new Vector3(offsetX, offsetY, offsetZ);
        transform.localRotation = Quaternion.Euler(angleX, angleY, angleZ);

        //Decrease trauma
        trauma -= traumaDecreaseSpeed * Time.deltaTime;
        trauma = Mathf.Max(trauma, 0);
    }

    #region Initialization
    private void Awake()
    {
        if (!Instance) Instance = this;
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        trauma = 0.0f;
    }

    private void OnEnable()
    {
        //Thanks to this, the component survives recompiles.
        if (!Instance) Instance = this;
        if (!transform) transform = GetComponent<Transform>();
    }
    #endregion


    [System.Serializable]
    private struct ShakeDimensions
    {
        public bool x;
        public bool y;
        public bool z;

        public ShakeDimensions(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}