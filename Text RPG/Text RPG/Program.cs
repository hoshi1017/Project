using System;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using static Text_RPG.Program.TextRpg;


namespace Text_RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextRpg.textrpg.StartGame();
        }
        public class TextRpg
        {
            public class Monster
            {   
                public static Random random = new Random();
                public string Name;
                public int Hp;
                public int Attack;
                public int Defense;
                public Monster(string name, int hp, int attack, int defense)
                {
                    Name = name;
                    Hp = hp;    
                    Attack = attack;    
                    Defense = defense;

                }
                public static Monster CreateRandomMonster()
                {
                    string[] MonsterNames = {"고블린", "오크", "드래곤","스켈레톤","좀비","슬라임" };
                    string name = MonsterNames[random.Next(MonsterNames.Length)];
                    int hp = random.Next(30, 50);
                    int attack = random.Next(5, 7);
                    int defense = random.Next(3, 6);

                    return new Monster(name, hp, attack, defense);

                }
                
                
                public static void EnterDungeonEntrance()
                {
                    Console.WriteLine("더 깊이 들어가볼까?\n");
                    Console.WriteLine("1. 네\n0. 도망가기\n");
                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 1)
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                    }

                    if (choice == 1)
                    {
                        EnterRealDungeon();
                    }
                    else
                    {
                        Console.WriteLine("도망친 곳에 낙원은 없다.\n");
                        Console.WriteLine("계속하시려면 엔터를 눌러주세요.\n");
                        Console.ReadLine();
                        TextRpg.textrpg.Play();
                    }

                }
                public static void EnterRealDungeon()
                {
                    Monster randomMonster = CreateRandomMonster();
                    randomMonster.DisplayMonsterInfo();
                }
                public void DisplayMonsterInfo()
                {
                    Console.WriteLine($"이름: {Name}");
                    Console.WriteLine($"체력: {Hp}");
                    Console.WriteLine($"공격력: {Attack}");
                    Console.WriteLine($"방어력: {Defense}");
                }
            }
            private List<Item> inventory = new List<Item>();
            private List<Item> shopItems = new List<Item>();


            private enum Job { 전사 = 1, 마법사, 도적, 궁수, 해적 }
            
            public static TextRpg textrpg = new TextRpg();

            
            public int Lv { get; set; } = 1;
            public int Atk { get; set; } = 10;

            public int Amr { get; set; } = 5;
            public int Hp { get; set; } = 100;
            public int Gold { get; set; } = 1500;
            int choice;
            public string name;
            int jobValue;
            string jobName;

            string welcome = "스파르타 마을에 오신 여러분 환영합니다.\n";
            public bool IsDead = false;

            public class Item 
            {
                public bool isPurchased = false;
                public string ItemName { get; set; }
                public int ItemAtk { get; set; }
                public int ItemAmr { get; set; }
                public string ItemInfo { get; set; }

                public int ItemValue { get; set; }

                public Item(string itemName, int itemAtk, int itemAmr, string itemInfo, int itemValue)
                {
                    ItemName = itemName;
                    ItemAtk = itemAtk;
                    ItemAmr = itemAmr;
                    ItemInfo = itemInfo;
                    ItemValue = itemValue;
                        
                }



            }


            
        



            public void InitalizeShopItems()
            {
                shopItems.Add(new Item("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500));
                shopItems.Add(new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 4500));
                shopItems.Add(new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 600));
                shopItems.Add(new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
                shopItems.Add(new Item("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
                shopItems.Add(new Item("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
            }


            public void StartGame()
            {
                InitalizeShopItems();
                Console.WriteLine(welcome);
                Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                Console.ReadLine();
                Console.Clear();
                MakeName();

            }
            public void MakeName()
            {
                Console.Clear();
                Console.WriteLine(welcome);
                Console.WriteLine("원하시는 이름을 설정해주세요.");


                do
                {
                    name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                    }
                }
                while (string.IsNullOrWhiteSpace(name));

                Console.WriteLine($"입력하신 이름은 {name} 입니다\n");
                Console.WriteLine("1. 저장\n2. 취소\n");             
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");


                while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }

                if (choice == 1)
                {
                    Console.WriteLine("직업선택으로 이동합니다.\n");
                    Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                    Console.ReadLine();
                    ChooseJob();

                }
                else
                {
                    MakeName();
                }
            }







            public void ChooseJob()
            {
                
                Console.Clear();
                Console.WriteLine(welcome);
                Console.WriteLine("원하시는 직업을 선택해 주세요.\n");
                
                Console.WriteLine("1. 전사\n2. 마법사\n3. 도적\n4. 궁수\n5. 해적\n");


                while (!(int.TryParse(Console.ReadLine(), out jobValue) && jobValue >= 1 && jobValue <= 5))
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                    
                }


                jobName = Enum.GetName(typeof(Job), jobValue);
                Console.WriteLine("내가 선택한 직업은 {0}. 드디어 초보자 탈출이다!\n", jobName);
                Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                Console.ReadLine();
                Play();



            }

            public void Play()
            {
                Console.Clear();
                Console.WriteLine("던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입구\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");





                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }

                switch (choice)
                {
                    case 1:
                        ShowStatus();
                        break;
                    case 2:
                        ShowInventory();
                        break;
                    case 3:
                        ShowShop();
                        break;
                    case 4:
                        Monster.EnterDungeonEntrance();
                        
                        break;


                }

            }
            public void ShowStatus()
            {

                Console.Clear();
                Console.WriteLine("상태 보기\n\n");
                
                Console.WriteLine("Lv. " + Lv.ToString("D2"));
                Console.WriteLine(name +  " ( " + jobName + " )");
                Console.WriteLine("공격력 : " + Atk + " (+ " + (Atk - 10) + ")");
                Console.WriteLine("방어력 : " + Amr + " (+ " + (Amr - 5) + ")");
                Console.WriteLine("체  력 : " + Hp);
                Console.WriteLine("Gold : " + Gold + " G\n");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice != 0)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }
                if (choice == 0)
                {
                    Play();
                }


            }
            public void ShowInventory()
            {

                Console.Clear();
                Console.WriteLine("인벤토리\n\n");
                Console.WriteLine("[아이템 목록]\n\n");
                Console.WriteLine("1. 장착관리");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 1)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }
                if (choice == 0)
                {
                    Play();
                }
                else if (choice == 1)
                {
                    EquipManageMent();
                }





            }

            public void ShowShop()
            {


                Console.Clear();
                Console.WriteLine("상점\n\n");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 1))
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }
                if (choice == 0)
                {
                    Play();
                }
                else if (choice == 1)
                {

                    ShopInventory();

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }
            }

            private void ShopInventory()
            {   
                Console.Clear();
                Console.WriteLine("상점\n\n");
                Console.WriteLine("[보유 골드]\n" + Gold + " G\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < shopItems.Count; i++)
                {
                    if (!shopItems[i].isPurchased)
                    {
                        Console.WriteLine($"{i + 1}. {shopItems[i].ItemName} (공격력: {shopItems[i].ItemAtk}, 방어력: {shopItems[i].ItemAmr}, {shopItems[i].ItemInfo} 가격: {shopItems[i].ItemValue} G)");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {shopItems[i].ItemName} (구매 완료)");
                    }
                  
                }
                Console.WriteLine("0. 나가기");


                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > shopItems.Count))
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }

                if (choice == 0)
                {
                    ShowShop();
                }
                else if(choice > 0 && choice <= shopItems.Count) 
                {
                    PurchaseItem(choice - 1);
                }
               
            }

            
            private void PurchaseItem(int index)
            {
                if (shopItems[index].ItemValue > Gold)
                {
                    Console.WriteLine("Gold가 부족합니다.\n");
                    Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                    Console.ReadLine();
                    ShopInventory();
                }
                else if (shopItems[index].isPurchased)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.\n");
                    Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                    Console.ReadLine();
                    ShopInventory();
                }
                else
                {
                    Item purchasedItem = shopItems[index];

                    Gold -= purchasedItem.ItemValue;


                    inventory.Add(purchasedItem);

                    Console.WriteLine($"아이템 {purchasedItem.ItemName}구매를 완료했습니다.");
                    shopItems[index].isPurchased = true;

                    Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                    Console.ReadLine();
                    ShopInventory();
                }
            }
            private bool[] equippedItems;
            public void EquipManageMent()
            {
                if (equippedItems == null || equippedItems.Length != inventory.Count)
                {
                    equippedItems = new bool[inventory.Count];
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("장착 관리\n\n");
                    Console.WriteLine("[아이템 목록]\n\n");


                    for (int i = 0; i < inventory.Count; i++)
                    {
                        string equippedMark = "[E]";
                        if (equippedItems[i] == true)
                        {
                            Console.WriteLine($"{i + 1}. {equippedMark}{inventory[i].ItemName} (공격력: {inventory[i].ItemAtk}, 방어력: {inventory[i].ItemAmr}, 가격: {inventory[i].ItemValue}G)");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. {inventory[i].ItemName} (공격력: {inventory[i].ItemAtk}, 방어력: {inventory[i].ItemAmr}, 가격: {inventory[i].ItemValue}G)");
                        }
                    }
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.\n");

                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > inventory.Count))
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                    }

                    if (choice == 0)
                    {
                        ShowInventory();
                        break;
                    }
                    else
                    {
                        ToggleEquip(choice - 1);
                    }
                }
            }


            private void ToggleEquip(int index)
            {
                var item = inventory[index];                
                equippedItems[index] = !equippedItems[index]; 

                if (equippedItems[index])
                {
                    Console.WriteLine($"아이템 {item.ItemName}이 장착되었습니다.\n");
                    Atk += item.ItemAtk;
                    Amr += item.ItemAmr;
                }
                else
                {
                    Console.WriteLine($"아이템 {item.ItemName}이 장착 해제되었습니다.\n");
                    Atk -= item.ItemAtk;
                    Amr -= item.ItemAmr;
                }

                Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                Console.ReadLine();
                


            }
            





        }
           
            


        
    }
}

    