//using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 7f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 death = new Vector2(25f, 25f);
    //State
    bool isAlive = true;

    //Cached component refernces
    Rigidbody2D myRigidBody;
    Animator myAnimator;
   // CapsuleCollider2D myBodyCollider;
    Collider2D myCollider2D;
    float gravityScaleAtStart;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
       // myBodyCollider = GetComponent < CapsuleCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }
    //
    // Update is called once per frame
    void Update()
    {
        if (!isAlive){ return; }

        changeRotation();       //to prevent rotation, seting rotation.z to 0
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
    }
    //
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("run", playerHasHorizontalSpeed);
    }

   private void ClimbLadder()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("climb", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("climb", playerHasVerticalSpeed);
    }

   

    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
       if(myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("dying");
            GetComponent<Rigidbody2D>().velocity = death;
        }
           
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void changeRotation()       //preventing rotation
    {
        var rotationVec = transform.rotation.eulerAngles;
        rotationVec.z = 0;
        transform.rotation = Quaternion.Euler(rotationVec);
    }
}


