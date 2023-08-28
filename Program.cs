using System.Security.Cryptography;
using TestPopcapBetaStuff;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Dictionary<string, string> nameToPassDictionary =new Dictionary<string, string>
{
    { "test", "test2".MD5Hash() }
};
app.MapGet("/beta_validate.php", (HttpRequest request) =>
{
    QueryString queryString = request.QueryString;
    var t = queryString.ToString().Split("&");
    t[0]=t[0].Remove(0,1);
    string prod=null;
    string ver=null;
    string name=null;
    string password=null;
    foreach (var pair in t)
    {
        var p = pair.Split('=');
        var _name = p[0];
        var _value = p[1];
        switch (_name)
        {
            case "prod": prod = _value;
                break;
            case "version": ver = _value;
                break;
            case "username": name = _value;
                break;
            case "pass": password = _value;
                break;
        }
    }

    if (name != null)
    {
        try
        {
            var realPass = nameToPassDictionary[name];
            if (realPass.Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                return "SUCCEEDED";
            }
        }
        catch (Exception e)
        {
            return "FAILED";
        }
     
    }
    return "FAILED";
});
app.Run();
