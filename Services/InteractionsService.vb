Imports Discord
Imports Discord.Interactions
Imports Microsoft.Extensions.DependencyInjection
Imports System.Reflection

Namespace Services

    Public NotInheritable Class InteractionsService

        Private ReadOnly _interaction As InteractionService
        Private ReadOnly _provider As IServiceProvider

        Public Sub New(services As IServiceProvider)
            _provider = services
            _interaction = _provider.GetRequiredService(Of InteractionService)()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Async Function Initialize() As Task
            Try
                Await _interaction.AddModulesAsync(Assembly.GetEntryAssembly(), _provider)

                For Each moduleInfo As ModuleInfo In _interaction.Modules
                    Console.WriteLine($"\u001b[97m[{Date.Now}]: [\u001b[93mMODULES\u001b[97m] => {moduleInfo.Name} \u001b[92mInstallé\u001b[97m")
                    AddHandler _interaction.Log, AddressOf OnInteractionLogAsync
                Next

            Catch ex As Exception
                Console.WriteLine($"\u001b[97m[{Date.Now}]: [\u001b[31mERREUR\u001b[97m] => Une erreur est survenue dans InteractionsService.cs \nINFO:\n{ex}")
            End Try
        End Function

        Private Shared Function OnInteractionLogAsync(log As LogMessage) As Task
            Console.WriteLine(log.Message)
            Return Task.CompletedTask
        End Function

    End Class

End Namespace