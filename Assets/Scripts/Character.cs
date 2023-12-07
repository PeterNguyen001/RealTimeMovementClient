using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2 velocityInPercent;
    private Vector2 positionInPercent;

    public void SetInitialPosition(float posX, float posY)
    {
        positionInPercent = new Vector2(posX, posY);
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        positionInPercent += (velocityInPercent * Time.deltaTime);
        Vector2 screenPos = new Vector2(positionInPercent.x * Screen.width, positionInPercent.y * Screen.height);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        newPos.z = 0;
        transform.position = newPos;
    }
}