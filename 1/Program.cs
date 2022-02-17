using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
	enum Type 
	{
		О,
		К,
	}
	struct PriceList
	{
		public string name;
		public Type t;
		public double salary;
		public int quantity;
		public void Table()
		{
			Console.WriteLine($"|{name,-22}|{t,-13}|{salary,-21}|{quantity,-14}|");
			Console.WriteLine(new String('-', 75));
		}
	}
	struct Log
	{
		public DateTime time;
		public string operation;
		public string name;
		public void LogConclusion()
		{
			Console.WriteLine($"{time} - {operation} {name}");
		}
	}
	public static void Main(string[] args)
	{
		PriceList Папка;
		Папка.name = "Папка";
		Папка.t = Type.К;
		Папка.salary = 4.75;
		Папка.quantity = 400;
		PriceList Бумага;
		Бумага.name = "Бумага А4 (пачка)";
		Бумага.t = Type.К;
		Бумага.salary = 45.90;
		Бумага.quantity = 100;
		PriceList Калькулятор;
		Калькулятор.name = "Калькулятор";
		Калькулятор.t = Type.О;
		Калькулятор.salary = 411.00;
		Калькулятор.quantity = 10;

		var table = new List<PriceList>();
		table.Add(Папка);
		table.Add(Бумага);
		table.Add(Калькулятор);
		var LogOperation = new List<Log>(50);
		DateTime ftime = DateTime.Now;
		DateTime stime = DateTime.Now;
		TimeSpan time = stime - ftime;
		bool working = true;
		bool error = true;
		do
		{
			Console.WriteLine("Выберите действие:");
			Console.WriteLine("1 - Просмотр таблицы");
			Console.WriteLine("2 - Добавить запись");
			Console.WriteLine("3 - Удалить запись");
			Console.WriteLine("4 - Обновить запись");
			Console.WriteLine("5 - Поиск записей");
			Console.WriteLine("6 - Просмотреть лог");
			Console.WriteLine("7 - Выход");
			int a = int.Parse(Console.ReadLine());
			if (a == 1)
			{
				Console.WriteLine(new String('-', 75));
				Console.WriteLine($"{"|Прайс-лист",-74}|");
				Console.WriteLine(new String('-', 75));
				Console.WriteLine($"{"|Наименование товара",-23}|{"Тип товара",-13}|{"Цена за 1 шт (грн)",-21}|{"Количество",-14}|");
				Console.WriteLine(new String('-', 75));
				for (int i = 0; i < table.Count; i++)
				{
					table[i].Table();
				}
				Console.WriteLine($"{"|Перечисляемый тип: К – канцтовары, О - оргтехника",-74}|");
				Console.WriteLine(new String('-', 75));
			}
			if (a == 2)
			{
				Console.Write("Введите наименование товара: ");
				string name = Console.ReadLine();
				var t = Type.О;
				double salary = 0;
				int quantity = 0;
				do
				{
					Console.Write("Введите тип товара (К – канцтовары, О - оргтехника): ");
					string tip = Console.ReadLine();
					if (tip == "К")
					{
						t = Type.К;
						error = false;
					}
					else if (tip == "О")
					{
						t = Type.О;
						error = false;
					}
					else
					{
						Console.WriteLine("Такого типа нет. Введите К или О");
					}
				}
				while (error);
				error = true;
				do
				{
					Console.Write("Введите цену за 1 шт (грн): ");
					salary = double.Parse(Console.ReadLine());
					while (salary <= 0)
                    {
						Console.WriteLine("Цена не может быть меньше или равна нулю");
						Console.WriteLine("Введите цену за 1 шт (грн) еще раз: ");
						salary = double.Parse(Console.ReadLine());
						
					}
					error = false;
				}
				while (error);
				error = true;
				do
				{
					Console.Write("Введите количество: ");
					quantity = int.Parse(Console.ReadLine());
					while (quantity <= 0)
					{
						Console.WriteLine("Количество не может быть меньше нуля");
						Console.WriteLine("Введите количество еще раз: ");
						quantity = int.Parse(Console.ReadLine());

					}
					error = false;
				}
				while (error);
				error = true;

				PriceList newPriceList;
				newPriceList.name = name;
				newPriceList.t = t;
				newPriceList.salary = salary;
				newPriceList.quantity = quantity;
				table.Add(newPriceList);

				Log newLog;
				newLog.time = DateTime.Now;
				newLog.operation = "Добавлена запись";
				newLog.name = name;
				LogOperation.Add(newLog);

				ftime = newLog.time;
				TimeSpan secondtime = ftime - stime;
				if (time < secondtime)
				{
					time = secondtime;
				}
				stime = newLog.time;
				Console.WriteLine();

			}
			if (a == 3)
			{
				int num = 0;
				string name = string.Empty;
				do
				{
					Console.WriteLine("Выберите номер строки для удаления: ");
					num = int.Parse(Console.ReadLine());
					if ((num > 0) && (num <= table.Count))
					{
						name = table[num - 1].name;
						table.RemoveAt(num - 1);
						error = false;
					}
					else
					{
						Console.WriteLine($"Здесь всего {table.Count} пунктов");
						Console.WriteLine("Выберите снова номер строки для удаления:  ");
					}
				}
				while (error);
				error = true;

				Log newLog;
				newLog.time = DateTime.Now;
				newLog.operation = "Удалена запись";
				newLog.name = name;
				LogOperation.Add(newLog);

				ftime = newLog.time;
				TimeSpan secondtime = ftime - stime;
				if (time < secondtime)
				{
					time = secondtime;
				}
				stime = newLog.time;
				Console.WriteLine();
			}
			if (a == 4)
			{
				string oldname = string.Empty;
				string name = string.Empty;
				var t = Type.О;
				double salary = 0;
				int quantity = 0;
				int number = 0;
				do
				{
					Console.WriteLine("Выберите номер строки для обновления: ");
					number = int.Parse(Console.ReadLine());
					if (number > 0 && number <= table.Count)
					{
						oldname = table[number - 1].name;
						Console.Write("Введите наименование товара: ");
						name = Console.ReadLine();
						do
						{
							Console.Write("Введите тип товара (К – канцтовары, О - оргтехника): ");
							string pos = Console.ReadLine();
							if (pos == "К")
							{
								t = Type.К;
								error = false;
							}
							else if (pos == "О")
							{
								t = Type.О;
								error = false;
							}
							else
							{
								Console.WriteLine("Такого типа нет. Введите К или О");
								Console.WriteLine();
							}
						}
						while (error);
						error = true;
						do
						{
							Console.Write("Введите цену за 1 шт (грн): ");
							salary = double.Parse(Console.ReadLine());
							while (salary <= 0)
							{
								Console.WriteLine("Цена не может быть меньше или равна нулю");
								Console.WriteLine("Введите цену за 1 шт (грн) еще раз: ");
								salary = double.Parse(Console.ReadLine());

							}
							error = false;
						}
						while (error);
						error = true;
						do
						{
							Console.Write("Введите количество: ");
							quantity = int.Parse(Console.ReadLine());
							while (quantity <= 0)
							{
								Console.WriteLine("Количество не может быть меньше нуля");
								Console.WriteLine("Введите количество еще раз: ");
								quantity = int.Parse(Console.ReadLine());

							}
							error = false;
						}
						while (error);
					}
					else
					{
						Console.WriteLine($"Здесь всего {table.Count} пунктов");
						Console.WriteLine("Выберите снова номер строки для удаления:  ");
					}
				}
				while (error);
				error = true;

				PriceList edit;
				edit.name = name;
				edit.t = t;
				edit.salary = salary;
				edit.quantity = quantity;
				table.Insert(number - 1, edit);
				table.Remove(table[number]);

				Log newLog;
				newLog.time = DateTime.Now;
				newLog.operation = "Обновлена запись: ";
				newLog.name = "Товар " + oldname + " был изменен на " + name;
				LogOperation.Add(newLog);

				ftime = newLog.time;
				TimeSpan secondtime = ftime - stime;
				if (time < secondtime)
				{
					time = secondtime;
				}
				stime = newLog.time;
				Console.WriteLine();
			}
			if (a == 5)
			{
				var pos = Type.О;
				do
				{
					Console.WriteLine("Введите что вы хотите найти (К – канцтовары, О - оргтехника): ");
					string select = Console.ReadLine();
					Console.WriteLine();
					if (select == "К" || select == "О")
					{
						if (select == "К")
						{
							pos = Type.К;
						}
						if (select == "О")
						{
							pos = Type.О;
						}
						for (int i = 0; i < table.Count; i++)
						{
							if (table[i].t == pos)
							{
								table[i].Table();
							}
						}
						error = false;
					}
					else
					{
						Console.WriteLine("Введите К или О");
					}
				}
				while (error);
				error = true;
				Console.WriteLine();
			}
			if (a == 6)
			{

                if (LogOperation.Count == 0)
                {
					Console.WriteLine("Пока не было предпринято никаких действий");
                }
				for (int i = 0; i < LogOperation.Count; i++)
				{
					LogOperation[i].LogConclusion();
				}
				Console.WriteLine();
				Console.WriteLine(time + " - Самый долгий период бездействия пользователя");
				Console.WriteLine();
			}
			if (a == 7)
			{
				working = false;
			}
			if (a < 1 || a > 7)
			{
				Console.WriteLine("Выберите действие от 1 до 7");
				Console.WriteLine();
			}
		}
		while (working);
		Console.WriteLine();
	}
}