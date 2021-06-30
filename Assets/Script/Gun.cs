using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Gun : MonoBehaviour {
    
    public float distance = 100f;
    public float damage = 10f;
    public Camera fpsCam;
    public GameObject hole;
    public float force;
    public Destruction destruct;
    private int bullets = 30;
    private bool reload = false;

    private float nexttime = 0f;


    GameObject magazin;
    GameObject health;
    Text text;
    Image healthbarImage;


    public New_Weapon_Recoil_Script recoil;

    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("health");
        healthbarImage = health.GetComponent<Image>();
        magazin = GameObject.FindGameObjectWithTag("Magaz");
        text = magazin.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {
        text.text = "" + bullets;
        healthbarImage.fillAmount = destruct.TakeHealth() / 100;
        if (Input.GetMouseButton(0) && Time.time >= nexttime && bullets>0 && !reload)
        {
            recoil.Fire();
            nexttime = Time.time + 0.1f;
            bullets--;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && bullets != 30 && !reload)
        {
            StartCoroutine(reloading(2.5f));
            reload = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
            destruct.TakeDamage(50f);
    }
    IEnumerator reloading(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);
        bullets = 30;
        reload = false;
    }
    void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, distance))
        {
            GameObject effect = Instantiate(hole, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 2f);
            Destruction destructable = hit.transform.GetComponent<Destruction>();
            if (destructable != null) {
                destructable.TakeDamage(damage);
            }
            Debug.Log(hit.rigidbody);
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }
    }
}
