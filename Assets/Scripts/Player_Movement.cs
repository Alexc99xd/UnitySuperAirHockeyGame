using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Rigidbody2D rb;
    private bool mousedown = false;
    private float time = 0;
    private float FullChargeUp = 2f;
    private Vector2 direction;
    private float ChargeUpVelocity = 30f;

    
    private float ChargeUpShotTimer = 0.5f;
    private float ChargeUpShotTimerTicker = 0;
    private bool ChargeUpMoving = false;
    private bool ChargeUpMovingTrigger = false;
    private float ChargeDamp = 0.80f;

    public float currentSpeed;
    public float acceleration;

    Vector2 moveDir;
    Vector2 moveDirCharge;

    public GameObject arrow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        arrow.transform.up = -direction;

        if (!ChargeUpMoving)
        {
            //joystick or WASD movement
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDir = new Vector2(moveX, moveY).normalized;

            //get mouse down
            if (Input.GetButton("Fire1") || mousedown) //left click
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
                mousedown = true;
            }
            else
            {
                mousedown = false;
            }

            if (mousedown)
            {
                //stop movement, charge up
                time += Time.deltaTime;
                arrow.SetActive(true);
            } else
            {
                arrow.SetActive(false);
            }


            if (Input.GetButtonUp("Fire1"))
            {
                //mouse button releases
                if (time > FullChargeUp)
                {
                    time = FullChargeUp; //max power
                }

                float PowerMult = time / FullChargeUp;

                //base powerMult
                if (PowerMult < 0.15f)//15 percent
                {
                    PowerMult = 0.15f;
                }

                //Debug.Log(PowerMult);
                moveDirCharge = direction.normalized * PowerMult * ChargeUpVelocity;
                time = 0;
                mousedown = false;
                ChargeUpMoving = true;
                ChargeUpMovingTrigger = true;

            }
        } 
        else
        {
            if(ChargeUpShotTimerTicker < ChargeUpShotTimer)
            {
                ChargeUpShotTimerTicker += Time.deltaTime;
            } else
            {
                ChargeUpMoving = false;
                ChargeUpShotTimerTicker = 0;
            }
            //Player just ended their charge up, they cannot move for 1 second

        }
        

    }

    private void FixedUpdate()
    {
        if (ChargeUpMoving)
        {
            if (ChargeUpMovingTrigger)
            {
                ChargeMove();
                ChargeUpMovingTrigger = false;
            }
            rb.velocity = rb.velocity * ChargeDamp;
        } else
        {
            Move();
        }

    }

    private void Move()
    {

        rb.velocity = moveDir * speed;
    }

    private void ChargeMove()
    {
        rb.velocity = moveDirCharge;
    }
}
