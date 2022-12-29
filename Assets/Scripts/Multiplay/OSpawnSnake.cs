using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OSpawnSnake : MonoBehaviour
{
    // Start is called before the first frame update
    public OSnake oSnakePrefab;
    void Start()
    {
        int rand_x = Random.Range(-10, 10);
        int rand_y = Random.Range(-10, 10);
        GameObject go = (GameObject) PhotonNetwork.Instantiate(oSnakePrefab.name, new Vector2(rand_x, rand_y), Quaternion.identity);
        OSnake snake = go.GetComponent<OSnake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
