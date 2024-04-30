using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class FrogMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private GameObject tonguePrefab;
    [SerializeField] private float fartForce = 500f;
    private bool canFart;
    public static event Action OnFart;
    public bool isGrounded =true;
    [SerializeField] public GameObject fartPrefab;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canFart)
        {
            Debug.Log("Farted");
            canFart = false;
            StartCoroutine(Fart());
        }

        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jumped");
            canFart = true;
            rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce));
        }
       
        

    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {


        


            if (collision.gameObject.CompareTag("Platform"))
            {
                isGrounded = true;
                canFart = false;
            }
               

        if (collision.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Dead!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }

    private IEnumerator Fart() {
        OnFart?.Invoke();
        GameObject fart = Instantiate(fartPrefab, transform.position, Quaternion.identity);

        rb2d.AddForce(new Vector2(fartForce, fartForce));
        yield return new WaitForSeconds(0.5f);
        Destroy(fart);
    }

   


}
