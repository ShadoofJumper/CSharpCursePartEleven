using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]


public class Manager : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }
    public static MissionManager Mission { get; private set; }

    private List<IGameManager> _startSequence; // all manager (dispatcher) 

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        Mission = GetComponent<MissionManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);
        _startSequence.Add(Mission);

        StartCoroutine(StartupManager());
    }

    private IEnumerator StartupManager()
    {
        NetworkService network = new NetworkService();

        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) // working untill all of manager will be working
        {
            //save start count of reeady manager each iteration
            int lastReady = numReady;
            numReady = 0;
            //then count ready
            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            //if ready bigger then last info then print info in console
            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
            yield return null; // stop on one framer befor next iteration
           }

            Debug.Log("All manager started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }

}
