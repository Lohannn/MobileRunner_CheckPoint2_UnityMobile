using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private MainMenuCanvas uiCanvas;

    private bool canBeHit = true;

    private AudioManager audioManager;
    private Player player;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        player = transform.parent.GetComponent<Player>();
    }

    private void Update()
    {
        if (audioManager == null) { 
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canBeHit) return;

        if (other.CompareTag("Fish") || other.CompareTag("Sardine"))
        {
            int scoreValue = other.GetComponent<Collectables>().GetPointsValue();

            PlayerData.AddScore(scoreValue);
            uiCanvas.ShowScoreGain(scoreValue);
            audioManager.PlaySFX(AudioManager.SFX_GAME_ITEM);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(AudioManager.SFX_CAT_DEATH);
            canBeHit = false;
            player.Lose();
        }

        if (other.CompareTag("Victory"))
        {
            canBeHit = false;
            player.Win();
        }
    }
}
