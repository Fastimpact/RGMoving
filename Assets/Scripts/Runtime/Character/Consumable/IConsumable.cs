
namespace RunGun.Gameplay
{
    public interface IConsumable
    {
        void Consume();
        int GetRemainingUses();
        void SetRemainingUses(int uses);
    }
    
}
