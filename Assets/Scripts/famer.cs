using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class famer : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 5f;

    private float movementX, movementY;

    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string WALK_ANIMATION = "walk";
    private string WALK_BACKWARD_ANIMATION = "walk backward";
    private string WALK_FOREWARD_ANIMATION = "walk foreward";

    private void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
         
    }

    

    // Update is called once per frame
    void Update()
    {
        FamerMoveKeyboard();
        AnimateFamer();
    }

    void FamerMoveKeyboard(){
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * moveForce;
    }

    void AnimateFamer(){
        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION,true);
            sr.flipX = false;
        }else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION,true);
            sr.flipX = true;
        }
        else{
            anim.SetBool(WALK_ANIMATION,false);
        }

        if (movementY > 0)
        {
            anim.SetBool(WALK_BACKWARD_ANIMATION,true);
            sr.flipX = false;
        }else if (movementY < 0)
        {
            anim.SetBool(WALK_FOREWARD_ANIMATION,true);
            sr.flipX = false;
        }
        else{
            anim.SetBool(WALK_FOREWARD_ANIMATION,false);
            anim.SetBool(WALK_BACKWARD_ANIMATION,false);
        }
        

    }
}
