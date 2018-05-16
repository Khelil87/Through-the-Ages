using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TigerMove : MonoBehaviour
{
    bool walking = false;
    bool idle = true;
    int walkHASH = Animator.StringToHash("Walking");
    int idleHASH = Animator.StringToHash("Idle");

    Animator anim;
	Transform player;               // Reference to the player's position.
	//PlayerHealth playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.


	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent  ();
		//enemyHealth = GetComponent  ();
		nav = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator>();
	}


	void Update ()
	{
        // If the enemy and the player have health left...
        //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
        // ... set the destination of the nav mesh agent to the player.
        walking = true;
        idle = false;
        anim.SetBool("Idle", idle);
        anim.SetBool("Walking", walking);
        nav.SetDestination (player.position);
		//}
		// Otherwise...
		//else
		//{
		// ... disable the nav mesh agent.
		//nav.enabled = false;
		//}
		//} 
	}
}