using System.Text;
using System.Text.RegularExpressions;

namespace Day09
{
    public class DecompressorVersion1
    { 
        private string input;
        private string output;
        private int length;
        public DecompressorVersion1(string input)
        {
            var markers = new Regex(@"(\(\d+x\d+\))+").Matches(input);

            int markerPos = 0;
            StringBuilder decoded = new StringBuilder();

            foreach(Match m in markers)
            {
                if(m.Index < markerPos) continue;

                var before = input.Substring(markerPos, m.Index - markerPos);
                decoded.Append(before);
                markerPos = m.Index;

                var markerEnd = m.Value.IndexOf(')');
                var splitPos = m.Value.IndexOf('x');
                
                var len = int.Parse(m.Value.Substring(1, splitPos - 1));
                var repeat = int.Parse(m.Value.Substring(splitPos + 1, markerEnd - splitPos - 1));

                var after = input.Substring(markerPos + markerEnd + 1, len);

                for(int x=0; x<repeat; ++x)
                {
                    decoded.Append(after);
                }

                markerPos += len + markerEnd + 1;
            }

            var remaining = input.Substring(markerPos);
            decoded.Append(remaining);

            this.input = input;
            this.output = decoded.ToString();
            this.length = output.Length;
        }

        public string Input { get { return input; } }
        public string Output { get { return output; } }
        public int Length { get { return length; } }
    }
}
