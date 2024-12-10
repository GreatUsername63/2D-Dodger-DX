using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The blue one
public class PlayerControllerX : MonoBehaviour
{
    private float speed = 10f;
    private float lowerLimit = -6.5f;
    private float upperLimit = 7.5f;
    GameManager gameManager;
    SpawnManager spawnManager;
    AudioManager audioManager;
    Invecibility invecibilityScript;
    public GameObject plusOneText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        spawnManager = SpawnManager.Instance;
        audioManager = AudioManager.Instance;
        invecibilityScript = GetComponent<Invecibility>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
    }

    void MovePlayer()
    {
        if (gameManager.isGameOver) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }

    void ConstrainPlayerPosition()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, lowerLimit, upperLimit), transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (invecibilityScript.isActive)
            {
                spawnManager.SpawnRoid(true);
                spawnManager.decreaseRoidCount(true);
                GameObject.Instantiate(plusOneText, gameObject.transform);
                audioManager.PlaySmallExplosionSound();
                gameManager.scoreTime += 1;
                Destroy(other.gameObject);
                return;
            }
            Debug.Log("X dies here");
            gameManager.GameOver();
            Destroy(gameObject);
        }
        if (other.CompareTag("Powerup"))
        {
            Debug.Log("X gets power");
            gameManager.startInvencibility(false);
            Destroy(other.gameObject);
        }
    }
}
