using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeContoller : MonoBehaviour
{
    private string GROW_ANIMATION = "grow";
    private Rigidbody2D treeBody;
    private Animator treeAnim;
    private SpriteRenderer treeSr;

    private void Awake()
    {
        treeBody = GetComponent<Rigidbody2D>();
        treeAnim = GetComponent<Animator>();
        treeSr = GetComponent<SpriteRenderer>();
        treeAnim.SetBool(GROW_ANIMATION, false); // Setting initial value
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Farmer"))
        {
            Debug.Log("Collided with Farmer");
            treeAnim.SetBool(GROW_ANIMATION, true);        
        }
        else
        {
            Debug.Log("Collided with something else");
        }
    }
}
