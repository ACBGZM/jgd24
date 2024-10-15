using Unity.Mathematics;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}