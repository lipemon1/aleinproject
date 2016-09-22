using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


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

    public bool DeveEsconderLifeBar1
    {
        get
        {
            return DeveEsconderLifeBar;
        }

        set
        {
            DeveEsconderLifeBar = value;
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
    public Collider2D viktorCollider;

    [Header("Variáveis para Combate")]
    public Transform hand;
    public Transform punchLimit;
    public bool hitSomeEnemy;
    public Animator enemyAnimator;
    public bool isAttacking = false;

    [Tooltip("controle para que só aplique dano no objeto dps do tempo da animação")]
    public bool podeLancarDano;
    public float tempoAnimacao;
    public float tempoPadraoAnimacao = 1f;
    public bool gettinHit = false;

    public bool estaNaInstalacao = false;

    [Header("Outros")]
    private float directionX; //valor do input do jogador
    private GameController gameController;

    public GameObject PlayerMesh;
    private Animator PlayerAnimator;

    public bool facingRight;

    public bool canRun;
    public bool canSneak;
    public bool canAttack;


    public AudioSource[] andarSfx;

    private bool readyToInteract;
    Vector2 clickedPosition;
    RaycastHit2D hit;
    private string clickedObjectName;
    private bool interacted;
    private bool interact;
    private bool clickedToMove = false;
    public GameObject clickedObject;

    public float Life;
    public GameObject sliderLife;
    public Text textLife;

    public bool hitPorta;
    public PortaBehaviour portaBehav;

    [Header("Flipar No Início")]
    public bool flipOneMoreTime = false;

    private bool DeveEsconderLifeBar;

    // Use this for initialization
    void Start()
    {
        canMove = true;

        tempoAnimacao = tempoPadraoAnimacao;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        if (sliderLife == null)
        {
            sliderLife = GameObject.FindGameObjectWithTag("LifeSlider");
        }
        if (textLife == null)
        {
            textLife = GameObject.FindGameObjectWithTag("LifeText").GetComponent<Text>();
        }

        // adiciona o script TriggeerComentarios ao player caso ja nao o tenha
        if (GetComponent<TriggersComentarios>() == null)
        {
            gameObject.AddComponent<TriggersComentarios>();
        }

        onMobile = gameController.GetOnMobile();
        Life = 100;

        walking = true;
        sneaking = false;
        running = false;
        facingRight = true;

        PlayerAnimator = PlayerMesh.GetComponent<Animator>();

        if (flipOneMoreTime)
        {
            Flip();
        }

        runningSpeed = walkingSpeed * runMultiplier;
    }

    public void HideLifeBar()
    {
        sliderLife.SetActive(false);
    }
    public void ShowLifeBar()
    {
        sliderLife.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F9))
        {
            Life = 100;
        }
        if (podeLancarDano == false)
        {
            if (tempoAnimacao > 0)
            {
                tempoAnimacao -= Time.deltaTime;
            }
            if (tempoAnimacao < 0)
            {
                podeLancarDano = true;
                tempoAnimacao = tempoPadraoAnimacao;
            }

        }

        HandleMouseClick();
        InteractionTime();

        if (DeveEsconderLifeBar1 == true)
        {
            HideLifeBar();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AplicarDano(10);
        }
        if (Life <= 0)
        {
            Life = 0;
            gameController.GameOver();
        }
        AndarSfx();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canMove == true)
        {
            PlayerMovement();
            if (estaNaInstalacao == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
                {
                        Attack();
                }
            }
        }
        if (estaNaInstalacao == true)
            CalculateAttack();
    }

    void CalculateAttack()
    {

        Debug.DrawLine(hand.position, punchLimit.position, Color.blue); //desenha uma linha para debug (ver na Scene)

        RaycastHit2D hit = Physics2D.Raycast(hand.position, punchLimit.position, (punchLimit.position.x - hand.position.x));

        if (hit != null && hit.collider != null)
        {

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hitSomeEnemy = true;
                enemyAnimator = hit.collider.gameObject.GetComponent<Animator>();
            }
            else if (hit.collider.gameObject.CompareTag("PortaQuebravel"))
            {

                hitPorta = true;

                portaBehav = hit.collider.gameObject.GetComponent<PortaBehaviour>();
            }
            else
            {
                hitSomeEnemy = false;
                hitPorta = false;
            }
        }
    }

    void EnableAttack()
    {
        isAttacking = false;
    }

    void Attack()
    {
        isAttacking = true;
        Invoke("EnableAttack", 0.5f);
        int punch;
        if (directionX != 0) //esta em movimento
        {
            punch = (int)Random.Range(1, 3);
            PlayerAnimator.SetTrigger("Punch" + punch.ToString());
        }
        else
        {
            punch = (int)Random.Range(1, 4);
            PlayerAnimator.SetTrigger("Punch" + punch.ToString());
        }
        if (hitPorta)
        {
            if (podeLancarDano == true)
            {

                podeLancarDano = false;
                portaBehav.AplicarDano(10);
            }
        }

        if (hitSomeEnemy)
        {
            switch (punch)
            {
                case 1:
                    Invoke("KillEnemyPunched", 0.42f);
                    break;
                case 2:
                    Invoke("KillEnemyPunched", 0.42f);
                    break;
                case 3:
                    Invoke("KillEnemyPunched", 0.23f);
                    break;
            }
        }
    }

    void KillEnemyPunched()
    {
        enemyAnimator.SetTrigger("Punched");
        enemyAnimator.gameObject.GetComponent<EnemyBehaviour>().KillEnemy();
    }

    public void AplicarDano(float value)
    {
        Life -= value;
        sliderLife.GetComponent<Slider>().value = Life;
        textLife.text = "" + Life;
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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            //ToggleRun(); //função de correr            
            running = true;
            PlayerAnimator.SetBool("Running", running);
            actualSpeed = runningSpeed;
        }
        else
        {
            //ToggleRun(); //função de correr            
            running = false;
            PlayerAnimator.SetBool("Running", running);
            actualSpeed = walkingSpeed;
        }

        if (!running)
        {
            if (estaNaInstalacao == true)
            {
                //Código para pegar quando o jogador apertar control esquerdo para
                //entrar no modo sneaking
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    sneaking = true;
                    PlayerAnimator.SetBool("Sneaking", sneaking);
                    actualSpeed = sneakingSpeed;
                    viktorCollider.offset = new Vector2(0, -1.45f);
                }
                else
                {
                    sneaking = false;
                    PlayerAnimator.SetBool("Sneaking", sneaking);
                    actualSpeed = walkingSpeed;
                    viktorCollider.offset = new Vector2(0, 1.31f);
                }
            }
        }

        /*
        //Código para pegar quando o jogador apertar control esquerdo para
        //entrar no modo sneaking
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleSneaking(); //função para sneakar
        }
        */

        if (facingRight && directionX < 0)
        {
            Flip();
        }
        else if (!facingRight && directionX > 0)
        {
            Flip();
        }

        if (sneaking && walking)
        {
            PlayerAnimator.SetBool("Sneaking", true);
        }
        else
        {
            PlayerAnimator.SetBool("Sneaking", false);
        }

        //velocidade utilizada na movimentação recebe a 
        //velocidade de andar por padrao
        //actualSpeed = walkingSpeed;
        walking = true; //jogador sempre está andando?

        //Para utilizar os botoões de trigger descomenta a linha abaixo
        //changeSpeed(); MOdifica a velocidade

        //Enquanto o jogador pressionar o botão de correr
        //o jogador terá sua velocidade multiplicada por "runMultiplier"
        if (onMobile)
        {
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
        }


        //linha que movimento o jogador

        //para saber para onde ir já que o persongem não está flipando
        if (directionX < 0)
        {
            transform.Translate(new Vector3(-directionX * actualSpeed * Time.deltaTime, 0f, 0f));
        }
        else if (directionX > 0)
        {
            transform.Translate(new Vector3(directionX * actualSpeed * Time.deltaTime, 0f, 0f));
        }



        if (directionX != 0)
        {
            PlayerAnimator.SetBool("Walking", true);
        }
        else
            PlayerAnimator.SetBool("Walking", false);
    }

    public void FicarParado()
    {
        canMove = false;
        PlayerAnimator.SetBool("Walking", false);
        PlayerAnimator.SetBool("Running", false);
    }


    public void Flip()
    {
        facingRight = !facingRight;
        //PlayerMesh.GetComponent<SpriteRenderer>().flipX = !facingRight;
        transform.Rotate(Vector3.up * 180); //o flip tem que ser por rotation pra a animação não bugar inteira =/
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
                clickedObjectName = hit.collider.gameObject.GetComponent<Item>().item;
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

    public void GettinHit(bool hit)
    {
        gettinHit = hit;
    }

    void AndarSfx()
    {
        if (directionX != 0)
        {
            if (gameController.GetSceneName() == "Cena1-ChegadaEmCasa" || gameController.GetSceneName() == "Cena1.1-ChegadaEmCasa")
            {
                andarSfx[1].Stop();
                if (!andarSfx[0].isPlaying)
                {
                    andarSfx[0].Play();
                }
            }
            else
            {
                andarSfx[0].Stop();
                if (!andarSfx[1].isPlaying)
                {
                    andarSfx[1].Play();
                }
            }
        }
        else
        {
            andarSfx[0].Stop();
            andarSfx[1].Stop();
        }

        if (running)
        {
            andarSfx[0].pitch = 1.2f;
            andarSfx[1].pitch = 1.2f;
        }
        else
        {
            andarSfx[0].pitch = 1.0f;
            andarSfx[1].pitch = 1.0f;
        }
    }
}
