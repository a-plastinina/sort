# sort
[Ioc.cs] IoC расширяемая фабрика

[AbstactSortFactory.cs]
AbstactSortFactory абстрактная фабрика для сортировки 
AbstactSortFactory.CreateCommand - фабричный метод получения команды для сортировки

SortInserted конкретная фабрика для сортировки вставкой 
SortMerged конкретная фабрика для сортировки слиянием 
SortSelected конкретная фабрика для сортировки выбором

Папка Command 
[SortInsertedCommand.cs] 
[SortMergedCommand.cs] 
[SortSelectedCommand.cs]
Команды для сортировки массива, реализуют ICommand 

[Application.cs] 
Клиент 
Получает на вход тип сортировки, файл с исходными данными, записывает результат сортировки в файл
