using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement playerMovement;
    public Player2Movement player2Movement;
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player2")
        {
            if (player2Movement.percent == 0)
            {
                player2Movement.KBCounter2 = player2Movement.KBTotalTime2 * 0.1f;
            }

            else
            {
                player2Movement.KBCounter2 = player2Movement.KBTotalTime2 * player2Movement.percent * 0.1f;
            }


            if (collider.transform.position.x <= transform.position.x)
            {

                player2Movement.Knockfromright2 = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                player2Movement.Knockfromright2 = false;
            }
            player2Movement.percent += 10.5f;
        }
    }
}
