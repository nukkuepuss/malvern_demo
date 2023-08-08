/// <summary>
/// MySharedData.cs
/// data persistance
/// NB there are no UnityEngine libraries or MonoBehaviour inheritance allowed here and this script is not attached to a GamObject
/// </summary>

public static class MySharedData {

    // for tracking whether the tutorial has been completed
    public static bool hasCompletedTutorial = false;

    // for keeping track of which house was last selected from the haus clipboard
    public enum hausClipboards { house, hillfort, conference, none};
    public static hausClipboards lastActiveClipboard;

    // for keeping track of the wind slider position
    public static int windSliderPos = 0;

    // for keeping track of the movement speed settings (so that the slider colliders only activate when adjacent)
    public static int movementSliderPos = 5;
    public static int boostSliderPos = 5;
}
