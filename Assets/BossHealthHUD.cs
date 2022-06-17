using UnityEngine;

public class BossHealthHUD : BarHUDElement
{

    public BossAttack _boss;

    void OnEnable()
    {
        BossAttack.OnHealthChange += UpdateBar;
    }

    void OnDisable()
    {
        BossAttack.OnHealthChange -= UpdateBar;
    }

    public void UpdateBar()
    {
        fillRBar(_boss.Health, 200.0f);
    }

}
