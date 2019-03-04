using System.Linq;
using System.Threading.Tasks;

namespace Exercism.Analyzers.CSharp
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Logging.Configure();

            var directory = args.FirstOrDefault();
            return await Analyzer.Analyze(directory);
        }
    }
}