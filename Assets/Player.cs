using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private string playerName = "Player";

    public int Points { get; private set; }


    public void addPoint()
    {
        Points++;
        Debug.Log("Added point, now has " + Points + " points.");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
