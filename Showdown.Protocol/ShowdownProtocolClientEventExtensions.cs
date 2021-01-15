using System;
using System.Threading.Tasks;
using Showdown.Protocol.Events;

namespace Showdown.Protocol
{
    public static class ShowdownProtocolClientEventExtensions
    {
        private static Task<T> EventToTask<T>(Action<EventHandler<T>> attach, Action<EventHandler<T>> detach)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            void Handler(object? sender, T eventArgs) => taskCompletionSource.SetResult(eventArgs);
            attach(Handler);
            taskCompletionSource.Task.ContinueWith(_ => detach(Handler));
            return taskCompletionSource.Task;
        }

        public static Task<PopupEventArgs> PopupEvent(this ShowdownProtocolClient c) =>
            EventToTask<PopupEventArgs>(h => c.Popup += h, h => c.Popup -= h);

        public static Task<PmEventArgs> PmEvent(this ShowdownProtocolClient c) =>
            EventToTask<PmEventArgs>(h => c.Pm += h, h => c.Pm -= h);

        public static Task<UserCountEventArgs> UserCountEvent(this ShowdownProtocolClient c) =>
            EventToTask<UserCountEventArgs>(h => c.UserCount += h, h => c.UserCount -= h);

        public static Task<NameTakenEventArgs> NameTakenEvent(this ShowdownProtocolClient c) =>
            EventToTask<NameTakenEventArgs>(h => c.NameTaken += h, h => c.NameTaken -= h);

        public static Task<ChallStrEventArgs> ChallStrEvent(this ShowdownProtocolClient c) =>
            EventToTask<ChallStrEventArgs>(h => c.ChallStr += h, h => c.ChallStr -= h);

        public static Task<UpdateUserEventArgs> UpdateUserEvent(this ShowdownProtocolClient c) =>
            EventToTask<UpdateUserEventArgs>(h => c.UpdateUser += h, h => c.UpdateUser -= h);

        public static Task<FormatsEventArgs> FormatsEvent(this ShowdownProtocolClient c) =>
            EventToTask<FormatsEventArgs>(h => c.Formats += h, h => c.Formats -= h);

        public static Task<UpdateSearchEventArgs> UpdateSearchEvent(this ShowdownProtocolClient c) =>
            EventToTask<UpdateSearchEventArgs>(h => c.UpdateSearch += h, h => c.UpdateSearch -= h);

        public static Task<UpdateChallengesEventArgs> UpdateChallengesEvent(this ShowdownProtocolClient c) =>
            EventToTask<UpdateChallengesEventArgs>(h => c.UpdateChallenges += h, h => c.UpdateChallenges -= h);

        public static Task<QueryResponseEventArgs> QueryResponseEvent(this ShowdownProtocolClient c) =>
            EventToTask<QueryResponseEventArgs>(h => c.QueryResponse += h, h => c.QueryResponse -= h);

        public static Task<InitEventArgs> RoomInitEvent(this ShowdownProtocolClient c) =>
            EventToTask<InitEventArgs>(h => c.RoomInit += h, h => c.RoomInit -= h);

        public static Task<TitleEventArgs> RoomTitleEvent(this ShowdownProtocolClient c) =>
            EventToTask<TitleEventArgs>(h => c.RoomTitle += h, h => c.RoomTitle -= h);

        public static Task<UsersEventArgs> RoomUsersEvent(this ShowdownProtocolClient c) =>
            EventToTask<UsersEventArgs>(h => c.RoomUsers += h, h => c.RoomUsers -= h);

        public static Task<MessageEventArgs> RoomMessageEvent(this ShowdownProtocolClient c) =>
            EventToTask<MessageEventArgs>(h => c.RoomMessage += h, h => c.RoomMessage -= h);

        public static Task<HtmlEventArgs> RoomHtmlEvent(this ShowdownProtocolClient c) =>
            EventToTask<HtmlEventArgs>(h => c.RoomHtml += h, h => c.RoomHtml -= h);

        public static Task<UHtmlEventArgs> RoomUHtmlEvent(this ShowdownProtocolClient c) =>
            EventToTask<UHtmlEventArgs>(h => c.RoomUHtml += h, h => c.RoomUHtml -= h);

