using System.Text.RegularExpressions;

public class User
{

    public string Surname { get; set; }
    public string Name { get; set; }
    private string _phoneNumber;
    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (IsValidPhoneNumber(value) )
            {
                _phoneNumber = value;
            }
            else
            {
                Console.WriteLine("Invalid phone number or already in use. Please enter a valid and unique phone number.");
            }
        }
    }

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            if (IsValidEmail(value) )
            {
                _email = value;
            }
            else
            {
                Console.WriteLine("Invalid email or already in use. Please enter a valid and unique email.");
            }
        }
    }

    public User()
    {
        Surname = "";
        Name = "";
        _phoneNumber = ""; 
        _email = "";
    }

    public User(string surname, string name, string phoneNumber, string email) : this()
    {
        Surname = surname;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public override string ToString()
    {
        return $"{Surname} {Name}  {PhoneNumber}  {Email}";
    }

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
    }

    public void EditUser()
    {
        Console.Write("Enter Surname: ");
        Surname = Console.ReadLine()!;

        Console.Write("Enter Name: ");
        Name = Console.ReadLine()!;

        Console.Write("Enter Phone Number: ");
        PhoneNumber = Console.ReadLine()!;

        Console.Write("Enter Email: ");
        Email = Console.ReadLine()!;
    }
}
