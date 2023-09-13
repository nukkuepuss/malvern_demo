/// <summary>
/// BoxHand.cs
/// for specifying which hand (controller) the text box should be parented to in the tutorial
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jonrummery.edison {

    public class BoxHand : MonoBehaviour {

        [HideInInspector]
        public enum handChoice { left, right };

        public handChoice whichHand;
    }
}
