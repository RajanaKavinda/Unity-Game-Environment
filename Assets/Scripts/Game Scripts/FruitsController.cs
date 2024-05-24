using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;

public class FruitsController : MonoBehaviour
{
    
    
    private Animator treeAnim;
    private SpriteRenderer treeSr;
    private EnergyStatusController energyStatusController;

    private string GROW1_ANIMATION = "grow1";
    private string GROW2_ANIMATION = "grow2";
    private string GROW3_ANIMATION = "grow3";
    private string GROW4_ANIMATION = "grow4";
    private string GROW5_ANIMATION = "grow5";
    private string GROW6_ANIMATION = "grow6";
    private string GROW7_ANIMATION = "grow7";
    private string GROW8_ANIMATION = "grow8";
    private string GROW9_ANIMATION = "grow9";
    private string GROW10_ANIMATION = "grow10";
    private string GROW11_ANIMATION = "grow11";
    private string DIE1_ANIMATION = "die1";
    private string DIE2_ANIMATION = "die2";

    private void Awake(){
        treeAnim = GetComponent<Animator>();
        treeSr = GetComponent<SpriteRenderer>();
        // Find and store reference to EnergyStatusController
        energyStatusController = FindObjectOfType<EnergyStatusController>();

        treeAnim.SetBool(GROW1_ANIMATION, false);
        treeAnim.SetBool(GROW2_ANIMATION, false);
        treeAnim.SetBool(GROW3_ANIMATION, false);
        treeAnim.SetBool(GROW4_ANIMATION, false);
        treeAnim.SetBool(GROW5_ANIMATION, false);
        treeAnim.SetBool(GROW6_ANIMATION, false);
        treeAnim.SetBool(GROW7_ANIMATION, false);
        treeAnim.SetBool(GROW8_ANIMATION, false);
        treeAnim.SetBool(GROW9_ANIMATION, false);
        treeAnim.SetBool(GROW10_ANIMATION, false);
        treeAnim.SetBool(GROW11_ANIMATION, false);
        treeAnim.SetBool(DIE1_ANIMATION, false);
        treeAnim.SetBool(DIE2_ANIMATION, false);
    }

    void Start(){
        // Start the update coroutine
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine(){
        while (true)
        {
            // Wait for 10 seconds before the next update
            yield return new WaitForSeconds(10f);
            
            // Update the tree animation
            UpdateTreeAnimation();

            
        }
    }

    void UpdateTreeAnimation()
    {
        int fruits = energyStatusController.fruits;
            if (fruits<=-5){
                treeAnim.SetBool(DIE1_ANIMATION, true);
                treeAnim.SetBool(DIE2_ANIMATION, true);

                treeAnim.SetBool(GROW1_ANIMATION, false);
                treeAnim.SetBool(GROW2_ANIMATION, false);
                treeAnim.SetBool(GROW3_ANIMATION, false);
                treeAnim.SetBool(GROW4_ANIMATION, false);
                treeAnim.SetBool(GROW5_ANIMATION, false);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            }else if (fruits<0){
                treeAnim.SetBool(DIE1_ANIMATION, true);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, false);
                treeAnim.SetBool(GROW2_ANIMATION, false);
                treeAnim.SetBool(GROW3_ANIMATION, false);
                treeAnim.SetBool(GROW4_ANIMATION, false);
                treeAnim.SetBool(GROW5_ANIMATION, false);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            }else if (fruits<=5){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, false);
                treeAnim.SetBool(GROW2_ANIMATION, false);
                treeAnim.SetBool(GROW3_ANIMATION, false);
                treeAnim.SetBool(GROW4_ANIMATION, false);
                treeAnim.SetBool(GROW5_ANIMATION, false);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            }else if (fruits<=10){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, false);
                treeAnim.SetBool(GROW3_ANIMATION, false);
                treeAnim.SetBool(GROW4_ANIMATION, false);
                treeAnim.SetBool(GROW5_ANIMATION, false);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=20){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, false);
                treeAnim.SetBool(GROW4_ANIMATION, false);
                treeAnim.SetBool(GROW5_ANIMATION, false);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=30){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, false);
                treeAnim.SetBool(GROW5_ANIMATION, false);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=40){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, false);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=50){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, true);
                treeAnim.SetBool(GROW6_ANIMATION, false);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=60){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, true);
                treeAnim.SetBool(GROW6_ANIMATION, true);
                treeAnim.SetBool(GROW7_ANIMATION, false);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=70){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, true);
                treeAnim.SetBool(GROW6_ANIMATION, true);
                treeAnim.SetBool(GROW7_ANIMATION, true);
                treeAnim.SetBool(GROW8_ANIMATION, false);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=80){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, true);
                treeAnim.SetBool(GROW6_ANIMATION, true);
                treeAnim.SetBool(GROW7_ANIMATION, true);
                treeAnim.SetBool(GROW8_ANIMATION, true);
                treeAnim.SetBool(GROW9_ANIMATION, false);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=90){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, true);
                treeAnim.SetBool(GROW6_ANIMATION, true);
                treeAnim.SetBool(GROW7_ANIMATION, true);
                treeAnim.SetBool(GROW8_ANIMATION, true);
                treeAnim.SetBool(GROW9_ANIMATION, true);
                treeAnim.SetBool(GROW10_ANIMATION, false);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            } else if (fruits<=100){
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, true);
                treeAnim.SetBool(GROW6_ANIMATION, true);
                treeAnim.SetBool(GROW7_ANIMATION, true);
                treeAnim.SetBool(GROW8_ANIMATION, true);
                treeAnim.SetBool(GROW9_ANIMATION, true);
                treeAnim.SetBool(GROW10_ANIMATION, true);
                treeAnim.SetBool(GROW11_ANIMATION, false);
            }else{
                treeAnim.SetBool(DIE1_ANIMATION, false);
                treeAnim.SetBool(DIE2_ANIMATION, false);

                treeAnim.SetBool(GROW1_ANIMATION, true);
                treeAnim.SetBool(GROW2_ANIMATION, true);
                treeAnim.SetBool(GROW3_ANIMATION, true);
                treeAnim.SetBool(GROW4_ANIMATION, true);
                treeAnim.SetBool(GROW5_ANIMATION, true);
                treeAnim.SetBool(GROW6_ANIMATION, true);
                treeAnim.SetBool(GROW7_ANIMATION, true);
                treeAnim.SetBool(GROW8_ANIMATION, true);
                treeAnim.SetBool(GROW9_ANIMATION, true);
                treeAnim.SetBool(GROW10_ANIMATION, true);
                treeAnim.SetBool(GROW11_ANIMATION, true);
            }
        
    }

    

}