using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace santennio.utils.ui
{
    public class FixLayoutWithFit : MonoBehaviour
    {
        // Start is called before the first frame update
        void OnEnable()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        }
        
    }
}


