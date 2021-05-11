using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerRound : MonoBehaviour
{
    public GoalCounter goal_T1;
    public GoalCounter goal_T2;

    //text
    public TextMeshProUGUI text_team1;
    public TextMeshProUGUI text_team2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text_team1.text = "Team 1: " + goal_T1.goals;
        text_team2.text = "Team 2: " + goal_T2.goals;
    }
}
