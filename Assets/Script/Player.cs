using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public float velocidade;
    public float velocidadeDeCorrer = 1.5f;
    private Vector3 moveDirecao;
    public float gravidade = -9.81f;
    private Vector3 moveParaBaixo;

    public Transform objPulo;
    public float Distancia = 0.4f;
    public LayerMask  layerMask;
    public bool podePular;
    public float forcaPulo = 3f;
    public float rotacao = 90f;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Movimentacao()
    {
        podePular = Physics.CheckSphere (objPulo.position, Distancia, layerMask);

        if(podePular && moveParaBaixo.y < 0)
        {
            moveParaBaixo.y = -2f;
        }
        transform.rotation = Quaternion.Euler(0, rotacao, 0);
        if(controller.isGrounded)
        {
            if(Input.GetKey(KeyCode.D))
            {
                animator.SetInteger("Animacao", 1);
                velocidade = 0.5f;

                 rotacao = 90f;  //Mathf.Atan2(moveDirecao.x,moveDirecao.z) * Mathf.Rad2Deg;
                
                print (rotacao);

                if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) )    
                {
                    velocidade = velocidadeDeCorrer;
                    animator.SetInteger("Animacao", 4);
                }
                if(Input.GetKey(KeyCode.D) && Input.GetButtonDown("Jump") && podePular )
                {                    
                     animator.SetInteger("Animacao", 5);
                     moveParaBaixo.y = Mathf.Sqrt(forcaPulo * -2f * gravidade);
                }
                moveDirecao = Vector3.right * velocidade;   
            }
            if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                moveDirecao = Vector3.zero;
                animator.SetInteger("Animacao", 0);
            }
            if(Input.GetKey(KeyCode.A))
            {
                rotacao = -90;
                moveDirecao = -Vector3.right * velocidade;  
                animator.SetInteger("Animacao", 1);
                velocidade = 0.5f; 
            }
            
        }

        if(Input.GetButtonDown("Jump") && podePular)
        {
            animator.SetInteger("Animacao", 5);
            moveParaBaixo.y = Mathf.Sqrt(forcaPulo * -2f * gravidade);
        }
      
      
        moveParaBaixo.y += gravidade * Time.deltaTime;
        // moveDirecao = transform.TransformDirection(moveDirecao);
        controller.Move(moveDirecao * Time.deltaTime);
        controller.Move(moveParaBaixo * Time.deltaTime);

        if(Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetInteger("Animacao", 3);
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetInteger("Animacao", 0);
        }
   
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();
    }
}
