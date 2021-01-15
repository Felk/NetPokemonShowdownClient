using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Showdown.Protocol.Events;
using static Showdown.Protocol.Parsing;

namespace Showdown.Protocol
{
    public sealed class ShowdownProtocolClient
    {
        private readonly ILogger<ShowdownProtocolClient> _logger;

        private IAsyncMessageStream? _stream;

        public class AnyEventArgs : EventArgs
        {
            public string? RoomId { get; }
            public string Type { get; }
            public string? Data { get; }

            public AnyEventArgs(string? roomId, string type, string? data)
            {
                RoomId = roomId;
                Type = type;
                Data = data;
            }

            public override string ToString() => $"({RoomId ?? "<lobby>"}) {Type}: {Data}";
        }

        public event EventHandler<AnyEventArgs>? Any;
        private void OnAny(AnyEventArgs e) => Any?.Invoke(this, e);

        #region global events

        public event EventHandler<PopupEventArgs>? Popup;
        public event EventHandler<PmEventArgs>? Pm;
        public event EventHandler<UserCountEventArgs>? UserCount;
        public event EventHandler<NameTakenEventArgs>? NameTaken;
        public event EventHandler<ChallStrEventArgs>? ChallStr;
        public event EventHandler<UpdateUserEventArgs>? UpdateUser;
        public event EventHandler<FormatsEventArgs>? Formats;
        public event EventHandler<UpdateSearchEventArgs>? UpdateSearch;
        public event EventHandler<UpdateChallengesEventArgs>? UpdateChallenges;
        public event EventHandler<QueryResponseEventArgs>? QueryResponse;
        private void OnPopup(PopupEventArgs e) => Popup?.Invoke(this, e);
        private void OnPm(PmEventArgs e) => Pm?.Invoke(this, e);
        private void OnUserCount(UserCountEventArgs e) => UserCount?.Invoke(this, e);
        private void OnNameTaken(NameTakenEventArgs e) => NameTaken?.Invoke(this, e);
        private void OnChallStr(ChallStrEventArgs e) => ChallStr?.Invoke(this, e);
        private void OnUpdateUser(UpdateUserEventArgs e) => UpdateUser?.Invoke(this, e);
        private void OnFormats(FormatsEventArgs e) => Formats?.Invoke(this, e);
        private void OnUpdateSearch(UpdateSearchEventArgs e) => UpdateSearch?.Invoke(this, e);
        private void OnUpdateChallenges(UpdateChallengesEventArgs e) => UpdateChallenges?.Invoke(this, e);
        private void OnQueryResponse(QueryResponseEventArgs e) => QueryResponse?.Invoke(this, e);

        #endregion

        #region room init events

        public event EventHandler<InitEventArgs>? RoomInit;
        public event EventHandler<TitleEventArgs>? RoomTitle;
        public event EventHandler<UsersEventArgs>? RoomUsers;
        private void OnRoomInit(InitEventArgs e) => RoomInit?.Invoke(this, e);
        private void OnRoomTitle(TitleEventArgs e) => RoomTitle?.Invoke(this, e);
        private void OnRoomUsers(UsersEventArgs e) => RoomUsers?.Invoke(this, e);

        #endregion

        #region room events

