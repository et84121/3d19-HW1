using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour
{
    List<Transform> Bodylist = new List<Transform>();

    //prefab body object
    public GameObject Body;
    private bool isEaten = false;
    private bool isGameEnded = false;

    public float gameSpeed = 0.1f;
    public float burstTime = 0.5f;

    Vector2 direction = Vector2.up;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = gameSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameEnded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isGameEnded = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction = Vector2.up;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction = Vector2.left;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction = Vector2.down;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction = Vector2.right;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(FiveSecondsCooldown());
            }
        }
    }
    void FixedUpdate()
    {
        if (!isGameEnded)
        {
            //get snakeHead current position
            Vector3 vpos = transform.position;
            //move snake
            transform.Translate(direction);

            if (isEaten)
            {
                GameObject Bodyper = Instantiate(Body, vpos, Quaternion.identity, GameObject.Find("Grid").transform);
                Bodylist.Insert(0, Bodyper.transform);
                isEaten = false;
            }


            if (Bodylist.Count > 0)
            {
                Bodylist[Bodylist.Count - 1].position = vpos;
                Bodylist.Insert(0, Bodylist[Bodylist.Count - 1]);
                Bodylist.RemoveAt(Bodylist.Count - 1);
            }

            //caculate socre
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + Bodylist.Count;
        }
    }

    public IEnumerator FiveSecondsCooldown()
    {
        Time.timeScale = gameSpeed * 4;
        yield return new WaitForSeconds(burstTime); // 
        Time.timeScale = gameSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Destroy(other.gameObject);
            isEaten = true;
            // generate another food
            GameObject.Find("GameController").GetComponent<GameController>().GenerateFood();
        }
        // detecte snakeHead if eat snake self body
        else if (other.tag == "Body")
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "You eat youself body!!\nPress space-bar to start another game";
            GameObject.Find("ScoreText").GetComponent<Text>().fontSize = 105;
            isGameEnded = true;
        }
        else
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "You hit the Wall!!\nPress space-bar to start another game";
            GameObject.Find("ScoreText").GetComponent<Text>().fontSize = 105;
            isGameEnded = true;
        }
    }
}
