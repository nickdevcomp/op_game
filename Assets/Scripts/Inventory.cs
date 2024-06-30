using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour // review(30.06.2024): Зачем наследование от монобеха?
{
    // review(30.06.2024): Мне кажется, что стоило инвентарную систему сделать чуть по-другому, т.к. у вас все предметы подразумеваются в единственном экземпляре, то лучше было бы использовать enum
    public static int Ticket;
    public static int Dkr;
    public static int Ship;
    public static int Morsynka;
    public static int Feather;
    public static int Key;

    private void Start()
    {
        Ticket = 0;
        Dkr = 0;
        Ship = 0;
        Morsynka = 0;
        Feather = 0;
        Key = 0;
    }
}

// review(30.06.2024): Я говорю про что-то такое
public static class Inventory_Refactored
{
    private static readonly HashSet<Item> Items = new();

    public static bool Contains(Item item) => Items.Contains(item);

    public static void Add(Item item) => Items.Add(item);

    public static void Remove(Item item) => Items.Remove(item);

    public static IEnumerable<Item> GetAll() => Items;
}

public enum Item
{
    Ticket,
    Dkr,
    Ship,
    Morsynka,
    Feather,
    Key,
}
