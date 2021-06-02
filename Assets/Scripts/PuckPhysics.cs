using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckPhysics : MonoBehaviour
{
    public Stat_Manager stat_manager;
    public PhotonView photonView;
    public Rigidbody2D rb;
    public float topSpeed;
    private bool wallhit = false;
    private bool playerhit = false;

    private Vector2 wallNormal;

    private float goalBounce = 1.35f;
    private float playerBounce = 1.25f;
    private float wallDamp = 0.85f;
    private float friction = 0.996f;

    private int last_touched_by_team;
    private int last_touched_by_player;

    private bool MayHitGoal;
    private int mayhitgoal_number = 0;


    // Start is called before the first frame update
    void Start()
    {
        stat_manager = GameObject.Find("StatsManager").GetComponent<Stat_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: possible spricting friction

        //do raycast to see if it will hit any goal
        //Vector2 endpoint = new Vector2(transform.position.x, transform.position.y) + rb.velocity * 1000;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), rb.velocity);
        //Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), endpoint);
        //Debug.Log(hit.collider.tag);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "goal1")
            {
                mayhitgoal_number = 1;
                MayHitGoal = true;
            }
            else if (hit.collider.tag == "goal2")
            {
                mayhitgoal_number = 2;
                MayHitGoal = true;
            }
            else
            {
                MayHitGoal = false;
            }
        }

        //Debug.Log(MayHitGoal);
    }

    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        //Debug.Log(rb.velocity.magnitude);
        //Top speed on puck
        rb.velocity = rb.velocity * friction;
        CheckTopSpeed();
        

    }

    private void CheckTopSpeed()
    {
        if (rb.velocity.magnitude > topSpeed)
        {
            rb.velocity = rb.velocity.normalized * topSpeed;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "border")
        {
            rb.velocity = rb.velocity * wallDamp;
            wallhit = true;
            wallNormal = collision.GetContact(0).normal;
        }

        if (collision.collider.tag == "goal1")
        {
            rb.velocity = rb.velocity * goalBounce;
            //TODO: put a slightly random angle in
            ChangeAngle(10);

            if(last_touched_by_team == 1)
            {
                //count own goal to player
                if(last_touched_by_player!= 0)
                {
                    stat_manager.team1[last_touched_by_player - 1].own_goals++;
                }
            } else
            {
                //count goal to player
                if (last_touched_by_player != 0)
                {
                    stat_manager.team2[last_touched_by_player - 1].goals++;
                }
            }

        }

        if (collision.collider.tag == "goal2")
        {
            rb.velocity = rb.velocity * goalBounce;
            //TODO: put a slightly random angle in
            ChangeAngle(10);

            if (last_touched_by_team == 2)
            {
                //count own goal to player
                if (last_touched_by_player != 0)
                {
                    stat_manager.team2[last_touched_by_player - 1].own_goals++;
                }
            }
            else
            {
                //count goal to player
                if (last_touched_by_player != 0)
                {
                    stat_manager.team1[last_touched_by_player - 1].goals++;
                }
            }

        }

        if (collision.collider.tag == "Player")
        {
            rb.velocity = rb.velocity * playerBounce;
            playerhit = true;
            Player_Movement a = collision.gameObject.GetComponent<Player_Movement>();
            last_touched_by_team = a.team_number;
            last_touched_by_player = a.player_number;

            if (MayHitGoal)
            {
                if(mayhitgoal_number == last_touched_by_team)
                {
                    //get a save for that player if changed direction enough

                    if (last_touched_by_team == 1)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), rb.velocity);
                        if (hit.collider != null)
                        {
                            if (hit.collider.tag == "goal1")
                            {
                                //no
                            }
                            else
                            {
                                stat_manager.team1[last_touched_by_player - 1].saves++;
                                Debug.Log("SAVE!");
                            }
                        }
                        //stat_manager.team1[last_touched_by_player - 1].saves++;

                    } else
                    {
                        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), rb.velocity);
                        if (hit.collider != null)
                        {
                            if (hit.collider.tag == "goal2")
                            {
                                //no
                            }
                            else
                            {
                                stat_manager.team2[last_touched_by_player - 1].saves++;
                                Debug.Log("SAVE!");
                            }
                        }
                        //stat_manager.team2[last_touched_by_player - 1].saves++;

                    }
                    
                }
            }

            //touches
            //shots and shots on goal
            if (last_touched_by_team == 1)
            {
                stat_manager.team1[last_touched_by_player - 1].touches++;
                Debug.Log(collision.rigidbody.velocity.magnitude);
                if(collision.rigidbody.velocity.magnitude > 3.5)
                {
                    stat_manager.team1[last_touched_by_player - 1].shots++;
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), rb.velocity);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "goal2")
                        {
                            stat_manager.team1[last_touched_by_player - 1].shots_on_goal++;
                        }

                    }
                }
                

            }
            else
            {
                stat_manager.team2[last_touched_by_player - 1].touches++;
                if (collision.rigidbody.velocity.magnitude > 3.5) 
                {
                    stat_manager.team2[last_touched_by_player - 1].shots++;
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), rb.velocity);
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "goal1")
                        {
                            stat_manager.team2[last_touched_by_player - 1].shots_on_goal++;
                        }

                    }
                }
            }

            

            
        }

        //cheeck topspeed
        CheckTopSpeed();



    }

    private void ChangeAngle(float deviation)
    {
        rb.velocity = rotate(rb.velocity, Mathf.Deg2Rad*deviation);
    }

    private Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
