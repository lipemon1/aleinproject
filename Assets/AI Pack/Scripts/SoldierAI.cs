using UnityEngine;
using System.Collections;

/// <summary>
/// Crie uma tag para seu jogador com nome Player
/// Crie uma layerMask para seu jogador com nome PlayerLayer
/// Crie uma layerMask para seu inimigo com nome EnemyLayer
/// 
/// Crie um Objeto para ser o ponto de inicio da sua patrulha e outro para ser o final
/// 
/// Nas "Configurações de Layer" coloque o número das layers correspondentes para que possa ignora-la
/// </summary>

public class SoldierAI : MonoBehaviour
{

    [Header("Configuração de Layer")]
    public int playerLayerNumber = 8;
    public int enemyLayerNumber = 9;

    [Header("Controle Manual de Guarda")]
    public bool alreadyStarted = false; //para saber se o guarda ja foi iniciado
    public bool startItSelf = false; //se deseja que o guarda se ative sozinho

    [Header("Status de Ação")]
    public bool patrolling; //boleano para dizer que o inimigo esta patrulhando
    public bool chasing; // diz se o inimigo esta perseguindo ou nao
    public bool wasChasing; //para saber se voce estava perseguindo

    [Header("Status de Movimento")]
    public bool canMove; //boleano para sbaer se o inimigo pode ou nao se mexer
    public bool isGoingRight; //se o inimigo esta indo para direita ou nao
    public bool walking; //se o inimigo esta andando
    public bool idle; //se o inimigo esta parado
    public bool running; //se o inimigo esta correndo, deve correr quando estiver perseguindo

    [Header("Velocidades de Movimento")]
    public float patrolSpeed = 2f; //velocidade durante a patrulha
    public float chaseSpeed = 5f; // velocidade durante a perseguiução
    public float chaseWaitTime = 5f; //tempo que o inimigo fica parado até parar de perseguir
    public float patrolWaitTime = 1f; //tempo que o inimigo fica parado no ponto de patrulha

    [Header("Posições de Patrulha")]
    public Transform startPos; //posição inicial da patrulha
    public Transform endPos; //posição final da patrulha

    [Header("Detecção do Jogador")]
    public bool spotted = false; // se o inimigo encontrou o jogador
    public Transform visionLimit; //alcance maximo de visao do inimigo
    public Transform eye; //posicao do olho do inimigo
    public GameObject alertSign; //sinal de alerta
    public GameObject lostAlertSign; //sinal de jogador perdido
    public GameObject player; //jogador

    [Header("Atirar no Jogador")]
    public bool isAbleToShoot = false; //se ja pode atirar no jogador
    public Transform visionToShootLimit; //alcance maximo para poder atirar
    public bool isShooting; //permitir que o inimigo aitre
    public GameObject bullet;
    public GameObject gun;
    public float fireRate = 2;
    public float timeToShoot = 2;

    [Header("Animações")]
    public Animator soldierAnimator;
    private bool isAlive = true;

    // Use this for initialization
    void Start()
    {
        // Pega as variaveis filhos do Inimigo
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (eye == null)
        {
            eye = transform.GetChild(0).transform;
        }

        if (visionLimit == null)
        {
            visionLimit = transform.GetChild(1).transform;
        }

        if (alertSign == null)
        {
            alertSign = transform.GetChild(2).gameObject;
        }

        if (lostAlertSign == null)
        {
            lostAlertSign = transform.GetChild(4).gameObject;
        }

        if (visionToShootLimit == null)
        {
            visionToShootLimit = transform.GetChild(3);
        }

        if (soldierAnimator == null)
        {
            soldierAnimator = GetComponent<Animator>();
        }

        if (startItSelf)
        {
            Initiate(); //começa a AI do guarda
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            //se patrolling for true, o inimigo fica patrulhando
            if (patrolling)
            {
                Patrol();
            }

            //se chaseing for true, o inimigo fica perseguindo
            if (chasing)
            {
                Chase();
            }

            //se o inimigo estiver atirando
            if (isShooting)
            {
                Shoot();
            }

            //visao do inimigo
            EnemyVision();

            //raio de tiro do inimigo
            EnemyVisionToShot();

            Physics2D.IgnoreLayerCollision(playerLayerNumber, enemyLayerNumber); //ignora colisoes de quem estiver nessas layers
            AnimateSoldier();
        }
    }

