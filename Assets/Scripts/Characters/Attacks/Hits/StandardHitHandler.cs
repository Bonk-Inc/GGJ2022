using UnityEngine;

public class StandardHitHandler : Hittable
{
    private const int NpcKarmaHitDecrease = 200;
    private const int EnemyKarmaHitDecrease = 20;
    
    [SerializeField]
    private Health health;

    public override void Hit(HitData hitData)
    {
        var hitDamage = hitData.damage;
        SetPlayerKarmaBasedOnAttack(hitData.attackerTag, hitData.attackingTag, hitDamage);
        health.Damage(hitDamage);
    }

    private void SetPlayerKarmaBasedOnAttack(string attackerTag, string attackingTag, int damage)
    {
        if (attackerTag != "Player")
            return;

        switch (attackingTag)
        {
            case "Enemy":
                GameManager.instance.karma.Decrease(EnemyKarmaHitDecrease * damage);
                break;
            case "Neutral":
                GameManager.instance.karma.Decrease(NpcKarmaHitDecrease * damage);
                break;
        }
    }
}
