using Microsoft.Extensions.DependencyInjection;

namespace MLB_Ranking;

class Program
{

    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IRankingService, RankingService>()
            .AddSingleton<IScoreManagerService, ScoreManagerService>()
            .BuildServiceProvider();

        var rankingService = serviceProvider.GetService<IRankingService>();
        rankingService.PrintMLBRanking();
    }

}
