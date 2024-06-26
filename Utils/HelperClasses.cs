namespace OtmApi.Utils;

public class ManyPlayersResponseData
{
    public PlayerResponseData[] Users { get; set; } = null!;
}

public class PlayerResponseData
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Avatar_url { get; set; } = null!;
    public int Global_rank { get; set; }
    public string Country_code { get; set; } = null!;
    public Statistics_rulesets Statistics_rulesets { get; set; } = null!;
}

public class Statistics_rulesets
{
    public Osu Osu { get; set; } = null!;

}

public class Osu
{
    public int? Global_rank { get; set; }

}

public class TopicResponseObj
{
    public Post[] Posts { get; set; } = null!;
}

public class Post
{
    public string Id { get; set; } = null!;
    public Body Body { get; set; } = null!;
}

public class Body
{
    public string Html { get; set; } = null!;
    public string Raw { get; set; } = null!;
}


//match classes

public class MatchResponseObj
{
    public Event[] Events { get; set; } = null!;
}

public class Event
{
    public Game? Game { get; set; } = null!;
}

public class Game
{
    public long Beatmap_id { get; set; }
    public string Team_type { get; set; } = null!;
    public string[] Mods { get; set; } = null!;
    public Beatmap? Beatmap { get; set; }
    public ScoreObj[] Scores { get; set; } = null!;

}
public class BeatmapResponseObj
{
    public List<Beatmap> Beatmaps { get; set; } = null!;
}

public class Beatmap
{
    public long Id { get; set; }
    public long Beatmapset_id { get; set; }
    public string Version { get; set; } = null!;
    public decimal Difficulty_rating { get; set; }
    public int Total_length { get; set; }
    public decimal Bpm { get; set; }
    public decimal Ar { get; set; }
    public decimal Accuracy { get; set; } // OD?
    public decimal Cs { get; set; }
    public string Url { get; set; } = null!;

    public Beatmapset Beatmapset { get; set; } = null!;
}

public class Beatmapset
{
    public string Artist { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Creator { get; set; } = null!;
    public Covers Covers { get; set; } = null!;

}

public class Covers
{
    public string Cover { get; set; } = null!;
    public string Slimcover { get; set; } = null!;
}

public class ScoreObj
{
    public double Accuracy { get; set; }
    public int Score { get; set; }
    public Match Match { get; set; } = null!;
    public int User_id { get; set; }
}

public class Match
{
    public string Team { get; set; } = null!;
}


public class Map
{
    public string Title { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public List<string> Mods { get; set; } = null!;
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public int Diff { get; set; }
}

public class MatchResponseObjV1
{
    public GameV1[] Games { get; set; } = null!;
}

public class GameV1
{
    public int Beatmap_id { get; set; }
    public int Team_type { get; set; }
    public int Mods { get; set; }
    public ScoreObjV1[] Scores { get; set; } = null!;


}

public class ScoreObjV1
{
    public int Team { get; set; }
    public int Score { get; set; }
    public int User_id;
}

public class MapV1
{
    public int Beatmap_id { get; set; }
    public int Mods { get; set; }
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public int Diff { get; set; }

    public string? Title { get; set; }
    public string? ImgUrl { get; set; }
    public string? SlimcoverUrl { get; set; }

}


public class AttributeResponse
{
    public Attributes Attributes { get; set; } = null!;
}

public class Attributes
{
    public decimal Star_rating { get; set; }
    public decimal Approach_rate { get; set; }
    public decimal Overall_difficulty { get; set; }
}

public class ChallongeTournamentResp
{
    public ChallongeTournament Tournament { get; set; } = null!;
}

public class ChallongeTournament
{
    public int Id { get; set; }
}
