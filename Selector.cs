using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTML_Serializer
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        #region 1
        public static Selector FromQueryString(string queryString)
        {
            var selectors = queryString.Split(' ');
            var rootSelector = new Selector();
            var currentSelector = rootSelector;
            List<string> parts = new List<string>();

            foreach (var selectorStr in selectors)
            {
                parts = Regex.Matches(selectorStr, @"(?:\.|#)?[\w-]+")
                               .Cast<Match>()
                               .Select(m => m.Value)
                               .ToList();

                for (int i = 0; i < parts.Count; i++)
                {
                    var part = parts[i];

                    if (string.IsNullOrEmpty(part))
                    {
                        continue;
                    }

                    if (part.StartsWith("#"))
                    {
                        currentSelector.Id = part.Substring(1);
                    }
                    else if (part.StartsWith("."))
                    {
                        currentSelector.Classes.Add(part.Substring(1));
                    }
                    else
                    {
                        if (i == 0 && (HtmlHelper.Instance.HtmlTags.Contains(part)
                            || HtmlHelper.Instance.HtmlVoidTags.Contains(part)))
                        {
                            currentSelector.TagName = part;
                        }
                    }


                }
                var newSelector = new Selector();
                currentSelector.Child = newSelector;
                newSelector.Parent = currentSelector;
                currentSelector = newSelector;
            }

            return rootSelector;
        }

        public override string ToString()
        {
            var result = TagName ?? string.Empty;

            if (!string.IsNullOrEmpty(Id))
            {
                result += $"#{Id}";
            }

            if (Classes.Any())
            {
                result += $".{string.Join(".", Classes)}";
            }

            return result;
        }
        #endregion
        public Selector()
        {
            Classes = new List<string>();
        }



    }
}