        public event EventHandler<MessageEventArgs>? RoomMessage;
        public event EventHandler<HtmlEventArgs>? RoomHtml;
        public event EventHandler<UHtmlEventArgs>? RoomUHtml;
        public event EventHandler<UHtmlChangeEventArgs>? RoomUHtmlChange;
        public event EventHandler<JoinEventArgs>? RoomJoin;
        public event EventHandler<LeaveEventArgs>? RoomLeave;
        public event EventHandler<NameEventArgs>? RoomName;
        public event EventHandler<ChatEventArgs>? RoomChat;
        public event EventHandler<NotifyEventArgs>? RoomNotify;
        public event EventHandler<ChatWithTimestampEventArgs>? RoomChatWithTimestamp;
        public event EventHandler<TimestampEventArgs>? RoomTimestamp;
        public event EventHandler<BattleEventArgs>? RoomBattle;
        private void OnRoomMessage(MessageEventArgs e) => RoomMessage?.Invoke(this, e);
        private void OnRoomHtml(HtmlEventArgs e) => RoomHtml?.Invoke(this, e);
        private void OnRoomUHtml(UHtmlEventArgs e) => RoomUHtml?.Invoke(this, e);
        private void OnRoomUHtmlChange(UHtmlChangeEventArgs e) => RoomUHtmlChange?.Invoke(this, e);
        private void OnRoomJoin(JoinEventArgs e) => RoomJoin?.Invoke(this, e);
        private void OnRoomLeave(LeaveEventArgs e) => RoomLeave?.Invoke(this, e);
        private void OnRoomName(NameEventArgs e) => RoomName?.Invoke(this, e);
        private void OnRoomChat(ChatEventArgs e) => RoomChat?.Invoke(this, e);
        private void OnRoomNotify(NotifyEventArgs e) => RoomNotify?.Invoke(this, e);
        private void OnRoomChatWithTimestamp(ChatWithTimestampEventArgs e) => RoomChatWithTimestamp?.Invoke(this, e);
        private void OnRoomTimestamp(TimestampEventArgs e) => RoomTimestamp?.Invoke(this, e);
        private void OnRoomBattle(BattleEventArgs e) => RoomBattle?.Invoke(this, e);

        #endregion

        #region tourname events

        // TODO

        #endregion

        #region battle init events

        public event EventHandler<PlayerEventArgs>? BattlePlayer;
        public event EventHandler<TeamSizeEventArgs>? BattleTeamSize;
        public event EventHandler<GameTypeEventArgs>? BattleGameType;
        public event EventHandler<GenEventArgs>? BattleGen;
        public event EventHandler<TierEventArgs>? BattleTier;
        public event EventHandler<RatedEventArgs>? BattleRated;
        public event EventHandler<RatedInfoEventArgs>? BattleRatedInfo;
        public event EventHandler<RuleEventArgs>? BattleRule;
        public event EventHandler<ClearPokeEventArgs>? BattleClearPoke;
        public event EventHandler<PokeEventArgs>? BattlePoke;
        public event EventHandler<TeamPreviewEventArgs>? BattleTeamPreview;
        public event EventHandler<StartEventArgs>? BattleStart;
        private void OnBattlePlayer(PlayerEventArgs e) => BattlePlayer?.Invoke(this, e);
        private void OnBattleTeamSize(TeamSizeEventArgs e) => BattleTeamSize?.Invoke(this, e);
        private void OnBattleGameType(GameTypeEventArgs e) => BattleGameType?.Invoke(this, e);
        private void OnBattleGen(GenEventArgs e) => BattleGen?.Invoke(this, e);
        private void OnBattleTier(TierEventArgs e) => BattleTier?.Invoke(this, e);
        private void OnBattleRated(RatedEventArgs e) => BattleRated?.Invoke(this, e);
        private void OnBattleRatedInfo(RatedInfoEventArgs e) => BattleRatedInfo?.Invoke(this, e);
        private void OnBattleRule(RuleEventArgs e) => BattleRule?.Invoke(this, e);
        private void OnBattleClearPoke(ClearPokeEventArgs e) => BattleClearPoke?.Invoke(this, e);
        private void OnBattlePoke(PokeEventArgs e) => BattlePoke?.Invoke(this, e);
        private void OnBattleTeamPreview(TeamPreviewEventArgs e) => BattleTeamPreview?.Invoke(this, e);
        private void OnBattleStart(StartEventArgs e) => BattleStart?.Invoke(this, e);

        #endregion

        #region battle progress events

        public event EventHandler<ClearMessageBarEventArgs>? BattleClearMessageBar;
        public event EventHandler<RequestEventArgs>? BattleRequest;
        public event EventHandler<InactiveEventArgs>? BattleInactive;
        public event EventHandler<InactiveOffEventArgs>? BattleInactiveOff;
        public event EventHandler<UpkeepEventArgs>? BattleUpkeep;
        public event EventHandler<TurnEventArgs>? BattleTurn;
        public event EventHandler<WinEventArgs>? BattleWin;
        public event EventHandler<TieEventArgs>? BattleTie;
        private void OnBattleClearMessageBar(ClearMessageBarEventArgs e) => BattleClearMessageBar?.Invoke(this, e);
        private void OnBattleRequest(RequestEventArgs e) => BattleRequest?.Invoke(this, e);
        private void OnBattleInactive(InactiveEventArgs e) => BattleInactive?.Invoke(this, e);
        private void OnBattleInactiveOff(InactiveOffEventArgs e) => BattleInactiveOff?.Invoke(this, e);
        private void OnBattleUpkeep(UpkeepEventArgs e) => BattleUpkeep?.Invoke(this, e);
        private void OnBattleTurn(TurnEventArgs e) => BattleTurn?.Invoke(this, e);
        private void OnBattleWin(WinEventArgs e) => BattleWin?.Invoke(this, e);
        private void OnBattleTie(TieEventArgs e) => BattleTie?.Invoke(this, e);

