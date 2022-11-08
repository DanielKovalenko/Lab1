using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab1_sharp
{
    public static class Extensions
    {
        public static T[] RemoveAt<T>(this T[] array, int index) //видалення елемента масиву
        {
            var new_array = new List<T>(array); //створення списку з елеметнами старого масиву
            new_array.RemoveAt(index);
            return new_array.ToArray();
        }
        public static T[] Append<T>(this T[] array, T item) //додавання елемента в масив
        {
            List<T> list = new List<T>(array);
            list.Add(item);
            return list.ToArray();
        }
    }
    class Mobile_operator
    {
        Dictionary<string, double> call = new Dictionary<string, double>(); //колекція для зберігання історії дзвінків
        enum Names
        {
            Новачок,
            Любитель,
            Професiонал,
            Фахiвець
        }
        enum Services
        {
            Bezlim,
            Home,
            Calling,
            TV
        }
        private string _Tariff_name;
        private double _Price_for_min;
        private double _Balance;
        private string _Phone_number;
        private string[] _Tariff_services;
        public string Tariff_name
        {
            set
            {
                _Tariff_name = value;
            }
            get
            {
                return _Tariff_name;
            }
        }
        public double Price_for_min
        {
            set
            {
                _Price_for_min = value;
            }
            get
            {
                return _Price_for_min;
            }
        }
        public double Balance
        {
            set
            {
                _Balance = value;
            }
            get
            {
                return _Balance;
            }
        }
        public string Phone_number
        {
            set
            {
                string pattern = @"\d{10}"; //тільки 10 цифр
                var match = Regex.Match(value, pattern); // зрівнюємо з вводом
                if (match.Success) // якщо введення відповідає
                {
                    _Phone_number = value;
                }
                else { Console.WriteLine("Введите только цифры"); }
            }
            get
            {
                return _Phone_number;
            }
        }
        public string[] Tariff_services
        {
            set
            {
                _Tariff_services = value;
            }
            get
            {
                return _Tariff_services;
            }
        }

        public Mobile_operator() // конструктор без параметров
        {
            Random rnd = new Random();
            Tariff_name = Enum.GetName(typeof(Names), rnd.Next(4));
            Price_for_min = 0;
            Balance = 0;
            Phone_number = "0000000000";
            Tariff_services = new string[] { Enum.GetName(typeof(Services), rnd.Next(4))};
            Console.WriteLine(string.Format("Ваш тариф: \nНазва: {0} \nЦiна за хвилину: {1} \nВаш баланс: {2} \nНомер телефону: {3} \n", Tariff_name, Price_for_min, Balance, Phone_number));
        }
        public Mobile_operator(double price, double balance, string number)
        {
            Random rnd = new Random();
            Tariff_name = Enum.GetName(typeof(Names), rnd.Next(4));
            Price_for_min = price;
            Balance = balance;
            Phone_number = number;
            Tariff_services = new string[] { Enum.GetName(typeof(Services), rnd.Next(4))};
            Console.WriteLine(string.Format("\nВаш тариф: \nНазва: {0} \nЦiна за хвилину: {1} \nВаш баланс: {2} \nНомер телефону: {3} \n", Tariff_name, Price_for_min, Balance, Phone_number));
        }

        public double Balance_inc(double summa)
        {
            Balance += summa;
            Console.WriteLine($"\nВаш поточний баланс {Balance}");
            return Balance;
        }

        public string New_tariff(string tariff)
        {

            Tariff_name = tariff;
            Console.WriteLine($"\nВи змiнили тариф на {Tariff_name}");
            return Tariff_name;
        }
        public double Call(double minutes, string number)
        {
            double price = minutes * Price_for_min;
            Console.WriteLine($"Ви здiйснили дзвiнок тривалiстю {minutes} по номеру {number} Загальна вартiсть дзвiнка = {price}");
            call.Add(number, price);
            Balance -= price;
            Console.WriteLine($"Ваш поточний баланс {Balance}");
            return price;
        }
        public void Call_History()
        {
            Console.WriteLine("\nIсторiя дзвiнкiв: ");
            foreach(var call in call)
            {
                Console.WriteLine($"\nНомер телефону : {call.Key} Вартiсть дзвiнка {call.Value}");
            }
        } // метод отримання історії дзвінків
        public void Delete_or_Add_Service(string choice)
        {
            Console.WriteLine("\nВашi послуги: ");
            foreach (string i in Tariff_services)
            {
                Console.WriteLine(i);
            }
            switch (choice.ToLower())
            {
                case "delete":
                    Console.WriteLine("\nВведiть послугу, яку Ви бажаєте видалити: ");
                    string service_for_delete = Console.ReadLine();
                    var zxc = Array.IndexOf(Tariff_services, service_for_delete);
                    string[] arr = Tariff_services.RemoveAt(zxc);
                    Tariff_services = arr;
                    break;
                case "add":
                    Console.WriteLine("\nСписок послуг для додавання:");
                    foreach (var p in Enum.GetNames(typeof(Services)))
                    {
                        Console.WriteLine(p);
                    }
                    service:
                    Console.WriteLine("\nВведiть послугу, яку потрiбно додати: ");
                    string service_for_add = Console.ReadLine();
                    if (Tariff_services.Contains(service_for_add))
                    {
                        Console.WriteLine("\nТака послуга вже мiститься, хочете пiдключити iншу: (Yes/No)");
                        string choice123 = Console.ReadLine().ToLower();
                        switch(choice123)
                        {
                            case "yes":
                                goto service;

                            case "no":
                                break;
                            default:
                                break;
                        }    
                    }
                    else
                    {
                        switch (service_for_add)
                        {
                            case "Calling":
                                Balance -= 150;
                                Console.WriteLine("\nЗ вашого балансу буде знято 150 грн");
                                break;
                            case "Home":
                                Balance -= 100;
                                Console.WriteLine("\nЗ вашого балансу буде знято 100 грн");
                                break;
                            case "TV":
                                Balance -= 200;
                                Console.WriteLine("\nЗ вашого балансу буде знято 200 грн");
                                break;
                            case "Bezlim":
                                Balance -= 500;
                                Console.WriteLine("\nЗ вашого балансу буде знято 500 грн");
                                break;
                            default:
                                break;
                        }
                        Tariff_services = Tariff_services.Append(service_for_add);
                        break;
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("\nВашi послуги: ");
            foreach (string i in Tariff_services)
            {
                Console.WriteLine(i);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Mobile_operator Operator = new Mobile_operator(2.0, 1000, "1234567890"); // Ініціалізуючий конструктор
            Mobile_operator Operator1 = new Mobile_operator(); // Конструктор без аргументів
            Operator.Balance_inc(150); // збільшення балансу на 150 грн
            Operator.New_tariff("New");
            Operator.Call(50, "3805465958");
            Operator.Call(123, "3805412358");
            Operator.Call_History();
            Operator.Delete_or_Add_Service("add");
            Operator.Delete_or_Add_Service("delete");


        }
    }
}