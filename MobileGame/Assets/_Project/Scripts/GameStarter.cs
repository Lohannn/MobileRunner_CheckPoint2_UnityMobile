using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public int gameStartDelay = 3;
    public bool IsGameRunning { get; set; } = false;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas countdownCanvas;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        if (!GameStateManager.hasGameStarted && GameStateManager.inGame && !IsGameRunning)
        {
            countdownCanvas.gameObject.SetActive(true);
            StartCoroutine(StartGame());
        }
    }

    public IEnumerator StartGame()
    {
        IsGameRunning = true;
        audioManager.PlaySFX(AudioManager.SFX_UI_BUTTON);

        while (gameStartDelay > 0)
        {
            yield return new WaitForSeconds(1.0f);
            gameStartDelay--;
            audioManager.PlaySFX(AudioManager.SFX_UI_BUTTON);
        }

        yield return new WaitForSeconds(1.0f);
        audioManager.PlayMusic(AudioManager.MUSIC_GAME_THEME);
        GameStateManager.hasGameStarted = true;
        menuCanvas.gameObject.SetActive(true);
    }
}
