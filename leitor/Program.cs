using System.Text;
using System.Text.Json;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var mqttFactory = new MqttFactory();
        var client = mqttFactory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
        .WithClientId("teste2")
        .WithTcpServer("0.0.0.0", 1883)
        .WithCleanSession()
        .Build();
        client.UseConnectedHandler(async e =>
        {
            Console.WriteLine("conectado");
            var topicFilter = new TopicFilterBuilder()
            .WithTopic("trabalho")
            .Build();
            await client.SubscribeAsync(topicFilter);
        });
        client.UseDisconnectedHandler(e =>
        {
            Console.WriteLine("desconectado");
        });
        client.UseApplicationMessageReceivedHandler(async e =>
        {
            Console.WriteLine($"mensagem recebida");
            var mensagem = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            var url = $"https://interoperabilidade-json-9eaa9-default-rtdb.firebaseio.com/envios/envios.json";
            var httpClient = new HttpClient();
            Console.WriteLine("");
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(mensagem);
                var response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                Console.WriteLine("Dados enviados para o BaaS");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro");
            }
        });
        await client.ConnectAsync(options);
        Console.ReadLine();
        await client.DisconnectAsync();
    }
}