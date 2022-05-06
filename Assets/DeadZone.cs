using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    [SerializeField]
    Player opponent;

    [SerializeField]
    GameObject ballPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        opponent.addPoint();
        Destroy(collision.gameObject);
        Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
    }

}
