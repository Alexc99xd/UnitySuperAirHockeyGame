using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerRound : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;

    public GoalCounter goal_T1;
    public GoalCounter goal_T2;

    //text
    public TextMeshProUGUI text_team1;
    public TextMeshProUGUI text_team2;

    //spawns
    public GameObject T1_spawn_A;
    public GameObject T2_spawn_A;

    public Text PingText;

    private void Awake()
    {
        GameCanvas.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        text_team1.text = "Team 1: " + 0;
        text_team2.text = "Team 2: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        PingText.text = "Ping: " + PhotonNetwork.GetPing();
    }

    public void UpdateScore()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            text_team1.text = "Team 1: " + goal_T2.goals;
            text_team2.text = "Team 2: " + goal_T1.goals;
        }

    }

    public void SpawnPlayerT1()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(T1_spawn_A.transform.position.x, T1_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        GameCanvas.SetActive(false);
    }

    public void SpawnPlayerT2()
    {
        GameObject prefab = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(T2_spawn_A.transform.position.x, T2_spawn_A.transform.position.y), Quaternion.identity, 0);
        prefab.GetComponent<SpriteRenderer>().color = Color.magenta;
        GameCanvas.SetActive(false);
    }
}
