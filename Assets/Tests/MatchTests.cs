using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Tests
{
    public class MatchTests
    {
        [Test]
        public async Task PlayRoundOfMatch()
        {
            var sut = new Match(b => new RandomPlayer(b));

            sut.Round.Should().Be(1);

            await sut.AskForCode();
            await sut.PlayRound();

            sut.Round.Should().Be(2);
        }
    }
}