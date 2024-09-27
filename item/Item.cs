using System.Security.Cryptography.X509Certificates;

class Item
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

class ItemDBs
{
    public List<Item> Items { get; set; }

    public ItemDBs()
    {
        Items = new List<Item>
        {
            //string rare, string name, int type, int akp, string desc, int value
            new Item("커먼","목검", 0, 5, "단단한 목검이다.", 500),
            new Item("커먼","철검", 0, 6, "평범한 철검이다.", 800),
            new Item("언커먼","카타나", 0, 8, "날카로운 카타나다.", 1200),
            new Item("언커먼","재련된 강철검", 0, 12, "강철 검이라고 해서 다 같은 강철 검은 아니지", 1500),
            new Item("레어","초진동검", 0, 15, "첨단 기술이 적용된 검이다.", 2000),
            new Item("레어","카타나", 0, 17, "작열하는 불의 기운을 담고 있는 도.", 2500),
            new Item("커먼","면 셔츠", 1, 3, "면으로 만든 셔츠다. 최소한의 방어력을 제공한다.", 500),
            new Item("언커먼","가죽 조끼", 1, 5, "가죽으로 만든 셔츠다. 낮은 방어력을 제공한다.", 800),
            new Item("언커먼","가죽 스커트", 1, 5, "가죽으로 만든 치마다. 남자도 입을 수 있다.", 800),
            new Item("레어","체인메일", 1, 9, "철사 따위로 만든 고리를 엮은 사슬 형태로 된 갑옷이다.", 1500),
            new Item("레어","플레이트 아머", 1, 12, "고딕 양식 판금 갑옷이다.", 2000),
        };
    }
    public void DisplayItems()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Console.WriteLine($"{i+1}  |  {Items[i].Name}  |  {(Items[i].Type == 0 ? "공격력" : "방어력")} + {Items[i].Akp}  |  {Items[i].Desc}  |  {Items[i].Value}");
        }
    }

}