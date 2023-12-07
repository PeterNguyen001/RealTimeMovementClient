using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameObject character;
    Vector2 characterVelocityInPercent;
    Vector2 characterPositionInPercent;
 
    void Start()
    {
       
        NetworkClientProcessing.SetGameLogic(this);

        Sprite circleTexture = Resources.Load<Sprite>("Circle");

        character = new GameObject("Character");

        character.AddComponent<SpriteRenderer>();
        character.GetComponent<SpriteRenderer>().sprite = circleTexture;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            SendIuputToServer(InputSendToSever.noKey);
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                SendIuputToServer(InputSendToSever.wdKey);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                SendIuputToServer(InputSendToSever.waKey);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                SendIuputToServer(InputSendToSever.sdKey);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                SendIuputToServer(InputSendToSever.saKey);
            }
            else if (Input.GetKey(KeyCode.D))
                SendIuputToServer(InputSendToSever.dKey);
            else if (Input.GetKey(KeyCode.A))
                SendIuputToServer(InputSendToSever.aKey);
            else if (Input.GetKey(KeyCode.W))
                SendIuputToServer(InputSendToSever.wKey);
            else if (Input.GetKey(KeyCode.S))
                SendIuputToServer(InputSendToSever.sKey);
        }
        
        AddVelocity(characterVelocityInPercent.x, characterVelocityInPercent.y);

    }

    void SendIuputToServer(int input)
    {
        NetworkClientProcessing.SendMessageToServer($"{ClientToServerSignifiers.PlayerInput},{input}", TransportPipeline.FireAndForget);
    }
    public void AddVelocity( float velocityX, float velocityY)
    {
        characterVelocityInPercent.x = velocityX;
        characterVelocityInPercent.y = velocityY;
        characterPositionInPercent += (characterVelocityInPercent * Time.deltaTime);

        Vector2 screenPos = new Vector2(characterPositionInPercent.x * (float)Screen.width, characterPositionInPercent.y * (float)Screen.height);
        Vector3 characterPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        characterPos.z = 0;
        character.transform.position = characterPos;
    }
}

public static class InputSendToSever
{
    public const int noKey = 0;
    public const int wdKey = 1;
    public const int waKey = 2;
    public const int sdKey = 3;
    public const int saKey = 4;
    public const int dKey  = 5;
    public const int aKey  = 6;
    public const int wKey  = 7;
    public const int sKey  = 8;
}
