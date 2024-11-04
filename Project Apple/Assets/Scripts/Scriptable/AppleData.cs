using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAppleData", menuName = "Scriptable/AppleData", order = 0)]
public class AppleData : ScriptableObject
{
    [SerializeField] string key;
    public string Key => key;
    [SerializeField] Vector3 rotation;
    public Vector3 Rotation => rotation;
    [SerializeField] Vector3 scale;
    public Vector3 Scale => scale;
    [SerializeField] GameObject prefab;
    public GameObject Prefab => prefab;
    [SerializeField] Sprite selectSprite;
    public Sprite Sprite => selectSprite;

    public bool IsUnlocked;
    public bool IsEquiped;
}
