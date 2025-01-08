using System.Collections.Generic;
using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [NonSerialized]
    public State currentState;

    [SerializeField, Tooltip("A lista de todos os estados possíveis do contexto (GameObject)")]
    public List<State> states = new List<State>();

    [SerializeField, Tooltip("O estado inicial do contexto (GameObject).")]
    public State initialState = null;

    void Start()
    {
        if (initialState == null)
            throw new NullReferenceException("O estado inicial é nulo! Defina ele para que possa ser gerenciado corretamente.");

        currentState = initialState;

        currentState.EnterState(this);
    }

    void Update()
    {
        if (currentState == null)
            throw new NullReferenceException("O estado inicial não foi definido!");

        currentState.UpdateState(this);
    }

    public void SwitchState(State state)
    {
        currentState = state;

        state.EnterState(this);
    }

    public void SetInicialState(State state)
    {
        initialState = state;
    }
}
