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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_componenteMovimento == null)
        {
            Debug.LogError("O componente de movimento n�o foi definido corretamente!");

            return;
        }

        // Obt�m o vetor 2D de dire��o a partir do input
        Vector2 direcaoJoystick = _movimentoJogadorReferencia.action.ReadValue<Vector2>();

        // Cria um vetor 3D para movimentar apenas no x e z (sendo Y a vertical)
        Vector3 direcao = new(direcaoJoystick.x, 0, direcaoJoystick.y);

        // Utiliza o m�todo do componente de movimento e movimenta o jogador
        _componenteMovimento.Movimentar(gameObject, direcao);

        bool botaoPularPressionado = _puloJogadorReferencia.action.triggered;

        if (botaoPularPressionado)
        {
            _componenteMovimento.Pular(gameObject);
        }
    }
}
