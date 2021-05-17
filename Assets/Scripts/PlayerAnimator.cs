using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Player player;
    public Animator playerAnimator;
    public GameObject playerObject;
    // Awake is called before the Start method
    void Awake()
    {
        player = (Player)GetComponent(typeof(Player));
        playerAnimator = GetComponentInChildren<Animator>();
        playerObject = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( (player.currentStateName == "Chasing" || player.currentStateName == "Following" || player.currentStateName == "BallCarrying") && player.TryGetComponent(out AIPlayer aiplayer) == true)
        {
            Debug.Log("animating running");
            playerAnimator.SetBool("Down",false);
            playerAnimator.SetBool("Tackling",false);
            playerAnimator.SetBool("Scoring",false);
            playerAnimator.SetBool("Catching",false);
            playerAnimator.SetBool("Grabbing",false);
            playerAnimator.SetBool("Running",true);
        }else if (player.currentStateName == "Catching"  )
        {
            playerObject.GetComponent<Animation>() ; 
            Debug.Log("animating catching");
            // playerAnimator.SetBool("Running",false);
            playerAnimator.SetBool("Catching",true);
        }else if (player.currentStateName == "Scoring"  )
        {
            Debug.Log("animating scoring");
            // playerAnimator.SetBool("Running",false);
            playerAnimator.SetBool("Scoring",true);
        }else if (player.currentStateName == "Tackling"  )
        {
            Debug.Log("animating tackling");
            // playerAnimator.SetBool("Running",false);
            playerAnimator.SetBool("Tackling",true);
        }else if (player.currentStateName == "GoingDown"  )
        {
            Debug.Log("animating being tackled");
            // playerAnimator.SetBool("Running",false);
            playerAnimator.SetBool("GoingDown",true);
        }else if (player.currentStateName == "Down"  )
        {
            Debug.Log("animating being down");
            playerAnimator.SetBool("GoingDown",false);
            playerAnimator.SetBool("Down",true);
        }
        else if (player.currentStateName == "Grabbing"  )
        {
            Debug.Log("animating pick up");
            // playerAnimator.SetBool("Running",false);
            playerAnimator.SetBool("Grabbing",true);
        }
        // else if (player.currentStateName == "Showboating"  )
        // {
        //     playerAnimator.SetBool("Catching",true);
        // }
        else {  //ready state
            playerAnimator.SetBool("Down",false);
            playerAnimator.SetBool("Tackling",false);
            playerAnimator.SetBool("Scoring",false);
            playerAnimator.SetBool("Catching",false);
            playerAnimator.SetBool("GoingDown",false);
            playerAnimator.SetBool("Running",false);
            }
    }
}
