using System;
using System.Linq;
using System.Threading.Tasks;

namespace Runtime.Domain
{
    public interface Codebreaker
    {
        Task AttemptGuess();
    }

    public interface Codemaker
    {
        Task PlaceSecretCode();
        Task GiveFeedback();
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
            board.PinSecretCodePegs(RandomComb());
            return Task.CompletedTask;
        }

        public Task AttemptGuess()
        {
            board.PinGuessPegs(RandomComb());
            return Task.CompletedTask;
        }

        public Task GiveFeedback()
        {
            board.PinFeedbackPegs(RandomFeedbackNoAllBlacks());
            return Task.CompletedTask;
        }

        #region Support methods
        Combination RandomComb()
        {
            var pegs = new CodeColor[Combination.PegsCount];

            for(var i = 0; i < pegs.Length; i++)
                pegs[i] = CodeColorExtensions.RandomCodeColor(random);

            return new Combination(pegs);
        }

        GuessFeedback RandomFeedbackNoAllBlacks()
        {
            var pegs = new KeyColor[Combination.PegsCount];

            for(var i = 0; i < pegs.Length; i++)
                pegs[i] = KeyColorExtensions.RandomKeyColor(random);

            return pegs.All(c => c == KeyColor.Black)
                ? RandomFeedbackNoAllBlacks()
                : new GuessFeedback(pegs);
        }
        #endregion
    }
}