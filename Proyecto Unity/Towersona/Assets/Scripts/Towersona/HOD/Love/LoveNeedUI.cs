using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LoveNeedUI : MonoBehaviour
{
    private Slider slider;
    private LoveNeed loveNeed;

    public void SetWatchedLoveNeed(LoveNeed loveNeed)
    {
        this.loveNeed = loveNeed;
    }

    private void Update()
    {
        Debug.Assert(loveNeed, "LoveNeedUI has no LoveNeed asigned to it.", this);

        slider.value = loveNeed.CurrentLevel;
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = LoveNeed.MAX_LEVEL;
    }
}
