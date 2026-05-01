using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gameStartDelay = 3;      
    public static bool inGame;

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
            print(gameStartDelay);
        }

        yield return new WaitForSeconds(1.0f);
        print("GO!");
        inGame = true;
    }
}
