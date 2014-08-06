using Orchard.Localization;
using Orchard.Tokens;
using Orchard.Utility.Extensions;
using System;

namespace Codinlab.SSEO.Tokens {
    public class SeoTokens : ITokenProvider {
        public SeoTokens() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context) {
            context.For("Text")
                .Token("Excerpt:*", T("Excerpt:<text length>"), T("Removes HTML tags and limit text length."), "Text")
                ;
        }

        public void Evaluate(EvaluateContext context) {
            context.For<String>("Text", () => "")
                .Token( // {Text.Excerpt:<length>}
                    token => {
                        if (token.StartsWith("Excerpt:", StringComparison.OrdinalIgnoreCase)) {
                            var param = token.Substring("Excerpt:".Length);
                            return param;
                        }
                        return null;
                    },
                    (token, t) => Excerpt(t, token))
                ;
        }

        private static string Excerpt(string token, string param) {
            if (String.IsNullOrEmpty(token)) {
                return String.Empty;
            }

            int index = param.IndexOf(',');
            int limit = Int32.Parse(param);

            string excerpt = token.RemoveTags(true).ReplaceNewLinesWith(" ").Trim();

            return excerpt.Length > limit ? excerpt.Substring(0, limit) + "..." : excerpt;
        }
    }
}