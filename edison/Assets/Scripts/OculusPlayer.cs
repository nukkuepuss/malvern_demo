using UnityEngine;
//
//For easily accesing local head and hand anchors
//
namespace com.jonrummery.edison {

    public class OculusPlayer : MonoBehaviour
    {
        public GameObject head;
        public GameObject rightHand;
        public GameObject leftHand;

        public static OculusPlayer instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}
