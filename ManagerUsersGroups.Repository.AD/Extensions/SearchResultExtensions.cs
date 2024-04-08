using System.DirectoryServices;

namespace ManagerUsersGroups.Repository.AD.Extensions
{
    public static class SearchResultExtensions
    {
        public static string GetProp(this SearchResult searchResult, string prop) =>
            searchResult.Properties[prop].Cast<string>().FirstOrDefault(string.Empty);

        public static string GetSid(this SearchResult searchResult, string prop) =>
            string.Empty;
    }
}
