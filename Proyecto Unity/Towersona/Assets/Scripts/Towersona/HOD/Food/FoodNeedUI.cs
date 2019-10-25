using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FoodNeedUI : MonoBehaviour
{
    private Slider slider;
    private FoodNeed foodNeed;

    public void SetWatchedFoodNeed(FoodNeed foodNeed)
    {
        this.foodNeed = foodNeed;
        slider.maxValue = foodNeed.MaxLevel;
    }

    private void Update()
    {
        Debug.Assert(foodNeed, "FoodNeedUI has no FoodNeed asigned to it.", this);
        slider.value = foodNeed.CurrentLevel;

        //TODO: Estaría bien tener una indicación de cómo quedaría la barra si soltaras la comida en este momento. Una segunda barra semitransparente o algo.
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
    }
}
