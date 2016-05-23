using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class CharacterControl : MonoBehaviour
{

    public float m_AnimSpeed = 1.5f;       //アニメーション再生速度
    public float m_LookSmoother = 3.0f;   //カメラの動きの設定スムージング
    public bool m_UseCurves = true;

    public bool m_IsChangeAngle = false;
    public float m_UseCurvesHeight = 0.5f;
    public int m_CameraType = 0;

    //--------------------------------
    private Vector3 m_MoveDirection,
                    m_CameraDirection;

    private float m_RotateX = 0,
                    m_RotateY = 0;
    //--------------------------------

    //キャラクターコントロール用パラメーター
    [SerializeField]
    private float m_ForwardSpeed = 7.0f,   //前進速度
                  m_BackwardSpeed = 7.0f,  //後退速度
                  m_RotateSpeed = 2.0f,    //旋回速度
                  m_JumpPower = 3.0f;      //ジャンプ量

    private CapsuleCollider m_Col;        //カプセルコライダの参照
    private Rigidbody m_Rib;              //リジッドボディの参照

    private Vector3 m_Velocity;           //カプセルコライダの移動量
    private float m_OrgColHeght;
    private Vector3 m_OrgVecColCenter;

    private Animator m_Anim;                          //アタッチされるアニメーターへの参照
    private AnimatorStateInfo m_CurrentBaseState;     //base layerで使われる、アニメーターの現在の状態の参照

    private GameObject m_CameraObject;                //メインカメラへの参照

    //各アニメーターのステートへの参照
    static int m_IdleState = Animator.StringToHash("Base Layer.Idle"),
               m_RunState = Animator.StringToHash("Base Layer.Run"),
               m_JumpState = Animator.StringToHash("Base Layer.Jump"),
               m_Attack1 = Animator.StringToHash("Base Layer.Attack1"),
               m_Attack2 = Animator.StringToHash("Base Layer.Attack2"),
               m_Attack3 = Animator.StringToHash("Base Layer.Attack3");

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Col = GetComponent<CapsuleCollider>();
        m_Rib = GetComponent<Rigidbody>();

        m_CameraObject = GameObject.FindWithTag("MainCamera");
        m_OrgColHeght = m_Col.height;
        m_OrgVecColCenter = m_Col.center;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Input.GetJoystickNames();

        m_Anim.SetFloat("SpeedRight", horizontal);
        m_Anim.speed = m_AnimSpeed;
        m_CurrentBaseState = m_Anim.GetCurrentAnimatorStateInfo(0);
        m_Rib.useGravity = true;

        //
        LookRunState(horizontal, vertical);

        JumpProc();

        //各攻撃アニメーション処理
        PrayerAttack1();
        PlayerAttack2();
        PlayerAttack3();

        //カメラの位置と軸のデータ取得と左スティック入力による移動処理
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = cameraForward * (vertical * 0.1f) + Camera.main.transform.right * (horizontal * 0.1f);
        transform.position += direction;
        transform.LookAt(transform.position + direction);


        if (m_CurrentBaseState.fullPathHash == m_RunState)
        {
            if (m_UseCurves)
            {
                resetCollider();
            }
        }
    }//-----------------------------------------------

    private void LookRunState(float horizontal, float vertical)
    {
        if (vertical == 0 && horizontal == 0)
        {
            m_Anim.SetBool("Run", false);
        }
        else if (vertical > 0.5f)
        {
            m_Anim.SetBool("Run", true);
        }
        else if (vertical < -0.5f)
        {
            m_Anim.SetBool("Run", true);
        }
        else if (horizontal > 0.5f)
        {
            m_Anim.SetBool("Run", true);
        }
        else if (horizontal < -0.5f)
        {
            m_Anim.SetBool("Run", true);
        }
    }

    private void PlayerAttack3()
    {
        //XboxではYの割り当てがJumpになってるので今回はJumpに設定
        if (Input.GetButton("Jump"))
        {
            if (m_CurrentBaseState.fullPathHash == m_IdleState)
            {
                if (!m_Anim.GetBool("Attack3"))
                {
                    m_Anim.SetBool("Attack3", true);
                }
            }
        }
        else if (m_CurrentBaseState.fullPathHash == m_Attack3)
        {
            if (!m_Anim.IsInTransition(0))
            {
                m_Anim.SetBool("Attack3", false);
            }
        }
    }

    private void PlayerAttack2()
    {
        if (Input.GetButton("Fire3"))
        {
            if (m_CurrentBaseState.fullPathHash == m_IdleState)
            {
                if (!m_Anim.GetBool("Attack2"))
                {
                    m_Anim.SetBool("Attack2", true);
                }
            }
        }
        else if (m_CurrentBaseState.fullPathHash == m_Attack2)
        {
            if (!m_Anim.IsInTransition(0))
            {
                m_Anim.SetBool("Attack2", false);
            }
        }
    }

    private void PrayerAttack1()
    {
        if (Input.GetButton("Fire2"))
        {
            if (m_CurrentBaseState.fullPathHash == m_IdleState)
            {
                if (!m_Anim.GetBool("Attack1"))
                {
                    m_Anim.SetBool("Attack1", true);
                }
            }
        }
        else if (m_CurrentBaseState.fullPathHash == m_Attack1)
        {

            if (!m_Anim.IsInTransition(0))
            {
                m_Anim.SetBool("Attack1", false);
            }
        }
    }

    private void JumpProc()
    {
        //スペースキー入力によるジャンプ処理
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_CurrentBaseState.fullPathHash == m_IdleState || m_CurrentBaseState.fullPathHash == m_RunState)
            {
                if (!m_Anim.IsInTransition(0))
                {
                    if (!m_Anim.GetBool("Jump"))
                    {
                        m_Rib.AddForce(Vector3.up * m_JumpPower, ForceMode.VelocityChange);
                        m_Anim.SetBool("Jump", true);
                    }
                }
            }
        }
        //ジャンプ中の処理
        else if (m_CurrentBaseState.fullPathHash == m_JumpState)
        {
            //ステートがトラジション中でない時
            if (!m_Anim.IsInTransition(0))
            {
                m_Anim.SetBool("Jump", false);
            }
        }
        //Idle中の処理
        else if (m_CurrentBaseState.fullPathHash == m_IdleState)
        {
            if (m_UseCurves) { resetCollider(); }
        }
    }

    //キャラクターコライダーサイズのリセット関数
    //コンポーネントのHeight,Centerの初期値を戻す
    void resetCollider()
    {
        m_Col.height = m_OrgColHeght;
        m_Col.center = m_OrgVecColCenter;
    }

    void OnTriggerEnter(Collider col)
    {
        string layerName = LayerMask.LayerToName(col.gameObject.layer);

        if (layerName == "CameraTopPosStage")
        {
            m_CameraType = 1;
        }
    }

    void OnTriggerExit(Collider col)
    {
        string layerName = LayerMask.LayerToName(m_Col.gameObject.layer);

        if (layerName == "CameraTopPosStage")
        {
            m_CameraType = 0;
        }
    }


}
