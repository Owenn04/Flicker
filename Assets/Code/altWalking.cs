using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altWalking : MonoBehaviour
{

    public Rigidbody2D playerRigidBody;
    private Vector2 moveInput;
    public Camera cam;
    Vector2 mousePos;
    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        moveInput.Normalize();

        playerRigidBody.velocity = moveInput * 10;  

        Vector2 lookDir = mousePos - playerRigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        playerRigidBody.rotation = angle;
        
    }
}
