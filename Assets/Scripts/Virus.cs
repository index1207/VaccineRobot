using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Virus : MonoBehaviour
{
    public int hp;
    public float speed;
    public bool useBullet;
    public GameObject bullet;
    public int power;

    [SerializeField]
    private Sprite[] images;
    Rigidbody2D rigid;
    SpriteRenderer render;

    int killScore = 10;
    private float fireCool = 0.8f;

    public void Fire(int bpm)
    {
        fireCool -= Time.deltaTime;
        if (fireCool <= 0)
        {
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 1f), Quaternion.identity);
            fireCool = 60.0f / bpm;
        }
    }

    private void OnHit(int damage)
    {
        hp -= damage;
        render.sprite = images[1];
        Invoke("ReturnImg", 0.1f);
    }

    private void ReturnImg()
    {
        render.sprite = images[0];
    }

    public virtual void OnDie()
    {
        GameManager.Instance.Score += killScore;
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            OnHit(collision.gameObject.GetComponent<Bullet>().power);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GameManager.Instance.Pain += bullet.GetComponent<Bullet>().power / 2;
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        rigid.velocity = Vector3.down * speed;
        rigid.gravityScale = 0;

        GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;

        useBullet = false;
    }

    protected virtual void Update()
    {
        if(useBullet) Fire(80);
        if (hp <= 0)
        {
            OnDie();
        }
    }
}
