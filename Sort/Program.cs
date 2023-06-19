﻿using Microsoft.Extensions.Configuration;
using Sort.App;

const int LENGTH = 50;
const int MAX_VALUE = 200;

IConfiguration config = new ConfigurationBuilder() 
    .AddJsonFile("appsettings.json")
    .Build();

var sourceArrayPath = config["sourceArray"] ?? "";
var resultArrayPath = config["resultArray"] ?? "";
var resFolder = config["resFolder"] ?? ".\\";

using (var file = new FileHelper(sourceArrayPath))
{
    if (!File.Exists(sourceArrayPath))
    {
        file.WriteArray("", ArrayHelper.CreateRandom(LENGTH, MAX_VALUE));
    }
}

IoC.Resolve<ICommand>("IoC.Register", "Sort.Inserted"
    , (object[] args) => new SortInsertedCommand((int[])args[0]))
    .Execute();
IoC.Resolve<ICommand>("IoC.Register", "Sort.Selected"
    , (object[] args) => new SortSelectedCommand((int[])args[0]))
    .Execute();
IoC.Resolve<ICommand>("IoC.Register", "Sort.Merged"
    , (object[] args) => new SortMergedCommand((int[])args[0]))
    .Execute();

/// вариант 1
IoC.Resolve<ICommand>("IoC.Register", "Factory.SortInserted"
        , (object[] args) => AbstactSortFactory.Create("Sort.Inserted", args[0].ToString(), args[1].ToString()))
    .Execute();
IoC.Resolve<ICommand>("IoC.Register", "Factory.SortMerged"
        , (object[] args) => AbstactSortFactory.Create("Sort.Merged", args[0].ToString(), args[1].ToString()))
    .Execute();
IoC.Resolve<ICommand>("IoC.Register", "Factory.SortSelected"
        , (object[] args) => AbstactSortFactory.Create("Sort.Selected", args[0].ToString(), args[1].ToString()))
    .Execute();

IoC.Resolve<AbstactSortFactory>("Factory.SortInserted", sourceArrayPath, resFolder + "result1.txt").Execute();
IoC.Resolve<AbstactSortFactory>("Factory.SortMerged", sourceArrayPath, resFolder + "result2.txt").Execute();
IoC.Resolve<AbstactSortFactory>("Factory.SortSelected", sourceArrayPath, resFolder + "result3.txt").Execute();

/// вариант 2
IoC.Resolve<ICommand>("IoC.Register", "Factory.Sort"
    , (object[] args) => new SortFactory(args[0].ToString(), args[1].ToString(), args[2].ToString()))
    .Execute();

IoC.Resolve<SortFactory>("Factory.Sort", "Sort.Selected", sourceArrayPath, resFolder+"result1.txt").Execute();
IoC.Resolve<SortFactory>("Factory.Sort", "Sort.Merged", sourceArrayPath, resFolder+"result2.txt").Execute();
IoC.Resolve<SortFactory>("Factory.Sort", "Sort.Inserted", sourceArrayPath, resFolder+"result3.txt").Execute();