        public static Task<UHtmlChangeEventArgs> RoomUHtmlChangeEvent(this ShowdownProtocolClient c) =>
            EventToTask<UHtmlChangeEventArgs>(h => c.RoomUHtmlChange += h, h => c.RoomUHtmlChange -= h);

        public static Task<JoinEventArgs> RoomJoinEvent(this ShowdownProtocolClient c) =>
            EventToTask<JoinEventArgs>(h => c.RoomJoin += h, h => c.RoomJoin -= h);

        public static Task<LeaveEventArgs> RoomLeaveEvent(this ShowdownProtocolClient c) =>
            EventToTask<LeaveEventArgs>(h => c.RoomLeave += h, h => c.RoomLeave -= h);

        public static Task<NameEventArgs> RoomNameEvent(this ShowdownProtocolClient c) =>
            EventToTask<NameEventArgs>(h => c.RoomName += h, h => c.RoomName -= h);

        public static Task<ChatEventArgs> RoomChatEvent(this ShowdownProtocolClient c) =>
            EventToTask<ChatEventArgs>(h => c.RoomChat += h, h => c.RoomChat -= h);

        public static Task<NotifyEventArgs> RoomNotifyEvent(this ShowdownProtocolClient c) =>
            EventToTask<NotifyEventArgs>(h => c.RoomNotify += h, h => c.RoomNotify -= h);

        public static Task<ChatWithTimestampEventArgs> RoomChatWithTimestampEvent(this ShowdownProtocolClient c) =>
            EventToTask<ChatWithTimestampEventArgs>(
                h => c.RoomChatWithTimestamp += h, h => c.RoomChatWithTimestamp -= h);

        public static Task<TimestampEventArgs> RoomTimestampEvent(this ShowdownProtocolClient c) =>
            EventToTask<TimestampEventArgs>(h => c.RoomTimestamp += h, h => c.RoomTimestamp -= h);

        public static Task<BattleEventArgs> RoomBattleEvent(this ShowdownProtocolClient c) =>
            EventToTask<BattleEventArgs>(h => c.RoomBattle += h, h => c.RoomBattle -= h);

        public static Task<PlayerEventArgs> BattlePlayerEvent(this ShowdownProtocolClient c) =>
            EventToTask<PlayerEventArgs>(h => c.BattlePlayer += h, h => c.BattlePlayer -= h);

        public static Task<TeamSizeEventArgs> BattleTeamSizeEvent(this ShowdownProtocolClient c) =>
            EventToTask<TeamSizeEventArgs>(h => c.BattleTeamSize += h, h => c.BattleTeamSize -= h);

        public static Task<GameTypeEventArgs> BattleGameTypeEvent(this ShowdownProtocolClient c) =>
            EventToTask<GameTypeEventArgs>(h => c.BattleGameType += h, h => c.BattleGameType -= h);

        public static Task<GenEventArgs> BattleGenEvent(this ShowdownProtocolClient c) =>
            EventToTask<GenEventArgs>(h => c.BattleGen += h, h => c.BattleGen -= h);

        public static Task<TierEventArgs> BattleTierEvent(this ShowdownProtocolClient c) =>
            EventToTask<TierEventArgs>(h => c.BattleTier += h, h => c.BattleTier -= h);

        public static Task<RatedEventArgs> BattleRatedEvent(this ShowdownProtocolClient c) =>
            EventToTask<RatedEventArgs>(h => c.BattleRated += h, h => c.BattleRated -= h);

        public static Task<RatedInfoEventArgs> BattleRatedInfoEvent(this ShowdownProtocolClient c) =>
            EventToTask<RatedInfoEventArgs>(h => c.BattleRatedInfo += h, h => c.BattleRatedInfo -= h);

        public static Task<RuleEventArgs> BattleRuleEvent(this ShowdownProtocolClient c) =>
            EventToTask<RuleEventArgs>(h => c.BattleRule += h, h => c.BattleRule -= h);

        public static Task<ClearPokeEventArgs> BattleClearPokeEvent(this ShowdownProtocolClient c) =>
            EventToTask<ClearPokeEventArgs>(h => c.BattleClearPoke += h, h => c.BattleClearPoke -= h);

