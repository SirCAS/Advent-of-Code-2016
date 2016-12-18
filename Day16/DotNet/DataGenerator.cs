using System.Linq;
using System.Text;

namespace Day16
{
    public class DataGenerator
    {
        public string GenerateData(string a)
        {
            var b = string.Concat(a.Reverse()).Replace('0', 'H').Replace('1', '0').Replace('H', '1');
            return a + "0" + b;
        }

        public string CalculateChecksum(string data)
        {
            int x=0;
            StringBuilder result = new StringBuilder();
            while(x < data.Length-1)
                result.Append(data[x++] == data[x++] ? "1" : "0");

            if(result.Length % 2 == 0)
                return CalculateChecksum(result.ToString());

            return result.ToString();
        }

        public string CalculateDiskChecksum(string state, int length)
        {
            while(state.Length < length)
                state = GenerateData(state);

            state = state.Substring(0, length);

            return CalculateChecksum(state);
        }
    }
}