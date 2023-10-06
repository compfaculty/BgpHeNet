using BgpHeNet;

var client = new BgpNetService();
try
{
    var prefixes = await client.GetPrefixes(args[0]);
    foreach (var prefix in prefixes)
    {
        foreach (var u in await client.GetDns(prefix))
        {
            Console.WriteLine(u);
        }
    }

}
catch (Exception e)
{
    Console.WriteLine("looks you forget AS number , i.e. BgpHeNet.exe AS51115");
    Environment.Exit(1);
}

// Console.ReadKey();