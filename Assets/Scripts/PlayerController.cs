using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float speed;
    public bool[] hasPowerup; //[Invulnerable, Stop, Anhilate]
    public GameObject explosion;
    public GameObject powerupIndicator;
    private float powerupStrength = 800.0f;
    // Start is called before the first frame update
    void Start()
    {
        hasPowerup = new bool[3];
        speed = 15;
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        if (transform.position.y < 20) { playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); }
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if (transform.position.y < -10) { Explode(); }
        if (Input.GetKeyDown("2") && hasPowerup[1]) { Powerup2Fire(); hasPowerup[1] = false; }
        if (Input.GetKeyDown("3") && hasPowerup[2]) { Powerup3Fire(); hasPowerup[2] = false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided!" + other.CompareTag("Powerup1") + other.CompareTag("Powerup2") + other.CompareTag("Powerup3"));
        if(other.CompareTag("Powerup1"))
        {
            hasPowerup[0] = true;
            Destroy(other.gameObject);

            StartCoroutine(Powerup1CountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);

        } else if (other.CompareTag("Powerup2"))
        {
            hasPowerup[1] = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Powerup3"))
        {
            hasPowerup[2] = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") /*&& hasPowerup[0]*/)
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (transform.position - other.gameObject.transform.position);
            enemyRigidbody.AddForce(-awayFromPlayer * powerupStrength, ForceMode.Impulse);

        }
    }

    public void Explode()
    {
        Destroy(this.gameObject.GetComponent<MeshRenderer>());
        this.gameObject.tag = "gone";
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 2);
        explosion.GetComponent<ParticleSystem>().Play();
        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.tag == "Enemy")
            {
                hit.GetComponent<EnemyController>().Explode();
            }
            else if (hit.gameObject.tag == "Player")
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


    IEnumerator Powerup1CountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup[0] = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void Powerup2Fire()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void Powerup3Fire()
    {
        this.gameObject.tag = "gone";
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 2);
        explosion.GetComponent<ParticleSystem>().Play();
        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.tag == "Enemy")
            {
                hit.GetComponent<EnemyController>().Explode();
            }
            else if (hit.gameObject.tag == "Player")
            {
                hit.GetComponent<PlayerController>().Explode();
            }
        }
        this.gameObject.tag = "gone";
        powerupIndicator.gameObject.SetActive(false);
    }
}
