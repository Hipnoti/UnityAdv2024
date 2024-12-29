using UnityEngine;

[CreateAssetMenu(fileName = "Fire Hazard Data",
    menuName = "Obstacles/Fire Hazard Data", order = 0)]
public class FireHazardScriptableObject : ScriptableObject
{
    public int damage;
    // [SerializeField] private int minimumDamage;
    // [SerializeField] private int maximumDamage;
    //
    // public int GetRandomFireDamage()
    // {
    //     return Random.Range(minimumDamage, maximumDamage + 1);
    // }
}
