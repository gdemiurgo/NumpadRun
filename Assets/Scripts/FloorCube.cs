using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCube : MonoBehaviour
{
    [SerializeField] private TextMesh numText;

    public void SetNumText(int num)
    {
        numText.text = num.ToString();
    }
}
