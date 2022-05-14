using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string playerName;

    [SerializeField]
    private TMP_Text scoreDisplay;
    [SerializeField]
    private TMP_Text nameDisplay;

    public int Points { get; private set; }


    public void addPoint()
    {
        Points++;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreDisplay.text = Points.ToString();
    }


    private void Awake()
    {
        playerName = gameObject.name;
        Debug.Log(playerName);
    }

    // Start is called before the first frame update
    void Start()
    {
        nameDisplay.text = playerName;
        Debug.Log("Start was called");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
