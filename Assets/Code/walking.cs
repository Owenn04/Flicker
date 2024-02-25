using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{

    public Rigidbody2D playerRigidBody;
    private Vector2 moveInput;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        
        if(moveInput != Vector2.zero){
            animator.SetFloat("x-input", moveInput.x);
            animator.SetFloat("y-input", moveInput.y);
            animator.SetBool("isWalking", true);
        }else{
            animator.SetBool("isWalking", false);
        }
        
        moveInput.Normalize();

        playerRigidBody.velocity = moveInput * 4;
        
    }
}
