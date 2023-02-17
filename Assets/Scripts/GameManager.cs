using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; set; }
    public int Score { get; set; }
    public int Hp { get; set; }
    public int Pain { get; set; }

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text stageText;

    [SerializeField]
    private Slider pain;

    [SerializeField]
    private Slider hp;

    public int Stage { get; set; }

    [SerializeField]
    private GameObject vaccineRobot;

    private float startTime = 3;
    private GameObject vaccine;
    
    public IEnumerator StartNext()
    {
        Stage++;
        vaccine = Instantiate(vaccineRobot, new Vector2(0, -6), Quaternion.identity);
        vaccine.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
        yield return new WaitForSeconds(1.5f);
        vaccine.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        Stage = 0;
        Score = 0;
        Hp = 100;
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Stage == 0)
        {
            startTime -= Time.deltaTime;
            print(startTime);
            if (startTime < 0f)
            {
                StartCoroutine(StartNext());
            }
        }
        
        pain.value = Pain / 100f;
        hp.value = Hp / 100f;

        scoreText.text = string.Format("{0:N0}", Score);
        stageText.text = $"Stage : {Stage}";
    }
}
