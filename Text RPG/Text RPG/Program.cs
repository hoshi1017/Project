using System;






namespace Text_RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextRpg.Item.InitalizeShopItems();
            TextRpg.textrpg.StartGame();
            
        }
        public class TextRpg
        {
            
            public class Monster
            {
                public static Random random = new Random();
                public string Name { get; set; }
                public string NickName { get; set; }


                public Monster(string name, string nickname)
                {
                    Name = name;
                    NickName = nickname;

                }
                public static Monster CreateRandomMonster()
                {
                    string[] MonsterNames = { "고블린", "오크", "드래곤", "스켈레톤", "좀비", "슬라임" };
                    string name = MonsterNames[random.Next(MonsterNames.Length)];
                    string[] MonsterNickNames = { "[악독한]", "[지저분한]", "[어린]", "[우호적인]", "[공격적인]" };
                    string nickName = MonsterNickNames[random.Next(MonsterNickNames.Length)];
                    return new Monster(name, nickName);

                }




            }
            private List<Item> inventory = new List<Item>();
            public static List<Item> shopItems = new List<Item>();


            private enum Job { 전사 = 1, 마법사, 도적, 궁수, 해적 }

            public static TextRpg textrpg = new TextRpg();


            public int Lv { get; set; } = 1;
            public int Atk { get; set; } = 10;

            public static int Amr { get; set; } = 5;
            public static int MaxHp { get; set; } = 100;
            public static int CurrentHp { get; set; } = 100;
            public static int Gold { get; set; } = 1500;
            public static bool IsAlive { get; set; } = true;
            int choice;
            public string name;
            int jobValue;
            string jobName;

            string welcome = "스파르타 마을에 오신 여러분 환영합니다.\n";


            public class Item
            {
                public bool IsPurchased = false;
                public string ItemName { get; set; }
                public int ItemAtk { get; set; }
                public int ItemAmr { get; set; }
                public string ItemInfo { get; set; }

                public int ItemPrice { get; set; }

                public Item(string itemName, int itemAtk, int itemAmr, string itemInfo, int itemPrice)
                {
                    ItemName = itemName;
                    ItemAtk = itemAtk;
                    ItemAmr = itemAmr;
                    ItemInfo = itemInfo;
                    ItemPrice = itemPrice;

                }
                public static void InitalizeShopItems()
                {
                    shopItems.Add(new Item("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500));
                    shopItems.Add(new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 4500));
                    shopItems.Add(new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 600));
                    shopItems.Add(new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
                    shopItems.Add(new Item("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
                    shopItems.Add(new Item("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
                }


            }










            public void StartGame()
            {
                Console.Clear();
                Console.WriteLine(welcome);
                Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                Console.ReadLine();
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
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입구\n5. 휴식하기");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");





                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
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
                        EnterDungeonEntrance();

                        break;
                    case 5:
                        GetRest();
                        break;


                }

            }
            public void ShowStatus()
            {

                Console.Clear();
                Console.WriteLine("상태 보기\n\n");

                Console.WriteLine("Lv. " + Lv.ToString("D2"));
                Console.WriteLine(name + " ( " + jobName + " )");
                Console.WriteLine("공격력 : " + Atk + " (+ " + (Atk - 10) + ")");
                Console.WriteLine("방어력 : " + Amr + " (+ " + (Amr - 5) + ")");
                Console.WriteLine($"체  력 :  {CurrentHp}");
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
                Console.WriteLine("1. 장착 관리");
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
                    if (!shopItems[i].IsPurchased)
                    {
                        Console.WriteLine($"{i + 1}. {shopItems[i].ItemName} (공격력: {shopItems[i].ItemAtk}, 방어력: {shopItems[i].ItemAmr}, {shopItems[i].ItemInfo} 가격: {shopItems[i].ItemPrice} G)");
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
                else if (choice > 0 && choice <= shopItems.Count)
                {
                    PurchaseItem(choice - 1);
                }

            }


            private void PurchaseItem(int index)
            {
                if (shopItems[index].ItemPrice > Gold)
                {
                    Console.WriteLine("Gold가 부족합니다.\n");
                    Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                    Console.ReadLine();
                    ShopInventory();
                }
                else if (shopItems[index].IsPurchased)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.\n");
                    Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.");
                    Console.ReadLine();
                    ShopInventory();
                }
                else
                {
                    Item purchasedItem = shopItems[index];

                    Gold -= purchasedItem.ItemPrice;


                    inventory.Add(purchasedItem);

                    Console.WriteLine($"아이템 {purchasedItem.ItemName}구매를 완료했습니다.");
                    shopItems[index].IsPurchased = true;

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
                            Console.WriteLine($"{i + 1}. {equippedMark}{inventory[i].ItemName} (공격력: {inventory[i].ItemAtk}, 방어력: {inventory[i].ItemAmr}, 가격: {inventory[i].ItemPrice}G)");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. {inventory[i].ItemName} (공격력: {inventory[i].ItemAtk}, 방어력: {inventory[i].ItemAmr}, 가격: {inventory[i].ItemPrice}G)");
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
                        var item = inventory[choice - 1];
                        equippedItems[choice - 1] = !equippedItems[choice - 1];

                        if (equippedItems[choice - 1])
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
            public static void EnterDungeonEntrance()
            {
                int choice;

                Console.Clear();
                Console.WriteLine("더 깊이 들어가볼까?\n");
                Console.WriteLine("1. 쉬운 던전 l 방어력 5 이상 권장\n2. 일반 던전 l 방어력 11 이상 권장\n3. 어려운 던전 l 방어력 17 이상 권장\n0. 도망가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }

                if (choice >= 1 && choice <= 3)
                {
                    EnterRealDungeon(choice);
                }
                else if (choice == 0)
                {
                    Console.WriteLine("도망친 곳에 낙원은 없다.\n");
                    Console.WriteLine("계속하시려면 엔터를 눌러주세요.\n");
                    Console.ReadLine();
                    textrpg.Play();
                }




            }
            public static void EnterRealDungeon(int value)
            {
                Random rand = new Random();
                if (value == 1)
                {
                    Monster randomMonster = Monster.CreateRandomMonster();
                    Console.WriteLine($"{randomMonster.NickName} {randomMonster.Name}을 만났습니다.\n");
                    if (Amr - 5 > 0)
                    {
                        
                        CurrentHp -= rand.Next(20 + (5 - Amr), 35 + (5 - Amr));
                        
                        
                        if (CurrentHp <= 0)
                        {
                            textrpg.Die(); 
                        }
                        else
                        {
                            Console.WriteLine("던전을 클리어했습니다.\n");
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Gold += 1500;
                            Console.WriteLine($"현재골드: {Gold}\n");
                            
                            
                            Console.ReadLine();
                            EnterDungeonEntrance(); 
                        }
                    }
                    else
                    {
                        
                        if (rand.Next(1, 11) < 5)
                        {
                            Console.WriteLine("던전을 클리어하지 못했습니다.\n");
                            CurrentHp = CurrentHp / 2;
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();

                            
                            

                            
                            if (CurrentHp <= 0)
                            {
                                textrpg.Die(); 
                            }
                            else
                            {
                                EnterDungeonEntrance(); 
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("던전을 클리어했습니다.\n");
                            CurrentHp -= rand.Next(20 + (5 - Amr), 35 + (5 - Amr));
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Gold += 1500;
                            Console.WriteLine($"현재골드: {Gold}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();

                            
                            

                            
                            if (CurrentHp <= 0)
                            {
                                textrpg.Die(); 
                            }
                            else
                            {
                                EnterDungeonEntrance(); 
                            }
                            
                        }   

                    }
                }
                else if (value == 2)
                {
                    if (Amr - 11 > 0)
                    {
                        
                        CurrentHp -= rand.Next(20 + (11 - Amr), 35 + (11 - Amr));

                       
                        if (CurrentHp <= 0)
                        {
                            textrpg.Die(); 
                        }
                        else
                        {
                            Console.WriteLine("던전을 클리어했습니다.\n");
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Gold += 1700;
                            Console.WriteLine($"현재골드: {Gold}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();
                            EnterDungeonEntrance(); 
                        }
                    }
                    else
                    {
                        
                        if (rand.Next(1, 11) < 5)
                        {
                            Console.WriteLine("던전을 클리어하지 못했습니다.\n");
                            CurrentHp = CurrentHp / 2;
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();

                            
                            

                           
                            if (CurrentHp <= 0)
                            {
                                textrpg.Die(); 
                            }
                            else
                            {
                                EnterDungeonEntrance(); 
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("던전을 클리어했습니다.\n");
                            CurrentHp -= rand.Next(20 + (11 - Amr), 35 + (11 - Amr));
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Gold += 1700;
                            Console.WriteLine($"현재골드: {Gold}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();

                            
                            

                            
                            if (CurrentHp <= 0)
                            {
                                textrpg.Die(); 
                            }
                            else
                            {
                                EnterDungeonEntrance(); 
                            }
                            
                        }
                    }
                }
                else
                {
                    Monster randomMonster = Monster.CreateRandomMonster();
                    Console.WriteLine($"{randomMonster.NickName} {randomMonster.Name}을 만났습니다.\n");
                    if (Amr - 17 > 0)
                    {
                        
                        CurrentHp -= rand.Next(20 + (17 - Amr), 35 + (17 - Amr));

                        
                        if (CurrentHp <= 0)
                        {
                            textrpg.Die(); 
                        }
                        else
                        {
                            Console.WriteLine("던전을 클리어했습니다.\n");
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Gold += 2500;
                            Console.WriteLine($"현재골드: {Gold}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();
                            EnterDungeonEntrance(); 
                        }
                    }
                    else
                    {
                       
                        if (rand.Next(1, 11) < 5)
                        {
                            Console.WriteLine("던전을 클리어하지 못했습니다.\n");
                            CurrentHp = CurrentHp / 2;
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();

                            
                            

                            
                            if (CurrentHp <= 0)
                            {
                                textrpg.Die(); 
                            }
                            else
                            {
                                EnterDungeonEntrance();
                            }
                        }
                        else
                        {
                            Console.WriteLine("던전을 클리어했습니다.\n");
                            CurrentHp -= rand.Next(20 + (17 - Amr), 35 + (17 - Amr));
                            Console.WriteLine($"현재체력: {CurrentHp}\n");
                            Gold += 2500;
                            Console.WriteLine($"현재골드: {Gold}\n");
                            Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                            Console.ReadLine();
                            

                            
                            

                           
                            if (CurrentHp <= 0)
                            {
                                textrpg.Die(); 
                            }
                            else
                            {
                                EnterDungeonEntrance(); 
                            }
                            
                        }
                    }
                }

            }
            public void Die()
            {
                Console.Clear();
                Console.WriteLine("플레이어가 사망하셨습니다.\n");
                Console.WriteLine("초기 화면으로 이동합니다.\n계속 하시려면 엔터키를 눌러주세요.\n");

                Console.ReadLine();
                StartGame();
            }
            public void GetRest()
            {
                int choice;
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다.(보유 골드: {Gold} G)\n");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요\n");
                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 1))
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                }
                if(choice == 0)
                {
                    textrpg.Play();

                }
                else
                {
                    Console.WriteLine("휴식을 완료하였습니다.\n");
                    Gold -= 500;
                    CurrentHp = 100;
                    Console.WriteLine("계속 하시려면 엔터키를 눌러주세요.\n");
                    Console.ReadLine();
                    
                    textrpg.Play();
                }
            }   

                        
        }
    }    
}

    