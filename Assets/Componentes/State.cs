using UnityEngine;

/// <summary>
/// Determina o estado de um contexto (GameObject). O Estado � definido por sua a��o quando � iniciado, sua a��o a cada frame, e sua a��o a ser encerrado.
/// </summary>
public abstract class State : MonoBehaviour
{
    /// <summary>
    /// Sua a��o ao ser inicializado. Equivalente ao 'Start()'
    /// </summary>
    public abstract void EnterState(StateManager context);

    /// <summary>
    /// Sua a��o realizada a cada frame. Equivalente ao 'Update()'
    /// </summary>
    public abstract void UpdateState(StateManager context);

    /// <summary>
    /// Sua a��o realizada ao ser encerrado. Equivalente ao 'OnDisable()'
    /// </summary>
    public abstract void ExitState(StateManager context);
}
