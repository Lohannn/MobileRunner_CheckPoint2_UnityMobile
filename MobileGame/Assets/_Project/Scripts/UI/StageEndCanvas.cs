using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEndCanvas : MonoBehaviour
{
    private bool isFadingIn = true;
    private float fadeSpeed;

    private AudioManager audioManager;
    private CanvasGroup canvaGroup;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        canvaGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        if (!isFadingIn) return;

        canvaGroup.alpha = Mathf.Lerp(canvaGroup.alpha, 1, fadeSpeed * Time.deltaTime);

        if ((1 - canvaGroup.alpha) <= 0.1f && canvaGroup.alpha != 1)
        {
            canvaGroup.alpha = 1;
        }
    }

    public void StartFadeIn(float fadeSpeed)
    {
        isFadingIn = true;
        this.fadeSpeed = fadeSpeed;
    }

    public void RetryStage()
    {
        audioManager.PlaySFX(AudioManager.SFX_UI_BUTTON);
        PlayerData.ResetScore();
        GameStateManager.hasGameStarted = false;
        CameraManager.isFollowing = true;
        CameraManager.inFollowPosition = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        audioManager.PlaySFX(AudioManager.SFX_UI_BUTTON);
        PlayerData.ResetScore();
        GameStateManager.hasGameStarted = false;
        GameStateManager.inGame = false;
        CameraManager.isFollowing = false;
        CameraManager.inFollowPosition = false;
        audioManager.PlayMusic(AudioManager.MUSIC_MENU_THEME);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
