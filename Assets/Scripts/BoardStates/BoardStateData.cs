using System.Collections.Generic;

public enum BoardStates
{
    Grounded,
    FreeFall,
    CatchWindow
}

public struct BoardStateData
{
    public BoardStates ThisState;
    public List<BoardStates> IncompatibleWith;
}