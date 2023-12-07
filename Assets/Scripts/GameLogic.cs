using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    Vector2 characterVelocityInPercent;
    Vector2 characterPositionInPercent;
 
    Dictionary<int, Character> players = new Dictionary<int, Character>();
    void Start()
    {
        NetworkClientProcessing.SetGameLogic(this);
    }

    public void CreateNewPlayer(int id, float posX, float posY)
    {
        GameObject character = new GameObject("Character");
        character.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Circle");
        Character newCharacter = character.AddComponent<Character>();
        newCharacter.SetInitialPosition(posX, posY);
        players.Add(id, newCharacter);
    }

    public void RemovePlayer(int playerId)
    {
        if(players.ContainsKey(playerId)) 
        {
            Destroy(players[playerId].gameObject);
            players.Remove(playerId);
        }
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

        MovePlayers();
    }

    void SendIuputToServer(int input)
    {
        NetworkClientProcessing.SendMessageToServer($"{ClientToServerSignifiers.PlayerInput},{input}", TransportPipeline.FireAndForget);
    }
    public void AddVelocity(int id, float velocityX, float velocityY)
    {
        if (players.ContainsKey(id))
        {
            players[id].velocityInPercent = new Vector2(velocityX, velocityY);
        }
    }

    public void MovePlayers()
    {
        foreach (var playerEntry in players)
        {
            Character character = playerEntry.Value;
            character.UpdatePosition();
        }
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
