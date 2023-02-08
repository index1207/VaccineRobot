using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager manager;
    static GameManager Instance
    {
        get
        {
            return manager;
        }
        set
        {
            manager = value;
        }
    }

    public Text scoreText;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
        {
            manager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format("{0:N0}", score);
    }
}
