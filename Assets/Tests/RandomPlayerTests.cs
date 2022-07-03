using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;
using static Tests.CombinationBuilder;

namespace Tests
{
    public class RandomPlayerTests
    {
        [Test]
        public async Task Codemaker_PlacesSecretCode_InBoard()
        {
            var doc = BoardBuilder.Board().WithoutSecretCode().Build();
            Codemaker sut = new RandomPlayer(doc);

            await sut.PlaceSecretCode();
            doc.IsGuessTurn.Should().BeTrue();
        }

        [Test]
        public async Task Codebreaker_AttemptsGuess_InBoard()
        {
            var doc = BoardBuilder.Board().Build();
            Codebreaker sut = new RandomPlayer(doc);

            doc.IsGuessTurn.Should().BeTrue();
            await sut.AttemptGuess();
            doc.IsGuessTurn.Should().BeFalse();
        }

        [Test]
        public async Task Codemaker_GiveFeedback_InBoard()
        {
            var doc = BoardBuilder.Board().Build();
            doc.PinGuessPegs(Combination().AllRandom().Build());
            Codemaker sut = new RandomPlayer(doc);

            doc.IsGuessTurn.Should().BeFalse();
            await sut.GiveFeedback();
            doc.IsGuessTurn.Should().BeTrue();
        }
    }
}