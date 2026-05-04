using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Delay Settings")]
    [SerializeField] private Canvas canvasCountdown;
    [SerializeField] private Text textCountdown;

    [SerializeField] private GameStarter gameStarter;

    void Start()
    {
        canvasCountdown.enabled = true;
    }

    void Update()
    {
        if (!GameStateManager.hasGameStarted)
        {
            if (gameStarter.gameStartDelay != 0)
            {
                textCountdown.text = gameStarter.gameStartDelay.ToString();
            }
            else
            {
                textCountdown.text = "GO!";
            }
        }
        else
        {
            if (canvasCountdown.enabled)
            {
                canvasCountdown.enabled = false;
            }
        }
    }
}
