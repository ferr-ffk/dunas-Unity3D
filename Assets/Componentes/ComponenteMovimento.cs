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

    /// <summary>
    /// Movimenta o objeto passado em uma direção dada.
    /// </summary>
    /// <param name="objeto">O GameObject a ser movimentado</param>
    /// <param name="direcao">O Vector3 representando a direção</param>
    public void Movimentar(GameObject objeto, Vector3 direcao)
    {
        // Calcula a direção desejada de movimento
        Vector3 velocidadeAlvo = direcao * _velocidade;

        // Calcula o valor da aceleração para ser utilizada no Lerp
        float valorAceleracao = 1 - Mathf.Exp(-_aceleracao * Time.deltaTime);

        // Calcula o Lerp, que é basicamente uma forma de deixar mais suave o movimento com aceleração
        Vector3 direcaoLerp = Vector3.Lerp(velocidadeAtual, velocidadeAlvo, valorAceleracao);

        // Movimenta o objeto
        objeto.transform.Translate(direcaoLerp *  _velocidade * Time.deltaTime);

        // Atualiza a variável local para o valor mais recente da velocidade do objeto
        velocidadeAtual = objeto.transform.position;
    }

    /// <summary>
    /// Usa a velocidade de pulo do componente para movimentar o objeto na direção Y+.
    /// </summary>
    /// <param name="objeto"></param>
    public void Pular(GameObject objeto)
    {
        Vector3 direcao = new(0, _velocidadePulo);

        objeto.transform.Translate(direcao * _velocidadePulo * Time.deltaTime);
    }
}
