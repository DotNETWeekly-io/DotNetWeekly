global using System.Text.RegularExpressions;
using System.Diagnostics;


var regex = new Regex("a");
var isMatch = regex.IsMatch("Learn C# language");

Debug.Assert(isMatch == true);

var matches = regex.Matches("Learn C# Language");
Debug.Assert(3 == matches.Count);

regex = new Regex("^hello$");

isMatch = regex.IsMatch("hello");
Debug.Assert(isMatch == true);

isMatch = regex.IsMatch("Hello world");
Debug.Assert(isMatch == false);


regex = new Regex(@"\d");

matches = regex.Matches(@"9841 Shadow Way St Sunland, California(CA)");

Debug.Assert(4==matches.Count);
Debug.Assert("9" == matches[0].Value);
Debug.Assert("8" == matches[1].Value);
Debug.Assert("4" == matches[2].Value);
Debug.Assert("1" == matches[3].Value);


regex = new Regex(@"\d+");
var match = regex.Match(@"9841 Shadow Way St
                            Sunland, California(CA)");

Debug.Assert("9841" == match.Value);

regex = new Regex("[sg]old");
matches = regex.Matches("sold cold gold");
Debug.Assert(2 == matches.Count);
Debug.Assert("sold" == matches[0].Value);
Debug.Assert("gold" == matches[1].Value);

regex = new Regex(@"</?\w+>");

var result = regex.Replace("<b>Hello, <i>world</i></b>", string.Empty);

Debug.Assert("Hello, world" == result);