using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SSFood;
    public int xLimit = 13;
    public int yLimit = 7;

    public bool directPoint;
    public Vector2 point;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void GenerateFood()
    {
        if (directPoint)
        {
            Instantiate(
            SSFood,
            point,
            Quaternion.identity,
            GameObject.Find("Grid").transform
            );
        }
        else
        {
            int x = (int)GameObject.Find("Grid").transform.position.x;
            int y = (int)GameObject.Find("Grid").transform.position.y;
            Instantiate(
            SSFood,
            new Vector2(Random.Range(-xLimit+x, xLimit+x), Random.Range(-yLimit+y, yLimit+y)),
            Quaternion.identity,
            GameObject.Find("Grid").transform
            );
        }
        
    }

}
