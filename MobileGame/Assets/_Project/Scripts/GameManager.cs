using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gameStartDelay = 3;      
    public static bool inGame;
    [SerializeField] private Canvas menuCanvas;

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        while(gameStartDelay > 0)
        {
            yield return new WaitForSeconds(1.0f);
            gameStartDelay--;
        }

        yield return new WaitForSeconds(1.0f);
        inGame = true;
        menuCanvas.gameObject.SetActive(true);
    }
}