        #endregion

        #region battle major action events

        public event EventHandler<MoveEventArgs>? BattleMove;
        public event EventHandler<SwitchEventArgs>? BattleSwitch;
        public event EventHandler<DragEventArgs>? BattleDrag;
        public event EventHandler<DetailsChangeEventArgs>? BattleDetailsChange;
        public event EventHandler<ReplaceEventArgs>? BattleReplace;
        public event EventHandler<SwapEventArgs>? BattleSwap;
        public event EventHandler<CantEventArgs>? BattleCant;
        public event EventHandler<FaintEventArgs>? BattleFaint;
        private void OnBattleMove(MoveEventArgs e) => BattleMove?.Invoke(this, e);
        private void OnBattleSwitch(SwitchEventArgs e) => BattleSwitch?.Invoke(this, e);
        private void OnBattleDrag(DragEventArgs e) => BattleDrag?.Invoke(this, e);
        private void OnBattleDetailsChange(DetailsChangeEventArgs e) => BattleDetailsChange?.Invoke(this, e);
        private void OnBattleReplace(ReplaceEventArgs e) => BattleReplace?.Invoke(this, e);
        private void OnBattleSwap(SwapEventArgs e) => BattleSwap?.Invoke(this, e);
        private void OnBattleCant(CantEventArgs e) => BattleCant?.Invoke(this, e);
        private void OnBattleFaint(FaintEventArgs e) => BattleFaint?.Invoke(this, e);

        #endregion

        #region battle minor action events

        // TODO

        #endregion

        public ShowdownProtocolClient(ILogger<ShowdownProtocolClient> logger)
        {
            _logger = logger;
            _stream = null;
        }

        public async Task Run(IAsyncMessageStream stream, CancellationToken cancellationToken)
        {
            _stream = stream;
            await ReadForever(cancellationToken);
        }

