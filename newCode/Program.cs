// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
///////////////////////////////////////////////////
//ortam değişkenine uygulamayı ekleme
const string name = "PATH";
const EnvironmentVariableTarget scope = EnvironmentVariableTarget.User; // or User
var oldValue = Environment.GetEnvironmentVariable(name, scope);
var path = Directory.GetCurrentDirectory();
if (oldValue != null && !oldValue.Contains(path))
{
    var newValue = oldValue + @";" + path + ";";
    Environment.SetEnvironmentVariable(name, newValue, scope);
}
///////////////////////////////////////////////////////
string desc = "";
if (args.Length > 0)
{
    var descIndex = Array.FindIndex(args, x => x.Contains("--desc") || x.Contains("--description") ||x.Contains("--d"));
    desc = descIndex == -1 ? "" : args[++descIndex];
    if (!args[0].TrimStart().StartsWith("--"))
    {
        createFolder(args[0]);
    }else
        createFolder(Environment.GetEnvironmentVariable("USERPROFILE") + "\\çöplük");

}
else
{
    createFolder(Environment.GetEnvironmentVariable("USERPROFILE") + "\\çöplük");
}

void createFolder(string path)
{
    var p = Directory.CreateDirectory(path + "\\" +
                                      DateTime.Now.ToString("d MMMM yyyy HH/mm/ss",
                                          CultureInfo.CreateSpecificCulture("tr-TR"))+
                                      "   " + desc).FullName ;
    
    Process process = new ();
    process.StartInfo.FileName = "cmd.exe";
    process.StartInfo.Arguments = "/C code \"" + p + "\"";
    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
    process.Start();
}