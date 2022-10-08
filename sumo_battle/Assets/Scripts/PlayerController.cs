using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private Rigidbody playerRb;
    public float forwardInput;
    private GameObject foaclPoint;
    public bool hasPowerup = false;
    private float powerupstrength = 15.0f;
    public GameObject powerupIndacator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //get the focalpoint of the camera
        foaclPoint = GameObject.Find("Focalpoint");
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        //move the player in respect to the camera
        playerRb.AddForce(foaclPoint.transform.forward * speed * forwardInput);
        //set the powerup position to the players position
        powerupIndacator.transform.position = transform.position + new Vector3(0,-0.6f,0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = new(0,0,0);
        }

    }
    /// <summary>
    /// Triger collition with the powerup
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            hasPowerup = true;  
            Destroy(other.gameObject);
            //show the powerup indicator
            powerupIndacator.gameObject.SetActive(true);
            //starts the timer for the powerup
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    /// <summary>
    /// method for setting the powerup timer
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7); 
        hasPowerup = false;
        powerupIndacator.gameObject.SetActive(false);

    }
    /// <summary>
    /// Collision with the enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy") && hasPowerup){
            //get the rigid body of the enemy
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            //get the enemy closer to the player
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            //push the enemy away from player
            enemyRb.AddForce(awayFromPlayer * powerupstrength,ForceMode.Impulse);
            Debug.Log("Collision with " + collision.gameObject.name + "has poewr up set to " + hasPowerup); 
        }
    }
}
