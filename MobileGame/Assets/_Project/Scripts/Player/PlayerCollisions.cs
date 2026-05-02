using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private MainMenuManager uiCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish") || other.CompareTag("Sardine"))
        {
            int scoreValue = other.GetComponent<Collectables>().GetPointsValue();

            PlayerData.AddScore(scoreValue);
            uiCanvas.ShowScoreGain(scoreValue);

            Destroy(other.gameObject);
        }
    }
}
