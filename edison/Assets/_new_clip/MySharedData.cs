/// <summary>
/// MySharedData.cs
/// data persistance
/// NB there are no UnityEngine libraries or MonoBehaviour inheritance allowed here and this script is not attached to a GamObject
/// </summary>

public static class MySharedData {

    // for keeping track of which house was last selected from the haus clipboard
    public enum hausClipboards { house, hillfort, conference};
    public static hausClipboards lastActiveClipboard;

    // for keeping track of the wind slider position
    public static int windSliderPos = 0;
}
