﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace CoreRCON.Parsers.Csgo;

public record RoundEndScore(int CTScore, int TScore, string WinningTeam) : IParseable<RoundEndScore>;

public sealed class RoundEndScoreParser : RegexParser<RoundEndScore>
{
    public RoundEndScoreParser() : base(@"Team ""(?<winning_team>.+?)"" triggered ""SFUI_Notice_.+?_Win"" \(CT ""(?<ct_score>\d+)""\) \(T ""(?<t_score>\d+)""\)")
    {
    }

    protected override RoundEndScore Load(GroupCollection groups) => new(
        int.Parse(groups["ct_score"].Value, CultureInfo.InvariantCulture),
        int.Parse(groups["t_score"].Value, CultureInfo.InvariantCulture),
        groups["winning_team"].Value
    );
}
