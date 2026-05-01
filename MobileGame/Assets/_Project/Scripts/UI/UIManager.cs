using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Delay Settings")]
    [SerializeField] private Canvas canvasCountdown;
    [SerializeField] private Text textCountdown;

    void Start()
    {
        canvasCountdown.enabled = true;
    }

    void Update()
    {
        if (!GameManager.inGame)
        {
            if (GameManager.gameStartDelay != 0)
            {
                textCountdown.text = GameManager.gameStartDelay.ToString();
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
