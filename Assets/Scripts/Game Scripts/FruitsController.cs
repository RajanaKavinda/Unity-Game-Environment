using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;

public class FruitsController : MonoBehaviour
{
    [SerializeField]
    private int trees = 4;
    private int fruitLimit = 100;
    private Animator treeAnim;
    private SpriteRenderer treeSr;
    private int treeNumber;
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
    }

    void Start()
    {
        // Get the name of the GameObject
        string gameObjectName = gameObject.name;

        // Define a regular expression pattern to match numbers
        string pattern = @"\d+";

        // Match the pattern in the name
        Match match = Regex.Match(gameObjectName, pattern);

        // Check if a match is found
        if (match.Success)
        {
            // Extract the number from the match
            string numberStr = match.Value;

            // Convert the string number to an integer
            treeNumber = int.Parse(numberStr);

            // Print the number to the console
            Debug.Log("Number in name: " + treeNumber);

            // Start the update coroutine
            StartCoroutine(UpdateCoroutine());
        }
        else
        {
            Debug.LogError("No number found in the GameObject name.");
        }
    }

    IEnumerator UpdateCoroutine(){
        while (true)
        {
            // Update the tree animation
            UpdateTreeAnimation();

            // Wait for 10 seconds before the next update
            yield return new WaitForSeconds(5f);
        }
    }

    void UpdateTreeAnimation()
    {
        Debug.LogError("********************************* ");
        long filledTrees = 0;
        long remainFruits = 0;
        if (energyStatusController.totalFruits>0){
            Debug.LogError("###########################################");
            filledTrees = (long) energyStatusController.totalFruits/fruitLimit;
            remainFruits = energyStatusController.totalFruits - filledTrees * fruitLimit;
            Debug.LogError("totalFruits "+energyStatusController.totalFruits);
            Debug.LogError("filledTrees "+filledTrees);
            Debug.LogError("remainFruits "+remainFruits);

        }
        Debug.LogError("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%5");
        Debug.LogError("totalFruits "+energyStatusController.totalFruits);
        Debug.LogError("filledTrees "+filledTrees);
        Debug.LogError("remainFruits "+remainFruits);
        Debug.LogError("treeNumber "+treeNumber);
        if (treeNumber <= filledTrees){
            Debug.LogError("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
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
        }else if (treeNumber == filledTrees+1 && remainFruits>0){
            if (remainFruits<=10){
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
            } else if (remainFruits<=20){
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
            } else if (remainFruits<=30){
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
            } else if (remainFruits<=40){
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
            } else if (remainFruits<=50){
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
            } else if (remainFruits<=60){
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
            } else if (remainFruits<=70){
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
            } else if (remainFruits<=80){
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
            } else if (remainFruits<=90){
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
            } else{
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
            }
        }else{
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
        }
    }

    

}
