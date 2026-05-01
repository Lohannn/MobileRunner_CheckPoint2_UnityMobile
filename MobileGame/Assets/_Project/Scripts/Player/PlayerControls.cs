using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchStart;
    private Vector2 touchEnd;

    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.touchCount == 0) 
        {
            return;
        }

        Swipe();
    }

    private void Swipe()
    {
        touch = Input.GetTouch(0); 

        if(touch.phase == TouchPhase.Began) 
        {
            touchStart = touch.position;
        }    
        else if(touch.phase == TouchPhase.Moved)
        {
            touchEnd = touch.position;
        }
        else if(touch.phase == TouchPhase.Ended)
        {
            Vector2 touchValue = (touchEnd - touchStart);

            
            if(Mathf.Abs(touchValue.x) > Mathf.Abs(touchValue.y))
            {
                // MOVIMENTO HORIZONTAL  (ESQUERDA OU DIREITA)

                if (touchStart.x < touchEnd.x) 
                {
                    player.ChangeLane(1);
                }
                else
                {
                    player.ChangeLane(-1);
                }

            }
            else
            {
                // VERTICAL
                if (touchStart.y < touchEnd.y)
                {
                    //print("Cima");
                    player.Jump();
                }
                else
                {
                    //print("Baixo");
                }
            }

        }
    }
}
