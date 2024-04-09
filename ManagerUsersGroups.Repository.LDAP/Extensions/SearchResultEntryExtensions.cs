using System.DirectoryServices.Protocols;
using System.Security.Principal;
using System.Text;

namespace ManagerUsersGroups.Repository.LDAP.Extensions
{
    public static class SearchResultEntryExtensions
    {
        public static string GetProp(this SearchResultEntry searchResultEntry, string prop) =>
            Encoding.UTF8.GetString(searchResultEntry.Attributes[prop].Cast<byte[]>().FirstOrDefault(Array.Empty<byte>()));

        public static IEnumerable<string> GetPropArray(this SearchResultEntry searchResultEntry, string prop) =>
            searchResultEntry.Attributes[prop].Cast<byte[]>().Select(Encoding.UTF8.GetString);

        public static string GetSid(this SearchResultEntry searchResultEntry) =>
            new SecurityIdentifier(searchResultEntry.Attributes["objectSid"].Cast<byte[]>().First(), 0).ToString();

    }
}
