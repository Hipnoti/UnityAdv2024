using UnityEngine;


[CreateAssetMenu(fileName = "Fire Hazard Data",
    menuName = "Obstacles/Fire Hazard Data", order = 0)]
public class FireHazardScriptableObject : ScriptableObject
{
    // public uint Damage => fireDamage;
    //
    // [Header("Damage")]
    // [SerializeField] private uint fireDamage;
    [SerializeField] private int minimumDamage;
    [SerializeField] private int maximumDamage;

    public uint GetRandomFireDamage()
    {
        return (uint)Random.Range(minimumDamage, maximumDamage + 1);
    }
}
