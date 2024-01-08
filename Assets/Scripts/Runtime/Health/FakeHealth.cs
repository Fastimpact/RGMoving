namespace RunGun.Gameplay
{
    public class FakeHealth : IHealth
    {
        public bool IsAlive => true;
        
        public bool IsInvulnerable => true;

        public int Value => 100;

        public int MaxValue => 100;
        
        public void TakeDamage(int damage)
        {
            
        }

        public void Heal(int heal)
        {
        }

        public void SetInvulnerability(bool isInvulnerable)
        {
        }
    }
}