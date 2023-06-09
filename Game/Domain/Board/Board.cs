namespace Game.Domain.Board;

using Primitives;

public class Board
{
    private readonly BoardDimensions boardDimensions;
    private List<Landmine> landmines;

    public Board(BoardDimensions boardDimensions, IMineCreator mineCreator)
    {
        this.boardDimensions = boardDimensions;
        landmines = mineCreator.CreateMines(boardDimensions).ToList();
    }

    public void DetonateLandmine(Player player)
    {
        var landmine = landmines.Find(l => l.InSamePosition(player) && !l.IsExploded());
        if (landmine is not null)
        {
            player.HitLandmine(landmine);
        }
    }

    public bool MoveIsValid(Position playerPosition, Direction direction)
    {
        if (direction == Direction.Up)
            return playerPosition.GetRow() + 1 < boardDimensions.BoardLength;
        if (direction == Direction.Right)
            return playerPosition.GetColumn() + 1 < boardDimensions.BoardWidth;        
        if (direction == Direction.Left)
            return playerPosition.GetColumn() - 1 >= 0;
        if (direction == Direction.Down)
            return playerPosition.GetRow() - 1 >= 0;
        return false;
    }

    public bool IsPlayerAtTopOfBoard(Player player)
    {
        return boardDimensions.BoardLength == player.GetPosition().GetRow() + 1;
    }
}