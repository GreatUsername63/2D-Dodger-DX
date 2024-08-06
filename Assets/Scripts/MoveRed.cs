using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveRed : MonoBehaviour
{
    private float speed = 10f;
    private float limit = -12f; //Coordinate on x to dissapear
    private SpawnManager spawnerReference;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        int speedRNG = Random.Range(0,3);
        if(speedRNG >= 2){
            speed += 8f;
        }

        spawnerReference = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.isGameActive) return;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(transform.position.x <= limit){
            Destroy(gameObject);
        }
    }

    //Destroy and spawn a new one in it's place
    private void OnDestroy() {
        spawnerReference.SpawnRoid(true);
    }
}
