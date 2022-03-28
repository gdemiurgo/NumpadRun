using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public static PlatformPool instance;

    [SerializeField] private Platform[] platforms;

    private int platformLength;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RequestPlatform(int length)
    {
        Platform newPlatform = platforms[length - 1];

        foreach(GameObject platform in newPlatform.platforms)
        {
            if(!platform.activeInHierarchy)
            {
                return platform;
            }
        }

        return null;
    }
}
