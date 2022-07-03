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

    public interface Player : Codebreaker, Codemaker { }
}