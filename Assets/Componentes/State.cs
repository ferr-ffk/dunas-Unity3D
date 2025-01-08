using UnityEngine;

/// <summary>
/// Determina o estado de um contexto (GameObject). O Estado é definido por sua ação quando é iniciado, sua ação a cada frame, e sua ação a ser encerrado.
/// </summary>
public abstract class State : MonoBehaviour
{
    /// <summary>
    /// Sua ação ao ser inicializado. Equivalente ao 'Start()'
    /// </summary>
    public abstract void EnterState(StateManager context);

    /// <summary>
    /// Sua ação realizada a cada frame. Equivalente ao 'Update()'
    /// </summary>
    public abstract void UpdateState(StateManager context);

    /// <summary>
    /// Sua ação realizada ao ser encerrado. Equivalente ao 'OnDisable()'
    /// </summary>
    public abstract void ExitState(StateManager context);
}
