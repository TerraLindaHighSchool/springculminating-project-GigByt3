using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float speed;
    public bool hasPowerup;
    public GameObject explosion;
    public GameObject powerupIndicator;
    private float powerupStrength = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        } else
            if(other.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (transform.position - other.gameObject.transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

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

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

}