        public static Task<PokeEventArgs> BattlePokeEvent(this ShowdownProtocolClient c) =>
            EventToTask<PokeEventArgs>(h => c.BattlePoke += h, h => c.BattlePoke -= h);

        public static Task<TeamPreviewEventArgs> BattleTeamPreviewEvent(this ShowdownProtocolClient c) =>
            EventToTask<TeamPreviewEventArgs>(h => c.BattleTeamPreview += h, h => c.BattleTeamPreview -= h);

        public static Task<StartEventArgs> BattleStartEvent(this ShowdownProtocolClient c) =>
            EventToTask<StartEventArgs>(h => c.BattleStart += h, h => c.BattleStart -= h);

        public static Task<ClearMessageBarEventArgs> BattleClearMessageBarEvent(this ShowdownProtocolClient c) =>
            EventToTask<ClearMessageBarEventArgs>(h => c.BattleClearMessageBar += h, h => c.BattleClearMessageBar -= h);

        public static Task<RequestEventArgs> BattleRequestEvent(this ShowdownProtocolClient c) =>
            EventToTask<RequestEventArgs>(h => c.BattleRequest += h, h => c.BattleRequest -= h);

        public static Task<InactiveEventArgs> BattleInactiveEvent(this ShowdownProtocolClient c) =>
            EventToTask<InactiveEventArgs>(h => c.BattleInactive += h, h => c.BattleInactive -= h);

        public static Task<InactiveOffEventArgs> BattleInactiveOffEvent(this ShowdownProtocolClient c) =>
            EventToTask<InactiveOffEventArgs>(h => c.BattleInactiveOff += h, h => c.BattleInactiveOff -= h);

        public static Task<UpkeepEventArgs> BattleUpkeepEvent(this ShowdownProtocolClient c) =>
            EventToTask<UpkeepEventArgs>(h => c.BattleUpkeep += h, h => c.BattleUpkeep -= h);

        public static Task<TurnEventArgs> BattleTurnEvent(this ShowdownProtocolClient c) =>
            EventToTask<TurnEventArgs>(h => c.BattleTurn += h, h => c.BattleTurn -= h);

        public static Task<WinEventArgs> BattleWinEvent(this ShowdownProtocolClient c) =>
            EventToTask<WinEventArgs>(h => c.BattleWin += h, h => c.BattleWin -= h);

        public static Task<TieEventArgs> BattleTieEvent(this ShowdownProtocolClient c) =>
            EventToTask<TieEventArgs>(h => c.BattleTie += h, h => c.BattleTie -= h);

        public static Task<MoveEventArgs> BattleMoveEvent(this ShowdownProtocolClient c) =>
            EventToTask<MoveEventArgs>(h => c.BattleMove += h, h => c.BattleMove -= h);

        public static Task<SwitchEventArgs> BattleSwitchEvent(this ShowdownProtocolClient c) =>
            EventToTask<SwitchEventArgs>(h => c.BattleSwitch += h, h => c.BattleSwitch -= h);

        public static Task<DragEventArgs> BattleDragEvent(this ShowdownProtocolClient c) =>
            EventToTask<DragEventArgs>(h => c.BattleDrag += h, h => c.BattleDrag -= h);

        public static Task<DetailsChangeEventArgs> BattleDetailsChangeEvent(this ShowdownProtocolClient c) =>
            EventToTask<DetailsChangeEventArgs>(h => c.BattleDetailsChange += h, h => c.BattleDetailsChange -= h);

        public static Task<ReplaceEventArgs> BattleReplaceEvent(this ShowdownProtocolClient c) =>
            EventToTask<ReplaceEventArgs>(h => c.BattleReplace += h, h => c.BattleReplace -= h);

        public static Task<SwapEventArgs> BattleSwapEvent(this ShowdownProtocolClient c) =>
            EventToTask<SwapEventArgs>(h => c.BattleSwap += h, h => c.BattleSwap -= h);

        public static Task<CantEventArgs> BattleCantEvent(this ShowdownProtocolClient c) =>
            EventToTask<CantEventArgs>(h => c.BattleCant += h, h => c.BattleCant -= h);

        public static Task<FaintEventArgs> BattleFaintEvent(this ShowdownProtocolClient c) =>
            EventToTask<FaintEventArgs>(h => c.BattleFaint += h, h => c.BattleFaint -= h);
    }
}
