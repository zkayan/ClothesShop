using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cloth", menuName = "Cloth")]
public class SO_Cloth : ScriptableObject
{
    public string ClothName;
    public float ClothPrice;
    public string bodyPartType;
    public int bodyPartAnimationID;
    public Sprite Icon;

    public List<AnimationClip> allBodyPartAnimations = new List<AnimationClip>();
}