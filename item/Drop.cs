namespace TeamTextRPG.Byungchul
{
    class Drop
    {
        public string Name { get; }
        public int Typ { get; }  //0 = HP 포션 1 = MP포션 2 = 판매 아이템
        public string Desc { get; }
        public int Value { get; }

        public string DisplayTypeText
        {
            get
            {
                if (Typ == 0)
                    return "HP 회복";
                else if (Typ == 1)
                    return "MP 회복";
                else if (Typ == 2)
                    return "판매 아이템";
                else return "";
            }
        }

        public Drop(string name, int typ, string desc, int value)
        {
            Name = name;
            Typ = typ;
            Desc = desc;
            Value = value;
        }

        public string DropInfoText()
        {
            return $"{Name}  |  {DisplayTypeText} |  {Desc}";
        }

    }



}
