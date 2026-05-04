using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private Image pausePanel;
    [SerializeField] private Slider coveredDistanceSlider;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreGainText;

    [SerializeField] private float waitBeforeFade;
    [SerializeField] private float fadeDuration;

    [SerializeField] private Player player;

    private int lastScore = 0;

    private bool isPaused = false;
    private Coroutine scoreGainCoroutine;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        pausePanel.gameObject.SetActive(false);
        scoreGainText.color = new Color(scoreGainText.color.r, scoreGainText.color.g, scoreGainText.color.b, 0);
    }

    private void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        if (coveredDistanceSlider.value != (player.transform.position.z / player.GetDistanceToWin()))
        {
            coveredDistanceSlider.value = player.transform.position.z / player.GetDistanceToWin();
        }

        if (lastScore != PlayerData.score)
        {
            scoreText.text = PlayerData.score.ToString().PadLeft(4, '0');
            lastScore = PlayerData.score;
        }
    }

    public void ShowScoreGain(int scoreGained)
    {
        scoreGainText.text = "+" + scoreGained.ToString();

        if (scoreGainCoroutine != null) StopCoroutine(scoreGainCoroutine);

        scoreGainCoroutine = StartCoroutine(FadeScoreRoutine());
    }

    private IEnumerator FadeScoreRoutine()
    {
        Color textColor = scoreGainText.color;
        textColor.a = 1;
        scoreGainText.color = textColor;

        yield return new WaitForSeconds(waitBeforeFade);

        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            scoreGainText.color = textColor;
            yield return null;
        }

        textColor.a = 0;
        scoreGainText.color = textColor;
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }

    public void TogglePause()
    {
        audioManager.PlaySFX(AudioManager.SFX_UI_BUTTON);

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
}
