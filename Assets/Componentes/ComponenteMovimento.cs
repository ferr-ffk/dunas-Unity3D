using UnityEngine;

public class ComponenteMovimento : MonoBehaviour
{
    [SerializeField, Tooltip("A velocidade do objeto. Deve ser definida.")]
    private float _velocidade;

    [SerializeField, Tooltip("A velocidade de pulo, sendo pulo o movimento em direção Y+. Deve ser definida.")]
    private float _velocidadePulo;

    [SerializeField, Tooltip("A aceleração do objeto, por padrão 1.")]
    private float _aceleracao = 1.0f;

    private Vector3 velocidadeAtual = Vector3.zero;

    private GameObject _camera;

    private void Start()
    {
        // Congela a rotação do objeto para que ele não caia kkk
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }

    /// <summary>
    /// Movimenta o objeto passado em uma direção dada.
    /// </summary>
    /// <param name="objeto">O GameObject a ser movimentado</param>
    /// <param name="direcao">O Vector3 representando a direção</param>
    /// <param name="forward">O vetor representando a frente do movimento, útil caso o componente seja do jogador, e sua rotação muda constantemente por conta da câmera</param>
    public void Movimentar(GameObject objeto, Vector3 direcao, Quaternion forward = default)
    {
        // Transforma o vetor de movimento com base na direção dele (se esta existir), para que o movimento esteja sempre alinhado com a câmera
        Vector3 direcaoMovimento = forward * direcao;

        // Calcula a direção desejada de movimento
        Vector3 velocidadeAlvo = direcaoMovimento * _velocidade;

        // Calcula o valor da aceleração para ser utilizada no Lerp
        float valorAceleracao = 1 - Mathf.Exp(-_aceleracao * Time.deltaTime);

        // Calcula o Lerp, que é basicamente uma forma de deixar mais suave o movimento com aceleração
        Vector3 direcaoLerp = Vector3.Lerp(velocidadeAtual, velocidadeAlvo, valorAceleracao);

        // Movimenta o objeto
        objeto.transform.Translate(direcaoLerp * _velocidade * Time.deltaTime);

        // Atualiza a variável local para o valor mais recente da velocidade do objeto
        velocidadeAtual = velocidadeAlvo;
    }

    /// <summary>
    /// Usa a velocidade de pulo do componente para movimentar o objeto na direção Y+.
    /// </summary>
    /// <param name="objeto"></param>
    public void Pular(GameObject objeto)    
    {
        objeto.GetComponent<Rigidbody>().AddForce(transform.up * _velocidadePulo, ForceMode.Impulse);
    }
}
