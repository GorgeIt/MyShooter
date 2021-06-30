using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Destruction : MonoBehaviourPunCallbacks
{
    public float health = 100f;
    public GameObject Player;

    public void TakeDamage(float damage)
    {
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
            health -= damage;
            if (health <= 0)
            {
                Die();
            } 
    }

    void Die() {

        StartCoroutine(reloading(5f));
        gameObject.transform.position = new Vector3(100f, 0.88f, 7.4f);
    }
    IEnumerator reloading(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);
        gameObject.transform.position = new Vector3(Random.Range(-2, 2), 0.88f, Random.Range(-2, 2));
        health = 100f;
    }
    public void Respawn()
    {
        Vector3 Position = new Vector3(Random.Range(-2, 2), 0.88f, Random.Range(-2, 2));
        PhotonNetwork.Instantiate(Player.name, Position, Quaternion.identity);
    }

    public float TakeHealth()
    {
        return health;
    }

    
}
