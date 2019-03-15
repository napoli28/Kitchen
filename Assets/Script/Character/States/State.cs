using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterState
{
    void EnterExecute();
    void UpdateExecute();
    void ExitExecute();
    void HandleInput();
}