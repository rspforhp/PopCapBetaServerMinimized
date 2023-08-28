using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace TestPopcapBetaStuff;

public static class Extension
{
    public static string MD5Hash(this string s)
    {
        byte[] ansi = Encoding.ASCII.GetBytes(s);
        byte[] hash = MD5.HashData(ansi);
        var r= Convert.ToHexString(hash);
        return r;
    }
}