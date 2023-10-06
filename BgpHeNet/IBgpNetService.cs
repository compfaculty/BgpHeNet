using BgpHeNet.Models;

namespace BgpHeNet;

public interface IBgpNetService
{
    public Task<List<Prefix>> GetPrefixesByAsnName(string name);
    public Task<List<Url>> GetUrlsByPredixAddress(string address);
    public Task<List<Url>> GetUrlsByIpAddress(string ip);
    public Task<ASN> GetASNByIpAddress(string ip);
    public Task<ASN> GetASNByPrefixAddress(string address);
    public Task<ASN> GetASNByUrl(string url);
}