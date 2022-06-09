using UnityEngine;

public class BossHealthHUD : BarHUDElement
{

    private BossAttack _boss;

    void OnEnable()
    {
        BossAttack.OnHealthChange += UpdateBar;
        Player.OnRespawn += () => { Invoke("findBoss", 2.0f); } ;
    }

    void OnDisable()
    {
        BossAttack.OnHealthChange -= UpdateBar;
        Player.OnRespawn -= () => { Invoke("findBoss", 2.0f); };
    }

    void findBoss(){
        _boss = GameObject.FindObjectOfType<BossAttack>();
        if (_boss == null) gameObject.SetActive(false);
    }

    void UpdateBar()
    {
        fillRBar(_boss.Health, 100.0f);
    }

}
