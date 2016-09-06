using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviour : MonoBehaviour {

    [Header("Testando no Celular?")]
    public bool onMobile; //para saber se estamos no mobile, controle manual

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
    

	// Use this for initialization
	void Start () {
        walking = true;
        sneaking = false;
        running = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // Condição para verificar se estamos jogando no pc ou no celular
        //Caso estejamos no pc o jogo recebe input do teclado
        //se estiver no mobile, receberá input do joystick na tela
        if (!onMobile) {
            directionX = Input.GetAxis("Horizontal"); //input do teclado
        } else
        {
            directionX = CrossPlatformInputManager.GetAxis("Horizontal"); //input do joystick
        }
        
        //Código para pegar quando o jogador apertar shift esquerdo para correr
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleRun(); //função de correr
        }

        //Código para pegar quando o jogador apertar control esquerdo para
        //entrar no modo sneaking
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleSneaking(); //função para sneakar
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
            actualSpeed *= runMultiplier;
        }

        //linha que movimento o jogador
        transform.Translate(new Vector3(directionX * actualSpeed * Time.deltaTime, 0f, 0f));
	}

    /// <summary>
    /// Função que modifica a velocidade de acordo com o toggle que esta ativo
    /// </summary>
    void changeSpeed()
    {
        if(running)
        {
            actualSpeed = runningSpeed;
        }
        else if (sneaking)
        {
            actualSpeed = sneakingSpeed;
        }
        else if(walking)
        {
            actualSpeed = walkingSpeed;
        }
        

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
}
