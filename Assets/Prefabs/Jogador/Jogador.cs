using UnityEngine;
using UnityEngine.InputSystem;

public class Jogador : MonoBehaviour
{
    [SerializeField, Tooltip("O componente de movimento deste jogador.")]
    private ComponenteMovimento _componenteMovimento;

    [SerializeField, Tooltip("A ação de input definida para movimento.")]
    private InputActionReference _movimentoJogadorReferencia;

    [SerializeField, Tooltip("A ação de input definida para pulo.")]
    private InputActionReference _puloJogadorReferencia;

    [SerializeField, Tooltip("O tempo em segundos para que a corrida seja automaticamente ativada.")]
    private float _delayCorridaAutomatica = 0.5f;

    [SerializeField, Tooltip("A armature do jogador.")]
    private GameObject _armature;

    private bool correndo = false;

    private GameObject _camera;

    private Animator _animator;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");

        _animator = GetComponent<Animator>();
    }

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

        bool haMovimento = direcao.magnitude > 0;
        bool corridaAutomaticaAtivada = _componenteMovimento.tempoAndando > _delayCorridaAutomatica;

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
        }
        else
        {
            // Reseta a velocidade alvo para a velocidade inicial, sem o multiplicador de velocidade ao correr
            _componenteMovimento.VelocidadeAlvo = _componenteMovimento.VelocidadeInicial;
        }

        // Se há movimento, ativa a animação de andar
        _animator.SetBool("Andando", haMovimento);

        // Movimenta a armature pra que o modelo vire pra direção que esteja andando
        // ISSO DEU CERTO NA TERCEIRA TENTATIVA MEREÇO UM TROFÉU
        if (haMovimento)
        {
            // A multiplicação de vetores é o segredo, faz com que a rotação da direção seja rotacionada com base no alinhamento da câmera
            Quaternion rotacaoDesejada = Quaternion.LookRotation(direcao) * rotacaoCameraApontando;

            // Interpola a rotação, deixando ela mais suave
            // Multiplica por um número grandde pra que ela seja *quase* instantânea
            _armature.transform.rotation = Quaternion.Slerp(_armature.transform.rotation, rotacaoDesejada, Time.deltaTime * 8);
        }

        // Define a velocidade para uso do animador
        // Velocidade > 4 : Animação de corrida, caso contrário, animação de andar
        _animator.SetFloat("Velocidade", _componenteMovimento.velocidadeAtual.magnitude);

        // Define o multiplicador de andar para a animação, baseado na velocidade atual do jogador
        // Faz uma regra de 3 para definir quanto o jogador esta andando em relação a velocidade alvo
        float multiplicadorAndando = _componenteMovimento.velocidadeAtual.magnitude / _componenteMovimento.VelocidadeAlvo;

        // Define o multiplicador de andar para a animação
        _animator.SetFloat("MultiplicadorAndando", multiplicadorAndando);

        // Utiliza o método do componente de movimento e movimenta o jogador
        _componenteMovimento.Movimentar(gameObject, direcao, rotacaoCameraApontando);

        // Verifica se o botão de pular foi pressionado
        bool botaoPularPressionado = _puloJogadorReferencia.action.triggered;

        if (botaoPularPressionado)
        {
            _componenteMovimento.Pular(gameObject);
        }
    }
}
