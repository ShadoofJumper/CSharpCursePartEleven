using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]


public class Manager : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }

    private List<IGameManager> _startSequence; // all manager (dispatcher) 

    void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);
        StartCoroutine(StartupManager());
    }

    private IEnumerator StartupManager()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
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
            Debug.Log("Progress: "+numReady +"/"+numModules);
            yield return null; // stop on one framer befor next iteration
           }

            Debug.Log("All manager started up");
    }

}
