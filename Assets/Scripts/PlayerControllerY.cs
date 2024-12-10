using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The red one
public class PlayerControllerY : MonoBehaviour
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
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
    }

    void ConstrainPlayerPosition()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, lowerLimit, upperLimit));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (invecibilityScript.isActive)
            {
                spawnManager.SpawnRoid(false);
                spawnManager.decreaseRoidCount(false);
                GameObject.Instantiate(plusOneText, gameObject.transform);
                audioManager.PlaySmallExplosionSound();
                gameManager.scoreTime += 1;
                Destroy(other.gameObject);
                return;
            }
            Debug.Log("Y dies here");
            gameManager.GameOver();
            Destroy(gameObject);
        }
        if (other.CompareTag("Powerup"))
        {
            Debug.Log("Y gets power");
            gameManager.startInvencibility(true);
            Destroy(other.gameObject);
        }
    }
}
