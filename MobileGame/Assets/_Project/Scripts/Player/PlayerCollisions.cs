using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private MainMenuCanvas uiCanvas;
    
    private Player player;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish") || other.CompareTag("Sardine"))
        {
            int scoreValue = other.GetComponent<Collectables>().GetPointsValue();

            PlayerData.AddScore(scoreValue);
            uiCanvas.ShowScoreGain(scoreValue);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Obstacle"))
        {
            player.Lose();
        }

        if (other.CompareTag("Victory"))
        {
            player.Win();
        }
    }
}
