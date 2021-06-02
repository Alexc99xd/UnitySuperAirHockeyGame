using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerRound : MonoBehaviour
{
    public TimeManager time_manager;
    public Stat_Manager stat_manager;
    private const byte Timer_raise_event = 0;
    private const byte Team_send_event = 1;
    public PhotonView photonView;

    public GameObject PlayerPrefab;

    public GameObject PlayerPrefab11;
    public GameObject PlayerPrefab12;
    public GameObject PlayerPrefab13;
    public GameObject PlayerPrefab14;
    public GameObject PlayerPrefab21;
    public GameObject PlayerPrefab22;
    public GameObject PlayerPrefab23;
    public GameObject PlayerPrefab24;


    public GameObject PuckPrefab;
    public GameObject GameCanvas;
    public GameObject StartBorder;
    public GameObject Stats_Page;
    //public GoalCounter goal_T1;
    //public GoalCounter goal_T2;
    private int goals_team1;
    private int goals_team2;

    //text
    public TextMeshProUGUI text_team1;
    public TextMeshProUGUI text_team2;

    //spawns
    public GameObject T1_spawn_A;
    public GameObject T2_spawn_A;
    public GameObject puck_spawn;

    public Text PingText;

    public TextMeshProUGUI ui_timer;
    private int currentMatchTime;
    private Coroutine timerCourtine;

    private bool gameStart = false;
    private bool gameEnd = false;

    private int playerNumber1 = 1;
    private int playerNumber2 = 1;

    public TextMeshProUGUI team1_1;
    public TextMeshProUGUI team1_2;
    public TextMeshProUGUI team1_3;
    public TextMeshProUGUI team1_4;

    public TextMeshProUGUI team2_1;
    public TextMeshProUGUI team2_2;
    public TextMeshProUGUI team2_3;
    public TextMeshProUGUI team2_4;

    private GameObject puckObj;

    private void Awake()
    {
        StringsManager.Reset();
        GameCanvas.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        text_team1.text = "Team 1: " + 0;
        text_team2.text = "Team 2: " + 0;
        currentMatchTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("leave?");
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
        }
        PingText.text = "Ping: " + PhotonNetwork.GetPing();
        if (PhotonNetwork.IsMasterClient)
        {
            if (!gameStart && Input.GetKeyDown(KeyCode.P))
            {
                gameStart = true;
                gameEnd = false;
                StartGame();
                time_manager.StartGame();
            }

            if (gameEnd)
            {

                //get names (holy shit this kinda sucks)
                //GameObject p1_1 = GameObject.Find("1_1");
                //GameObject p1_2 = GameObject.Find("1_2");
                //GameObject p1_3 = GameObject.Find("1_3");
                //GameObject p1_4 = GameObject.Find("1_4");
                //GameObject p2_1 = GameObject.Find("2_1");
                //GameObject p2_2 = GameObject.Find("2_2");
                //GameObject p2_3 = GameObject.Find("2_3");
                //GameObject p2_4 = GameObject.Find("2_4");

                ////change to ternary when it works first
                //if(p1_1 != null)
                //{
                //    stat_manager.team1[0].name = p1_1.GetComponent<Player_Movement>().name_player;
                //    Debug.Log(stat_manager.team1[0].name);
                //}
                //if (p1_2 != null)
                //{
                //    stat_manager.team1[1].name = p1_2.GetComponent<Player_Movement>().name_player;
                //}
                //if (p1_3 != null)
                //{
                //    stat_manager.team1[2].name = p1_3.GetComponent<Player_Movement>().name_player;
                //}
                //if (p1_4 != null)
                //{
                //    stat_manager.team1[3].name = p1_4.GetComponent<Player_Movement>().name_player;
                //}
                //if (p2_1 != null)
                //{
                //    stat_manager.team2[0].name = p2_1.GetComponent<Player_Movement>().name_player;
                //}
                //if (p2_2 != null)
                //{
                //    stat_manager.team2[1].name = p2_2.GetComponent<Player_Movement>().name_player;
                //}
                //if (p2_3 != null)
                //{
                //    stat_manager.team2[2].name = p2_3.GetComponent<Player_Movement>().name_player;
                //}
                //if (p2_4 != null)
                //{
                //    stat_manager.team2[3].name = p2_4.GetComponent<Player_Movement>().name_player;
                //}

                stat_manager.team1[0].name = StringsManager.t1p1;

                stat_manager.team1[1].name = StringsManager.t1p2;

                stat_manager.team1[2].name = StringsManager.t1p3;

                stat_manager.team1[3].name = StringsManager.t1p4;

                stat_manager.team2[0].name = StringsManager.t2p1;

                stat_manager.team2[1].name = StringsManager.t2p2;

                stat_manager.team2[2].name = StringsManager.t2p3;

                stat_manager.team2[3].name = StringsManager.t2p4;

                stat_manager.team1[0].rating = CalcRating(stat_manager.team1[0].goals, stat_manager.team1[0].own_goals, stat_manager.team1[0].saves, stat_manager.team1[0].touches, stat_manager.team1[0].shots_on_goal);
                stat_manager.team1[1].rating = CalcRating(stat_manager.team1[1].goals, stat_manager.team1[1].own_goals, stat_manager.team1[1].saves, stat_manager.team1[1].touches, stat_manager.team1[1].shots_on_goal);
                stat_manager.team1[2].rating = CalcRating(stat_manager.team1[2].goals, stat_manager.team1[2].own_goals, stat_manager.team1[2].saves, stat_manager.team1[2].touches, stat_manager.team1[2].shots_on_goal);
                stat_manager.team1[3].rating = CalcRating(stat_manager.team1[3].goals, stat_manager.team1[3].own_goals, stat_manager.team1[3].saves, stat_manager.team1[3].touches, stat_manager.team1[3].shots_on_goal);
                stat_manager.team2[0].rating = CalcRating(stat_manager.team2[0].goals, stat_manager.team2[0].own_goals, stat_manager.team2[0].saves, stat_manager.team2[0].touches, stat_manager.team2[0].shots_on_goal);
                stat_manager.team2[1].rating = CalcRating(stat_manager.team2[1].goals, stat_manager.team2[1].own_goals, stat_manager.team2[1].saves, stat_manager.team2[1].touches, stat_manager.team2[1].shots_on_goal);
                stat_manager.team2[2].rating = CalcRating(stat_manager.team2[2].goals, stat_manager.team2[2].own_goals, stat_manager.team2[2].saves, stat_manager.team2[2].touches, stat_manager.team2[2].shots_on_goal);
                stat_manager.team2[3].rating = CalcRating(stat_manager.team2[3].goals, stat_manager.team2[3].own_goals, stat_manager.team2[3].saves, stat_manager.team2[3].touches, stat_manager.team2[3].shots_on_goal);

                Debug.Log("before send stats");
                photonView.RPC("SendStats1", RpcTarget.All, 1,
                    stat_manager.team1[0].name, 
                    stat_manager.team1[0].goals, 
                    stat_manager.team1[0].own_goals, 
                    stat_manager.team1[0].saves, 
                    stat_manager.team1[0].touches, 
                    stat_manager.team1[0].shots, 
                    stat_manager.team1[0].shots_on_goal, 
                    stat_manager.team1[0].distance_travelled, 
                    stat_manager.team1[0].rating
                );
                photonView.RPC("SendStats1", RpcTarget.All, 2,

                    stat_manager.team1[1].name,
                    stat_manager.team1[1].goals,
                    stat_manager.team1[1].own_goals,
                    stat_manager.team1[1].saves,
                    stat_manager.team1[1].touches,
                    stat_manager.team1[1].shots,
                    stat_manager.team1[1].shots_on_goal,
                    stat_manager.team1[1].distance_travelled,
                    stat_manager.team1[1].rating
                );
                photonView.RPC("SendStats1", RpcTarget.All, 3,

                    stat_manager.team1[2].name,
                    stat_manager.team1[2].goals,
                    stat_manager.team1[2].own_goals,
                    stat_manager.team1[2].saves,
                    stat_manager.team1[2].touches,
                    stat_manager.team1[2].shots,
                    stat_manager.team1[2].shots_on_goal,
                    stat_manager.team1[2].distance_travelled,
                    stat_manager.team1[2].rating
                );
                photonView.RPC("SendStats1", RpcTarget.All, 4,

                    stat_manager.team1[3].name,
                    stat_manager.team1[3].goals,
                    stat_manager.team1[3].own_goals,
                    stat_manager.team1[3].saves,
                    stat_manager.team1[3].touches,
                    stat_manager.team1[3].shots,
                    stat_manager.team1[3].shots_on_goal,
                    stat_manager.team1[3].distance_travelled,
                    stat_manager.team1[3].rating
                );

                photonView.RPC("SendStats2", RpcTarget.All, 1,
                    stat_manager.team2[0].name,
                    stat_manager.team2[0].goals,
                    stat_manager.team2[0].own_goals,
                    stat_manager.team2[0].saves,
                    stat_manager.team2[0].touches,
                    stat_manager.team2[0].shots,
                    stat_manager.team2[0].shots_on_goal,
                    stat_manager.team2[0].distance_travelled,
                    stat_manager.team2[0].rating
                );
                photonView.RPC("SendStats2", RpcTarget.All, 2,

                    stat_manager.team2[1].name,
                    stat_manager.team2[1].goals,
                    stat_manager.team2[1].own_goals,
                    stat_manager.team2[1].saves,
                    stat_manager.team2[1].touches,
                    stat_manager.team2[1].shots,
                    stat_manager.team2[1].shots_on_goal,
                    stat_manager.team2[1].distance_travelled,
                    stat_manager.team2[1].rating
                );
                photonView.RPC("SendStats2", RpcTarget.All, 3,

                    stat_manager.team2[2].name,
                    stat_manager.team2[2].goals,
                    stat_manager.team2[2].own_goals,
                    stat_manager.team2[2].saves,
                    stat_manager.team2[2].touches,
                    stat_manager.team2[2].shots,
                    stat_manager.team2[2].shots_on_goal,
                    stat_manager.team2[2].distance_travelled,
                    stat_manager.team2[2].rating
                );
                photonView.RPC("SendStats2", RpcTarget.All, 4,

                    stat_manager.team2[3].name,
                    stat_manager.team2[3].goals,
                    stat_manager.team2[3].own_goals,
                    stat_manager.team2[3].saves,
                    stat_manager.team2[3].touches,
                    stat_manager.team2[3].shots,
                    stat_manager.team2[3].shots_on_goal,
                    stat_manager.team2[3].distance_travelled,
                    stat_manager.team2[3].rating
                );

                CalcStats();
                photonView.RPC("CalcStats", RpcTarget.All);
                PhotonNetwork.Destroy(puckObj); //bye bye puck
                Stats_Page.SetActive(true);
            }
            
        }
    }


    private float CalcRating(int goals, int own, int saves, int touches, int shots_on_goal)
    {
        if(touches == 0)
        {
            return 0;
        }
        int total_goals = goals + own;
        int activity = saves + touches + 10;
        float p_m_saves_weight = goals*1.5f - own*1.75f + saves + shots_on_goal;
        int pm_cooler = goals - own + shots_on_goal;
        float touch_efficiency = (shots_on_goal * 1.5f + saves) / (5.0f + touches);
        float init = 1;
        if(total_goals != 0)
        {
            init += Mathf.Pow( (goals+0.1f) / (total_goals + 0.0f), 1.10f); //goals mult
            init -= Mathf.Pow((own + 0.1f) / (total_goals + 0.0f), 1.25f); //own goals multiplier
        } 
        else //sitting duck!
        {
            init = init / 2;
        }
        if (activity != 0)
        {
            if(saves != 0)
                init += Mathf.Pow( (saves+0.1f) / (activity + 0.0f), 1.05f); //saves mult (could be neg)
            if(p_m_saves_weight != 0)
                init += Mathf.Pow( (p_m_saves_weight+0.1f) / (activity + 0.0f), 1.10f); //activity mult
            if(pm_cooler != 0)
                init += Mathf.Pow( (pm_cooler+0.1f) / (activity + 0.0f), 1.1f); //shots and p/m
            //init += (float)(-1.5*Mathf.Pow(2.7f,-touch_efficiency)+1)*Mathf.Log10(touches/2) + touches*(1.0f/15);
        } 
        else
        {
            init = 0;
        }


        return init/1.5f; //xd



    }

    [PunRPC]
    private void CalcStats()
    {
        team1_1.text = stat_manager.team1[0].name + "\n\n" + stat_manager.team1[0].goals + "\n" + stat_manager.team1[0].own_goals + "\n" + stat_manager.team1[0].saves + "\n" + stat_manager.team1[0].touches + "\n" + stat_manager.team1[0].shots + "\n" + stat_manager.team1[0].shots_on_goal + "\n" + stat_manager.team1[0].distance_travelled + "\n" + stat_manager.team1[0].rating;
        team1_2.text = stat_manager.team1[1].name + "\n\n" + stat_manager.team1[1].goals + "\n" + stat_manager.team1[1].own_goals + "\n" + stat_manager.team1[1].saves + "\n" + stat_manager.team1[1].touches + "\n" + stat_manager.team1[1].shots + "\n" + stat_manager.team1[1].shots_on_goal + "\n" + stat_manager.team1[1].distance_travelled + "\n" + stat_manager.team1[1].rating;
        team1_3.text = stat_manager.team1[2].name + "\n\n" + stat_manager.team1[2].goals + "\n" + stat_manager.team1[2].own_goals + "\n" + stat_manager.team1[2].saves + "\n" + stat_manager.team1[2].touches + "\n" + stat_manager.team1[2].shots + "\n" + stat_manager.team1[2].shots_on_goal + "\n" + stat_manager.team1[2].distance_travelled + "\n" + stat_manager.team1[2].rating;
        team1_4.text = stat_manager.team1[3].name + "\n\n" + stat_manager.team1[3].goals + "\n" + stat_manager.team1[3].own_goals + "\n" + stat_manager.team1[3].saves + "\n" + stat_manager.team1[3].touches + "\n" + stat_manager.team1[3].shots + "\n" + stat_manager.team1[3].shots_on_goal + "\n" + stat_manager.team1[3].distance_travelled + "\n" + stat_manager.team1[3].rating;
        team2_1.text = stat_manager.team2[0].name + "\n\n" + stat_manager.team2[0].goals + "\n" + stat_manager.team2[0].own_goals + "\n" + stat_manager.team2[0].saves + "\n" + stat_manager.team2[0].touches + "\n" + stat_manager.team2[0].shots + "\n" + stat_manager.team2[0].shots_on_goal + "\n" + stat_manager.team2[0].distance_travelled + "\n" + stat_manager.team2[0].rating;
        team2_2.text = stat_manager.team2[1].name + "\n\n" + stat_manager.team2[1].goals + "\n" + stat_manager.team2[1].own_goals + "\n" + stat_manager.team2[1].saves + "\n" + stat_manager.team2[1].touches + "\n" + stat_manager.team2[1].shots + "\n" + stat_manager.team2[1].shots_on_goal + "\n" + stat_manager.team2[1].distance_travelled + "\n" + stat_manager.team2[1].rating;
        team2_3.text = stat_manager.team2[2].name + "\n\n" + stat_manager.team2[2].goals + "\n" + stat_manager.team2[2].own_goals + "\n" + stat_manager.team2[2].saves + "\n" + stat_manager.team2[2].touches + "\n" + stat_manager.team2[2].shots + "\n" + stat_manager.team2[2].shots_on_goal + "\n" + stat_manager.team2[2].distance_travelled + "\n" + stat_manager.team2[2].rating;
        team2_4.text = stat_manager.team2[3].name + "\n\n" + stat_manager.team2[3].goals + "\n" + stat_manager.team2[3].own_goals + "\n" + stat_manager.team2[3].saves + "\n" + stat_manager.team2[3].touches + "\n" + stat_manager.team2[3].shots + "\n" + stat_manager.team2[3].shots_on_goal + "\n" + stat_manager.team2[3].distance_travelled + "\n" + stat_manager.team2[3].rating;
        Stats_Page.SetActive(true);
    }
    [PunRPC]
    private void SendStats1(int player, string _name, int _goals, int _own_goals, int _saves, int _touches, int _shots, int _shots_on_goal, int _distancetravelled, float _rating)
    {
        stat_manager.team1[player-1].name = _name;
        stat_manager.team1[player-1].goals = _goals;
        stat_manager.team1[player-1].own_goals = _own_goals;
        stat_manager.team1[player-1].saves = _saves;
        stat_manager.team1[player-1].touches = _touches;
        stat_manager.team1[player-1].shots = _shots;
        stat_manager.team1[player-1].shots_on_goal = _shots_on_goal;
        //stat_manager.team1[player-1].distance_travelled = _distancetravelled;
        stat_manager.team1[player - 1].rating = _rating;

    }
    [PunRPC]
    private void SendStats2(int player, string _name, int _goals, int _own_goals, int _saves, int _touches, int _shots, int _shots_on_goal, int _distancetravelled, float _rating)
    {
        stat_manager.team2[player - 1].name = _name;
        stat_manager.team2[player - 1].goals = _goals;
        stat_manager.team2[player - 1].own_goals = _own_goals;
        stat_manager.team2[player - 1].saves = _saves;
        stat_manager.team2[player - 1].touches = _touches;
        stat_manager.team2[player - 1].shots = _shots;
        stat_manager.team2[player - 1].shots_on_goal = _shots_on_goal;
        //stat_manager.team2[player - 1].distance_travelled = _distancetravelled;
        stat_manager.team2[player - 1].rating = _rating;

    }

    [PunRPC]
    private void SendNameAndDistance1(int player, string name, int distance)
    {
        stat_manager.team1[player - 1].name = name;
        stat_manager.team1[player - 1].distance_travelled = distance;
    }

    [PunRPC]
    private void SendNameAndDistance2(int player, string name, int distance)
    {
        stat_manager.team2[player - 1].name = name;
        stat_manager.team2[player - 1].distance_travelled = distance;
    }



    public void EndGame()
    {
        gameEnd = true;
    }

    private void RefreshTimer()
    {
        string minutes = (currentMatchTime / 60).ToString("00");
        string seconds = (currentMatchTime % 60).ToString("00");
        ui_timer.text = $"{minutes}:{seconds}";
    }
    public void UpdateScore(int team)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(team == 1)
            {
                goals_team1++;
            } else
            {
                goals_team2++;
            }
            text_team1.text = "Team 1: " + goals_team1;
            text_team2.text = "Team 2: " + goals_team2;
            photonView.RPC("UpdateScoreOthers", RpcTarget.All, goals_team1, goals_team2);
        }


    }

    [PunRPC]
    public void UpdateScoreOthers(int goal1, int goal2)
    {
        text_team1.text = "Team 1: " + goal1;
        text_team2.text = "Team 2: " + goal2;
    }

    public void SpawnSpec()
    {
        GameCanvas.SetActive(false);
    }

    public void SpawnPlayerT1_1()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab11.name, new Vector2(T1_spawn_A.transform.position.x, T1_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "1_1";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);
        Debug.Log("P1 spawn");
        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();
        //Team_S(PhotonNetwork.NickName, 1, prefabb);
    }

    public void SpawnPlayerT1_2()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab12.name, new Vector2(T1_spawn_A.transform.position.x, T1_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "1_2";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);
        Debug.Log("P1 spawn");
        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();
        //Team_S(PhotonNetwork.NickName, 1, prefabb);
    }

    public void SpawnPlayerT1_3()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab13.name, new Vector2(T1_spawn_A.transform.position.x, T1_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "1_3";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);
        Debug.Log("P1 spawn");
        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();
        //Team_S(PhotonNetwork.NickName, 1, prefabb);
    }

    public void SpawnPlayerT1_4()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab14.name, new Vector2(T1_spawn_A.transform.position.x, T1_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "1_4";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);
        Debug.Log("P1 spawn");
        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();
        //Team_S(PhotonNetwork.NickName, 1, prefabb);
    }

    public void SpawnPlayerT2_1()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab21.name, new Vector2(T2_spawn_A.transform.position.x, T2_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "2_1";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);

        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();
        //Team_S(PhotonNetwork.NickName, 2, prefabb);
    }

    public void SpawnPlayerT2_2()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab22.name, new Vector2(T2_spawn_A.transform.position.x, T2_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "2_2";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);

        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();
        //Team_S(PhotonNetwork.NickName, 2, prefabb);
    }

    public void SpawnPlayerT2_3()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab23.name, new Vector2(T2_spawn_A.transform.position.x, T2_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "2_3";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);

        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();


    }

    public void SpawnPlayerT2_4()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab24.name, new Vector2(T2_spawn_A.transform.position.x, T2_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.name = "2_4";
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);

        //Player_Movement prefabb = prefab.GetComponent<Player_Movement>();


    }

    //public void OnPhotonInstantiate(PhotonMessageInfo info)
    //{
    //    object[] data = info.photonView.InstantiationData;
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        string name = (string)data[0];
    //        int team = (int)data[1];
    //        //Player_Movement prefab = (Player_Movement)data[2];
    //        //prefab.team_number = team;



    //        if (team == 1)
    //        {
    //            //prefab.player_number = playerNumber1;
    //            playerNumber1++;
    //        }
    //        else
    //        {
    //            //prefab.player_number = playerNumber2;
    //            playerNumber2++;
    //        }

    //        //stat_manager.AddMember(team, prefab.player_number, name);
    //    }
    //}

    public void StartGame()
    {
        Stats_Page.SetActive(false);
        StartBorder.SetActive(false);
        //InitializeTimer();
        puckObj = PhotonNetwork.Instantiate(PuckPrefab.name, new Vector2(puck_spawn.transform.position.x, puck_spawn.transform.position.y), Quaternion.identity, 0);
        

    }

    //private IEnumerator Timer()
    //{
    //    yield return new WaitForSeconds(1f);
    //    currentMatchTime -= 1;

    //    if(currentMatchTime <= 0)
    //    {
    //        timerCourtine = null;
    //        gameEnd = true;
    //    }
    //    else
    //    {
    //       // Debug.Log("timer...?");
    //        RefreshTimer_S();
    //        RefreshTimer();
    //        timerCourtine = StartCoroutine(Timer());
    //    }
    //}

    //public void RefreshTimer_S()
    //{
    //    object[] package = new object[] { currentMatchTime };
    //    PhotonNetwork.RaiseEvent(
    //        Timer_raise_event,
    //        package,
    //        new RaiseEventOptions { Receivers = ReceiverGroup.All }, 
    //        new SendOptions { Reliability = true }
    //        );
    //}

    //public void Team_S(string name, int team, Player_Movement prefab)
    //{
    //    object[] package = new object[] { name, team, prefab};
    //    PhotonNetwork.RaiseEvent(
    //        Team_send_event,
    //        package,
    //        new RaiseEventOptions { Receivers = ReceiverGroup.All },
    //        new SendOptions { Reliability = true }
    //        );
    //}

    //public void Team_R(object[] data)
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        string name = (string)data[0];
    //        int team = (int)data[1];
    //        Player_Movement prefab = (Player_Movement)data[2];
    //        prefab.team_number = team;
            

            
    //        if (team == 1)
    //        {
    //            prefab.player_number = playerNumber1;
    //            playerNumber1++;
    //        }
    //        else
    //        {
    //            prefab.player_number = playerNumber2;
    //            playerNumber2++;
    //        }

    //        stat_manager.AddMember(team, prefab.player_number, name);
    //    }
    //}


    //public void RefreshTimer_R(object[] data)
    //{
    //    currentMatchTime = (int)data[0];
    //    RefreshTimer();
    //}

    //public void OnEvent(EventData photonEvent)
    //{
    //    if (photonEvent.Code >= 200) return;

    //    byte e = photonEvent.Code;
    //    object[] o = (object[])photonEvent.CustomData;

    //    switch (e)
    //    {
    //        case Timer_raise_event:
    //            RefreshTimer_R(o);
    //            break;
    //        case Team_send_event:
    //            Team_R(o);
    //            break;

    //    }
    //}
}
