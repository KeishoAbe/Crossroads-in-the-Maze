using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class CharacterControl : MonoBehaviour
{

    public float animSpeed = 1.5f;       //アニメーション再生速度
    public float lookSmoother = 3.0f;   //カメラの動きの設定スムージング
    public bool useCurves = true;

    public bool isChangeAngle = false;
    public float useCurvesHeight = 0.5f;
    public int _cameratype = 0;

    //--------------------------------
    private Vector3 moveDirection,
                    cameraDirection;

    private float rotateX = 0,
                    rotateY = 0;
    //--------------------------------

    //キャラクターコントロール用パラメーター
    [SerializeField]
    private float forwardSpeed = 7.0f,   //前進速度
                 backwardSpeed = 7.0f,  //後退速度
                 rotateSpeed = 2.0f,    //旋回速度
                 jumpPower = 3.0f;      //ジャンプ量

    private CapsuleCollider col;        //カプセルコライダの参照
    private Rigidbody rib;              //リジッドボディの参照

    private Vector3 velocity;           //カプセルコライダの移動量
    private float orgColHeght;
    private Vector3 orgVecColCenter;

    private Animator anim;                          //アタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBasestate;     //base layerで使われる、アニメーターの現在の状態の参照

    private GameObject cameraObject;                //メインカメラへの参照

    //各アニメーターのステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle"),
               runState = Animator.StringToHash("Base Layer.Run"),
               jumpState = Animator.StringToHash("Base Layer.Jump"),
               Attack1 = Animator.StringToHash("Base Layer.Attack1"),
               Attack2 = Animator.StringToHash("Base Layer.Attack2"),
               Attack3 = Animator.StringToHash("Base Layer.Attack3");

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rib = GetComponent<Rigidbody>();

        cameraObject = GameObject.FindWithTag("MainCamera");
        orgColHeght = col.height;
        orgVecColCenter = col.center;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Input.GetJoystickNames();
        anim.SetFloat("Speed", vertical);
        //anim.SetFloat("Direction", horizontal);
        anim.speed = animSpeed;
        currentBasestate = anim.GetCurrentAnimatorStateInfo(0);
        rib.useGravity = true;

        velocity = new Vector3(horizontal, 0, vertical);
        velocity = transform.TransformDirection(velocity);

        if (vertical > 0.1) { velocity *= forwardSpeed; }
        else if (vertical < -0.1) { velocity *= forwardSpeed; }
        else if (horizontal > 0.1) { velocity *= forwardSpeed; }
        else if (horizontal < -0.1) { velocity *= forwardSpeed; }

        JumpProc();

        //各攻撃アニメーション処理
        PrayerAttack1();
        PlayerAttack2();
        PlayerAttack3();

        //上下キーによるキャラクター移動
        transform.localPosition += velocity * Time.fixedDeltaTime;
        //左右キーによるキャラクター旋回

        rotateX = Input.GetAxis("Vertical");
        rotateY = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(rotateY, 0, rotateX);
        transform.LookAt(transform.position + moveDirection);


        if (currentBasestate.fullPathHash == runState)
        {
            if (useCurves)
            {
                resetCollider();
            }
        }
    }//-----------------------------------------------


    private void PlayerAttack3()
    {
        //XboxではYの割り当てがJumpになってるので今回はJumpに設定
        if (Input.GetButton("Jump"))
        {
            if (currentBasestate.fullPathHash == idleState)
            {
                if (!anim.GetBool("Attack3"))
                {
                    anim.SetBool("Attack3", true);
                }
            }
        }
        else if (currentBasestate.fullPathHash == Attack3)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Attack3", false);
            }
        }
    }

    private void PlayerAttack2()
    {
        if (Input.GetButton("Fire3"))
        {
            if (currentBasestate.fullPathHash == idleState)
            {
                if (!anim.GetBool("Attack2"))
                {
                    anim.SetBool("Attack2", true);
                }
            }
        }
        else if (currentBasestate.fullPathHash == Attack2)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Attack2", false);
            }
        }
    }

    private void PrayerAttack1()
    {
        if (Input.GetButton("Fire2"))
        {
            if (currentBasestate.fullPathHash == idleState)
            {
                if (!anim.GetBool("Attack1"))
                {
                    anim.SetBool("Attack1", true);
                }
            }
        }
        else if (currentBasestate.fullPathHash == Attack1)
        {

            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Attack1", false);
            }
        }
    }

    private void JumpProc()
    {
        //スペースキー入力によるジャンプ処理
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentBasestate.fullPathHash == idleState || currentBasestate.fullPathHash == runState)
            {
                if (!anim.IsInTransition(0))
                {
                    if (!anim.GetBool("Jump"))
                    {
                        rib.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                        anim.SetBool("Jump", true);
                    }
                }
            }
        }
        //ジャンプ中の処理
        else if (currentBasestate.fullPathHash == jumpState)
        {
            //ステートがトラジション中でない時
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Jump", false);
            }
        }
        //Idle中の処理
        else if (currentBasestate.fullPathHash == idleState)
        {
            if (useCurves) { resetCollider(); }
        }
    }

    //キャラクターコライダーサイズのリセット関数
    //コンポーネントのHeight,Centerの初期値を戻す
    void resetCollider()
    {
        col.height = orgColHeght;
        col.center = orgVecColCenter;
    }

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);

        if (layerName == "CameraTopPosStage")
        {
            _cameratype = 1;
        }
    }

    void OnTriggerExit(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);

        if (layerName == "CameraTopPosStage")
        {
            _cameratype = 0;
        }
    }


}
