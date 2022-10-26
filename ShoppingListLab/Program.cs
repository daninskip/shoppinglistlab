using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Reflection.Metadata;

string itemOrdered = "";

Dictionary<string, decimal> menuItems = new Dictionary<string, decimal>()
{
    { "apple", 0.99m},
    { "banana", 0.59m},
    { "cauliflower", 1.59m},
    { "dragonfruit", 2.19m},
    { "elderberry", 1.79m},
    { "figs", 2.09m},
    { "grapefruit", 1.99m},
    { "honeydew", 3.49m},

};

List<string> userOrder = new List<string>();

Console.WriteLine("Welcome to the market.");
ShowMenu();

#region methods;

void ShowMenu()
{
    Console.WriteLine("Here is our current menu list");
    Console.WriteLine("{0,-15} {1,5}", "Item", "Price");
    Console.WriteLine("{0,-15} {1,5}", "===============", "=====");
    foreach (var item in menuItems)
    {
        Console.WriteLine("{0,-15} {1,5}", item.Key, "$" + item.Value);
    }
    OrderItem();
}

void OrderItem()
{
    string itemOrderedClean;
    bool validItem = false;
    Console.WriteLine("Which item would you like to add to your order?");
    itemOrdered = Console.ReadLine();
    itemOrderedClean = itemOrdered.Trim().ToLower();
    validItem = VerifyItem(itemOrderedClean);
    userOrder.Add(itemOrderedClean);
    Console.WriteLine($"{itemOrdered} has been added to your order");
    DetermineWheterToContinue();
}

bool VerifyItem(string itemOrderedClean)
{
    bool isValid = false;
    while (isValid == false)
    {
        if (menuItems.ContainsKey(itemOrderedClean))
        {
            isValid = true;
            //continue;
        }
        else
        {
            isValid = false;
            Console.WriteLine("That is not a valid item, please try again.");
            ShowMenu();
            OrderItem();
            //break;
        }

    }
    return isValid;

}

void DetermineWheterToContinue()
{
    Console.WriteLine("Would you like to add another item?");
    Console.WriteLine("Enter YES to add more items, enter MENU to display the menu again. Enter anything else if ordering is complete.");
    string input = Console.ReadLine().Trim().ToLower();
    switch (input)
    {
        case "yes":
            OrderItem();
            break;
        case "menu":
            ShowMenu();
            break;
        default:
            ShowOrder();
            break;
    }
}

void ShowOrder()
{
    decimal sumPrice = 0;
    Console.WriteLine("Items currently on your order are:");

    var itemsOrdered = menuItems.Where(p => userOrder.Contains(p.Key))
    .ToDictionary(p => p.Key, p => p.Value)
    .OrderByDescending (p => p.Value);

    foreach( var item in itemsOrdered)
    {
        Console.WriteLine("{0,-15} {1,5}", item.Key, "$" + item.Value);
        sumPrice += item.Value;
    }

    Console.WriteLine("{0,-15} {1,5}", "===============", "=====");
    Console.WriteLine("{0,-15} {1,5}", "Total cost:", "$" + sumPrice);

    var mostExpensive = itemsOrdered.MaxBy(kvp => kvp.Value);
    var leastExpensive = itemsOrdered.MinBy(kvp => kvp.Value);

    Console.WriteLine($"The most expensive item on your order is {mostExpensive.Key} with a cost of {mostExpensive.Value}");
    Console.WriteLine($"The least expensive item on your order is {leastExpensive.Key} with a cost of {leastExpensive.Value}");
    Environment.Exit(0);


}

#endregion





