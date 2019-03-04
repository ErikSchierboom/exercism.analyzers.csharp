using System.Linq;
using System.Threading.Tasks;

namespace Exercism.Analyzers.CSharp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Logging.Configure();

            await Analyzer.Analyze(args.First());
        }
    }
}