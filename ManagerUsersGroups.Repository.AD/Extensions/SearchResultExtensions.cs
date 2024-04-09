using System.DirectoryServices;
using System.Security.Principal;

namespace ManagerUsersGroups.Repository.AD.Extensions
{
    public static class SearchResultExtensions
    {
        public static string GetProp(this SearchResult searchResult, string prop) =>
            searchResult.Properties[prop].Cast<string>().FirstOrDefault(string.Empty);

        public static string GetSid(this SearchResult searchResult) =>
            new SecurityIdentifier(searchResult.Properties["objectSid"].Cast<byte[]>().First(), 0).ToString();
    }
}
