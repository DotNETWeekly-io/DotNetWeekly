CancellationTokenSource soruce = new CancellationTokenSource();
Task.Run(() =>
{
    Console.ReadKey();
    soruce.Cancel();
});
CancellationToken token = soruce.Token;
try
{
    while (true)
    {
        token.ThrowIfCancellationRequested();
        Console.WriteLine("Cancel operation in 10 seconds");
        await Task.Delay(10000, token);
    }
}
catch (Exception e)
{
    Console.WriteLine($"{e.GetType()}: {e.ToString()}");
}


soruce.Dispose();