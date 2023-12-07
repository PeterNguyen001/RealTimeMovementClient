using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkClientProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {
        const int playerId =  1;
        const int vec2X = 2;
        const int vec2Y = 3;

        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if(signifier == ServerToClientSignifiers.CreateNewPlayer)
        {
            gameLogic.CreateNewPlayer(int.Parse(csv[playerId]), float.Parse(csv[vec2X]), float.Parse(csv[vec2Y]));
        }
        else if (signifier == ServerToClientSignifiers.PlayerVelocity)
        {
            gameLogic.AddVelocity(int.Parse(csv[playerId]) ,float.Parse(csv[vec2X]), float.Parse(csv[vec2Y]));
        }
        else if(signifier == ServerToClientSignifiers.RemovePlayer)
        {

        }
    }

    static public void SendMessageToServer(string msg, TransportPipeline pipeline)
    {
        networkClient.SendMessageToServer(msg, pipeline);
    }

    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkClient.IsConnected();
    }
    static public void ConnectToServer()
    {
        networkClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkClient networkClient;
    static GameLogic gameLogic;

    static public void SetNetworkedClient(NetworkClient NetworkClient)
    {
        networkClient = NetworkClient;
    }
    static public NetworkClient GetNetworkedClient()
    {
        return networkClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int asd = 1;
    public const int PlayerInput = 2;
    public const int updateHeartbeat = 5;
}

static public class ServerToClientSignifiers
{
    public const int asd = 1;
    public const int PlayerVelocity = 2;
    public const int OtherPlayersVelocity = 3;
    public const int CreateNewPlayer = 4;
    public const int RemovePlayer = 5;
}

#endregion

