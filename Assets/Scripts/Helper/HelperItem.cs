using UnityEngine;

[CreateAssetMenu(fileName = "new Helper", menuName = "Drawable/Helper")]
public class HelperItem : ScriptableObject {

    public int ID;
    public Sprite Portrait;
    public string Text;

}
