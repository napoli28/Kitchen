internal class CharacterState_Empty : CharacterBehaviour, ICharacterState
{
    private Character _character;

    public CharacterState_Empty(Character character)
    {
        _character = character;
    }

    public void EnterExecute()
    {
        throw new System.NotImplementedException();
    }

    public void ExitExecute()
    {
        throw new System.NotImplementedException();
    }

    public void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateExecute()
    {
        throw new System.NotImplementedException();
    }
}