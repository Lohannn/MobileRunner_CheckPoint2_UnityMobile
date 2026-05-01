using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private RoadGenerator roadGenerator;

    private Player player;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadEnd"))
        {
            roadGenerator.GenerateRoad(player.transform.position.z, player.GetDistanceToWin());
        }
    }
}
