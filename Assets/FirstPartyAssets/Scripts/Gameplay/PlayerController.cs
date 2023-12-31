using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField] Rigidbody rb;
    private Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip steps;
    public MimosBar mimosBar;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        AnimatorType();

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            animator.SetBool("IsWalking", true);
            MovePlayerRelativeToCamera();
            animator.SetBool("IsBad", false);
            animator.SetBool("IsMid", false);
            animator.SetBool("IsOkey", false);
            //rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        if (animator.GetBool("IsWalking") && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(steps);
        }
        
    }

    void MovePlayerRelativeToCamera()
    {
        //Importar los datos de los vectores
        float playermovX = Input.GetAxis("Horizontal");
        float playermovZ = Input.GetAxis("Vertical");

        //Traer las coordenadas de la camara
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        //El valor de y de la camara no influye en el movimiento del personaje
        forward.y = 0;
        right.y = 0;

        //Normalizar los valores
        forward = forward.normalized;
        right = right.normalized;

        //Multiplicar los valores de los vectores del jugador y la camara
        Vector3 forwardRelativeVerticalInput = playermovZ * forward;
        Vector3 rightRelativeHorizontalInput = playermovX * right;

        //Crear una camara relativa al movimiento
        Vector3 cameraRelativeMovement = (forwardRelativeVerticalInput + rightRelativeHorizontalInput).normalized;

        float targetAngle = Mathf.Atan2(cameraRelativeMovement.x, cameraRelativeMovement.z) *Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        transform.Translate(cameraRelativeMovement * speed * Time.fixedDeltaTime, Space.World);
    }

    public void AnimatorType()
    {
        if (mimosBar.index == 0)
        {
            animator.SetBool("IsBad", true);
            animator.SetBool("IsMid", false);
            animator.SetBool("IsOkey", false);
        }
        else if (mimosBar.index == 1)
        {
            animator.SetBool("IsBad", false);
            animator.SetBool("IsMid", true);
            animator.SetBool("IsOkey", false);
        }
        else if (mimosBar.index == 2)
        {
            animator.SetBool("IsBad", false);
            animator.SetBool("IsMid", false);
            animator.SetBool("IsOkey", true);
        }
    }
}
