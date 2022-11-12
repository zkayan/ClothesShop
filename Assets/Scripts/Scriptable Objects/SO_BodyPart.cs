using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")]
public class SO_BodyPart : ScriptableObject
{
    public string ClothName;
    public float ClothPrice;
    public string bodyPartType;
    public int bodyPartAnimationID;

    public List<AnimationClip> allBodyPartAnimations = new List<AnimationClip>();
}