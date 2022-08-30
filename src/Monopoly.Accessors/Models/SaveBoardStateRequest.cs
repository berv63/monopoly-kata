namespace Monopoly.Accessors.Models
{
    public class SaveBoardStateRequest
    {
        public int? GameId { get; set; }
        public BoardState BoardState { get; set; }
    }
}