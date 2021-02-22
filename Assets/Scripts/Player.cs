using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxhp = 3;
    public int hp = 3;
    Animator animator;
    SpriteRenderer sprite;
    [SerializeField, Range(0.1f, 20f)]
    float moveSpeed = 2f;
    [SerializeField, Range(0.1f, 15f)]
    float jumpForce = 5;
    Rigidbody2D rb2D;
    [SerializeField]
    ContactFilter2D groundFilter;
    PlayerControls playerControls;
    [SerializeField]
    float invulnSeconds = 1;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip hurtSoundEffect;
    [SerializeField]
    AudioClip victorySoundEffect;
    [SerializeField]
    AudioClip defeatSoundEffect;
    bool isInvulnerable;
    [SerializeField]
    GameObject lossScreen;
    [SerializeField]
    GameObject winScreen;
    
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();   
    }
    void Start()
    {
        playerControls.Gameplay.Jump.performed += ctx => Jump();
        playerControls.Gameplay.Power.performed += ctx => Power();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable() 
    {
        playerControls.Disable();
    }

    void Update()
    {
        transform.Translate(Vector2.right * axis.x * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if(IsGrounded)
        {
            animator.SetTrigger("jump");
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Power()
    {
        if(IsGrounded)
        {
            animator.SetTrigger("power");
        }
    }

    void LateUpdate() 
    {
        sprite.flipX = flipSprite;
        animator.SetFloat("moveX", Mathf.Abs(axis.x));
    }

    void FixedUpdate() 
    {
        animator.SetBool("isGrounded", IsGrounded);
    }

    Vector2 axis => playerControls.Gameplay.Movement.ReadValue<Vector2>();
    bool flipSprite => axis.x > 0 ? false : axis.x < 0 ? true : sprite.flipX;
    bool IsGrounded => rb2D.IsTouching(groundFilter);

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy")) 
        {
            if (!isInvulnerable)
            {
                hp--;
                AudioSource.PlayClipAtPoint(hurtSoundEffect, transform.position);

                if (hp <= 0)
                {
                    moveSpeed = 0f;
                    lossScreen.SetActive(true);
                    audioSource.Stop();
                    AudioSource.PlayClipAtPoint(defeatSoundEffect, transform.position);
                    BecomeInvulnerableForSeconds(15f);
                    StartCoroutine(RestartSceneAfterSeconds(14f));
                    return;
                }
            }
        }

        else if (other.CompareTag("Cat"))
        {
            audioSource.Stop();
            AudioSource.PlayClipAtPoint(victorySoundEffect, transform.position);
            winScreen.SetActive(true);
            moveSpeed = 0f;
            StartCoroutine(RestartSceneAfterSeconds(15f));
        }
    }

    IEnumerator RestartSceneAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HealPlayer()
    {
        if (maxhp > hp)
        {
            hp++;
        }
    }

    public void BecomeInvulnerableForSeconds(float seconds)
    {
        isInvulnerable = true;
        StartCoroutine(LoseInvulnerabilityAfterSeconds(seconds));
    }

    IEnumerator LoseInvulnerabilityAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isInvulnerable = false;
    }

    public void SpeedBoostForSeconds(float seconds, float speedBoost)
    {
        moveSpeed += speedBoost;
        StartCoroutine(LoseSpeedAfterSeconds(seconds, speedBoost));
    }

    IEnumerator LoseSpeedAfterSeconds(float seconds, float speedBoost)
    {
        yield return new WaitForSeconds(seconds);
        moveSpeed -= speedBoost;
    }
}
