
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MASKMAN : MonoBehaviour
{
    [Header("Target")]
    private Transform Player;

    [Header("HP")]
    [SerializeField] private int hp_Max;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Image hpImg;
    private bool isBeingDead = false;
    private int hp_Current;

    [Header("Jump")]
    [SerializeField] private Transform checkDown;
    private bool isTouchingDown;
    [SerializeField] private float checkRadius;
    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject _explosive;
    private Explosive explosive;

    [Header("Teleport")]
    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;
    private float midpoint;
    private bool isLeft=false;

    [Header("SummonSaw")]
    [SerializeField] private GameObject[] SummonSaws;
    private IEnumerator coroutineSummonSaw;

    [Header("Other")]
    [SerializeField] private GameObject DoorTrigger; 
    private int direction;
    private bool isStage2=false;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private GameObject SpawnRocks;

    void Start()
    {
        explosive = _explosive.GetComponent<Explosive>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hp_Current = hp_Max;
        midpoint = (PointA.position.x + PointB.position.x) / 2;
        coroutineSummonSaw = summonSaw();
        hpImg.gameObject.SetActive(true);
        hpBar.gameObject.SetActive(true);

    }

    private void OnEnable()
    {
        isBeingDead=false;
    }

    void Update()
    {
        hpBar.value = hp_Current;
        if (Player == null && isBeingDead == false)
        {
            isBeingDead = true;
            animator.SetTrigger("IsDead");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack"))
        {
            TakeDamage();
        }
    }


    public void TakeDamage()
    {
        hp_Current -= 1;
        if (hp_Current <= 0 && isStage2 ==false)
        {
            hp_Current = hp_Max;
            animator.SetTrigger("StageTwo");
            Physics2D.IgnoreLayerCollision(3, 22);
            Physics2D.IgnoreLayerCollision(23, 21);
            Physics2D.IgnoreLayerCollision(23, 21);
            isStage2 = true;
        }
        if(hp_Current <= 0 && isBeingDead==false && isStage2 == true)
        {
            isBeingDead = true;
            Physics2D.IgnoreLayerCollision(3, 22);
            Physics2D.IgnoreLayerCollision(23, 21);
            Physics2D.IgnoreLayerCollision(23, 21);
            animator.SetTrigger("IsDead");
        }
    }

    private void Death()
    {
        if (isBeingDead == true)
        {
            DoorTrigger.GetComponent<DoorTrigger>().CancelTrigger();
        }
    }

    public void RockDown()
    {
        SpawnRocks.SetActive(true);
    }

    public void Teleport()
    {
        if (Player != null)
        {
            if (Player.position.x - midpoint > 0)
            {
                transform.position = new Vector2(PointA.position.x, transform.position.y);
                isLeft = true;
            }
            else
            {
                transform.position = new Vector2(PointB.position.x, transform.position.y);
                isLeft = false;
            }
            Physics2D.IgnoreLayerCollision(3, 22, false);
            Physics2D.IgnoreLayerCollision(23, 21, false);
            Physics2D.IgnoreLayerCollision(23, 21, false);
            ChangedDirection();
            StartCoroutine(coroutineSummonSaw);
        }
    }

    #region Jump
    public void Jump(float jump_VectorX, float jump_VectorY)
    {
        if (checkDown != null)
        {
            isTouchingDown = Physics2D.OverlapCircle(checkDown.position, checkRadius, groundLayer);
        }
        if (isTouchingDown == true)
        {
            RockDown();
            explosive.Explosivee();
            SoundManager.Instance.PlaySound(jumpSoundEffect);
            ChangedDirection();

            rb.velocity = new Vector2(direction * jump_VectorX, jump_VectorY);
        }
        animator.SetFloat("VelocityY", rb.velocity.y);
    }

    public void ChangedDirection()
    {
        if (Player != null)
        {
            if (transform.position.x - Player.position.x > 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            if (direction == -1)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
        }
    }
    #endregion

    #region SummonSaw
    private IEnumerator summonSaw()
    {
        for(int j =19; j <25; j++)
        {
            SummonSaws[j].SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        for (int j = 19; j < 25; j++)
        {
            SummonSaws[j].SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        for (int j = 8; j < 19; j++)
        {
            SummonSaws[j].SetActive(true);
            SummonSaws[j].SetActive(false);
        }

        yield return new WaitForSeconds(2f);

        int n;
        if (isLeft == true)
        {
            n = 4;
        }
        else
        {
            n = 0;
        }

        int i=1+n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }
        i++;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        i=0+n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

        yield return new WaitForSeconds(1.5f);
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }
        i++;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

        yield return new WaitForSeconds(3f);
        i = 0 + n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }
        i++;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }
        i=3 + n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

        yield return new WaitForSeconds(2f);
        i = 0 + n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }
        i=2 + n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }
        i++;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        i = 0 + n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        i = 0 + n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        i = 0 + n;
        if (SummonSaws[i] != null)
        {
            SummonSaws[i].SetActive(true);
            SummonSaws[i].SetActive(false);
        }

    }

    #endregion
}
