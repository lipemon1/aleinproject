using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Fica olhando para os lados e quando vê o jogador ele chama um guarda
/// </summary>
public class DoctorAI : MonoBehaviour {

    [Header("Configuração")]
    public bool startDoctorAI = false; //para controlar quando a AI do médico deve começar
    public int playerLayerNumber = 8;
    public int enemyLayerNumber = 9;
    public bool isAlive = true;

    [Header("Detecção do Jogador")]
    public bool spotted; //se viu o jogador
    public bool alreadySpotted; //se ja encontrou o player alguma vez
    public bool alreadyCancelInvoke; //se ja cancelou o invoke de procurar pelo medico
    public Transform eye; //posicao do olho do médico
    public Transform endViewSight; //limite de visao do médico
    public GameObject player; //objeto do jogador
    public GameObject playerAlert; //alerta visual de perigo

    [Header("Chamada de Guarda")]
    public GameObject timeToCallSliderGameObject; //objeto que contem o slider
    public Slider timeToCallGuardUI; //slider para mostrar visualmente o tempo para chamar um guarda
    public float timeToCallGuard; //tempo até chamar os guardas
    public bool callingGuard; //se esta chamando um guarda ou nao
    public bool guardWasCalled; //se ja chamou um guarda ou nao

    [Header("Movimentação")]
    public bool isGoingRight = true; //verifica se esta indo para a direita ou esquerda
    public float timeToLookAround = 1f; //tempo que o medico fica parado olhando para uma direção

    [Header("Animações")]
    public Animator doctorAnimator;

	// Use this for initialization
	void Start () {

        if(eye == null)
        {
            eye = transform.GetChild(0).transform; //pega o inicio de visao do médico
        }

        if (endViewSight == null)
        {
            endViewSight = transform.GetChild(1).transform; //pega o final da visao do médico
        }

        if (timeToCallSliderGameObject == null)
        {
            timeToCallSliderGameObject = transform.GetChild(2).transform.GetChild(0).transform.gameObject;// pega o gameobject do slider
        }

        if (timeToCallGuardUI == null)
        {
            timeToCallGuardUI = timeToCallSliderGameObject.GetComponent<Slider>(); //pega o slider
        }

        if(playerAlert == null)
        {
            playerAlert = transform.GetChild(3).gameObject; // pega o sinal de alerta
        }
        
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if(doctorAnimator == null)
        {
            doctorAnimator = GetComponent<Animator>();
        }
               

        InitiateDoctorAI(); //Inicia a lógica do doctor

        PrepareDoctorUI(); //prepara o canvas do medico
	}
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            if (startDoctorAI)
            {
                //vai procurar o jogador até que encontre uma vez
                if (!alreadySpotted) //se ainda nao encontrou o jogador
                {
                    LookingForPlayer(); //continua procurando
                }
                else //se encontrou o jogador
                {
                    playerAlert.SetActive(true); //exibe sinal de alerta
                    if (!alreadyCancelInvoke)
                    {
                        Debug.Log("Cancelando o LookingAround"); //apenas debug
                        CancelInvoke("LookingAround"); //cancela o invoke
                        alreadyCancelInvoke = true; //cancela o invoke para ficar olhando para os lados

                        StartCallingGuard(); //começa a chamar um guarda
                    }
                }

                //se estiver chamando um guarda
                if (callingGuard)
                {
                    CallingGuard();
                }
            }

            Physics2D.IgnoreLayerCollision(playerLayerNumber, enemyLayerNumber); //ignora colisoes de quem estiver nessas layers
        }
    }

    /// <summary>
    /// Parametriza o canvas do medico
    /// </summary>
    void PrepareDoctorUI()
    {
        timeToCallGuardUI.maxValue = timeToCallGuard;
    }

    /// <summary>
    /// Inicia a chamada de um guarda
    /// é chamada quando o medico encontra o jogador
    /// </summary>
    void StartCallingGuard()
    {
        Debug.Log("Iniciando chmada de um médico");
        timeToCallSliderGameObject.SetActive(true); //habilita o gameobject do slider
        callingGuard = true; //habilita a chamada para o guarda
        doctorAnimator.SetTrigger("Calling Guard");
    }

    /// <summary>
    /// O que acontece quando esta chamando um guarda
    /// </summary>
    void CallingGuard()
    {
        if (!guardWasCalled) //se o guarda ainda nao foi chamado
        {
            timeToCallGuardUI.value = timeToCallGuard; //atualiza o valor da UI
            Debug.Log("Chamando um guarda em: " + timeToCallGuard.ToString() + " segundos");

            //se o tempo para chamar um guarda chegar a zero
            if (timeToCallGuard <= 0.0f)
            {
                Debug.Log("Chamando um guarda AGORA");
                //escrever o código para chamar um guarda aqui

                guardWasCalled = true;
            }

            timeToCallGuard -= Time.deltaTime; //diminui o valor para a chmada do guarda a cada segundo
        }

        LookingAtThePlayer(); //olha para o jogador
    }

    /// <summary>
    /// Após iniciar a chamada para o guarda, o médico fica olhando na direção do jogador
    /// </summary>
    void LookingAtThePlayer()
    {
        if (player.transform.position.x < transform.position.x) //se o jogador estiver a esquerda do medico
        {
            if (isGoingRight)//e o médico estiver olhando para a direita
            {
                Flip(); //vire para a esquerda
            }
        }
        else //se o jogador estiver a direita do médico
        {
            if (!isGoingRight)//e o medico estiver olhando para  esquerda
            {
                Flip(); //vire para a direita
            }
        }
    }

    /// <summary>
    /// cria uma visão para identificar quando ele encontrar o jogador
    /// </summary>
    void LookingForPlayer()
    {
        DoctorVision(); //visao do médico
    }

    /// <summary>
    /// Desenha um linecast para usar de visão para o médico
    /// </summary>
    void DoctorVision()
    {
        Debug.DrawLine(eye.position, endViewSight.position, Color.blue); //desenha uma linha para debug (ver na Scene)

        //cria uma linha como visao do inimigo, se encontrar alguma coisa com layer "PlayerLayer" deixa o SPOTTED true
        spotted = Physics2D.Linecast(eye.position, endViewSight.position, 1 << LayerMask.NameToLayer("PlayerLayer"));

        //se o jogador tiver sido visto
        if (spotted)
        {
            Debug.Log("Achei o Jogador");
            alreadySpotted = true; //marca que o jogador foi encontrado
        }
    }

    /// <summary>
    /// Fica olhando para os lados
    /// </summary>
    /// <returns></returns>
    void LookingAround()
    {
        //chamar animação de olhando para os lados aqui
        Debug.Log("Olhando para um lado");
        Flip();
    }

    /// <summary>
    /// Função para virar medico, nao podemos usar o flip do sprite renderer
    /// porque se não o raio de visao permanece para a mesma direção
    /// </summary>
    void Flip()
    {
        //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        transform.Rotate(Vector3.up * 180); //o flip tem que ser por rotation pra a animação não bugar inteira =/
        isGoingRight = !isGoingRight;
    }

    /// <summary>
    /// Inicia a lógica do Médico
    /// </summary>
    void InitiateDoctorAI()
    {
        startDoctorAI = true;
        InvokeRepeating("LookingAround", 0f, timeToLookAround);
    }

    public void KillDoctor()
    {
        isAlive = false;
        timeToCallSliderGameObject.transform.parent.gameObject.SetActive(false);
        //Destroy(timeToCallSliderGameObject.transform.parent.gameObject);
    }
}
