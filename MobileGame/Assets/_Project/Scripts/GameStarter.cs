using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public int gameStartDelay = 3;
    public bool IsGameRunning { get; set; } = false;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas countdownCanvas;

    private void Update()
    {
        if (!GameStateManager.hasGameStarted && GameStateManager.inGame && !IsGameRunning)
        {
            countdownCanvas.gameObject.SetActive(true);
            StartCoroutine(StartGame());
        }
    }

    public IEnumerator StartGame()
    {
        IsGameRunning = true;
        while (gameStartDelay > 0)
        {
            yield return new WaitForSeconds(1.0f);
            gameStartDelay--;
        }

        yield return new WaitForSeconds(1.0f);
        GameStateManager.hasGameStarted = true;
        menuCanvas.gameObject.SetActive(true);
    }
}
