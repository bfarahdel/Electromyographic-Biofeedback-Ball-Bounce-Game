using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBounce : MonoBehaviour
{
    public GameObject SensorData; // DON'T FORGET: add wrmhlRead to hierarchy and check it off
    wrmhlRead wrmhlRead;
    private string feedback;
    private bool FeedCheck;

    public Rigidbody rb;
    private float direction;
    private bool DirectCheck;

    public float BaseJump;
    private float Jump;
    public float JumpAdd; // adjust how high to jump after each correct attempt
    private bool TriggerZone = false;

    public Text score;
    int count;
    public Text highscore;

    void PrintScore ()
    {
        score.text = "Score: " + count;

        if (count > PlayerPrefs.GetInt("High Score", 0))
        {
            PlayerPrefs.SetInt("High Score", count);
            highscore.text = "High Score: " + count.ToString();
        }
        else
        {
            highscore.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        wrmhlRead = SensorData.GetComponent<wrmhlRead>(); // get sensor component
        count = 0;
        //PlayerPrefs.DeleteAll(); // run to delete high score
        PrintScore();
    }

    private void Update()
    {
        direction = rb.transform.InverseTransformDirection(rb.velocity).y;
        DirectCheck = direction < 0; // player traveling downwards

        feedback = wrmhlRead.data; // get data variable from wrmhlReadScript file
        PrintScore();
    }

    // Hitting the LaunchPad
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LaunchPad")
        {
            if (TriggerZone)
            {
                Jump = Jump + JumpAdd;
                TriggerZone = false;
                count = count + 1;
                FindObjectOfType<AudioManager>().Play("Correct");
            }
            else
            {
                Jump = BaseJump;
                count = 0;
            }

            rb.AddForce(Vector3.up * Jump);
        }
    }

    // In the cylinder zone, correct it from working when traveling upwards
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "JumpCheck")
        {
            FeedCheck = feedback == "Yes"; // Arduino sensor value greater than threshold
            if (FeedCheck && DirectCheck)
            {
                TriggerZone = true;
            }

            // Using Space bar
            /*if (Input.GetKeyDown(KeyCode.Space) && DirectCheck)
            {
                TriggerZone = true;
            }*/
        }
    }
    
}