        private void DispatchByType(string? roomId, string type, string data)
        {
            switch (type)
            {
                #region room events

                case "init":
                    OnRoomInit(new InitEventArgs(roomId, ParseRoomType(data)));
                    return;
                case "title":
                    OnRoomTitle(new TitleEventArgs(roomId, data));
                    return;
                case "users":
                    OnRoomUsers(new UsersEventArgs(
                        roomId, data.Split(',').Select(ParseUserWithStatus).ToImmutableList()));
                    return;
                case "":
                    OnRoomMessage(new MessageEventArgs(roomId, data));
                    return;
                case "html":
                    OnRoomHtml(new HtmlEventArgs(roomId, data));
                    return;
                case "uhtml":
                    {
                        (string name, string html) = SplitOffOne(data);
                        OnRoomUHtml(new UHtmlEventArgs(roomId, name, html));
                        return;
                    }
                case "uhtmlchange":
                    {
                        (string name, string html) = SplitOffOne(data);
                        OnRoomUHtmlChange(new UHtmlChangeEventArgs(roomId, name, html));
                        return;
                    }
                case "join":
                case "j":
                case "J":
                    OnRoomJoin(new JoinEventArgs(roomId, ParseUser(data), type != "J"));
                    return;
                case "leave":
                case "l":
                case "L":
                    OnRoomLeave(new LeaveEventArgs(roomId, ParseUser(data), type != "L"));
                    return;
                case "battle":
                case "b":
                case "B":
                    (string battleRoomId, string name1, string name2) = SplitOffTwo(data);
                    OnRoomBattle(new BattleEventArgs(battleRoomId, ParseUser(name1),
                        ParseUser(name2), type != "B"));
                    return;
                case "name":
                case "n":
                case "N":
                    {
                        (string name, string oldName) = SplitOffOne(data);
                        OnRoomName(new NameEventArgs(roomId, oldName, ParseUserWithStatus(name),
                            type != "N"));
                        return;
                    }
                case "chat":
                case "c":
                    {
                        (string name, string message) = SplitOffOne(data);
                        OnRoomChat(new ChatEventArgs(roomId, ParseUser(name), message));
                        return;
                    }
                case "notify":
                    {
                        string[] parts = data.Split('|', count: 3);
                        if (parts.Length < 2 || parts.Length > 3)
                            throw new ProtocolViolationException($"wrong number of parts, expected 2 or 3 in '{data}'");
                        OnRoomNotify(new NotifyEventArgs(roomId, parts[0], parts[1], parts.Length == 3 ? parts[2] : null));
                        return;
                    }
                case "c:":
                    {
                        (string timestamp, string name, string message) = SplitOffTwo(data);
                        DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(int.Parse(timestamp)).UtcDateTime;
                        OnRoomChatWithTimestamp(new ChatWithTimestampEventArgs(
                            roomId, utcDateTime, ParseUser(name), message));
                        return;
                    }
                case ":":
                    {
                        DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(int.Parse(data)).UtcDateTime;
                        OnRoomTimestamp(new TimestampEventArgs(utcDateTime));
                        return;
                    }

                #endregion room events

                #region global events

                case "popup":
                    OnPopup(new PopupEventArgs(data));
                    return;
                case "pm":
                    {
                        (string sender, string receiver, string message) = SplitOffTwo(data);
                        OnPm(new PmEventArgs(
                            ParseUser(sender), ParseUser(receiver), message));
                        return;
                    }
                case "usercount":
                    OnUserCount(new UserCountEventArgs(int.Parse(data)));
                    return;
                case "nametaken":
                    (string nameTakenUsername, string nameTakenMessage) = SplitOffOne(data);
                    OnNameTaken(new NameTakenEventArgs(nameTakenUsername, nameTakenMessage));
                    return;
                case "challstr":
                    OnChallStr(new ChallStrEventArgs(data));
                    return;
                case "updateuser":
                    {
                        (string user, string namedStr, string avatar, string settings) = SplitOffThree(data);
                        bool named = namedStr switch
                        {
                            "1" => true,
                            "0" => false,
                            _ => throw new ProtocolViolationException(
                                $"Expected 'named' to be 0 or 1, but was '{namedStr}'")
                        };
                        OnUpdateUser(new UpdateUserEventArgs(
                            ParseUser(user), named, avatar, settings));
                        return;
                    }
                case "formats":
                    {
                        IImmutableList<FormatSection> sections = ParseFormatSections(data).ToImmutableList();
                        OnFormats(new FormatsEventArgs(sections));
                        return;
                    }
                case "updatesearch":
                    OnUpdateSearch(new UpdateSearchEventArgs(data));
                    return;
                case "updatechallenges":
                    OnUpdateChallenges(new UpdateChallengesEventArgs(data));
                    return;
                case "queryresponse":
                    {
                        (string querytype, string json) = SplitOffOne(data);
                        OnQueryResponse(new QueryResponseEventArgs(querytype, json));
                        return;
                    }

                #endregion global events

                #region tournament events

                // TODO

                #endregion tournament events

                #region battle init events

                case "player":
                    {
                        (string playerStr, string username, string avatar, string ratingStr) = SplitOffThree(data);
                        PlayerNum player = ParsePlayerNum(playerStr);
                        int? rating = ratingStr == "" ? (int?)null : int.Parse(ratingStr);
                        OnBattlePlayer(new PlayerEventArgs(roomId, player, username, avatar, rating));
                        return;
                    }
                case "teamsize":
                    {
                        (string playerStr, string number) = SplitOffOne(data);
                        PlayerNum player = ParsePlayerNum(playerStr);
                        OnBattleTeamSize(new TeamSizeEventArgs(roomId, player, int.Parse(number)));
                        return;
                    }
                case "gametype":
                    OnBattleGameType(new GameTypeEventArgs(roomId, ParseGameType(data)));
                    return;
                case "gen":
                    OnBattleGen(new GenEventArgs(roomId, int.Parse(data)));
                    return;
                case "tier":
                    OnBattleTier(new TierEventArgs(roomId, data));
                    return;
                case "rated":
                    if (data.Any()) OnBattleRatedInfo(new RatedInfoEventArgs(roomId, data));
                    else OnBattleRated(new RatedEventArgs(roomId));
                    return;
                case "rule":
                    {
                        string[] parts = data.Split(": ", count: 2);
                        OnBattleRule(new RuleEventArgs(roomId, parts[0], parts[1]));
                        return;
                    }
                case "clearpoke":
                    if (data.Any()) throw new ProtocolViolationException($"Unexpected additional data for '{type}'");
                    OnBattleClearPoke(new ClearPokeEventArgs(roomId));
                    return;
                case "poke":
                    {
                        (string player, string details, string item) = SplitOffTwo(data);
                        OnBattlePoke(new PokeEventArgs(roomId, player, ParseDetails(details), item));
                        return;
                    }
                case "teampreview":
                    // TODO `|teampreview|24` ? max team size maybe?
                    // if (data.Any()) throw new ProtocolViolationException($"Unexpected additional data for '{type}'");
                    OnBattleTeamPreview(new TeamPreviewEventArgs(roomId));
                    return;
                case "start":
                    if (data.Any()) throw new ProtocolViolationException($"Unexpected additional data for '{type}'");
                    OnBattleStart(new StartEventArgs(roomId));
                    return;

                #endregion battle init events

                #region battle progress events

                // clearing the message bar is handled earlier due to its special nature
                case "request":
                    OnBattleRequest(new RequestEventArgs(roomId, data));
                    return;
                case "inactive":
                    OnBattleInactive(new InactiveEventArgs(roomId, data));
                    return;
                case "inactiveoff":
                    OnBattleInactiveOff(new InactiveOffEventArgs(roomId, data));
                    return;
                case "upkeep":
                    if (data.Any()) throw new ProtocolViolationException($"Unexpected additional data for '{type}'");
                    OnBattleUpkeep(new UpkeepEventArgs(roomId));
                    return;
                case "turn":
                    int turn = int.Parse(data);
                    OnBattleTurn(new TurnEventArgs(roomId, turn));
                    return;
                case "win":
                    OnBattleWin(new WinEventArgs(roomId, data));
                    return;
                case "tie":
                    if (data.Any()) throw new ProtocolViolationException($"Unexpected additional data for '{type}'");
                    OnBattleTie(new TieEventArgs(roomId));
                    return;

                #endregion battle progress events

                #region battle major action events

                case "move":
                    {
                        string[] parts = data.Split('|');
                        if (parts.Length < 3) throw new ProtocolViolationException("'move' must have at least 3 arguments");
                        bool miss = false;
                        bool still = false;
                        string? anim = null;
                        foreach (string moreInfo in parts.Skip(3))
                        {
                            if (moreInfo == "[miss]") miss = true;
                            else if (moreInfo == "[still]") still = true;
                            else if (moreInfo.StartsWith("[anim] ")) anim = moreInfo.Substring("[anim] ".Length);
                            else throw new ProtocolViolationException($"Unknown move appendix '{moreInfo}'");
                        }

                        OnBattleMove(new MoveEventArgs(
                            roomId,
                            ParsePokemon(parts[0]),
                            parts[1],
                            parts[2] == "" ? null : parts[2], // TODO is target a pokemon? would make sense
                            miss,
                            still,
                            anim));
                        return;
                    }
                case "switch":
                case "drag":
                case "detailschange":
                case "replace":
                    {
                        (string pokemonStr, string detailsStr, string rest) = SplitOffTwo(data);
                        Pokemon pokemon = ParsePokemon(pokemonStr);
                        Details details = ParseDetails(detailsStr);

                        string[] parts = rest.Split(' ', count: 2);
                        string? status = parts.Length == 2 && parts[1] != "" ? parts[1] : null;
                        string[] hpParts = parts[0].Split("/", count: 2);
                        if (hpParts.Length != 2)
                            throw new ProtocolViolationException(
                                $"expected HP in the form A/B, not '{parts[0]}'");
                        int hpDividend = int.Parse(hpParts[0]);
                        int hpDivisor = int.Parse(hpParts[1]);
                        switch (type)
                        {
                            case "switch":
                                OnBattleSwitch(new SwitchEventArgs(
                                    roomId, pokemon, details, hpDividend, hpDivisor, status));
                                return;
                            case "drag":
                                OnBattleDrag(new DragEventArgs(
                                    roomId, pokemon, details, hpDividend, hpDivisor, status));
                                return;
                            case "detailschange":
                                OnBattleDetailsChange(new DetailsChangeEventArgs(
                                    roomId, pokemon, details, hpDividend, hpDivisor, status));
                                return;
                            case "replace":
                                OnBattleReplace(new ReplaceEventArgs(
                                    roomId, pokemon, details, hpDividend, hpDivisor, status));
                                return;
                            default:
                                throw new ArgumentException($"unexpected argument '{type}'");
                        }
                    }
                case "swap":
                    {
                        (string pokemon, string position) = SplitOffOne(data);
                        OnBattleSwap(new SwapEventArgs(roomId, ParsePokemon(pokemon), int.Parse(position)));
                        return;
                    }
                case "cant":
                    {
                        string[] parts = data.Split('|', count: 3);
                        if (parts.Length < 2)
                            throw new ProtocolViolationException(
                                $"Expected at least 2 arguments for 'cant', but received: {data}");
                        OnBattleCant(new CantEventArgs(roomId, ParsePokemon(parts[0]), parts[1],
                            parts.Length > 2 ? parts[2] : null));
                        return;
                    }
                case "faint":
                    OnBattleFaint(new FaintEventArgs(roomId, ParsePokemon(data)));
                    return;

                    #endregion battle major action events

                    #region battle minor action events

                    // TODO

                    #endregion battle minor action events
            }

            _logger.LogWarning($"unrecognized message type '{type}', content: {data}");
        }

