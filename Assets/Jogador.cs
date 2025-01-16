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

        Quaternion forward = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);

        // Utiliza o método do componente de movimento e movimenta o jogador
        _componenteMovimento.Movimentar(gameObject, direcao, forward);

        bool botaoPularPressionado = _puloJogadorReferencia.action.triggered;

        if (botaoPularPressionado)
        {
            _componenteMovimento.Pular(gameObject);
        }
    }
}
