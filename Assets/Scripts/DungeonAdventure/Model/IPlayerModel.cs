public interface IPlayerModel
{
    float HP { get; set; }
    float MP { get; set; }
    float MovSpeed { get; set;}
    float AtkSpeed { get; set;}

    void ApplyDamage(float damage);
}
