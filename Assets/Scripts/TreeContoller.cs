using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeContoller : MonoBehaviour
{
    [SerializeField]
    private float energyConsumption = 19f;

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
    private string HARVEST_ANIMATION = "harvest";
    private string REGROW_ANIMATION = "regrow";
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
        treeAnim.SetBool(REGROW_ANIMATION, false);
        treeAnim.SetBool(HARVEST_ANIMATION, false);
    }

    private void Update()
    {
        if (energyConsumption<4){
            treeAnim.SetBool(REGROW_ANIMATION, true);
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
        }else if (energyConsumption<8){
            treeAnim.SetBool(REGROW_ANIMATION, true);
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
        }else if (energyConsumption<12){
            treeAnim.SetBool(REGROW_ANIMATION, true);
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
        }else if (energyConsumption<16){
            treeAnim.SetBool(REGROW_ANIMATION, true);
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
        }else if (energyConsumption<20){
            treeAnim.SetBool(REGROW_ANIMATION, true);
            treeAnim.SetBool(GROW1_ANIMATION, true);
            treeAnim.SetBool(GROW2_ANIMATION, false);
            treeAnim.SetBool(GROW3_ANIMATION, false);
            treeAnim.SetBool(GROW4_ANIMATION, false);
            treeAnim.SetBool(GROW5_ANIMATION, false);

            treeAnim.SetBool(DIE1_ANIMATION, true);
            treeAnim.SetBool(DIE2_ANIMATION, true);
            treeAnim.SetBool(DIE3_ANIMATION, true);
            treeAnim.SetBool(DIE4_ANIMATION, true);
            treeAnim.SetBool(DIE5_ANIMATION, false);
            treeAnim.SetBool(DIE6_ANIMATION, false);
        }else if (energyConsumption<24){
            treeAnim.SetBool(REGROW_ANIMATION, true);
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
            treeAnim.SetBool(DIE6_ANIMATION, false);
        }else{
            treeAnim.SetBool(REGROW_ANIMATION, false);
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
        }
        
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
