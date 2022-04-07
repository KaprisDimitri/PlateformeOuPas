using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InputsStorage")]
public class InputsStorage : ScriptableObject
{
    public KeyCode right;
    public KeyCode left;
    public KeyCode jump;
    public KeyCode changeWorld;
}
