using BgpHeNet.Models;
using HtmlAgilityPack;

namespace BgpHeNet;

class BgpNetService : IBgpNetService
{
    private const string UserAgent =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36";


    public async Task<List<string?>> GetDns(string cidr)
    {
        List<string?> result = new();
        using HttpClient client = new();
        client.BaseAddress = new Uri("https://bgp.he.net/");
        client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);

        try
        {
            HttpResponseMessage response = await client.GetAsync($"net/{cidr}#_dns").ConfigureAwait(false);
            response.EnsureSuccessStatusCode(); // Throws an exception if not successful
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var doc = new HtmlDocument();
            doc.LoadHtml(responseBody);

            var rows = doc.DocumentNode.SelectNodes("//div[@id='dns']//table//tbody//tr");
            if (rows is null)
            {
                return result;
            }

            foreach (var row in rows)
            {
                // string? ip = row.SelectSingleNode(".//td[1]//a")?.InnerText.Trim();
                // string? ptr = row.SelectSingleNode(".//td[2]//a")?.InnerText.Trim();
                var urlsNodes = row?.SelectNodes(".//td[3]//a");
                if (urlsNodes is null) continue;
                var urls = urlsNodes.Select(urlNode => urlNode.InnerText.Trim()).ToList();
                result.AddRange(urls);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }

        return result;
    }

    public async Task<List<string>> GetPrefixes(string asn)
    {
        List<string> result = new();
        using HttpClient client = new();
        client.BaseAddress = new Uri("https://bgp.he.net/");
        client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);

        try
        {
            HttpResponseMessage response = await client.GetAsync($"{asn}#_prefixes");
            response.EnsureSuccessStatusCode(); // Throws an exception if not successful

            string responseBody = await response.Content.ReadAsStringAsync();

            // Console.WriteLine(responseBody);
            var doc = new HtmlDocument();
            doc.LoadHtml(responseBody);

            var rows = doc.DocumentNode.SelectNodes("//*[@id='table_prefixes4']//tbody//tr");
            var urlStr = string.Empty;
            foreach (var row in rows)
            {
                string? cidr = row.SelectSingleNode(".//td[1]//a")?.InnerText.Trim();
                if (cidr != null) result.Add(cidr);
                // Console.WriteLine($"CIDR: {cidr}");
                // Console.WriteLine("------------------------------------------------------------");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }

        return result;
    }

    public Task<List<Prefix>> GetPrefixesByAsnName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<Url>> GetUrlsByPredixAddress(string address)
    {
        throw new NotImplementedException();
    }

    public Task<List<Url>> GetUrlsByIpAddress(string ip)
    {
        throw new NotImplementedException();
    }

    public Task<ASN> GetASNByIpAddress(string ip)
    {
        throw new NotImplementedException();
    }

    public Task<ASN> GetASNByPrefixAddress(string address)
    {
        throw new NotImplementedException();
    }

    public Task<ASN> GetASNByUrl(string url)
    {
        throw new NotImplementedException();
    }
}