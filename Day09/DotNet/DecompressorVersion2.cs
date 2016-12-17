using System.Text.RegularExpressions;

namespace Day09
{
    public class DecompressorVersion2
    { 
        private string input;
        private ulong length;
        public DecompressorVersion2(string input)
        {
            this.input = input;
            this.length = DecompressChunck(input);
        }

        private ulong DecompressChunck(string input)
        {
            var markers = new Regex(@"(\(\d+x\d+\))+").Matches(input);

            int markerPos = 0;
            ulong result = 0;

            foreach(Match m in markers)
            {
                if(m.Index < markerPos) continue;

                var before = input.Substring(markerPos, m.Index - markerPos);
                result += (ulong) before.Length;
                markerPos = m.Index;

                var markerEnd = m.Value.IndexOf(')');
                var splitPos = m.Value.IndexOf('x');
                
                var len = int.Parse(m.Value.Substring(1, splitPos - 1));
                var repeat = int.Parse(m.Value.Substring(splitPos + 1, markerEnd - splitPos - 1));

                var after = input.Substring(markerPos + markerEnd + 1, len);
                var test = DecompressChunck(after);

                for(int x=0; x<repeat; ++x)
                {
                    result += test;
                }

                markerPos += len + markerEnd + 1;
            }

            result += (ulong) input.Substring(markerPos).Length;

            return result;
        }

        public ulong Length { get { return length; } }
    }
}
