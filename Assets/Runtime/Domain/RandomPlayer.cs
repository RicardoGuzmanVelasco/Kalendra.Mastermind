using System.Threading.Tasks;

namespace Runtime.Domain
{
    public interface Codemaker
    {
        Task PlaceSecretCode();
    }

    public class RandomPlayer : Codemaker
    {
        readonly Board board;

        public RandomPlayer(Board board)
        {
            this.board = board;
        }

        public Task PlaceSecretCode()
        {
            throw new System.NotImplementedException();
        }
    }
}