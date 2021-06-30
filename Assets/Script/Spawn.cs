using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn : MonoBehaviour
{
    public GameObject Player;
    public float minx, minz, maxx, maxz;

    void Start()
    {
        Respawn();
    }
    public void Respawn()
    {
        Vector3 Position = new Vector3(Random.Range(minx, maxx), 0.88f, Random.Range(minz, maxz));
        PhotonNetwork.Instantiate(Player.name, Position, Quaternion.identity);
    }
}
