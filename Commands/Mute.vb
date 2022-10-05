Imports BotTemplate.Types
Imports Discord
Imports Discord.Interactions
Imports Discord.WebSocket

Namespace Commands

    Public Class Mute

        Inherits InteractionModuleBase(Of SocketInteractionContext(Of SocketInteraction))

        Private _time As Integer
        Private _timeSpan As TimeSpan

        <SlashCommand("mute", "Rendre un utilisateur muet.")>
        Public Async Function MuteAsync(guildUser As IGuildUser, timeType As TimeType, value As Integer) As Task
            Await DeferAsync()
            _time = value * timeType

            If _time > 2419200 Then
                Await FollowupAsync("Vous ne pouvez pas rendre muet un utilisateur plus de 4 semaines.")
                Return
            End If

            If guildUser.IsBot Then
                Await FollowupAsync("Vous ne pouvez pas réduire au silence un bot.")
                Return
            End If

            If guildUser.TimedOutUntil IsNot Nothing Then
                Await FollowupAsync("Cet utilisateur est déjà réduit au silence.")
                Return
            End If

            _timeSpan = TimeSpan.FromSeconds(_time)
            Await guildUser.SetTimeOutAsync(_timeSpan)
            Await FollowupAsync($"{guildUser.Id} a été réduit au silence pendant {value.ToString().Replace("0"c, " "c)} {timeType.ToString().ToLower()}(s) par {Context.User.Id} !")
        End Function

    End Class

End Namespace