using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{
    enum tongueStates {ready, launch, retract, latched };

    [Tooltip("The gameobject used to represent the tongue of the frog")]
    [SerializeField] GameObject tongue;
    [Tooltip("The speed the tongue moves at")]
    [SerializeField] float tongueSpeed = 1f;
    [Tooltip("How far can the tongue go")]
    [SerializeField] float tongueRange = 1f;
    [Tooltip("What tag do objects need for the tongue to latch to them")]
    [SerializeField] string latchableTag = "latchable";
    [Tooltip("What is the input manager string needed for this to use it's tongue")]
    [SerializeField] string tongueInput = "Fire1";
    [Tooltip("The states the tongue can be in. Mostly here for debugging")]
    [SerializeField] tongueStates tongueState;

    [Tooltip("Latching will create a joint. This is for debugging to make sure it is created")]
    [SerializeField] SpringJoint joint;
    [Tooltip("The max distance multiplier for how far the grappling tongue can go")]
    [SerializeField] float jointMaxDistanceMultiplier = 0.8f;
    [Tooltip("The min distance multiplier for how far the grappling tongue can go")]
    [SerializeField] float jointMinDistanceMultiplier = 0.25f;
    [Tooltip("How much spring does the joint have")]
    [SerializeField] float jointSpring = 4.5f;
    [Tooltip("How much damper does the joint have")]
    [SerializeField] float jointDamper = 7f;
    [Tooltip("How much mass does the joint have")]
    [SerializeField] float jointMassScale = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        tongueState = tongueStates.ready;
    }

    /// <summary>
    /// Watch what state tongueState is in and does the proper action
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        switch (tongueState)
        {
            case tongueStates.ready:
                TongueInput();
                break;
            case tongueStates.launch:
                LaunchTongue();
                break;
            case tongueStates.retract:
                RetractTongue();
                break;
            case tongueStates.latched:
                Swing();
                break;
        }

    }

    /// <summary>
    /// Awaits input before firing the tongue 
    /// </summary>
    void TongueInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tongueState = tongueStates.launch;
            tongue.GetComponent<Collider2D>().enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            tongue.GetComponent<Collider2D>().enabled = false;
            tongueState = tongueStates.ready;

        }
    }

    /// <summary>
    /// The tongue launches towards a certain position infront of the player determined by tongue range
    /// </summary>
    void LaunchTongue()
    {
        Vector2 targetPosition = new Vector2(gameObject.transform.localPosition.x + tongueRange, gameObject.transform.localPosition.y);
        tongue.transform.Translate(targetPosition * Time.deltaTime * tongueSpeed, gameObject.transform);
        if (tongue.transform.localPosition.x == targetPosition.x)
        {
            tongueState = tongueStates.retract;
            tongue.GetComponent<Collider2D>().enabled = false;
        }
    }

    /// <summary>
    /// Return the tongue back to the character
    /// </summary>
    void RetractTongue()
    {
        //Return the tongue to the mouth.
        //When it is back, release the input lock. 
        tongue.transform.Translate(gameObject.transform.localPosition * Time.deltaTime * tongueSpeed, gameObject.transform);
        if (tongue.transform.localPosition == gameObject.transform.localPosition)
        {
            tongueState = tongueStates.ready;
        }
    }

    /// <summary>
    /// Latch on to an object and create a joint connecting it to the player
    /// <param name="latch">What is the gameobject that this is connecting to</param>
    void LatchOnto(GameObject latch)
    {
        tongue.transform.position = latch.transform.position;
        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = latch.transform.position;

        float distanceFromPoint = Vector2.Distance(gameObject.transform.position, latch.transform.position);
        joint.maxDistance = distanceFromPoint * jointMaxDistanceMultiplier;
        joint.minDistance = distanceFromPoint * jointMinDistanceMultiplier;

        joint.spring = jointSpring;
        joint.damper = jointDamper;
        joint.massScale = jointMassScale;
    }

    /// <summary>
    /// Wait til the player presses the tongue button again and then release the latch.
    /// </summary>
    void Swing()
    {
        if (Input.GetButton(tongueInput))
        {
            Destroy(joint);
            tongueState = tongueStates.retract;
        }
    }

    /// <summary>
    /// Check to see if the tongue hit a latchable object. If it did, go to
    /// LatchOnto() while disabling the collider to make sure weirdness doesn't happen.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(latchableTag))
        { 
            tongueState = tongueStates.latched;
            tongue.GetComponent<Collider2D>().enabled = false;
            LatchOnto(collision.gameObject);
        }
    }
}
