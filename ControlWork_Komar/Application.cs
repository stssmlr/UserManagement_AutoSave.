using System.Collections.ObjectModel;

namespace ControlWork_Komar
{
    public class Application
    {
        private ObservableCollection<User> Users { get; set; }

        public Application()
        {
            Users = new ObservableCollection<User>();

        }

        public void Run()
        {
            while (true)
            {
                LoadUsersFromFile();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n1. Add User");
                Console.WriteLine("2. Edit User");
                Console.WriteLine("3. Delete User");
                Console.WriteLine("4. Show Users");
                Console.WriteLine("5. Search by Name/Email");
                Console.WriteLine("6. Save");
                Console.WriteLine("7. Exit");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine()!;
                Console.ForegroundColor = ConsoleColor.Gray;
                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        EditUser();
                        break;
                    case "3":
                        DeleteUser();
                        break;
                    case "4":
                        ShowUsers();
                        break;
                    case "5":
                        SearchByNameOrEmail();
                        break;
                    case "6":
                        SaveUsersToFile();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void SaveUsersToFile()
        {
            MyFile.SaveUsersToFile(Users);
        }
        private void LoadUsersFromFile()
        {
            Users = MyFile.LoadUsersFromFile("UserDetails.json");
        }

        private void AddUser()
        {
            User newUser = new User();
            newUser.EditUser();

            if (!IsDuplicate(newUser))
            {
                Users.Add(newUser);
                SaveUsersToFile();
                Console.WriteLine("User added successfully.");
            }
            else
            {
                Console.WriteLine("User with the same phone number or email already exists. Please enter unique information.");
            }
        }

        private void EditUser()
        {
            Console.Write("Enter the index of the user to edit: ");
            bool flag = false;

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < Users.Count)
            {
                // Отримати дані з файлу для редагування
                ObservableCollection<User> usersFromFile = MyFile.LoadUsersFromFile("UserDetails.json");
                User selectedUserFromFile = usersFromFile[index];

                // Дозволити користувачеві внести нові дані
                Console.WriteLine("Enter new details (press Enter to keep the existing value):");

                MyFile.RemoveUserByIndexFromFile(index);

                Console.Write($"1. Surname: {selectedUserFromFile.Surname} -> ");
                string newSurname = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(newSurname))
                {
                    selectedUserFromFile.Surname = newSurname;
                }

                Console.Write($"2. Name: {selectedUserFromFile.Name} ->");
                string newName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(newName))
                {
                    selectedUserFromFile.Name = newName;
                }

                while (!flag)
                {
                    Console.Write($"3. Phone Number: {selectedUserFromFile.PhoneNumber} -> ");
                    string newPhoneNumber = Console.ReadLine()!;
                    flag = true;
                    if (!string.IsNullOrEmpty(newPhoneNumber))
                    {
                        // Перевірити унікальність нового номеру телефону
                        if (!usersFromFile.Any(user => user != selectedUserFromFile && user.PhoneNumber == newPhoneNumber))
                        {
                            selectedUserFromFile.PhoneNumber = newPhoneNumber;
                            
                        }
                        else
                        {
                            Console.WriteLine("This phone number belongs to another user. No changes made.");
                            flag = false;
                        }
                    }

                }
                flag = false;
                while (!flag)
                {
                    Console.Write($"4. Email: {selectedUserFromFile.Email} -> ");
                    string newEmail = Console.ReadLine()!;
                    flag = true;
                    if (!string.IsNullOrEmpty(newEmail))
                    {
                        // Перевірити унікальність нової електронної адреси
                        if (!usersFromFile.Any(user => user != selectedUserFromFile && user.Email == newEmail))
                        {
                            selectedUserFromFile.Email = newEmail;
                        }
                        else
                        {
                            Console.WriteLine("This email belongs to another user. No changes made.");
                            flag = false;

                        }
                    }
                }
                // Зберегти оновлені дані у файл
                usersFromFile[index] = selectedUserFromFile;
                MyFile.SaveUsersToFile(usersFromFile);
                Console.WriteLine("User details updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        private bool IsDuplicate(User user)
        {
            return Users.Any(u => u != user && (u.PhoneNumber == user.PhoneNumber || u.Email == user.Email));
        }

        private void DeleteUser()
        {
            Console.Write("Enter the index of the user to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < Users.Count)
            {
                MyFile.RemoveUserByIndexFromFile(index);
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        private void ShowUsers()
        {
            Console.WriteLine("Users:");

            for (int i = 0; i < Users.Count; i++)
            {
                Console.WriteLine($"{i}. {Users[i]}");
            }
        }

        private void SearchByNameOrEmail()
        {
            Console.Write("Enter name or email to search: ");
            string userInput = Console.ReadLine()!;

            if (!string.IsNullOrEmpty(userInput))
            {
                User foundUser = Users.FirstOrDefault(user =>
                    user.Name.Equals(userInput, StringComparison.OrdinalIgnoreCase) ||
                    user.Email.Equals(userInput, StringComparison.OrdinalIgnoreCase))!;

                if (foundUser != null)
                {
                    Console.WriteLine($"Name: {foundUser.Name}\nSurname: {foundUser.Surname}\nPhone: {foundUser.PhoneNumber}\nEmail: {foundUser.Email}");
                }
                else
                {
                    Console.WriteLine($"User with name or email {userInput} not found.");
                }
            }
        }
    }
}
