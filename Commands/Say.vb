Imports Discord.Interactions
Imports Discord.WebSocket

Namespace Commands

    Public Class Say

        Inherits InteractionModuleBase(Of SocketInteractionContext(Of SocketInteraction))

        <SlashCommand("say", "Répéter un message.")>
        Public Async Function SayAsync(message As String) As Task
            Await DeferAsync()
            Await FollowupAsync(message, ephemeral:=True)
        End Function

    End Class

End Namespace