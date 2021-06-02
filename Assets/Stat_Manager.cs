using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Manager : MonoBehaviour
{
    public class Stat
    {
        public int team { get; set; }
        public int player_number { get; set; }
        public string name { get; set; }


        public int goals { get; set; }
        public int own_goals { get; set; }
        public int saves { get; set; }

        public int distance_travelled { get; set; }
        public int shots { get; set; }
        public int shots_on_goal { get; set; }
        public int touches { get; set; }

        public float rating { get; set; }

        public Stat()
        {
            //...
        }
    }

    public List<Stat> team1 = new List<Stat>();
    public List<Stat> team2 = new List<Stat>();

    // Start is called before the first frame update
    void Start()
    {
        team1.Add(new Stat());
        team1.Add(new Stat());
        team1.Add(new Stat());
        team1.Add(new Stat());

        team2.Add(new Stat());
        team2.Add(new Stat());
        team2.Add(new Stat());
        team2.Add(new Stat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMember(int team, int player, string name)
    {
        if(team == 1)
        {
            team1[player-1].name = name;
        } 
        else
        {
            team2[player - 1].name = name;
        }
    }
}
