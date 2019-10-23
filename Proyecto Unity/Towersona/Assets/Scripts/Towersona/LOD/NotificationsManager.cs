using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NotificationsManager : MonoBehaviour
{

    private TowersonaLOD towersonaLOD;
    private bool isNotifying;
    private TowersonaNeeds.Emotion currentNotifiedEmotion;
    private TowersonaNeeds needs;

	//[SerializeField] private GameObject notificationPrefab = null;
	//private GameObject notification;

	private void Start()
    {
        needs = GetComponent<TowersonaLOD>().towersona.towersonaNeeds;
    }

    void Update()
    {
        //TODO: Cambiar sistema de notificaciones

        if (needs == null) return;

        TowersonaNeeds.Emotion currentEmotion = needs.CurrentEmotion;

        bool isNotifiableEmotion = currentEmotion != TowersonaNeeds.Emotion.Fine;

        if (isNotifiableEmotion)
        {
            if (isNotifying && currentNotifiedEmotion != currentEmotion)
            {
                DestroyNotification();
            }

            CreateNotification(currentEmotion);
        }
    }

    //Recomiendo muy fuertemente no destruir y crear la notificación cada vez, esto generará mucha basura. 
    //Sería mejor tener un objeto NeedNotification permanente al que cambiar el sprite que enseña/desactivar el SpriteRenderer.
    private void CreateNotification(TowersonaNeeds.Emotion emotion)
    {
        Assert.AreNotEqual(emotion, TowersonaNeeds.Emotion.Fine, "Fine emotion shouldn't be notified.");

        //TODO: Notificaciones
        isNotifying = true;
        currentNotifiedEmotion = emotion;
        

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

        /*isNotifying = false;
        Destroy(notification);*/
    }
}
