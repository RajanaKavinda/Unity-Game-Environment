using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 5f;

    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string WALK_ANIMATION = "walk";
    private string WALK_BACKWARD_ANIMATION = "walk backward";
    private string WALK_FOREWARD_ANIMATION = "walk foreward";

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FarmerMoveKeyboard();
        AnimateFarmer();
    }

    void FarmerMoveKeyboard()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        Vector3 newPosition = transform.position + new Vector3(movementX, movementY, 0f) * Time.deltaTime * moveForce;

        // Check if the new position is within the ground boundaries
        Collider2D[] colliders = Physics2D.OverlapBoxAll(newPosition, new Vector2(1f, 1f), 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("ground"))
            {
                // Update position only if within ground boundaries
                myBody.MovePosition(newPosition);
                break;
            }
        }
    }

    void AnimateFarmer()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }

        if (movementY > 0)
        {
            anim.SetBool(WALK_BACKWARD_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementY < 0)
        {
            anim.SetBool(WALK_FOREWARD_ANIMATION, true);
            sr.flipX = false;
        }
        else
        {
            anim.SetBool(WALK_FOREWARD_ANIMATION, false);
            anim.SetBool(WALK_BACKWARD_ANIMATION, false);
        }
    }
}
