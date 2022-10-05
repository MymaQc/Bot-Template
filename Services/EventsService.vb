Imports BotTemplate.Utils
Imports Discord
Imports Discord.Interactions
Imports Discord.WebSocket
Imports Microsoft.Extensions.DependencyInjection

Namespace Services

    Public Class EventsService

        Private ReadOnly _client As DiscordSocketClient
        Private ReadOnly _interaction As InteractionService
        Private ReadOnly _provider As IServiceProvider

        Public Sub New(services As IServiceProvider)
            _provider = services
            _client = services.GetRequiredService(Of DiscordSocketClient)()
            _interaction = services.GetRequiredService(Of InteractionService)()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        <Obsolete>
        Public Function Initialize() As Task
            AddHandler _client.Ready, AddressOf OnReadyAsync
            AddHandler _client.InteractionCreated, AddressOf OnInteractionCreatedAsync
            Return Task.CompletedTask
        End Function

        Private Async Function OnReadyAsync() As Task
            Try
                Await _client.SetGameAsync("Discord !", Nothing, ActivityType.Playing)
                Await _client.SetStatusAsync(UserStatus.DoNotDisturb)
                Await _interaction.RegisterCommandsGloballyAsync()
                Console.WriteLine(_client.CurrentUser.Username & " est en ligne !")
            Catch exception As Exception
                Console.WriteLine($"\u001b[97m[{Date.Now}]: [\u001b[31mERREUR\u001b[97m] => Une erreur est survenue dans EventsService.cs \nINFO:\n{exception}")
            End Try
        End Function

        <Obsolete>
        Private Async Function OnInteractionCreatedAsync(socketInteraction As SocketInteraction) As Task
            Dim component As SocketMessageComponent = Nothing
            Try
                If Implement.Assign(component, TryCast(socketInteraction, SocketMessageComponent)) IsNot Nothing Then
                    Dim context As New SocketInteractionContext(Of SocketMessageComponent)(_client, component)
                    Dim result As IResult = Await _interaction.ExecuteCommandAsync(context, _provider)

                    If Not result.IsSuccess Then
                        Console.WriteLine(result.ErrorReason)
                    End If
                Else
                    Dim context As New SocketInteractionContext(Of SocketInteraction)(_client, socketInteraction)
                    Dim result As IResult = Await _interaction.ExecuteCommandAsync(context, _provider)

                    If Not result.IsSuccess Then
                        Console.WriteLine(result.ErrorReason)
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine(ex)
            End Try
        End Function

    End Class

End Namespace