Imports Discord
Imports Discord.Interactions
Imports Discord.WebSocket

Namespace Commands

    Public Class Unmute

        Inherits InteractionModuleBase(Of SocketInteractionContext(Of SocketInteraction))

        <SlashCommand("unmute", "Rendre la parole à un utilisateur")>
        Public Async Function UnmuteAsync(guildUser As IGuildUser) As Task
            Await DeferAsync()

            If guildUser.TimedOutUntil Is Nothing Then
                Await FollowupAsync("Cet utilisateur n'est pas muet.")
                Return
            End If

            Await guildUser.RemoveTimeOutAsync(RequestOptions.[Default])
            Await FollowupAsync($"{guildUser.Id} a retrouvé la parole grâce à {Context.User.Id} !")
        End Function

    End Class

End Namespace