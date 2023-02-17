using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVirus : Virus
{
    private Animator anim;

    public override void OnDie()
    {
        base.OnDie();
        GameManager.Instance.StartNext();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        anim.SetTrigger("OnHit");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

        useBullet = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
