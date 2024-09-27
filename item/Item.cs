public class Item
{
    public string Rare { get; }
    public string Name { get; }
    public int Type { get; }  //0 = 무기, 1 = 방어구
    public int Akp { get; }
    public string Desc { get; }
    public int Value { get; }

    public string DisplayTypeText
    {
        get
        {
            return Type == 0 ? "공격력" : "방어력";
        }
    }

    public Item(string rare, string name, int type, int akp, string desc, int value)
    {
        Rare = rare;
        Name = name;
        Type = type;
        Akp = akp;
        Desc = desc;
        Value = value;
    }

    public string ItemInfoText()
    {
        return $"{Name}  |  {DisplayTypeText} +{Akp}  |  {Desc} ";
    }
}

