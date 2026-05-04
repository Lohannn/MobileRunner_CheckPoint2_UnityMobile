using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEndCanvas : MonoBehaviour
{
    private bool isFadingIn = true;
    private float fadeSpeed;

    private CanvasGroup canvaGroup;

    private void Awake()
    {
        canvaGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
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
        PlayerData.ResetScore();
        GameStateManager.hasGameStarted = false;
        CameraManager.isFollowing = true;
        CameraManager.inFollowPosition = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        PlayerData.ResetScore();
        GameStateManager.hasGameStarted = false;
        GameStateManager.inGame = false;
        CameraManager.isFollowing = false;
        CameraManager.inFollowPosition = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
