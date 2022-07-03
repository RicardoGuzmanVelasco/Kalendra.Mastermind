using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class Match
    {
        readonly Board board;
        readonly Codemaker codemaker;
        readonly Codebreaker codebreaker;

        public Match([NotNull] Func<Board, Player> playerGetter)
        {
            board = new Board(Board.DefaultRowsCount, secretCode: null);
            codemaker = playerGetter.Invoke(board);
            codebreaker = playerGetter.Invoke(board);

            Require(codemaker).Not.Null();
            Require(codebreaker).Not.Null();
        }

        /// 1-BASED INDEX !!! 
        public int Round => board.Round;

        public async Task AskForCode()
        {
            await codemaker.PlaceSecretCode();
        }

        public async Task PlayRound()
        {
            await codebreaker.AttemptGuess();
            await codemaker.GiveFeedback();
        }
    }
}