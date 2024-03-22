using System;
using System.Data;
using System.Data.SqlClient;

namespace VegetablesFruitsApp;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=YourServerName;Initial Catalog=VegetablesFruitsAppDB;Integrated Security=True"; SqlConnection connection = new SqlConnection(connectionString);


        try
        {
            connection.Open();
            Console.WriteLine("Успешное подключение к базе данных \"Овощи и фрукты\"");

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Отобразить всю информацию из таблицы");
                Console.WriteLine("2. Отобразить все названия овощей и фруктов");
                Console.WriteLine("3. Отобразить все цвета");
                Console.WriteLine("4. Показать максимальную калорийность");
                Console.WriteLine("5. Показать минимальную калорийность");
                Console.WriteLine("6. Показать среднюю калорийность");
                Console.WriteLine("0. Выйти из программы");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayAllInformation(connection);
                        break;
                    case 2:
                        DisplayAllNames(connection);
                        break;
                    case 3:
                        DisplayAllColors(connection);
                        break;
                    case 4:
                        DisplayMaxCalories(connection);
                        break;
                    case 5:
                        DisplayMinCalories(connection);
                        break;
                    case 6:
                        DisplayAvgCalories(connection);
                        break;
                    case 0:
                        connection.Close();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при подключении к базе данных: " + ex.Message);
        }
    }

    static void DisplayAllInformation(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand("SELECT * FROM VegetablesFruitsApp", connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Название: {reader["Name"]}, Тип: {reader["Type"]}, Цвет: {reader["Color"]}, Калорийность: {reader["Calories"]}");
        }

        reader.Close();
    }

    static void DisplayAllNames(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand("SELECT Name FROM VegetablesFruitsApp", connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Название: {reader["Name"]}");
        }

        reader.Close();
    }

    static void DisplayAllColors(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand("SELECT DISTINCT Color FROM VegetablesFruitsApp", connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Цвет: {reader["Color"]}");
        }

        reader.Close();
    }

    static void DisplayMaxCalories(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand("SELECT MAX(Calories) FROM VegetablesFruitsApp", connection);
        object result = command.ExecuteScalar();

        Console.WriteLine($"Максимальная калорийность: {result}");
    }

    static void DisplayMinCalories(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand("SELECT MIN(Calories) FROM VegetablesFruitsApp", connection);
        object result = command.ExecuteScalar();

        Console.WriteLine($"Минимальная калорийность: {result}");
    }

    static void DisplayAvgCalories(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand("SELECT AVG(Calories) FROM VegetablesFruitsApp", connection);
        object result = command.ExecuteScalar();

        Console.WriteLine($"Средняя калорийность: {result}");
    }
}
