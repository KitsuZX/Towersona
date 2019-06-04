using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject notificationPrefab;

    private TowersonaLOD towersonaLOD;

    private bool isNotifying;
    private GameObject notification;
    private TowersonaNeeds.NeedType prevNeedType;

    private TowersonaNeeds needs;

    private void Start()
    {
        needs = GetComponent<TowersonaLOD>().towersona.towersonaNeeds;
    }

    void Update()
    {
        //TODO: Cambiar sistema de notificaciones

        if (needs == null) return;

        //Check for needs
        TowersonaNeeds.NeedType needType = needs.CheckIfShouldNotifyNeed();

        if (needType != TowersonaNeeds.NeedType.None && !isNotifying)
        {
            CreateNotification(needType);
        }

        if (isNotifying)
        {
            if (prevNeedType != needType)
            {
                DestroyNotification();

                if (needType != TowersonaNeeds.NeedType.None)
                {
                    CreateNotification(needType);
                }
            }
        }

        prevNeedType = needType;
    }

    private void CreateNotification(TowersonaNeeds.NeedType needType)
    {
		//TODO: Notificaciones
        isNotifying = true;

		/*
        Notification notification = Instantiate(notificationPrefab).GetComponent<Notification>();

        this.notification = notification.gameObject;

        Vector3 position = transform.position;
        position.x += 1f;
        position.y = 2f;
        position.z += 1f;

        notification.transform.position = position;

        notification.transform.rotation = Quaternion.Euler(35f, 0f, 0f);

        SpriteRenderer[] sr = notification.GetComponentsInChildren<SpriteRenderer>();
        notification.transform.SetParent(transform);

        SpriteRenderer spriteRenderer = new SpriteRenderer();

        foreach (SpriteRenderer s in sr)
        {
            if (s != GetComponent<SpriteRenderer>())
            {
                spriteRenderer = s;
            }
        }

        switch (needType)
        {
            case TowersonaNeeds.NeedType.Hunger:
                spriteRenderer.sprite = notification.hungerSprite;
                break;
            case TowersonaNeeds.NeedType.Love:
                spriteRenderer.sprite = notification.noLoveSprite;
                break;
            case TowersonaNeeds.NeedType.Shit:
                spriteRenderer.sprite = notification.shitSprite;
                break;
        }*/
    }

    private void DestroyNotification()
    {
        isNotifying = false;
        Destroy(notification);
    }
}
