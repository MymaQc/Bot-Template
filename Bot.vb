Imports BotTemplate.Services
Imports Discord
Imports Discord.Interactions
Imports Discord.WebSocket
Imports Microsoft.Extensions.DependencyInjection
Imports System.Threading

Module Bot

    Public Class Bot

        Private WithEvents Client As DiscordSocketClient
        Private WithEvents Interaction As InteractionService
        Private WithEvents Provider As IServiceProvider

        Private Sub New()
            Client = New DiscordSocketClient(New DiscordSocketConfig With {
                .LogLevel = LogSeverity.Debug,
                .UseInteractionSnowflakeDate = False,
                .MessageCacheSize = 1000
            })
            Interaction = New InteractionService(Client, New InteractionServiceConfig With {
                .LogLevel = LogSeverity.Debug,
                .DefaultRunMode = RunMode.Async,
                .ThrowOnError = True
            })
            Provider = BuildServiceProvider()
        End Sub

        <Obsolete>
        Sub Main()
            Run().GetAwaiter().GetResult()
        End Sub

        <Obsolete>
        Private Async Function Run() As Task
            Await New EventsService(Provider).Initialize()
            Await New InteractionsService(Provider).Initialize()
            Await New LogsService(Provider).Initialize()

            Await Client.LoginAsync(TokenType.Bot, "TOKEN")
            Await Client.StartAsync()
            Await Task.Delay(Timeout.Infinite)
        End Function

        Private Function BuildServiceProvider() As ServiceProvider
            Return New ServiceCollection().AddSingleton(Client).AddSingleton(Interaction).BuildServiceProvider()
        End Function

    End Class

End Module