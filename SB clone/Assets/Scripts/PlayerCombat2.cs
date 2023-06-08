using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2 : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Player2Movement player2Movement;

    public Animator animator;

    //public Transform attackPoint;

    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    private GameObject attackArea;

    private bool attacking = false;

    public float timeToAttack = 0.25F;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(attack());
        }
        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(false);
            }
        }
    }
    IEnumerator attack()
    {
        //Play attack animation
        attacking = true;
         
        attackArea.SetActive(true);
        attackArea.GetComponent<Collider2D>().enabled = false;
        
        animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.3f);
        attackArea.GetComponent<Collider2D>().enabled = true;
        //Detect enemies in range of attack

        Invoke("damage", 0.10f);
        //damage them


    }

    void damage()
    {
        /*Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We Hit " + enemy.name);
            if (enemy.gameObject.tag == "Player2")
            {
                if (player2Movement.percent == 0)
                {
                    player2Movement.KBCounter2 = player2Movement.KBTotalTime2 * 0.1f;
                }

                else
                {
                    player2Movement.KBCounter2 = player2Movement.KBTotalTime2 * player2Movement.percent * 0.1f;
                }


                if (enemy.transform.position.x <= transform.position.x)
                {

                    player2Movement.Knockfromright2 = true;
                }
                if (enemy.transform.position.x > transform.position.x)
                {
                    player2Movement.Knockfromright2 = false;
                }
                player2Movement.percent += 10.5f;
            }
        }
        */
    }

    private void OnDrawGizmosSelected()
    {
        /*if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        */  
    }
}
