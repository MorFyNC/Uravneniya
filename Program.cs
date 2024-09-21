Console.Write("Введите точность приближенных вычислений: ");

PolovinDelenie(Function_Task6);

decimal PolovinDelenie(Uravnenie ur)
{
    decimal accuracy = Convert.ToDecimal(Console.ReadLine());
    var tup = Otdelenie(0m, ur);
    decimal a = tup.Item1;
    decimal b = tup.Item2;
    while (b - a > 2*accuracy)
    {
        decimal c = (a + b) / 2;
        if (ur.Invoke(a) * ur.Invoke(c) < 0) b = c;
        else a = c;
    }
    var result =  (a + b) / 2;
    string formattedResult = String.Format("{0:F" + (accuracy.ToString().Length - 2) + "}", result);
    Console.WriteLine($"Ответ: {formattedResult}, точность: {accuracy}");
    return result;
}

(decimal, decimal) Otdelenie(decimal a, Uravnenie ur, decimal h = 0.1m)
{
    decimal x1 = a;
    decimal x2 = a + h;
    decimal y1 = ur.Invoke(x1);
    while(true)
    {
        decimal y2 = ur.Invoke(x2);
        if (y1 * y2 < 0)
        {
            Console.WriteLine($"a: {x1}, b: {x2}");
            return (x1, x2);
        }
        x1 = x2;
        x2 = x1 + h;
        y1 = y2;
    }
}

decimal SimpleIterations(Uravnenie ur, int maxIterations = 1000000)
{
    decimal accuracy = Convert.ToDecimal(Console.ReadLine());
    decimal x0 = Otdelenie(0m, ur).Item1;
    decimal x1 = ur.Invoke(x0, true);
    var counter = 0;
    while(Math.Abs(x1-x0) > accuracy && counter < maxIterations)
    {
        x0 = x1;
        x1 = ur.Invoke(x0, true);
        counter++;
    }
    if (counter == maxIterations)
    {
        Console.WriteLine("Недостаточно попыток, либо метод не подходит");
        return 0;
    }
    string formattedResult = String.Format("{0:F" + (accuracy.ToString().Length - 2) + "}", x1);
    Console.WriteLine($"Ответ: {formattedResult}, точность: {accuracy}");
    return x1;
}
    
decimal Function_Task1(decimal x, bool virazh_x = false)
{
    
    if (virazh_x) return x - 0.4m * (x * (decimal)Math.Sin((double)x) - 1); // x = x - 0.4(xsinx - 1)
    return x * (decimal)Math.Sin((double)x) - 1; //xsinx - 1 = 0
}

decimal Function_Task6(decimal x, bool virazh_x = false)
{
    if (virazh_x) return x - 0.25m * ((decimal)Math.Pow(2, (double)x) - 2m * (decimal)Math.Cos((double)x)); // x = x - 0.25(2^x - 2cosx)
    return (decimal)Math.Pow(2, (double)x) - 2m * (decimal)Math.Cos((double)x); // 2^x - 2cosx = 0
}

delegate decimal Uravnenie(decimal x, bool virazh_x = false);
