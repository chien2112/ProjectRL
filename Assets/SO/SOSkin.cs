using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSkin", menuName = "ScriptableObjects/Skin")]
public class SOSkin : ScriptableObject
{
    [Header("Skins")]
    public List<GameObject> hats;
    public List<GameObject> hairStyles;
    public List<GameObject> faces;
    public List<GameObject> heads;
    public List<GameObject> backs;
    public List<GameObject> outfits;

    [Header("Materials")]
    public List<Material> basicColors;

    [Header("Materials White Skin")]
    public List<Material> mat_Heads_White;
    public List<Material> mat_Outfits_White;

    [Header("Materials Dark Skin")]
    public List<Material> mat_Heads_Dark;
    public List<Material> mat_Outfits_Dark;
}
