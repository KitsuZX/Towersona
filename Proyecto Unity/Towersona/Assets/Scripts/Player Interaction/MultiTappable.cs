using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MultiTappable : MonoBehaviour, IPointerClickHandler
{
    public int tapCountForActivation = 2;
    public UnityEvent OnMultiTap;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == tapCountForActivation)
        {
            OnMultiTap.Invoke();
        }

        eventData.Use();
    }
}
