using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>()
    {
        new Players(1, 1500,"Nguyễn Văn A", new Region(0, "VN")),
        new Players(2, 300, "Trần Thị B", new Region(1, "VN1")),
        new Players(3, 800,  "Lê Văn C", new Region(2, "VN2")),
        new Players(4, 1200, "Phạm Thị D", new Region(3, "JS")),
        new Players(5, 600, "Nguyễn Văn E",  new Region(4, "VS"))
    };

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public string calculate_rank(int score) =>
        score >= 1000 ? "Kim cương" :
        score >= 500 ? "Vàng" :
        score >= 100 ? "Bạc" : "Hạng đồng";

    public void YC1(int ID, int score, string userName, Region IDregion) =>
        listPlayer.Add(new Players(ID, score, userName, IDregion));
    public void YC2()
    {
        Debug.Log("Y02-----Danh sách người chơi----- ");
        listPlayer.ForEach(player =>
        {
            Debug.Log($"ID: {player.ID}, Name: {player.userName}, Score: {player.score}, Region: {player.IDregion.Name}, Rank: {calculate_rank(player.score)}");
        });
    }
    public void YC3()
    {
        Debug.Log("Y03----Danh sách người chơi có điểm số thấp hơn điểm số hiện tại-----");
        Players currentPlayer = listPlayer.Last();

        var lowerScorePlayers = listPlayer
            .Where(p => p.score < currentPlayer.score)
            .ToList();

        Debug.Log($"Người chơi hiện tại: {currentPlayer.userName} - Score: {currentPlayer.score}");
        Debug.Log("Danh sách người chơi có score bé hơn:");

        if (lowerScorePlayers.Count == 0)
            Debug.Log("Không có người chơi nào có điểm nhỏ hơn người chơi hiện tại");
        else
            lowerScorePlayers.ForEach(player =>
                Debug.Log($"ID: {player.ID}, Name: {player.userName}, Score: {player.score}, Region: {player.IDregion.Name}, Rank: {calculate_rank(player.score)}")
            );
    }
    public void YC4()
    {
        Debug.Log("Y04----Tìm player theo id của người chơi hiện tại, in ra thông tin-----");
        int currentPlayerID = listPlayer.Last().ID;

        var player = listPlayer
            .FirstOrDefault(p => p.ID == currentPlayerID);

        if (player != null)
        {
            Debug.Log("Thông tin người chơi sau hiện tại:");
            Debug.Log($"ID: {player.ID}, Name: {player.userName}, Score: {player.score}, Region: {player.IDregion.Name}, Rank: {calculate_rank(player.score)}");
        }
        else
        {
            Debug.Log($"Không tìm thấy người chơi với ID = {currentPlayerID}");
        }
    }
    public void YC5()
    {
        Debug.Log("Y05----Xuất thông tin Players trong listPlayer theo thứ tự giảm dần-----");
        var sortedPlayers = listPlayer
         .OrderByDescending(p => p.score)
         .ToList();

        sortedPlayers.ForEach(player =>
            Debug.Log($"ID: {player.ID}, Name: {player.userName}, Score: {player.score}, Region: {player.IDregion.Name}, Rank: {calculate_rank(player.score)}")
        );
    }
    public void YC6()
    {
        Debug.Log("Y06----Xuất 5 players có score thấp nhất theo thứ tự tăng dần-----");
        var lowestPlayers = listPlayer
         .OrderBy(p => p.score)
         .Take(5)
         .ToList();

        lowestPlayers.ForEach(player =>
            Debug.Log($"ID: {player.ID}, Name: {player.userName}, Score: {player.score}, Region: {player.IDregion.Name}, Rank: {calculate_rank(player.score)}")
        );
    }
    public void YC7()
    {
        Debug.Log("YC7-----Tạo Thread tên BXH để tính score trung bình theo từng Region và lưu vào file-----");
        Thread thread = new Thread(() =>
        {
            CalculateAndSaveAverageScoreByRegion();
        });

        thread.Name = "BXH";
        thread.Start();
    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        var regionAverageScores = listPlayer
        .GroupBy(p => p.IDregion.Name)
        .Select(g => new
        {
            RegionName = g.Key,
            AverageScore = g.Average(p => p.score)
        })
        .OrderByDescending(r => r.AverageScore)
        .ToList();

        string filePath = Path.Combine(Application.dataPath, "bxhRegion.txt");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var region in regionAverageScores)
            {
                writer.WriteLine($"Region: {region.RegionName}, Average Score: {region.AverageScore:F2}");
            }
        }

        Debug.Log("BXH theo Region đã được lưu vào bxhRegion.txt");
    }

}

[SerializeField]
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    public int ID;
    public int score;
    public string userName;
    public Region IDregion;

    public Players(int ID, int score, string userName, Region IDregion)
    {
        this.ID = ID;
        this.score = score;
        this.userName = userName;

        this.IDregion = IDregion;
    }
}