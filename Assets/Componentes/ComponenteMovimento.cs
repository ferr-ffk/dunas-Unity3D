using UnityEngine;

public class ComponenteMovimento : MonoBehaviour
{
    [SerializeField, Tooltip("A velocidade do objeto. Deve ser definida.")]
    private float _velocidade;

    [SerializeField, Tooltip("A velocidade de pulo, sendo pulo o movimento em direção Y+. Deve ser definida.")]
    private float _velocidadePulo;

    [SerializeField, Tooltip("A aceleração do objeto, por padrão 1.")]
    private float _aceleracao = 1.0f;

    private Vector3 _velocidadeAtual = Vector3.zero;
    private Rigidbody _rigidbody;
    private CharacterController _characterController;

    private bool isJogador; 

    private void Start()
    {
        // Congela a rotação do objeto para que ele não caia rs
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody não encontrado!");
            return;
        } else
        {
            _rigidbody.freezeRotation = true;
        }

        // Pega o CharacterController do objeto, caso exista
        isJogador = TryGetComponent<CharacterController>(out _characterController);
    }

    /// <summary>
    /// Movimenta o objeto passado em uma direção dada.
    /// </summary>
    /// <param name="objeto">O GameObject a ser movimentado</param>
    /// <param name="direcao">O Vector3 representando a direção</param>
    /// <param name="forward">O vetor representando a frente do movimento, útil caso o componente seja do jogador, e sua rotação muda constantemente por conta da câmera</param>
    public void Movimentar(GameObject objeto, Vector3 direcao, Quaternion forward = default)
    {
        if (objeto == null)
        {
            Debug.LogError("Objeto não pode ser nulo!");
            return;
        }

        // Transforma o vetor de movimento com base na direção dele (se esta existir), para que o movimento esteja sempre alinhado com a câmera
        Vector3 direcaoMovimento = forward * direcao;

        // Calcula a direção desejada de movimento
        Vector3 velocidadeAlvo = direcaoMovimento * _velocidade;

        // Calcula o valor da aceleração para ser utilizada no Lerp
        float valorAceleracao = 1 - Mathf.Exp(-_aceleracao * Time.deltaTime);

        // Calcula o Lerp, que é basicamente uma forma de deixar mais suave o movimento com aceleração
        Vector3 direcaoLerp = Vector3.Lerp(_velocidadeAtual, velocidadeAlvo, valorAceleracao);


        // Movimenta o objeto
        // Se possui o componente de CharacterController, usa ele para o movimento, caso contrário usa o rigid body
        if (isJogador)
        {
            _characterController.Move(direcaoLerp * Time.deltaTime);
        } else
        {
            _rigidbody.MovePosition(direcaoLerp * Time.deltaTime);
        }

        // Atualiza a variável local para o valor mais recente da velocidade do objeto
        _velocidadeAtual = velocidadeAlvo;
    }

    /// <summary>
    /// Usa a velocidade de pulo do componente para movimentar o objeto na direção Y+.
    /// </summary>
    /// <param name="objeto">O GameObject a ser movimentado</param>
    public void Pular(GameObject objeto)
    {
        if (objeto == null)
        {
            Debug.LogError("Objeto não pode ser nulo!");
            return;
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody não encontrado!");
            return;
        }

        _rigidbody.AddForce(Vector3.up * _velocidadePulo, ForceMode.Impulse);
    }
}
