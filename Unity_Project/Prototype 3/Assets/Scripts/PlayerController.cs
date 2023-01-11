using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isGameOver = false;
    public bool isDash = false;
    private int jumpCount = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && jumpCount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            // 점프 모션
            playerAnim.SetTrigger("Jump_trig");
            jumpCount--;
            // 흙먼지 이펙트 정지, 점프 사운드 실행
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound);
        }

        if (!isGameOver && Input.GetKeyDown(KeyCode.Z))
        {
            isDash = true;
            playerAnim.SetBool("Dash_b", true);
        }
        if (!isGameOver && Input.GetKeyUp(KeyCode.Z))
        {
            isDash = false;
            playerAnim.SetBool("Dash_b", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌체(collision)가 바닥(Ground)이면
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 2;
            dirtParticle.Play();
        }
        // 충돌체가 방해물(Obstacle)이면
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            // 흙먼지 이펙트 정지, 충돌 이펙트 실행, 충돌사운드 실행
            dirtParticle.Stop();
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);

            Debug.Log("GameOver");
        }
    }
}
