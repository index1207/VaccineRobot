using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VaccineRobot : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField]
    [Range(2f, 10f)]
    private float speed;

    private SpriteRenderer render;
    private Rigidbody2D rigid;
    private Animator anim;

    private float fireCool = 0;

    bool isDamaged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDamaged)
        {
            if (collision.gameObject.layer == 3)
            {
                GameManager.Instance.Hp -= collision.gameObject.GetComponent<Bullet>().power;
                StartCoroutine(Blink());

                isDamaged = true;
                gameObject.layer = 3;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isDamaged)
        {
            if(collision.gameObject.layer == 3)
            {
                GameManager.Instance.Hp -= collision.gameObject.GetComponent<Virus>().power / 2;
                StartCoroutine(Blink());

                isDamaged = true;
                gameObject.layer = 3;
            }
        }
    }

    void Move()
    {
        rigid.velocity = Vector2.zero;
        anim.SetBool("isMoving", false);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rigid.velocity += Vector2.up * speed;
            anim.SetBool("isMoving", true);
            anim.SetFloat("Direction", rigid.velocity.x);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rigid.velocity += Vector2.down * speed;
            anim.SetBool("isMoving", true);
            anim.SetFloat("Direction", rigid.velocity.x);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rigid.velocity += Vector2.left * speed;
            anim.SetBool("isMoving", true);
            anim.SetFloat("Direction", rigid.velocity.x);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rigid.velocity += Vector2.right * speed;
            anim.SetBool("isMoving", true);
            anim.SetFloat("Direction", rigid.velocity.x);
        }
    }

    IEnumerator Blink()
    {
        int count = 0;
        while(count < 10)
        {
            if(count % 2 == 0)
            {
                render.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                render.color = new Color(1, 1, 1, 1);
            }
            yield return new WaitForSeconds(0.15f);

            count++;
        }
        isDamaged = false;
        gameObject.layer = 0;
    }

    void Fire(int bpm)
    {
        fireCool -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (fireCool <= 0)
            {
                Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
                fireCool = 60.0f / bpm;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameManager.Instance.Pain = GameManager.Instance.Stage == 1 ? 10 : 30;
    }

    // Update is called once per frame
    void Update()
    {
        Fire(550);
        Move();
    }
}
