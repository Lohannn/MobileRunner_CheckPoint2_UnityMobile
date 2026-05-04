using UnityEngine;
using UnityEngine.UI;

public class HomeCanvas : MonoBehaviour
{
    [SerializeField] private CameraManager mainCamera;

    [SerializeField] private Text recordText; 

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        recordText.text = PlayerData.scoreRecord.ToString().PadLeft(4, '0');

        if (GameStateManager.inGame)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (audioManager == null) {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
    }

    public void StartGame()
    {
        audioManager.StopMusic();
        audioManager.PlaySFX(AudioManager.SFX_UI_PLAY);

        mainCamera.StartFollowing();
        gameObject.SetActive(false);
    }
}
