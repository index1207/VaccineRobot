using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VaccineRobot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject powerBullet;

    [SerializeField]
    private float speed = 2.25f;

    private SpriteRenderer render;
    private Rigidbody2D rigid;
    private Animator anim;

    private bool isInv = true;
    private float fireCool = 0;

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

    void Fire(int bpm)
    {
        fireCool -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (fireCool <= 0)
            {
                var blt = Instantiate(bullet);
                blt.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, 0);
                fireCool = 60.0f / bpm;
            }
        }
    }

    IEnumerator Blink()
    {
        isInv = true;
        for(int i = 0; i < 5; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            render.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            render.color = Color.white;
        }
        isInv = false;
    }

    void ActivateInv()
    {
        StartCoroutine(Blink());
    }

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire(300);
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    ActivateInv();
        //}
    }
}
