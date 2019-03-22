using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    void EnterExecute();
    void UpdateExecute();
    void ExitExecute();
    void HandleInput();
}