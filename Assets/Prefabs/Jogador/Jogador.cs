using UnityEngine;
using UnityEngine.InputSystem;

public class Jogador : MonoBehaviour
{
    [SerializeField]
    private ComponenteMovimento _componenteMovimento;

    [SerializeField]
    private InputActionReference _movimentoJogadorReferencia;

    [SerializeField]
    private InputActionReference _puloJogadorReferencia;

    [SerializeField]
    private GameObject _pivoCamera;

    [SerializeField]
    private float _delayCorridaAutomatica = 0.5f;

    private bool correndo = false;

    private GameObject _camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (_componenteMovimento == null)
        {
            Debug.LogError("O componente de movimento não foi definido corretamente!");

            return;
        }

        // Obtém o vetor 2D de direção a partir do input
        Vector2 direcaoJoystick = _movimentoJogadorReferencia.action.ReadValue<Vector2>();

        // Cria um vetor 3D para movimentar apenas no x e z (sendo Y a vertical)
        Vector3 direcao = new(direcaoJoystick.x, 0, direcaoJoystick.y);

        // Utiliza a rotação que a câmera está apontando, e usa para que o jogador se movimente de acordo com ela
        Quaternion rotacaoCameraApontando = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);

        float tempoAndando = _componenteMovimento.tempoAndando;
        bool haMovimento = direcao.magnitude > 0;
        bool corridaAutomaticaAtivada = tempoAndando > _delayCorridaAutomatica;

        // Verifica se o jogador está andando e se o tempo de andar é maior que o delay
        // Ativa a corrida automática ao andar pelo tempo necessário
        if (haMovimento && corridaAutomaticaAtivada)
        {
            correndo = true;
        }

        // Verifica se o jogador parou de andar e reseta o estado de correr
        if (_componenteMovimento.tempoAndando == 0)
        {
            correndo = false;
        }

        // Define a velocidade alvo do jogador de acordo com o estado de correr
        if (correndo)
        {
            // Obtém o multiplicador de correr do componente de movimento e atualiza a velocidade alvo para correr
            _componenteMovimento.VelocidadeAlvo = _componenteMovimento.MultiplicadorCorrendo * _componenteMovimento.VelocidadeInicial;
        } else
        {
            // Reseta a velocidade alvo para a velocidade inicial, sem o multiplicador de velocidade ao correr
            _componenteMovimento.VelocidadeAlvo = _componenteMovimento.VelocidadeInicial;
        }

        // Utiliza o método do componente de movimento e movimenta o jogador
        _componenteMovimento.Movimentar(gameObject, direcao, rotacaoCameraApontando);

        bool botaoPularPressionado = _puloJogadorReferencia.action.triggered;
        bool noChao = _componenteMovimento.noChao;

        if (botaoPularPressionado && noChao)
        {
            _componenteMovimento.Pular(gameObject);
        }
    }
}
