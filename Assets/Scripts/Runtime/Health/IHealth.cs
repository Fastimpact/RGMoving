namespace RunGun.Gameplay
{
    public interface IHealth
    {
        bool IsAlive { get; }

        bool IsInvulnerable { get; }

        int Value { get; }
        
        int MaxValue { get; }
        
        void TakeDamage(int damage);

        void Heal(int heal);

        void SetInvulnerability(bool isInvulnerable);
    }
}