using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
            if (transform.position.y < -10) { Explode(); }
            if (tick > 50 && Type == EnmType.Bomb)
            {
                Explode();
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

    public void Explode()
    {
        Destroy(this.gameObject.GetComponent<MeshRenderer>());
        this.gameObject.tag = "gone";
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 2);
        explosion.GetComponent<ParticleSystem>().Play();
        foreach (Collider hit in hitColliders)
        {
            if(hit.gameObject.tag == "Enemy")
            {
                hit.GetComponent<EnemyController>().Explode();
            } else if(hit.gameObject.tag == "Player")
            {
                hit.GetComponent<PlayerController>().Explode();
            }
        }
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        Debug.Log("Wait and!");
        yield return new WaitForSeconds(2);
        Debug.Log("Destroy!");
        Destroy(this.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        armed = false;
    }
}
