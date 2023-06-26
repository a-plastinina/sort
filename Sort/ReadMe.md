# Адаптер

[Matrix](App/Matrix)
Клиент [Program.cs] вызывает программу [MatrixApplication.cs] через интерфейс [IMatrixAggregate.cs]

[IMatrixAggregate.cs] имеет один метод Execute() - запускает сложение матриц

[MatrixApplication.cs] принимает на вход два интерфейса [IReader.cs] и [IWriter.cs] для чтения исходных матриц 
и записи результата сложения

[MatrixRandomAdaptor.cs] адаптер для [MatrixApplication.cs], реализует генерацию исходных матриц 
и сохранении их в файл перед вызовом сложения


***

# Абстрактная расширяемая фабрика

[Ioc.cs]
IoC расширяемая фабрика

[AbstactSortFactory.cs]
AbstactSortFactory абстрактная фабрика для сортировки
AbstactSortFactory.CreateCommand - фабричный метод получения команды для сортировки 

SortInserted конкретная фабрика для сортировки вставкой
SortMerged конкретная фабрика для сортировки слиянием
SortSelected конкретная фабрика для сортировки выбором

[Command](ICommand)
Команды для сортировки массива, реализуют ICommand
[SortInsertedCommand.cs]
[SortMergedCommand.cs]
[SortSelectedCommand.cs]

[Application.cs]
Клиент
Получает на вход тип сортировки, файл с исходными данными, записывает результат сортировки в файл

Диаграмма
https://github.com/a-plastinina/sort/blob/version1/Sort/%D0%94%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0.pdf
