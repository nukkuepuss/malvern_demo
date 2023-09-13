/// <summary>
/// MySharedData.cs
/// data persistance
/// NB there are no UnityEngine libraries or MonoBehaviour inheritance allowed here and this script is not attached to a GamObject
/// </summary>

namespace com.jonrummery.edison {

    public static class MySharedData {

        // for tracking whether the tutorial has been completed
        public static bool hasCompletedTutorial = false;

        // for keeping track of which house was last selected from the haus clipboard
        public enum hausClipboards { house, hillfort, conference, none };
        public static hausClipboards lastActiveClipboard;

        // for keeping track of the wind slider position
        public static int windSliderPos = 0;

        // to check whether the sun has moved (flagged 'true' so that intial value can be diplayed)
        public static bool hasSunMoved = true;

        // for keeping track of the movement speed settings (so that the slider colliders only activate when adjacent)
        public static int movementSliderPos = 5;
        public static int boostSliderPos = 5;

        // for keeping track of which waypoint marker is next
        public static int nextWaypoint = 0;

        // so that waypoint prefabs can access manager script
        public static WaypointManager _waypointManager;
    }
}
