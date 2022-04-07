using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CollectableDonner")]
public class CollectableDonner : ScriptableObject
{
    [SerializeField] List<bool> collecter;

    public List<bool> Collecter
    {
        get { return collecter; }
    }

    public void SetCollect (int index)
    {
        collecter[index] = true;
    }
}
