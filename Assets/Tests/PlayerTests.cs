using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Tests
{
    public class PlayerTests
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
        public void Codebreaker_AttemptsGuess_InBoard()
        {
            var doc = BoardBuilder.Board().Build();
            Codebreaker sut = new RandomPlayer(doc);

            sut.AttemptGuess();
            doc.IsGuessTurn.Should().BeTrue();
        }
    }
}