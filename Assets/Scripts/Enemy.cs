using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnmType
    {
        Normal,
        Tele,
        Bomb
    }

    public EnmType Type;
    public float speed;
    public GameObject explosion;
    private Rigidbody enemyRb;
    private GameObject player;
    private bool go = false;
    private bool armed;
    private int tick;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(true)
        {
            if (player == null)
            {
                go = false;
            }
            if (go)
            {
                Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                enemyRb.AddForce(lookDirection * speed);
            }
            if (armed) { tick++; }
            if (transform.position.y < -2) { Blow(); }
            if (tick > 10 && Type == EnmType.Bomb)
            {
                Blow();
            }
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            armed = true;
        }
        Debug.Log("Eagle we have landed");
        go = true;
    }

    public void Blow()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 4);
        explosion.GetComponent<ParticleSystem>().Play();
        this.gameObject.tag = "gone";
        foreach (Collider hit in hitColliders)
        {
            if(hit.gameObject.tag == "Enemy")
            {
                hit.GetComponent<Enemy>().Blow();
            } else if(hit.gameObject.tag == "Player")
            {
                hit.GetComponent<PlayerControler>().Blow();
            }
        }
        Destroy(this.gameObject.GetComponent<MeshFilter>());
    }

    private void OnCollisionExit(Collision collision)
    {
        armed = false;
    }
}
