
namespace UIDefine
{
    public enum UIScreenType
    {
        title,
        battle,
        mapSelect,
        bakery,
    }

    [System.Flags]
    public enum BakeryUIFilterType
    {
        all,
        floor,
        liquid,
        sugar,
        leaven,
        butterfat
    }

    public enum PanelType
    {
        option,
        normal
    }
}