        private static (string, string) SplitOffOne(string str)
        {
            string[] split = str.Split('|', count: 2);
            if (split.Length < 2) throw new ProtocolViolationException($"too few parts, expected 2 in '{str}'");
            return (split[0], split[1]);
        }

        private static (string, string, string) SplitOffTwo(string str)
        {
            string[] split = str.Split('|', count: 3);
            if (split.Length < 3) throw new ProtocolViolationException($"too few parts, expected 3 in: '{str}'");
            return (split[0], split[1], split[2]);
        }

        private static (string, string, string, string) SplitOffThree(string str)
        {
            string[] split = str.Split('|', count: 4);
            if (split.Length < 4) throw new ProtocolViolationException($"too few parts, expected 4 in: '{str}'");
            return (split[0], split[1], split[2], split[3]);
        }

        private void Parse(string message, CancellationToken cancellationToken)
        {
            IEnumerable<string> lines = message.Split('\n').Where(s => s.Length > 0);
            string? roomId = null;
            bool first = true;
            foreach (string line in lines)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (first)
                {
                    first = false;
                    if (line.StartsWith(">"))
                    {
                        roomId = line.Substring(1);
                        continue;
                    }
                }

                if (!line.StartsWith("|"))
                {
                    OnRoomMessage(new MessageEventArgs(roomId, line));
                    continue;
                }

                if (line == "|")
                {
                    OnBattleClearMessageBar(new ClearMessageBarEventArgs(roomId));
                    continue;
                }

                string[] parts = line.Substring(1).Split('|', count: 2);
                string type = parts[0];
                string? data = parts.Length > 1 ? parts[1] : null;
                OnAny(new AnyEventArgs(roomId, type, data));
                try
                {
                    DispatchByType(roomId, type, data ?? ""); // TODO
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"failed to parse message line '{line}'");
                }
            }
        }

        private async Task ReadForever(CancellationToken cancellationToken)
        {
            string? message;
            while ((message = await _stream!.ReadAsync(cancellationToken)) != null)
            {
                try
                {
                    Parse(message, cancellationToken);
                }
                catch (ProtocolViolationException ex)
                {
                    _logger.LogError("failed to process incoming message: " + message, ex);
                }
            }

            // TODO disconnected
        }

        public async Task SendAsync(string? roomId, string message, CancellationToken? cancellationToken = null)
        {
            string msg = (roomId ?? "") + "|" + message;
            _logger.LogDebug($">> {msg}");
            await _stream!.WriteAsync(msg, cancellationToken);
        }
    }
}