using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

string json_text = "{\"Current\":{\"Time\":\"2023-06-18T20:35:06.722127+04:00\",\"Temperature\":29,\"Weathercode\":1,\"Windspeed\":2.1,\"Winddirection\":1},\"History\":[{\"Time\":\"2023-06-17T20:35:06.77707+04:00\",\"Temperature\":29,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1},{\"Time\":\"2023-06-16T20:35:06.777081+04:00\",\"Temperature\":22,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1},{\"Time\":\"2023-06-15T20:35:06.777082+04:00\",\"Temperature\":21,\"Weathercode\":4,\"Windspeed\":2.2,\"Winddirection\":1}]}";

var xml = """
    <?xml version="1.0" encoding="utf-8"?>
    <Data.Root xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Data.Root.Names>
    <Name>Name1</Name>
    <Name>Name2</Name>
    <Name>Name3</Name>
    </Data.Root.Names>
    <Data.Entry LinkedEntry="Name1">
    <Data.Name>Name2</Data.Name>
    </Data.Entry>
    <Data_x0023_ExtendedEntry LinkedEntry="Name2">
    <Data.Name>Name1</Data.Name>
    <Data_x0023_Extended>NameOne</Data_x0023_Extended>
    </Data_x0023_ExtendedEntry>
    </Data.Root>
    """;

DataRoot dr = new DataRoot();
dr.names = new string[] { "Name1", "Name2", "Name3" };
dr.entry = new DataEntry[2];
dr.entry[0] = new DataEntry();
dr.entry[0].linked_entry = dr.names[0];
dr.entry[0].name = dr.names[1];
dr.entry[1] = new Data_x { linked_entry = "Name2", name = "Name1", data = "NameOne"};
XmlSerializer serializer = new XmlSerializer(typeof(DataRoot));
serializer.Serialize(Console.Out, dr);

var value = new List<string>(FindValueInJSON(json_text));

var weatherInfo = JsonSerializer.Deserialize<WeatherInfo>(json_text);
 Console.ReadKey();


JsonDocument json = JsonDocument.Parse(json_text);
serializer.Serialize(Console.Out, json);



List<string> FindValueInJSON(string text)
{
    List<string> values = new List<string>();
    return values;
}
public class Weather
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public int Weathercode { get; set; }
    public double Windspeed { get; set; }
    public int Winddirection { get; set; }
}
public class WeatherInfo
{
    public Weather Current { get; set; }
    public List<Weather> History { get; set; }
}




[XmlRoot("Data.Root")]
public class DataRoot
{
    [XmlArray("Data.Root.Names")]
    [XmlArrayItem("Name")]
    public string[] names;

    [XmlElement(typeof(DataEntry))]
    [XmlElement(typeof(Data_x))]
    public DataEntry[] entry;
}
[XmlType("Data.Empty")]
public class DataEntry
{
    public string linked_entry;
    [XmlElement("Data.Name")]
    public string name;
}
[XmlType("Data#ExtendedEntry")]
public class Data_x : DataEntry
{
    [XmlElement("Data#Extended")]
    public string data;
}