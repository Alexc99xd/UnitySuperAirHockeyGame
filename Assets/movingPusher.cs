using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPusher : MonoBehaviour
{
    public float speed;
    public float cycleTime;
    private float time = 0;
    private bool forward = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > cycleTime)
        {
            time = 0;
            forward = !forward;
        } 
        else
        {
            if (forward)
            {
                //move forward
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                //move backwards
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            }

        }
    }
}
