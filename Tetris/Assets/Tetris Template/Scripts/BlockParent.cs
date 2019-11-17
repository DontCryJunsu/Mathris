using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockParent : MonoBehaviour
{
    public Number number;
    public BlockChild[] child;
    public void ReturnWhiteChild()
    {
        for(int i =0;i<child.Length;i++)
            child[i].ReturnWhite();
    }
}
