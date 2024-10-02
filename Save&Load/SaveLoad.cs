using System;
using System.IO;
using System.Text.Json;
using TeamTextRPG;

public class SaveLoadManager
{
    // 프로젝트 폴더 안에 세이브 파일을 저장할 경로 설정
    private static string saveFolder = AppDomain.CurrentDomain.BaseDirectory; // 프로젝트 폴더 경로
    private static string saveFileName = "savegame.json";
    private static string saveFilePath = Path.Combine(saveFolder, saveFileName);

    // 게임 데이터를 저장하는 메서드
    public static void SaveGame(Character player)
    {
        try
        {
            // 캐릭터 객체를 JSON 문자열로 직렬화
            string json = JsonSerializer.Serialize(player, new JsonSerializerOptions { WriteIndented = true });
            // 파일에 JSON 문자열 저장
            File.WriteAllText(saveFilePath, json);
            Console.WriteLine("게임이 저장되었습니다.");
            Thread.Sleep(300);
        }
        catch (Exception e)
        {
            Console.WriteLine("저장 중 오류 발생: " + e.Message);
        }
    }

    // 게임 데이터를 불러오는 메서드
    public static Character LoadGame()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                // 파일에서 JSON 문자열 읽기
                string json = File.ReadAllText(saveFilePath);
                // JSON 문자열을 캐릭터 객체로 역직렬화
                Character player = JsonSerializer.Deserialize<Character>(json);
                ResetItemReferences(player);

                Console.WriteLine("게임이 불러와졌습니다.");
                return player;
            }
            else
            {
                Console.WriteLine("저장된 게임이 없습니다.");
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("불러오기 중 오류 발생: " + e.Message);
            Thread.Sleep(10000);
            return null;
        }
    }

    // 파일 삭제 메서드
    public static void DeleteSaveFile()
    {
        try
        {
            // 파일이 존재하는지 확인
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
                Console.WriteLine("세이브 파일이 삭제되었습니다.");
                Thread.Sleep(3000);
                return;
            }
            else
            {
                Console.WriteLine("삭제할 세이브 파일이 존재하지 않습니다.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("파일 삭제 중 오류 발생: " + e.Message);
        }


    }

    private static void ResetItemReferences(Character player)
    {
        // 인벤토리 아이템 재설정
        for (int i = 0; i < player.Inventory.Count; i++)
        {
            player.Inventory[i] = FindItemInDatabase(player.Inventory[i]);
        }

        // 장착 아이템 재설정
        player.EquipWeapon = FindItemInDatabase(player.EquipWeapon);
        player.EquipArmor = FindItemInDatabase(player.EquipArmor);

        // 드롭 아이템 재설정
        for (int i = 0; i < player.DropInventory.Count; i++)
        {
            player.DropInventory[i] = FindDropInDatabase(player.DropInventory[i]);
        }
    }

    // 아이템 데이터베이스에서 아이템 찾기
    private static Item FindItemInDatabase(Item item)
    {
        if (item == null) return null;
        return Array.Find(Program.itemDb, i => i.Name == item.Name && i.Type == item.Type);
    }

    // 드롭 아이템 데이터베이스에서 아이템 찾기
    private static Drop FindDropInDatabase(Drop drop)
    {
        if (drop == null) return null;
        return Array.Find(Program.dropDB, d => d.Name == drop.Name && d.Type == drop.Type);
    }
}