    /// <summary>
    /// Cria um linecast que será utilizado como distancia para poder atirar no jogador
    /// </summary>
    void EnemyVisionToShot()
    {
        Debug.DrawLine(eye.position, visionToShootLimit.position, Color.blue); //desenha uma linha para debug (ver na Scene)

        //cria uma linha como visao do inimigo, se encontrar alguma coisa com layer "PlayerLayer" deixa o SPOTTED true
        isAbleToShoot = Physics2D.Linecast(eye.position, visionToShootLimit.position, 1 << LayerMask.NameToLayer("PlayerLayer"));

        //se o inimigo estiver perto o suficiente para atirar
        if (isAbleToShoot)
        {
            //Debug.Log("Posso atirar");
            isShooting = true;
        }
    }

    /// <summary>
    /// Escrever função de atirar no jogador aqui
    /// </summary>
    void Shoot()
    {
        Debug.Log("Atirando");

        if (timeToShoot <= 0.0f)
        {
            if (bullet == null)
            {
                Debug.Log("Objeto bala nao atribuido");
            }
            else
            {
                
            }
            GameObject tmp = Instantiate(bullet, gun.transform.position, Quaternion.identity) as GameObject;
            tmp.GetComponent<bulletBehaviour>().SetDirecao(isGoingRight == true ? 1 : 0);
            timeToShoot = fireRate;
        }

        timeToShoot -= Time.deltaTime;
    }

    /// <summary>
    /// Cria um linecast que será utilizado como raio de visao do inimigo
    /// </summary>
    void EnemyVision()
    {
        Debug.DrawLine(eye.position, visionLimit.position, Color.red); //desenha uma linha para debug (ver na Scene)

        //cria uma linha como visao do inimigo, se encontrar alguma coisa com layer "PlayerLayer" deixa o SPOTTED true
        spotted = Physics2D.Linecast(eye.position, visionLimit.position, 1 << LayerMask.NameToLayer("PlayerLayer"));

        //se o jogador tiver sido visto
        if (spotted)
        {
            StartChase(); //começa a perseguir
        }
        else //se ele nao foi visto
        {
            alertSign.SetActive(false); //esconde sinal de alerta
        }
    }

    /// <summary>
    /// Função para iniciar a perseguição pelo jogador
    /// </summary>
    void StartChase()
    {
        HideLostAlert(); //esconde sinal de alerta de jogador perdido
        //Debug.Log("Começando a perseguir");
        alertSign.SetActive(true); //mostra sinal de alerta
        patrolling = false; //para de patrulhar
        idle = false; //sai do idle
        running = true; //começa a correr
        walking = false; // para de andar
        chasing = true; //habilitando isso como true ele começa a perseguir
        isShooting = false;
    }

    /// <summary>
    /// Função executada enquando o chasing é verdadeira
    /// Inimigo deve chegar perto do jogador o suficiente para poder atirar nele.
    /// Ao poder atirar ele começa atirar
    /// </summary>
    void Chase()
    {
        //Debug.Log("Perseguindo..");
        wasChasing = true;

        // perseguir somente até voce poder atirar
        if (!isAbleToShoot)
        {
            //se o jogador estiver a esquerda do inimigo, o inimigo vai pra esquerda
            if (player.transform.position.x < transform.position.x)
            {
                //movimenta o inimigo para esquerda multiplicando pelo chaseSpeed
                transform.Translate(new Vector2(-1, 0) * chaseSpeed * Time.deltaTime);
            }

            //se o jogador estiver a direita do inimigo, o inimigo vai pra direita
            if (player.transform.position.x > transform.position.x)
            {
                //movimenta o inimigo para esquerda multiplicando pelo chaseSpeed
                transform.Translate(new Vector2(-1, 0) * chaseSpeed * Time.deltaTime);
            }
        }

        //se perdeu de vista
        if (!spotted)
        {
            chasing = false;
            running = false;
            canMove = false;
            walking = false;
            idle = false;
            StartCoroutine(LookingAround());
        }
    }

    /// <summary>
    /// Quando perde o jogador de vista olha para os dois lados
    /// </summary>
    /// <returns></returns>
    IEnumerator LookingAround()
    {
        //chamar animação de olhando para os lados aqui
        ShowLostAlert(); //mostra sinal de alerta de busca
        //Debug.Log("Olhando para um lado");
        Flip();
        yield return new WaitForSeconds(0.5f);

        if (!spotted)
        {
            //Debug.Log("Olhando para o outro");
            Flip();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            StartChase();
        }

        if (!spotted)
        {
            //Debug.Log("Olhando para um lado");
            Flip();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            StartChase();
        }

        if (!spotted)
        {
            //Debug.Log("Olhando para o outro");
            Flip();
        }
        else
        {
            StartChase();
        }

        if (!spotted)
        {
            wasChasing = false;
            StartPatrol();
        }
        else
        {
            StartChase();
        }

        HideLostAlert();
    }

