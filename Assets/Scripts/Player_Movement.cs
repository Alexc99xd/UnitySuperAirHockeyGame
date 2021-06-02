using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    public string name_player;
    public PhotonView photonView;
    // Start is called before the first frame update
    public float speed;
    public Rigidbody2D rb;
    private bool mousedown = false;
    private float time = 0;
    private float FullChargeUp = 2f;
    private Vector2 direction;
    private float ChargeUpVelocity = 25f;

    private float ShotTimer = 0.25f;
    private float currentShotTimer = 0;

    
    private float ChargeUpShotTimer = 0.1f;
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
    public GameObject power1;
    public GameObject power2;
    public GameObject powerGage;

    public int team_number; //team 1 or 2. Not team 0 doesn't make sense
    public int player_number; //player 1,2,3. starts at 1 because I hate myself


    private void Awake()
    {
        mTransform = transform;
        mTextOverTransform = mTextOverHead.transform;
        mTextOverHead.color = Color.white;

        if (photonView.IsMine)
        {
            mTextOverHead.text = PhotonNetwork.NickName;
            name_player = PhotonNetwork.NickName;
            StringsManager.ChangeString(team_number, player_number, name_player);
        }
        else
        {
            mTextOverHead.text = photonView.Owner.NickName;
            name_player = photonView.Owner.NickName;
            StringsManager.ChangeString(team_number, player_number, name_player);
        }
    }

    void Start()
    {
        power1.SetActive(false);
        power2.SetActive(false);
        powerGage.SetActive(false);
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
            currentShotTimer -= Time.deltaTime;
            arrow.transform.up = -direction;
            currentSpeed = rb.velocity.magnitude;
            //if (!ChargeUpMoving)
            //{
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
                powerGage.SetActive(true);
                //stop movement, charge up
                time += Time.deltaTime;
                arrow.SetActive(true);
                if(time >= 0.65f)
                {
                    power1.SetActive(true);
                    power2.SetActive(true);
                }

            }
            else
            {
                arrow.SetActive(false);
                powerGage.SetActive(false);
                power1.SetActive(false);
                power2.SetActive(false);
            }


            if (Input.GetButtonUp("Fire1"))
            {
                //mouse button releases
                if (time > FullChargeUp)
                {
                    time = FullChargeUp + 1; //max power
                }

                float PowerMult = time / FullChargeUp;

                //base powerMult
                if (time <= 0.65f)//50 percent
                {
                    PowerMult = 0f;
                    moveDirCharge = direction.normalized * PowerMult * ChargeUpVelocity;
                    time = 0;
                    mousedown = false;
                }
                else
                {
                    PowerMult = 1f;
                    moveDirCharge = direction.normalized * PowerMult * ChargeUpVelocity;
                    time = 0;
                    mousedown = false;
                    ChargeUpMovingTrigger = true;
                    currentShotTimer = ShotTimer;
                }

            }
            //}
            //else
            //{
            //    if (ChargeUpShotTimerTicker < ChargeUpShotTimer)
            //    {
            //        ChargeUpShotTimerTicker += Time.deltaTime;
            //    }
            //    else
            //    {
            //        ChargeUpMoving = false;
            //        ChargeUpShotTimerTicker = 0;
            //    }

            //}


            //if (ChargeUpShotTimerTicker < ChargeUpShotTimer)
            //{
            //    ChargeUpShotTimerTicker += Time.deltaTime;
            //}
            //else
            //{
            //    ChargeUpMoving = false;
            //    ChargeUpShotTimerTicker = 0;
            //}
        }

    }

    private void FixedUpdate()
    {

        if (photonView.IsMine)
        {
            //if (ChargeUpMoving)
            //{
            //    if (ChargeUpMovingTrigger)
            //    {
            //        ChargeMove();
            //        ChargeUpMovingTrigger = false;
            //    }

            //    rb.velocity = rb.velocity * ChargeDamp;
            //}
            if (ChargeUpMovingTrigger)
            {
                ChargeMove();
                ChargeUpMovingTrigger = false;
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
        rb.velocity += moveDir * Time.fixedDeltaTime * 12.5f; //accel
        if(rb.velocity.magnitude >= speed)
        {
             rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void ChargeMove()
    {
        //rb.velocity = moveDirCharge;
        rb.AddForce(moveDirCharge*100);
    }

    
}
