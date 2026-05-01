using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] roadPrefabs;
    [SerializeField] private GameObject victoryRoad;
    [SerializeField] private List<GameObject> currentRoads;

    [SerializeField] private float stepDistance = 63f;

    private bool victoryRoadGenerated = false;

    public void GenerateRoad(float distanceTraveled, float distanceToWin)
    {
        if (distanceTraveled >= (distanceToWin - (stepDistance * 2)))
        {
            GenerateVictoryRoad();
        }
        else
        {
            GenerateNewRoad();
        }
    }

    private void GenerateNewRoad()
    {
        int randomIndex = Random.Range(0, roadPrefabs.Length);
        Destroy(currentRoads[0]);
        currentRoads.RemoveAt(0);

        GameObject newRoad = Instantiate(roadPrefabs[randomIndex]);
        newRoad.transform.position = new Vector3(0, 0, currentRoads[^1].transform.position.z + stepDistance);
        currentRoads.Add(newRoad);
    }

    private void GenerateVictoryRoad()
    {
        if (victoryRoadGenerated) return;

        Destroy(currentRoads[0]);
        currentRoads.RemoveAt(0);

        GameObject newRoad = Instantiate(victoryRoad);
        newRoad.transform.position = new Vector3(0, 0, currentRoads[^1].transform.position.z + (stepDistance + 9));
        currentRoads.Add(newRoad);

        victoryRoadGenerated = true;
    }
}
