﻿using System.Text.RegularExpressions;
using CoreRCON.Parsers.Standard;

namespace CoreRCON.Parsers.Csgo;

public record FragAssist(Player Assister, Player Killed) : IParseable<FragAssist>;

public sealed class FragAssistParser : RegexParser<FragAssist>
{
    public FragAssistParser() : base(@$"(?<Assister>{PlayerParser.Shared.Pattern}) assisted killing (?<Killed>{PlayerParser.Shared.Pattern})?")
    {
    }

    protected override FragAssist Load(GroupCollection groups) => new(
        PlayerParser.Shared.Parse(groups["Assister"]),
        PlayerParser.Shared.Parse(groups["Killed"])
    );
}
