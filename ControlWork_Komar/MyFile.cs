using System.Collections.ObjectModel;
using System.Text.Json;

namespace ControlWork_Komar
{
    public static class MyFile
    {
        public static void SaveUsersToFile(ObservableCollection<User> users)
        {
            string filePath = "UserDetails.json";

            try
            {
                ObservableCollection<User> existingUsers = LoadUsersFromFile(filePath);

                // Додати нових користувачів до існуючих
                foreach (var user in users)
                {
                    if (!existingUsers.Any(existingUser => existingUser.PhoneNumber == user.PhoneNumber || existingUser.Email == user.Email))
                    {
                        existingUsers.Add(user);
                    }
                }

                // Зберегти оновлені дані у файл
                string json = JsonSerializer.Serialize(existingUsers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

        public static ObservableCollection<User> LoadUsersFromFile(string filePath)
        {
            ObservableCollection<User> existingUsers = new ObservableCollection<User>();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    existingUsers = JsonSerializer.Deserialize<ObservableCollection<User>>(json)!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }

            return existingUsers!;
        }
        public static void RemoveUserByIndexFromFile(int index)
        {
            string filePath = "UserDetails.json";

            try
            {
                ObservableCollection<User> existingUsers = LoadUsersFromFile(filePath);

                // Перевірити чи індекс знаходиться в межах списку
                if (index >= 0 && index < existingUsers.Count)
                {
                    existingUsers.RemoveAt(index);

                    // Зберегти оновлені дані у файл
                    string json = JsonSerializer.Serialize(existingUsers, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, json);

                    Console.WriteLine("User removed from file.");
                }
                else
                {
                    Console.WriteLine("Invalid index. User not removed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing user from file: {ex.Message}");
            }
        }
    }
}
