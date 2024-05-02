using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeContoller : MonoBehaviour
{
    [SerializeField]
    private float energyConsumption = 23f;

    private string GROW_ANIMATION = "grow";
    private string GROW1_ANIMATION = "grow1";
    private string GROW2_ANIMATION = "grow2";
    private string GROW3_ANIMATION = "grow3";
    private string GROW4_ANIMATION = "grow4";
    private string GROW5_ANIMATION = "grow5";

    private string DIE1_ANIMATION = "die1";
    private string DIE2_ANIMATION = "die2";
    private string DIE3_ANIMATION = "die3";
    private string DIE4_ANIMATION = "die4";
    private string DIE5_ANIMATION = "die5";
    private string DIE6_ANIMATION = "die6";
    private string DIE7_ANIMATION = "die7";
    private string DIE8_ANIMATION = "die8";
    private string DIE9_ANIMATION = "die9";

    private string HARVEST_ANIMATION = "harvest";

    private string REGROW_ANIMATION = "regrow";
    private string REGROW2_ANIMATION = "regrow2";
    private string REGROW3_ANIMATION = "regrow3";

    private Rigidbody2D treeBody;
    private Animator treeAnim;
    private SpriteRenderer treeSr;

    private void Awake()
    {
        treeBody = GetComponent<Rigidbody2D>();
        treeAnim = GetComponent<Animator>();
        treeSr = GetComponent<SpriteRenderer>();

        treeAnim.SetBool(GROW_ANIMATION, false); // Setting initial value
        treeAnim.SetBool(GROW1_ANIMATION, false);
        treeAnim.SetBool(GROW2_ANIMATION, false);
        treeAnim.SetBool(GROW3_ANIMATION, false);
        treeAnim.SetBool(GROW4_ANIMATION, false);
        treeAnim.SetBool(GROW5_ANIMATION, false);

        treeAnim.SetBool(DIE1_ANIMATION, false);
        treeAnim.SetBool(DIE2_ANIMATION, false);
        treeAnim.SetBool(DIE3_ANIMATION, false);
        treeAnim.SetBool(DIE4_ANIMATION, false);
        treeAnim.SetBool(DIE5_ANIMATION, false);
        treeAnim.SetBool(DIE6_ANIMATION, false);
        treeAnim.SetBool(DIE7_ANIMATION, false);
        treeAnim.SetBool(DIE8_ANIMATION, false);
        treeAnim.SetBool(DIE9_ANIMATION, false);

        treeAnim.SetBool(REGROW_ANIMATION, false);
        treeAnim.SetBool(REGROW2_ANIMATION, false);
        treeAnim.SetBool(REGROW3_ANIMATION, false);

        treeAnim.SetBool(HARVEST_ANIMATION, false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world coordinates
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Check if the mouse click is on the tree object
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                
                // Set HARVEST_ANIMATION to true
                treeAnim.SetBool(HARVEST_ANIMATION, true);
                Debug.Log("Tree clicked");
                StartCoroutine(DelayedHarvestAnimationReset());
            }
        }
        if (energyConsumption<5){
            treeAnim.SetBool(REGROW_ANIMATION, true);
            treeAnim.SetBool(REGROW2_ANIMATION, true);
            treeAnim.SetBool(REGROW3_ANIMATION, true);

            treeAnim.SetBool(GROW1_ANIMATION, true);
            treeAnim.SetBool(GROW2_ANIMATION, true);
            treeAnim.SetBool(GROW3_ANIMATION, true);
            treeAnim.SetBool(GROW4_ANIMATION, true);
            treeAnim.SetBool(GROW5_ANIMATION, true);
            
            treeAnim.SetBool(DIE1_ANIMATION, false);
            treeAnim.SetBool(DIE2_ANIMATION, false);
            treeAnim.SetBool(DIE3_ANIMATION, false);
            treeAnim.SetBool(DIE4_ANIMATION, false);
            treeAnim.SetBool(DIE5_ANIMATION, false);
            treeAnim.SetBool(DIE6_ANIMATION, false);
            treeAnim.SetBool(DIE7_ANIMATION, false);
            treeAnim.SetBool(DIE8_ANIMATION, false);
            
        }else if (energyConsumption<10){
            treeAnim.SetBool(REGROW_ANIMATION, true);
            treeAnim.SetBool(REGROW2_ANIMATION, true);
            treeAnim.SetBool(REGROW3_ANIMATION, true);

            treeAnim.SetBool(GROW1_ANIMATION, true);
            treeAnim.SetBool(GROW2_ANIMATION, true);
            treeAnim.SetBool(GROW3_ANIMATION, true);
            treeAnim.SetBool(GROW4_ANIMATION, true);
            treeAnim.SetBool(GROW5_ANIMATION, false);
            
            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, false);
            treeAnim.SetBool(DIE3_ANIMATION, false);
            treeAnim.SetBool(DIE4_ANIMATION, false);
            treeAnim.SetBool(DIE5_ANIMATION, false);
            treeAnim.SetBool(DIE6_ANIMATION, false);
            treeAnim.SetBool(DIE7_ANIMATION, false);
            treeAnim.SetBool(DIE8_ANIMATION, false);
            
        }else if (energyConsumption<15){
            treeAnim.SetBool(REGROW_ANIMATION, true);
            treeAnim.SetBool(REGROW2_ANIMATION, true);
            treeAnim.SetBool(REGROW3_ANIMATION, true);

            treeAnim.SetBool(GROW1_ANIMATION, true);
            treeAnim.SetBool(GROW2_ANIMATION, true);
            treeAnim.SetBool(GROW3_ANIMATION, true);
            treeAnim.SetBool(GROW4_ANIMATION, false);
            treeAnim.SetBool(GROW5_ANIMATION, false);
            
            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, true);
            treeAnim.SetBool(DIE3_ANIMATION, false);
            treeAnim.SetBool(DIE4_ANIMATION, false);
            treeAnim.SetBool(DIE5_ANIMATION, false);
            treeAnim.SetBool(DIE6_ANIMATION, false);
            treeAnim.SetBool(DIE7_ANIMATION, false);
            treeAnim.SetBool(DIE8_ANIMATION, false);
            
        }else if (energyConsumption<20){
            treeAnim.SetBool(REGROW_ANIMATION, true);
            treeAnim.SetBool(REGROW2_ANIMATION, true);
            treeAnim.SetBool(REGROW3_ANIMATION, true);

            treeAnim.SetBool(GROW1_ANIMATION, true);
            treeAnim.SetBool(GROW2_ANIMATION, true);
            treeAnim.SetBool(GROW3_ANIMATION, false);
            treeAnim.SetBool(GROW4_ANIMATION, false);
            treeAnim.SetBool(GROW5_ANIMATION, false);
            
            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, true);
            treeAnim.SetBool(DIE3_ANIMATION, true);
            treeAnim.SetBool(DIE4_ANIMATION, false);
            treeAnim.SetBool(DIE5_ANIMATION, false);
            treeAnim.SetBool(DIE6_ANIMATION, false);
            treeAnim.SetBool(DIE7_ANIMATION, false);
            treeAnim.SetBool(DIE8_ANIMATION, false);
            
        }else if (energyConsumption<25){
            treeAnim.SetBool(REGROW_ANIMATION, true);
            treeAnim.SetBool(REGROW2_ANIMATION, true);
            treeAnim.SetBool(REGROW3_ANIMATION, true);

            treeAnim.SetBool(GROW1_ANIMATION, true);
            treeAnim.SetBool(GROW2_ANIMATION, false);
            treeAnim.SetBool(GROW3_ANIMATION, false);
            treeAnim.SetBool(GROW4_ANIMATION, false);
            treeAnim.SetBool(GROW5_ANIMATION, false);
            
            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, true);
            treeAnim.SetBool(DIE3_ANIMATION, true);
            treeAnim.SetBool(DIE4_ANIMATION, false);
            treeAnim.SetBool(DIE5_ANIMATION, false);
            treeAnim.SetBool(DIE6_ANIMATION, false);
            treeAnim.SetBool(DIE7_ANIMATION, false);
            treeAnim.SetBool(DIE8_ANIMATION, false);
            
        }else if (energyConsumption<30){
            treeAnim.SetBool(REGROW_ANIMATION, true);
            treeAnim.SetBool(REGROW2_ANIMATION, true);
            treeAnim.SetBool(REGROW3_ANIMATION, false);

            treeAnim.SetBool(GROW1_ANIMATION, false);
            treeAnim.SetBool(GROW2_ANIMATION, false);
            treeAnim.SetBool(GROW3_ANIMATION, false);
            treeAnim.SetBool(GROW4_ANIMATION, false);
            treeAnim.SetBool(GROW5_ANIMATION, false);
            
            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, true);
            treeAnim.SetBool(DIE3_ANIMATION, true);
            treeAnim.SetBool(DIE4_ANIMATION, true);
            treeAnim.SetBool(DIE5_ANIMATION, true);
            treeAnim.SetBool(DIE6_ANIMATION, true);
            treeAnim.SetBool(DIE7_ANIMATION, false);
            treeAnim.SetBool(DIE8_ANIMATION, false);
            
        }else if (energyConsumption<35){
            treeAnim.SetBool(REGROW_ANIMATION, false);
            treeAnim.SetBool(REGROW2_ANIMATION, false);
            treeAnim.SetBool(REGROW3_ANIMATION, false);

            treeAnim.SetBool(GROW1_ANIMATION, false);
            treeAnim.SetBool(GROW2_ANIMATION, false);
            treeAnim.SetBool(GROW3_ANIMATION, false);
            treeAnim.SetBool(GROW4_ANIMATION, false);
            treeAnim.SetBool(GROW5_ANIMATION, false);
            
            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, true);
            treeAnim.SetBool(DIE3_ANIMATION, true);
            treeAnim.SetBool(DIE4_ANIMATION, true);
            treeAnim.SetBool(DIE5_ANIMATION, true);
            treeAnim.SetBool(DIE6_ANIMATION, true);
            treeAnim.SetBool(DIE7_ANIMATION, true);
            treeAnim.SetBool(DIE8_ANIMATION, false);
            
        }else{
            treeAnim.SetBool(REGROW_ANIMATION, false);
            treeAnim.SetBool(REGROW2_ANIMATION, false);
            treeAnim.SetBool(REGROW3_ANIMATION, false);

            treeAnim.SetBool(GROW1_ANIMATION, false);
            treeAnim.SetBool(GROW2_ANIMATION, false);
            treeAnim.SetBool(GROW3_ANIMATION, false);
            treeAnim.SetBool(GROW4_ANIMATION, false);
            treeAnim.SetBool(GROW5_ANIMATION, false);
            
            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, true);
            treeAnim.SetBool(DIE3_ANIMATION, true);
            treeAnim.SetBool(DIE4_ANIMATION, true);
            treeAnim.SetBool(DIE5_ANIMATION, true);
            treeAnim.SetBool(DIE6_ANIMATION, true);
            treeAnim.SetBool(DIE7_ANIMATION, true);
            treeAnim.SetBool(DIE8_ANIMATION, true);
            
        }
        
    }

    IEnumerator DelayedHarvestAnimationReset()
    {
        // Wait for random milliseconds
        yield return new WaitForSeconds(Random.Range(5f, 20f));
        
        treeAnim.SetBool(DIE9_ANIMATION, true);
        // Set HARVEST_ANIMATION back to false
        treeAnim.SetBool(HARVEST_ANIMATION, false);
        yield return new WaitForSeconds(0.5f);
        treeAnim.SetBool(DIE9_ANIMATION, false);
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
