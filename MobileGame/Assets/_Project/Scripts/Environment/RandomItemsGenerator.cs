using System.Linq;
using UnityEngine;

public class RandomItemsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsPrefabs;
    [SerializeField] private GameObject[] longItems;

    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnRandomItem();
    }

    private void SpawnRandomItem()
    {
        int longItemsCount = 0;
        foreach (var spawn in spawnPoints)
        {
            int randomIndex = Random.Range(0, itemsPrefabs.Length + 1);

            if (randomIndex >= itemsPrefabs.Length)
                continue;

            while (randomIndex < itemsPrefabs.Length && 
                   longItems.Contains(itemsPrefabs[randomIndex]) && longItemsCount >= 2)
            {
                randomIndex = Random.Range(0, spawnPoints.Length + 1);
            }

            GameObject stageItem = Instantiate(itemsPrefabs[randomIndex]);
            stageItem.transform.position = new Vector3(spawn.position.x, itemsPrefabs[randomIndex].transform.position.y, spawn.position.z);
            stageItem.transform.SetParent(transform.parent);

            if (longItems.Contains(itemsPrefabs[randomIndex]))
            {
                longItemsCount++;
            }
        }
    }
}
