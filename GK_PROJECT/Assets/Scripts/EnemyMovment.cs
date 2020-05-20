using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D my_body;
    public bool MoveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        my_body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(MoveRight)
         my_body.velocity = new Vector2(moveSpeed, 0f);
        else
         my_body.velocity = new Vector2(-moveSpeed, 0f);

    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        MoveRight =  !MoveRight;
    
    }

}

