using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class GenerateVirus : MonoBehaviour
{
    public GameObject[] virus;
    public GameObject[] bossVirus;

    private int spawnCount = 0;

    void SpawnVirus(bool boss, Vector2 location)
    {
        if(boss)
        {
            var obj = Instantiate(bossVirus[GameManager.Instance.Stage - 1]);
            obj.transform.position = location;
        }
        else
        {
            var obj = Instantiate(virus[GameManager.Instance.Stage - 1]);
            obj.transform.position = location;
        }
        spawnCount++;
    }

    IEnumerator Generate()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
            SpawnVirus(false, new Vector2(Random.Range(-6.7f, 6.7f), 4.6f));
        }
    }

    void Start()
    {
        StartCoroutine(Generate());
    }
    void Update()
    {
        if (spawnCount == 30)
        {
            SpawnVirus(true, new Vector2(0, 4));
        }
    }
}
