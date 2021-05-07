using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Player player;
    public Animator playerAnimator;
    // Awake is called before the Start method
    void Awake()
    {
        player = (Player)GetComponent(typeof(Player));
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentStateName == "Chasing" || player.currentStateName == "Following" || player.currentStateName == "BallCarrying"  )
        {
            playerAnimator.SetBool("Running",true);
            // playerAnimator.Play("HandClosing", 0, controller.gripValue);
        }
        else { playerAnimator.SetBool("Running",false);}
    }
}
