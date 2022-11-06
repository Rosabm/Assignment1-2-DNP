using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;
    
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return dataContainer!.posts;
        }
    }
    
    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.users;
        }
    }
    
    public ICollection<Comment> Comments
    {
        get
        {
            LoadData();
            return dataContainer!.comments;
        }
    }
    
    private void LoadData()
    {
        if (dataContainer != null) return;
    
        if (!File.Exists(filePath))
        {
            dataContainer = new ()
            {
                posts = new List<Post>(),
                users = new List<User>(),
                comments = new List<Comment>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
    
     public void SaveChanges()
        {
            string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, serialized);
            dataContainer = null;
        }
}