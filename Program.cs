using System;
using System.Collections.Generic;
using System.IO;

namespace Congratulation
{
    class Program
    {
        static Random rnd = new Random();
        static void Main()
        {
            Dictionary<string, Dictionary<string, int>> dictionary = new Dictionary<string, Dictionary<string, int>>();
            string[] words = null;
            string path;
            string[] lines; // Массив из всех строк, которые есть в файле. Прочитывается и построчно помещается в массив
            int i, j, m = 0;
            string run;
            string[] share;
            int[] vers;
            int count;
            int[] verN = null;

            string[] start;

            // Формирование словаря, где ключом является пара соседних слов,
            // значением - словарь из возможных следующих (3-х по счёту) слов
            do
            {
                dictionary.Clear();
                OutputMenu(menu, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                m = IntegerInputWithValidation("\nСделайте выбор праздника!\n", 0, menu.Length - 1);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (m != 0)
                {
                    Console.WriteLine();
                    path = m < 10? $"0{m}.txt": $"{m}.txt";
                    lines = File.ReadAllLines(path);
                    start = new string[lines.Length];
                    for (i = 0; i < lines.Length; i++)
                    {
                        words = lines[i].Split();
                        start[i] = words[0] + " " + words[1];
                        for (j = 0; j < words.Length - 1; j++)
                        {
                            if (j == words.Length - 2)
                            {
                                if (dictionary.ContainsKey(words[j] + " " + words[j + 1]))
                                {
                                    if (dictionary[words[j] + " " + words[j + 1]].ContainsKey("End"))
                                    {
                                        dictionary[words[j] + " " + words[j + 1]]["End"]++;
                                    }
                                    else
                                    {
                                        dictionary[words[j] + " " + words[j + 1]].Add("End", 1);
                                    }
                                }
                                else
                                {
                                    dictionary.Add(words[j] + " " + words[j + 1], new Dictionary<string, int>());
                                    dictionary[words[j] + " " + words[j + 1]].Add("End", 1);
                                }
                                continue;
                            }
                            else
                            {
                                if (dictionary.ContainsKey(words[j] + " " + words[j + 1]))
                                {
                                    if (dictionary[words[j] + " " + words[j + 1]].ContainsKey(words[j + 2]))
                                    {
                                        dictionary[words[j] + " " + words[j + 1]][words[j + 2]]++;
                                    }
                                    else
                                    {
                                        dictionary[words[j] + " " + words[j + 1]].Add(words[j + 2], 1);
                                    }
                                }
                                else
                                {
                                    dictionary.Add(words[j] + " " + words[j + 1], new Dictionary<string, int>());
                                    dictionary[words[j] + " " + words[j + 1]].Add(words[j + 2], 1);
                                }
                            }
                        }
                    }

                    run = start[rnd.Next(start.Length)];
                    int counter = 0;
                    string ex = "";
                    while (run != "End")
                    {
                        i = 0;
                        count = 0;
                        Console.Write(run + " ");
                        Dictionary<int, string> choise = new Dictionary<int, string>();
                        if (counter == 0)
                        {
                            vers = new int[dictionary[run].Count];
                            foreach (KeyValuePair<string, int> pair in dictionary[run])
                            {
                                count = count + pair.Value;
                                vers[i] = count;
                                choise.Add(vers[i], pair.Key);
                                i++;
                            }
                        }
                        else
                        {
                            vers = new int[dictionary[ex + " " + run].Count];
                            foreach (KeyValuePair<string, int> pair in dictionary[ex + " " + run])
                            {
                                count = count + pair.Value;
                                vers[i] = count;
                                choise.Add(vers[i], pair.Key);
                                i++;
                            }
                        }
                        verN = vers;
                        Array.Sort(verN);
                        int word = rnd.Next(count + 1);
                        for (j = 0; j < verN.Length; j++)
                        {
                            if (word <= verN[j])
                            {
                                if (counter == 0)
                                {
                                    share = run.Split();
                                    ex = share[1];
                                    run = choise[verN[j]];
                                    break;
                                }
                                else
                                {
                                    share = (ex + " " + run).Split();
                                    ex = share[1];
                                    run = choise[verN[j]];
                                    break;
                                }
                            }
                        }
                        counter++;
                    }
                }
                Console.WriteLine("\n");
            } while (m != 0);
        }

        static void OutputMenu(string[] m, int v) // Вывод любого меню, например, основного и вложенного
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int i, j, k = m.Length;
            int h = v * 5;
            for (i = 0; i < k; i++)
            {
                for (j = 0; j < h; j++)
                    Console.Write(" ");
                Console.WriteLine(m[i]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static string[] menu =
        {
            "0.  Завершение работы программы",
            "1.  День рождения",
            "2.  Новый год",
            "3.  День защитника Отечества",
            "4.  День Победы",
            "5.  Международный день пожилых людей",
            "6.  Рождество",
            "7.  День свадьбы",
            "8.  День знаний",
            "9.  Международный женский день",
            "10. День святого Валентина (для парней)",
            "11. День святого Валентина (для девушек)"
        };

        static public int IntegerInputWithValidation(string s, int left, int right) // Ввод целого числа с проверкой правильности ввода, в том числе принадлежности к указанному диапазону
        {
            bool ok;
            int a;
            do
            {
                Console.WriteLine(s);
                ok = int.TryParse(Console.ReadLine(), out a);
                if (ok)
                    if (a < left || a > right)
                        ok = false;
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nВведенные данные имеют неверный формат или не принадлежат диапазону [{left}; {right}]");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            } while (!ok);
            return a;
        }
    }
}