using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    public PhotonView photonView;
    // Start is called before the first frame update
    public float speed;
    public Rigidbody2D rb;
    private bool mousedown = false;
    private float time = 0;
    private float FullChargeUp = 1.8f;
    private Vector2 direction;
    private float ChargeUpVelocity = 30f;

    
    private float ChargeUpShotTimer = 0.5f;
    private float ChargeUpShotTimerTicker = 0;
    private bool ChargeUpMoving = false;
    private bool ChargeUpMovingTrigger = false;
    private float ChargeDamp = 0.825f;
    private float MovingFriction = 0.97f;

    public float currentSpeed;
    public float acceleration;

    Vector2 moveDir;
    Vector2 moveDirCharge;

    public Text mTextOverHead;
    public Transform mTransform;
    public Transform mTextOverTransform;

    public GameObject arrow;

    private void Awake()
    {
        mTransform = transform;
        mTextOverTransform = mTextOverHead.transform;
        mTextOverHead.color = Color.white;

        if (photonView.IsMine)
        {
            mTextOverHead.text = PhotonNetwork.NickName;
        }
        else
        {
            mTextOverHead.text = photonView.Owner.NickName;
        }
    }

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mTransform.position);
        screenPos.x += 50;
        screenPos.y += 40; 
        mTextOverTransform.position = screenPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            arrow.transform.up = -direction;
            currentSpeed = rb.velocity.magnitude;
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
                }
                else
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
                    if (PowerMult < 0.25f)//15 percent
                    {
                        PowerMult = 0.25f;
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
                if (ChargeUpShotTimerTicker < ChargeUpShotTimer)
                {
                    ChargeUpShotTimerTicker += Time.deltaTime;
                }
                else
                {
                    ChargeUpMoving = false;
                    ChargeUpShotTimerTicker = 0;
                }
                //Player just ended their charge up, they cannot move for 1 second

            }
        }

    }

    private void FixedUpdate()
    {

        if (photonView.IsMine)
        {
            if (ChargeUpMoving)
            {
                if (ChargeUpMovingTrigger)
                {
                    ChargeMove();
                    ChargeUpMovingTrigger = false;
                }

                rb.velocity = rb.velocity * ChargeDamp;
            }
            else
            {
                //if moving, have some friction
                rb.velocity *= MovingFriction;
                Move();
            }

            if (mousedown)
            {
                if (rb.velocity.magnitude > speed / 3f)
                {
                    rb.velocity = rb.velocity.normalized * speed / 3f;
                }
            }
        }

    }

    private void Move()
    {

        //rb.velocity = moveDir * speed;
        rb.velocity += moveDir * Time.fixedDeltaTime * 10;
        if(rb.velocity.magnitude >= speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void ChargeMove()
    {
        rb.velocity = moveDirCharge;
    }

    
}
