namespace TeamTextRPG.Byungchul
{
    public class Drop
    {
        public string Name { get; }
        public int Type { get; }  //0 = HP 포션 1 = MP포션 3,4 = 소모 아이템
        public string Desc { get; }
        public int Value { get; }

        public string DisplayTypeText
        {
            get
            {
                if (Type == 0)
                    return "HP 회복";
                else if (Type == 1)
                    return "MP 회복";
                else if (Type == 2)
                    return "판매 아이템";
                else return "";
            }
        }

        public Drop(string name, int type, string desc, int value)
        {
            Name = name;
            Type = type;
            Desc = desc;
            Value = value;
        }

        public string DropInfoText()
        {
            return $"{Name}  |  {DisplayTypeText} |  {Desc}";
        }

    }



}
