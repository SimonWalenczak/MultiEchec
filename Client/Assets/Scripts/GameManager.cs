using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;


public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public MovableHandler movableHandler;
	
	public Connection pioconnection;
	private List<Message> msgList = new List<Message>(); //  Messsage queue implementation
	private bool joinedroom = false;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
		{
			Debug.LogError("There is already another GameManager in this scene !");
		}
	}

	void Start() {
		Application.runInBackground = true;

		// Create a random userid 
		System.Random random = new System.Random();
		string userid = "Guest" + random.Next(0, 10000);

		Debug.Log("Starting");

		PlayerIO.Authenticate(
			"multichess-fkekrt3970yh1d18nu1woq",            //Your game id
			"public",                               //Your connection id
			new Dictionary<string, string> {        //Authentication arguments
				{ "userId", userid },
			},
			null,                                   //PlayerInsight segments
			MasterServerJoined,
			delegate (PlayerIOError error) {
				Debug.Log("Error connecting: " + error.ToString());
			}
		);
	}
	void MasterServerJoined(Client client)
    {
		Debug.Log("Successfully connected to Player.IO");

		// Comment out the line below to use the live servers instead of your development server
		client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);

		Debug.Log("CreateJoinRoom");
		//Create or join the room 
		client.Multiplayer.CreateJoinRoom(
			"1",                    //Room id. If set to null a random roomid is used
			"SimServer",                   //The room type started on the server
			true,                               //Should the room be visible in the lobby?
			null,
			null,
			RoomJoined,
			delegate (PlayerIOError error) {
				Debug.Log("Error Joining Room: " + error.ToString());
			}
		);
	}
	void RoomJoined(Connection connection)
    {
		Debug.Log("Joined Room.");
		// We successfully joined a room so set up the message handler
		pioconnection = connection;
		pioconnection.OnMessage += handlemessage;
		joinedroom = true;

		pioconnection.Send("TEST", "pierre", 15);
	}

	void handlemessage(object sender, Message m) {
		msgList.Add(m);
	}

	void Update() {
		// process message queue
		foreach (Message m in msgList) 
		{
			movableHandler.HandleMessage(m);
		}

		// clear message queue after it's been processed
		msgList.Clear();
	}

	private void OnDestroy()
	{
		pioconnection.Disconnect();
	}
}
