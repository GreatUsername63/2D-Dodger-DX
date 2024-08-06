using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject smallRoidRed;
    public GameObject bigRoidRed;
    public GameObject smallRoidBlue;
    public GameObject bigRoidBlue;
    public GameObject powerupRed;
    public GameObject powerupBlue;



    private float spawnMinHeight = 12f;
    private float spawnAddedHeight = 48f;
    private float spawnRange = 7.5f;
    public bool isSpawnerActive = true;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 11; i++){
            SpawnRoid(true);
            SpawnRoid(false);
        }
        InvokeRepeating("spawnPowerup",20f,30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Spawns a roid
    //Dim: false equals red, true equals blue
    public void SpawnRoid(bool dim){
        if(!isSpawnerActive){
            return;
        }

        float randomSpawnCoord = Random.Range(-spawnRange,spawnRange-1);

        //Decide at what height to spawn it
        float spawnHeight = spawnMinHeight + Random.Range(0,spawnAddedHeight);
        
        //Determines if the new roid should be big or not
        bool isBig = false;
        if(Random.Range(1,11) > 9){
            isBig = true;
        }

        //if(!dim&&!isBig) then use the red roid
        GameObject selectedPrefab = smallRoidRed;
        if(dim&&!isBig){
            selectedPrefab = smallRoidBlue;
        }
        if(!dim&&isBig){
            selectedPrefab = bigRoidRed;
        }
        if(dim&&isBig){
            selectedPrefab = bigRoidBlue;
        }

        Vector3 newPosition = new Vector3(spawnHeight,selectedPrefab.transform.position.y,randomSpawnCoord);
        if(dim){
            newPosition = new Vector3(randomSpawnCoord,selectedPrefab.transform.position.y,spawnHeight);
        }
        
        Instantiate(selectedPrefab,newPosition,selectedPrefab.transform.rotation);
    }

    void spawnPowerup(){
        if(!isSpawnerActive){
            return;
        }
        int dim = Random.Range(0,2);
        float randomSpawnCoord = Random.Range(-spawnRange+1,spawnRange);

        //Decide at what height to spawn it
        float spawnHeight = spawnMinHeight;

        GameObject selectedPrefab = powerupRed;
        if(dim==1){
            selectedPrefab = powerupBlue;
        }

        Vector3 newPosition = new Vector3(spawnHeight,selectedPrefab.transform.position.y,randomSpawnCoord);
        if(dim==1){
            newPosition = new Vector3(randomSpawnCoord,selectedPrefab.transform.position.y,spawnHeight);
        }
        
        Instantiate(selectedPrefab,newPosition,selectedPrefab.transform.rotation);
    }
}
