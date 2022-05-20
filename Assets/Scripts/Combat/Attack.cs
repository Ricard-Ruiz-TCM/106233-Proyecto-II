using UnityEngine;

[CreateAssetMenu(fileName = "new Attack", menuName = "Drawable/Attacks")]
public class Attack : ScriptableObject {

    public int Damage;
    public int InkCost;
    public float Range;
    public float Cooldown;

}
