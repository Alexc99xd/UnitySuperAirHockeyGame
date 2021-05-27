using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public PhotonView view;
    private bool started = false;
    private bool finished = false;
    public float time;
    public TextMeshProUGUI text;
    public GameManagerRound manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (started)
            {
                time -= Time.deltaTime;
                text.text = time.ToString();
                view.RPC("updateTime", RpcTarget.All, time.ToString());
                if (time <= 0)
                {
                    started = false;
                    finished = true;
                    manager.EndGame();
                }


            }
        }

    }

    [PunRPC]
    public void updateTime(string time)
    {
        text.text = time;
    }

    public void StartGame()
    {
        finished = false;
        started = true;
        time = 180;
    }
}
