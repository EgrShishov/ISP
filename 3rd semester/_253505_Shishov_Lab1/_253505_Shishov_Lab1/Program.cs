﻿using System;
using _253505_Shishov_Lab1.Collections;
using _253505_Shishov_Lab1.Entities;

 class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("hello, walter");

        Shop shop = new Shop();
        Journal journal = new Journal("My first journal");

        shop.NotifyGoodsChanged += journal.RegisterEvent;
        shop.NotifyClientChanged += journal.RegisterEvent;
        shop.NotifyOrderCreated += (string order_desc) => Console.WriteLine($" shop : new order created : {order_desc}");

        shop.AddGoods(new Goods(123, "notebook"));
        shop.AddGoods(new Goods(456, "phone"));
        shop.AddGoods(new Goods(12, "soap"));
        shop.AddGoods(new Goods(43, "mouse"));
        var me = new Client("shishov");
        shop.RegisterOrder(me, new MyCustomCollection<Goods>() { shop.GetGoods[0] , shop.GetGoods[1], shop.GetGoods[2] }, 4);

        shop.AddGoods(new Goods(54, "charger"));
        shop.AddGoods(new Goods(523, "headphones"));
        shop.AddGoods(new Goods(443, "fridge"));

        shop.GetGoods.Next();
        shop.GetGoods.Next();
        shop.GetGoods.Next();

        var client = new Client("bekarev");
        shop.RegisterOrder(client, new MyCustomCollection<Goods>{shop.GetGoods.Current()}, 3);

        Console.WriteLine(shop.GetTotalAmountByGood(shop.GetGoods[0]));

        Console.WriteLine("-------------\nJournal");
        journal.ShowEvents();

        try
        {
            MyCustomCollection<string> test = new MyCustomCollection<string>();
            test.Add("Stas");
            test.Add("Bekarev");
            test.Remove("Sergeevich");
        }
        catch (MyException){
            Console.WriteLine("MyException was thrown and caught");
        }

        try
        {
            MyCustomCollection<int> test = new MyCustomCollection<int>();
            for(int i = 0; i<100; i++)
            {
                test.Add(i);
            }

            Console.WriteLine(test[101]);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("IndexOutOfRangeException was thrown and caught");
        }
    }

    private static void Shop_NotifyOrderCreated(string action)
    {
        throw new NotImplementedException();
    }
}