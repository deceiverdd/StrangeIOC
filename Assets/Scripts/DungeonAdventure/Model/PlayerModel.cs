public class PlayerModel : IPlayerModel
{
    public float HP { get => hP; set => hP = value; }
    public float MP { get => mP; set => mP = value; }
    public float MovSpeed { get => movSpeed; set => movSpeed = value; }
    public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }

    private float hP;
    private float mP;
    private float movSpeed;
    private float atkSpeed;

    public PlayerModel()
    {

    }

    public void ApplyDamage(float damage)
    {
        HP -= damage;
    }
}
