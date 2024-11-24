using System;
using System.Collections.Generic;
class Program
{
    static Dictionary<string, int> auth = new Dictionary<string, int>()
    {
        {"viktor", 1996},
        {"qwerty", 123},
        {"maxim", 9999},
        {"sasha", 1111},
        {"carrot", 32322},
        {"Administrator", 12345}
    };
    public static bool Enter(string login, int password)
    {
        if (auth.ContainsKey(login))
        {
            return auth[login] == password;
        }
        Console.WriteLine("Логін невірний");
        return false;
    }
    public static void AdminMenu()
    {
        while (true)
        {
            Console.WriteLine("Меню адміністратора:");
            Console.WriteLine("1. Вивести список усіх логінів і паролів.");
            Console.WriteLine("2. Змінити пароль користувача.");
            Console.WriteLine("3. Вийти");

            string choise = Console.ReadLine();

            if( choise == "1")
            {
                Console.WriteLine("Список користувачів:");
                foreach (var user in auth)
                {
                    Console.WriteLine($"Логін: {user.Key}, Пароль: {user.Value}");
                }
            }
            else if( choise == "2")
            {
                Console.WriteLine("Введіть логін користувача для звіми пароля:");
                string userLogin = Console.ReadLine();
                if (auth.ContainsKey(userLogin))
                {
                    Console.WriteLine("Введіть новий пароль:");
                    int newPassword;
                    while (!int.TryParse(Console.ReadLine(), out newPassword))
                    {
                        Console.WriteLine("Пароль має бути числом. Спробуйте ще раз:");
                    }
                    auth[userLogin] = newPassword;
                    Console.WriteLine($"Пароль для користувача {userLogin} успішно змінено.");
                }
                else
                {
                    Console.WriteLine("Користувача з таким логіном не знайдено.");
                }
            }
            else if(choise == "3")
            {
                Console.WriteLine("Вихід з меню адміністратора.");
                break;
            }
            else
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }
        }
    }

    public static void ChangePassword(string login)
    {
        Console.WriteLine("Введіть новий пароль:");
        int newPassword;
        while(!int.TryParse(Console.ReadLine(),out newPassword))
        {
            Console.WriteLine("Пароль має бути числом. Спробуйте ще раз:");
        }
        auth[login] = newPassword;
        Console.WriteLine("Пароль успішно змінено");
    }

    public static void Main(string[] args)
    {
        bool status = true;
        int attempts = 0;
        while (status)
        {
            attempts++;
            if(attempts > 3)
            {
                Console.WriteLine("Ви ввели быльше 3-х разів невірно.");
                break;
            }
            Console.WriteLine("Авторизація. Введіть логін:");
            string userLogin = Console.ReadLine();
            Console.WriteLine("Введіть пароль:");
            int userPassword;
            if(!int.TryParse(Console.ReadLine(), out userPassword))
            {
                Console.WriteLine("Пароль не має бути числом.");
                continue;
            }
            //int userPassword = int.Parse(Console.ReadLine());
            bool result = Enter(userLogin, userPassword);

            if(result)
            {
                Console.WriteLine("Все співпадає");
                status = false;
            }
            if(userLogin == "Administrator")
            {
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Бажаєте змінити свій пароль? (так/ні)");
                if(Console.ReadLine().ToLower()  == "так")
                {
                    ChangePassword(userLogin);
                }
            }
        }
        Console.ReadKey();
    }
}