    /// <summary>
    /// Inicia a Patrulha do soldado
    /// </summary>
    void StartPatrol()
    {
        HideLostAlert(); //esconde sinal de alerta de jogador perdido
        //Debug.Log("Começando patrulha..");
        canMove = true; //pode se mover
        chasing = false; //para de perseguir
        patrolling = true; //começa a patrulhar
        running = false; //para de correr
    }

    /// <summary>
    /// Função que executa a patrulha
    /// </summary>
    void Patrol()
    {
        //Debug.Log("Patrulhando!");
        walking = true; //esta andando
        idle = false; //nao esta mais no idle

        //verifica se o inimigo esta indo para direita
        if (isGoingRight && canMove)
        {
            //Debug.Log("Indo para a direita...");
            //movimenta o inimigo para direita multiplicando pelo patrolSpeed
            transform.Translate(new Vector2(-1, 0) * patrolSpeed * Time.deltaTime);

            //quando o inimigo chegar a posição final da patrulha
            if (transform.position.x > endPos.position.x)
            {
                //Debug.Log("Cheguei ao meu ponto de patrulha direito. Parando e olhando para os lados..");
                //Parar inimigo e fazer ele olhar para os dois lados
                canMove = false; //desabilita a movimentação do inimmigo
                PatrolPointWait();
                Flip();
            }
        }
        else if (!isGoingRight && canMove) //se estiver indo para esquerda
        {
            //Debug.Log("Indo para a esquerda...");
            //movimenta o inimigo para esquerda multiplicando pelo patrolSpeed
            transform.Translate(new Vector2(-1, 0) * patrolSpeed * Time.deltaTime);

            //quando o inimigo chegar a posição final da patrulha
            if (transform.position.x < startPos.position.x)
            {
                //Debug.Log("Cheguei ao meu ponto de patrulha esquerdo. Parando e olhando para os lados..");
                //Parar inimigo e fazer ele olhar para os dois lados
                canMove = false; //desabilita a movimentação do inimmigo
                PatrolPointWait();
                Flip();
            }
        }
    }

    /// <summary>
    /// Função para deixar o inimigo parado no ponto de patrulha
    /// </summary>
    void PatrolPointWait()
    {
        patrolling = false; //para de patrulhar para olhar para os lados
        walking = false;
        idle = true;
        StartCoroutine(PayingAtention()); //faz ele ficar parado por um tempo
    }

    /// <summary>
    /// Inimigo fica parado durante patrolWaitTime segundos;
    /// </summary>
    /// <returns></returns>
    IEnumerator PayingAtention()
    {
        //chamar animação de olhando para os lados aqui
        //Debug.Log("Olhando para os lados");
        yield return new WaitForSeconds(patrolWaitTime);
        //Debug.Log("Terminei de olhar para os lados");
        StartPatrol();
    }

    /// <summary>
    /// Função para virar inimigo, nao podemos usar o flip do sprite renderer
    /// porque se não o raio de visao permanece para a mesma direção
    /// </summary>
    void Flip()
    {
        //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        transform.Rotate(Vector3.up * 180); //o flip tem que ser por rotation pra a animação não bugar inteira =/
        isGoingRight = !isGoingRight;
    }

    /// <summary>
    /// Mostra alerta de jogador perdido
    /// </summary>
    void ShowLostAlert()
    {
        lostAlertSign.SetActive(true);
    }

    /// <summary>
    /// Esconde o sinal de alerta de jogador perdido
    /// se o inimigo nao estiver mais em perseguição
    /// </summary>
    void HideLostAlert()
    {
        lostAlertSign.SetActive(false);
    }

    /// <summary>
    /// Inicia o modo guarda depois de sua animação ter sido feita
    /// É usado para caso o guarda seja spawnado
    /// </summary>
    void Initiate()
    {
        alreadyStarted = true; //guarda ja foi iniciado
        StartPatrol(); // inicia patrulha
    }

    /// <summary>
    /// Atualiza o animator
    /// </summary>
    /// <param name="idle"></param>
    /// <param name="walking"></param>
    /// <param name="running"></param>
    /// <param name="shooting"></param>
    /// <param name="lost"></param>
    void AnimateSoldier()
    {
        soldierAnimator.SetBool("idle", idle);
        soldierAnimator.SetBool("walking", walking);
        soldierAnimator.SetBool("running", running);
        soldierAnimator.SetBool("shooting", isShooting);
        soldierAnimator.SetBool("lostTarget", wasChasing);
    }

    public void KillSoldier()
    {
        Debug.LogWarning("Matar Soldado");
        isAlive = false;
        //desativar alertas
        alertSign.SetActive(false);
        lostAlertSign.SetActive(false);
    }

}
