Imports Discord
Imports Discord.WebSocket
Imports Microsoft.Extensions.DependencyInjection

Namespace Services

    Public Class LogsService

        Private ReadOnly _client As DiscordSocketClient

        Public Sub New(services As IServiceProvider)
            _client = services.GetRequiredService(Of DiscordSocketClient)()
        End Sub

        Public Function Initialize() As Task
            AddHandler _client.Log, AddressOf OnLogAsync
            Return Task.CompletedTask
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private Shared Function OnLogAsync(log As LogMessage) As Task
            Console.WriteLine($"\u001b[97m[{Date.Now}]: [\u001b[93m{log.Source}\u001b[97m] => {log.Message}")
            Return Task.CompletedTask
        End Function

    End Class

End Namespace