using System;
using System.Threading.Tasks;
using static Runtime.Domain.CodeColorExtensions;

namespace Runtime.Domain
{
    public interface Codebreaker
    {
        Task AttemptGuess();
    }

    public interface Codemaker
    {
        Task PlaceSecretCode();
    }

    public class RandomPlayer : Codemaker, Codebreaker
    {
        readonly Board board;
        readonly Random random;

        public RandomPlayer(Board board)
        {
            this.board = board;
            random = new Random();
        }

        public Task PlaceSecretCode()
        {
            board.PinSecretCode(RandomComb());
            return Task.CompletedTask;
        }

        public Task AttemptGuess()
        {
            return default;
        }

        #region Support methods
        Combination RandomComb()
        {
            var pegs = new CodeColor[Combination.PegsCount];

            for(var i = 0; i < pegs.Length; i++)
                pegs[i] = RandomColor(random);

            return new Combination(pegs);
        }
        #endregion
    }
}