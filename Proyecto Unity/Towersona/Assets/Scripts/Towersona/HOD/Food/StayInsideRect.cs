using UnityEngine;

public class StayInsideRect : MonoBehaviour
{
    public RectTransform rectSource;

    private Vector3[] corners = new Vector3[4];

    private void LateUpdate()
    {
        rectSource.GetWorldCorners(corners);
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, corners[0].x, corners[2].x);
        position.y = Mathf.Clamp(position.y, corners[0].y, corners[2].y);

        transform.position = position;
    }
}