using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGING : MonoBehaviour {

    GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>().gameObject;
    }

    public void ToCubeTemplate() {
        _player.transform.position = new Vector2(93.8f, -18.5f);
    }

    public void ToBombTemplate() {
        _player.transform.position = new Vector2(177.5f, -17.9f);
    }

    public void ToLightTemplate() {
        _player.transform.position = new Vector2(-2.2f, -64.5f);
    }

    public void ToCave() {
        _player.transform.position = new Vector2(239.2f, -17.2f);
    }

    public void ToForest() {
        GameManager.Instance.GOTOLVL1();
        FixtLight();
        _player.transform.position = new Vector2(-15.3f, -18.8f);
    }

    public void Respawn() {
        _player.GetComponent<Player>().Respawn();
    }
    
    public void FixtLight(){
        GameObject.FindObjectOfType<ShadowIntensity>().Fix();
    }

    public void FullINK(){
        _player.GetComponent<Player>().FillInk();
    }

    public void FullHP(){
        _player.GetComponent<Player>().FillHealth();
    }

    public void KillXDamage(){
        _player.GetComponent<Player>().TakeDamage(100, DEATH_CAUSE.D_DAMAGE);
    }

    public void KillXFall(){
        _player.GetComponent<Player>().TakeDamage(100, DEATH_CAUSE.D_FALL);
    }

    public void TakeDmg(){
        _player.GetComponent<PlayerCombat>().TakeDamage(Resources.Load<Attack>("ScriptableObjects/Attacks/Destroyer"));
    }
    
    public void BossTime() {
        _player.transform.position = new Vector2(82.6f, -59.2f);
    }

    public void GetAll() {
        _player.transform.position = new Vector2(-24.4f, -12.10f);
    }

    public IEnumerator GetAllTemplates(){
        yield return new WaitForEndOfFrame();
        ToCubeTemplate();
        yield return new WaitForEndOfFrame();
        ToBombTemplate();
        yield return new WaitForEndOfFrame();
        ToLightTemplate();
        yield return new WaitForEndOfFrame();
        ToForest();
        yield return new WaitForEndOfFrame();
    }

}
