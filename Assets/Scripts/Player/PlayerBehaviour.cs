using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviour : MonoBehaviour
{

    public bool canMove;
    public bool CanMove
    {
        get
        {
            return canMove;
        }

        set
        {
            canMove = value;
        }
    }

    [Header("Testando no Celular?")]
    private bool onMobile; //para saber se estamos no mobile, controle manual

    [Header("Velocidades")]
    public float runMultiplier = 2; //multiplicador de velocidade quando estiver correndo
    public float walkingSpeed = 5; //velocidade padrao de movimento
    public float runningSpeed = 10; //velocidade ao correr
    public float sneakingSpeed = 4; //velocidade de sneaking

    [Header("Velocidade Final")]
    public float actualSpeed; //velocidade atual

    [Header("Estados do Player")]
    public bool running; //controle para saber se esta correndo ou não
    public bool sneaking; //controle para saber se esta em modo sneaking ou não
    public bool walking; //controle para sabe se esta andando ou ano

    private float directionX; //valor do input do jogador
    private GameController gameController;

    public GameObject PlayerMesh;
    private Animator PlayerAnimator;

    public bool facingRight;

    public bool canRun;
    public bool canSneak;
    public bool canAttack;

    private bool readyToInteract;
    Vector2 clickedPosition;
    RaycastHit2D hit;
    private string clickedObjectName;
    private bool interacted;
    private bool interact;
    private bool clickedToMove = false;
    public GameObject clickedObject;



    // Use this for initialization
    void Start()
    {
        canMove = true;


        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // adiciona o script TriggeerComentarios ao player caso ja nao o tenha
        if (GetComponent<TriggersComentarios>() == null)
        {
            gameObject.AddComponent<TriggersComentarios>();
        }

        onMobile = gameController.GetOnMobile();

        walking = true;
        sneaking = false;
        running = false;
        facingRight = true;

        PlayerAnimator = PlayerMesh.GetComponent<Animator>();

    }

    void Update()
    {
        HandleMouseClick();
        InteractionTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canMove == true)
        {
            PlayerMovement();
        }
    }

    void PlayerMovement()
    {
        // Condição para verificar se estamos jogando no pc ou no celular
        //Caso estejamos no pc o jogo recebe input do teclado
        //se estiver no mobile, receberá input do joystick na tela

        if (!onMobile)
        {
            directionX = Input.GetAxis("Horizontal"); //input do teclado
        }
        else
        {
            directionX = CrossPlatformInputManager.GetAxis("Horizontal"); //input do joystick
        }

        //Código para pegar quando o jogador apertar shift esquerdo para correr
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //ToggleRun(); //função de correr            
            running = true;
            PlayerAnimator.SetBool("Running", true);
            actualSpeed *= runMultiplier;
        }

        //Código para pegar quando o jogador apertar control esquerdo para
        //entrar no modo sneaking
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleSneaking(); //função para sneakar
        }

        if (facingRight && directionX < 0)
        {
            Flip();
        }
        else if (!facingRight && directionX > 0)
        {
            Flip();
        }

        //velocidade utilizada na movimentação recebe a 
        //velocidade de andar por padrao
        actualSpeed = walkingSpeed;
        walking = true; //jogador sempre está andando?

        //Para utilizar os botoões de trigger descomenta a linha abaixo
        //changeSpeed(); MOdifica a velocidade

        //Enquanto o jogador pressionar o botão de correr
        //o jogador terá sua velocidade multiplicada por "runMultiplier"
        if (CrossPlatformInputManager.GetButton("Run"))
        {
            PlayerAnimator.SetBool("Running", true);
            actualSpeed *= runMultiplier;
            running = true;
        }
        else
        {
            PlayerAnimator.SetBool("Running", false);
            running = false;
        }

        //linha que movimento o jogador
        
        
            transform.Translate(new Vector3(directionX * actualSpeed * Time.deltaTime, 0f, 0f));
        

        if (directionX != 0)
        {
            PlayerAnimator.SetBool("Walking", true);
        }
        else
            PlayerAnimator.SetBool("Walking", false);
    }

    public void Flip()
    {
        facingRight = !facingRight;
        PlayerMesh.GetComponent<SpriteRenderer>().flipX = !facingRight;
    }
    /// <summary>
    /// Função que modifica a velocidade de acordo com o toggle que esta ativo
    /// </summary>
    void changeSpeed()
    {
        if (running)
        {
            actualSpeed = runningSpeed;
        }
        else if (sneaking)
        {
            actualSpeed = sneakingSpeed;
        }
        else if (walking)
        {
            actualSpeed = walkingSpeed;
        }
    }

    #region Gets and Sets

    public void SetCanRun(bool value)
    {
        canRun = value;
    }
    public void SetCanSneak(bool value)
    {
        canSneak = value;
    }
    public void SetCanAttack(bool value)
    {
        canAttack = value;
    }


    /// <summary>
    /// Ativa o modo de correr
    /// </summary>
    public void ToggleRun()
    {
        running = !running;
        walking = false;
        sneaking = false;

    }

    /// <summary>
    /// Ativa modo de andar
    /// </summary>
    public void ToggleWalking()
    {
        walking = !walking;
        running = false;
        sneaking = false;
    }

    /// <summary>
    /// Ativa modo sneaking
    /// </summary>
    public void ToggleSneaking()
    {
        sneaking = !sneaking;
        running = false;
        walking = false;
    }
    #endregion

    // lida com tudo o que acontece ao clicar
    private void HandleMouseClick()
    {
        // verificar se foi pressionado o botão esquerdo do mouse:
        //if (Input.GetMouseButtonDown(0))

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Cliquei");
            if (!gameController.GetMouseOverUI() && !gameController.onDialogue)
            {
                HandleClickOnObjects();
            }
        }
    }


    // verifica se clicou em objeto interativo
    private void HandleClickOnObjects()
    {
        clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(clickedPosition, Vector2.zero);
        //Debug.Log("handle mouse on object");
        if (hit != null && hit.collider != null)
        {
            //if (hit.collider.gameObject.transform.tag == "Interactive")
            //{
            //    clickedObject = hit.collider.gameObject;
            //    clickedObjectName = hit.collider.gameObject.name;
            //    Debug.Log("Cliquei em " + clickedObjectName);
            //    readyToInteract = false;
            //    interacted = false;
            //    interact = true;
            //}

            clickedObject = hit.collider.gameObject;
            if (clickedObject.tag == "Interactive")
            {
                clickedObjectName = hit.collider.gameObject.name;
                Debug.Log("Cliquei em " + clickedObjectName);
                readyToInteract = false;
                interacted = false;
                interact = true;
            }
        }
    }

    // chamada quando o jogador para de andar e se clicou em um objeto interativo
    private void InteractionTime()
    {
        if (interact /*&& readyToInteract*/)
        {

            if (!interacted)
            {
                // Debug.Log("Entrou em InteractionTime");
                CallInteraction(clickedObject, clickedObjectName);
                interacted = true;
                readyToInteract = false;
                interact = false;
            }
        }
    }
    // informa ao game controller qual foi o objeto clicado
    private void CallInteraction(GameObject clickedObject, string clicked_object_name)
    {
        // Debug.Log("Entrou em Player.CallInteraction");
        gameController.Interaction(clickedObject, clicked_object_name);
    }
}
