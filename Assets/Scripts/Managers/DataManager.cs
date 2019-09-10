using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private string _filename;
    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Data manager starting...");

        _network = service;
        _filename = Path.Combine(
                Application.persistentDataPath, "game.dat" // generate full path to file
            );

        status = ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add("inventory", Manager.Inventory.GetData());
        gamestate.Add("health", Manager.Player.health);
        gamestate.Add("maxHealth", Manager.Player.maxHealth);
        gamestate.Add("curLevel", Manager.Mission.curLevel);
        gamestate.Add("maxLevel", Manager.Mission.maxLevel);


        FileStream stream = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }


    public void LoadGameState()
    {
        if (!File.Exists(_filename))
        {
            Debug.Log("No saved game");
            return;
        }

        Dictionary<string, object> gamestate; // dictionary for save data

        BinaryFormatter formatter = new BinaryFormatter();//create formatter
        FileStream stream = File.Open(_filename, FileMode.Open);//get ctream from file
        gamestate = formatter.Deserialize(stream) as Dictionary<string, object>; //deserizlize stream
        stream.Close();//close stream

        Manager.Inventory.UpdateData((Dictionary<string, int>)gamestate["inventory"]);
        Manager.Player.UpdateData(
                (int)gamestate["health"],
                (int)gamestate["maxHealth"]
            );
        Manager.Mission.UpdateData(
            (int)gamestate["curLevel"],
            (int)gamestate["maxLevel"]
            );
        Manager.Mission.RestartCurent();

    }
}
