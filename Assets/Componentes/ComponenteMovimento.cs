using UnityEngine;

public class ComponenteMovimento : MonoBehaviour
{
    [SerializeField, Tooltip("A velocidade do objeto. Deve ser definida.")]
    private float _velocidade;

    [SerializeField, Tooltip("A velocidade de pulo, sendo pulo o movimento em dire��o Y+. Deve ser definida.")]
    private float _velocidadePulo;

    [SerializeField, Tooltip("A acelera��o do objeto, por padr�o 1.")]
    private float _aceleracao = 1.0f;

    private Vector3 _velocidadeAtual = Vector3.zero;
    private Rigidbody _rigidbody;
    private CharacterController _characterController;

    private bool isJogador; 

    private void Start()
    {
        // Congela a rota��o do objeto para que ele n�o caia rs
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody n�o encontrado!");
            return;
        } else
        {
            _rigidbody.freezeRotation = true;
        }

        // Pega o CharacterController do objeto, caso exista
        isJogador = TryGetComponent<CharacterController>(out _characterController);
    }

    /// <summary>
    /// Movimenta o objeto passado em uma dire��o dada.
    /// </summary>
    /// <param name="objeto">O GameObject a ser movimentado</param>
    /// <param name="direcao">O Vector3 representando a dire��o</param>
    /// <param name="forward">O vetor representando a frente do movimento, �til caso o componente seja do jogador, e sua rota��o muda constantemente por conta da c�mera</param>
    public void Movimentar(GameObject objeto, Vector3 direcao, Quaternion forward = default)
    {
        if (objeto == null)
        {
            Debug.LogError("Objeto n�o pode ser nulo!");
            return;
        }

        // Transforma o vetor de movimento com base na dire��o dele (se esta existir), para que o movimento esteja sempre alinhado com a c�mera
        Vector3 direcaoMovimento = forward * direcao;

        // Calcula a dire��o desejada de movimento
        Vector3 velocidadeAlvo = direcaoMovimento * _velocidade;

        // Calcula o valor da acelera��o para ser utilizada no Lerp
        float valorAceleracao = 1 - Mathf.Exp(-_aceleracao * Time.deltaTime);

        // Calcula o Lerp, que � basicamente uma forma de deixar mais suave o movimento com acelera��o
        Vector3 direcaoLerp = Vector3.Lerp(_velocidadeAtual, velocidadeAlvo, valorAceleracao);


        // Movimenta o objeto
        // Se possui o componente de CharacterController, usa ele para o movimento, caso contr�rio usa o rigid body
        if (isJogador)
        {
            _characterController.Move(direcaoLerp * Time.deltaTime);
        } else
        {
            _rigidbody.MovePosition(direcaoLerp * Time.deltaTime);
        }

        // Atualiza a vari�vel local para o valor mais recente da velocidade do objeto
        _velocidadeAtual = velocidadeAlvo;
    }

    /// <summary>
    /// Usa a velocidade de pulo do componente para movimentar o objeto na dire��o Y+.
    /// </summary>
    /// <param name="objeto">O GameObject a ser movimentado</param>
    public void Pular(GameObject objeto)
    {
        if (objeto == null)
        {
            Debug.LogError("Objeto n�o pode ser nulo!");
            return;
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody n�o encontrado!");
            return;
        }

        _rigidbody.AddForce(Vector3.up * _velocidadePulo, ForceMode.Impulse);
    }
}
