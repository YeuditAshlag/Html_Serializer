using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HTML_Serializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;

        public string[] HtmlTags { get; set; }
        public string[] HtmlVoidTags { get; set; }
        private HtmlHelper()
        {
            HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("jsonFiles/HtmlTags.json"));
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("jsonFiles/HtmlVoidTags.json"));
        }



    }
}
