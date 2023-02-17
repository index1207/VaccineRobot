using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)]
    private float speed = 5f;
    public int power;
    private Rigidbody2D rigid;

    private bool isEnemy;
    public bool IsEnemy
    {
        get
        {
            return isEnemy;
        }
        set
        { 
            isEnemy = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if(isEnemy) rigid.velocity = Vector2.down * speed;
        else rigid.velocity = Vector2.up * speed;
    }
}
