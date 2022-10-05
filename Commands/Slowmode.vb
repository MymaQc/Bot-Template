Imports Discord.Interactions
Imports Discord
Imports Discord.WebSocket
Imports BotTemplate.Types

Namespace Commands

    Public Class Slowmode

        Inherits InteractionModuleBase(Of SocketInteractionContext(Of SocketInteraction))

        Private _slowModeInterval As Integer

        <RequireBotPermission(ChannelPermission.ManageChannels)>
        <SlashCommand("slowmode", "Définir le slowmode.")>
        Public Async Function SlowmodeAsync(channel As ITextChannel, timeType As TimeType, value As Integer) As Task
            Await DeferAsync()
            _slowModeInterval = value * timeType

            If _slowModeInterval > 21600 Then
                Await FollowupAsync("Vous ne pouvez pas définir le mode ralenti à plus de 6 heures.")
                Return
            End If

            If channel.SlowModeInterval = 0 AndAlso timeType = TimeType.Aucun Then
                Await FollowupAsync("Le mode lent n'est pas activé dans ce salon.")
                Return
            End If

            Await channel.ModifyAsync(Function(textChannel)
                                          textChannel.SlowModeInterval = _slowModeInterval
                                      End Function)

            If timeType = TimeType.Aucun Then
                Await FollowupAsync($"Le mode lent a été retiré sur <#{channel.Id}> !")
            Else
                Await FollowupAsync($"Le mode lent a été défini sur <#{channel.Id}> à {value} {timeType.ToString().ToLower()}(s) !")
            End If
        End Function

    End Class

End Namespace