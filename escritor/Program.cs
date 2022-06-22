using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Esse é o escritor, aqui você digita algo, para o projeto leitor enviar para o BaaS");
        var mqttFactory = new MqttFactory();
        var client = mqttFactory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
        .WithClientId("teste1")
        .WithTcpServer("0.0.0.0", 1883)
        .WithCleanSession()
        .Build();
        client.UseConnectedHandler(e =>
        {
            Console.WriteLine("conectado");
        });
        client.UseDisconnectedHandler(e =>
        {
            Console.WriteLine("desconectado");
        });
        await client.ConnectAsync(options);
        Console.WriteLine("digite algo para ser enviado para o BaaS");
        var mensagem = Console.ReadLine();
        Publicar(client, mensagem);
        await client.DisconnectAsync();
    }

    private static async Task Publicar(IMqttClient client, string mensagem)
    {
        string messagePayload = mensagem;
        var message = new MqttApplicationMessageBuilder()
        .WithTopic("trabalho")
        .WithPayload(messagePayload)
        .WithAtLeastOnceQoS()
        .Build();
        if (client.IsConnected)
        {
            await client.PublishAsync(message);
        }
    }
}