using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCounter : MonoBehaviour
{
    public int team_that_gets_the_point;
    public int goals = 0;
    public GameManagerRound manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "puck")
        {
            goals++;
            manager.UpdateScore(team_that_gets_the_point);
        }
    }

}
