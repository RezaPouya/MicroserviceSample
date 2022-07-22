using IdentityServer4.Models;

namespace IdentityManagment.Extensions
{
    public static class Encoder
    {
        public static string EncodeWithSha256(this string input)
        {
            return input.Sha256();
        }
    }
}
