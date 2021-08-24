using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 6.0F;
    public float force = 15.0F;
    [SerializeField] private float forceBonus = 0;
    public float ForceBonus
    {
        get { return forceBonus; }
        set { forceBonus = value; }
    }

    public Rigidbody2D rigidBodyPlayer;
    // public SpriteRenderer[] renderers;
    public GroundDetection groundDetection;
    private Vector3 direction;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isJumping;
    [SerializeField] private Health health;
    public Health Health { get { return health; } }
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float shootForce = 20.0F;
    [SerializeField] private bool isCoolDown = false;
    public bool IsCoolDown
    {
        get { return isCoolDown; }
        set { isCoolDown = value; }
    }
    [SerializeField] private Item item;
    [SerializeField] private Image coolDownImage;
    private float currCoolDown;

    [SerializeField] private int coolDownTime = 3;
    private List<Arrow> arrowPool;
    [SerializeField] private int arrowsCount = 3;
    [SerializeField] private BuffReceiver buffReceiver;
    [SerializeField] private Camera playerCamera;
    private UICharacterController controller;


    #region Singleton
    public static Player Instance { get; set; }
    #endregion
    private void Awake()
    {
        Player.Instance = this;
        coolDownImage.gameObject.SetActive(isCoolDown);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player = this;

        arrowPool = new List<Arrow>();

        for (int i = 0; i < arrowsCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }

        buffReceiver.OnBuffsChanged += BuffProcessing;
    }

    public void InitUIController(UICharacterController uiController)
    {
        controller = uiController;
        controller.JumpButton.onClick.AddListener(Jump);
        controller.FireButton.onClick.AddListener(CheckShoot);

    }
    private void BuffProcessing()
    {
        foreach (Arrow arr in arrowPool)
            arr.TriggerDamage.DamageBonus = 0;
        ForceBonus = 0;


        for (int i = 0; i < buffReceiver.Buffs.Count; i++)
        {
            switch (buffReceiver.Buffs[i].type)
            {

                case BuffType.Damage:
                    foreach (Arrow arr in arrowPool)
                        arr.TriggerDamage.DamageBonus += (int)buffReceiver.Buffs[i].additiveBonus;
                    break;
                case BuffType.Force:
                    ForceBonus += buffReceiver.Buffs[i].additiveBonus;
                    break;
                case BuffType.Armor:
                    break;
            }
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {


        Move();


    }

    private void Update()
    {
        animator.SetBool("isGrounded", groundDetection.IsGrounded);

        if (!isJumping && !groundDetection.IsGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("GetDamage"))
            animator.SetTrigger("StartFall");
        isJumping = isJumping && !groundDetection.IsGrounded;

        /*#if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.Space))
                    Jump();
        #endif*/
    }

    private void OnDestroy()
    {
        playerCamera.transform.parent = null;
        playerCamera.enabled = true;
    }
    private void Move()
    {


        direction = Vector3.zero;
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;
#endif
        if (controller.LeftButton.IsPressed)
            direction = Vector3.left;
        if (controller.RightButton.IsPressed)
            direction = Vector3.right;

        direction *= speed;
        direction.y = rigidBodyPlayer.velocity.y;
        rigidBodyPlayer.velocity = direction;

        if (direction.x > 0)
            spriteRenderer.flipX = false;
        if (direction.x < 0)
            spriteRenderer.flipX = true;


        animator.SetFloat("speed", Mathf.Abs(direction.x));
    }
    private void Jump()
    {
        if (groundDetection.IsGrounded)
        {
            rigidBodyPlayer.AddForce(Vector2.up * (force + ForceBonus), ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }

    }
    public void SetArrow()
    {
        Arrow arrowInstance = GetArrowFromPool();
        arrowInstance.SetImpulse(Vector2.right,
            spriteRenderer.flipX ? -(force + ForceBonus) * shootForce : (force + ForceBonus) * shootForce, this);

        StartCoroutine(StartCoolDown());
    }
    void CheckShoot()
    {

        if (!isCoolDown && !isJumping && groundDetection.IsGrounded)
        {

            isCoolDown = true;
            buffReceiver.OnBuffsChanged();
            animator.SetTrigger("StartShoot");


        }


    }

    private Arrow GetArrowFromPool()
    {
        if (arrowPool.Count > 0)
        {
            var arrowTemp = arrowPool[0];
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            arrowPool.Remove(arrowTemp);
            return arrowTemp;
        }

        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
    }

    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowPool.Contains(arrowTemp))

            arrowPool.Add(arrowTemp);

        arrowTemp.gameObject.SetActive(false);
        arrowTemp.transform.parent = arrowSpawnPoint;
        arrowTemp.transform.position = arrowSpawnPoint.transform.position;

    }
    IEnumerator StartCoolDown()
    {
        coolDownImage.gameObject.SetActive(true);

        currCoolDown = 1.0f;
        float step = coolDownTime / 100.0f;

        while (currCoolDown > 0)
        {
            yield return new WaitForSeconds(step);
            currCoolDown -= (step);
            coolDownImage.fillAmount = currCoolDown;
        }
        isCoolDown = false;
        yield break;
    }



}
