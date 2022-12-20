using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CSharp_Util.Test.MqttTestServer
{
    internal class MqttTest1
    {
        public static async void Run()
        {
            // 从页面UI上获取到下列参数
            var ip = "0.0.0.0";
            var port = 1883;
            var username = "";
            var password = "";
            var deviceIP = "128.128.12.22";
            var serverIP = "128.128.12.70";
            string[] topics = new string[] { "mqtt/sample/1", "mqtt/sample/2" };

            // 创建或连接MQTT Broker
            CreateMqttServer( ip, port, username, password);
            
            // 配置设备参数
            string retMsg2 = SetDeviceMqttConfig(deviceIP, serverIP, port, username, password);
            if ("成功".Equals(retMsg2))
            {
                string retMsg3 = GetDeviceMqttConfig(deviceIP);
                    if ("成功".Equals(retMsg3))
                    {
                        Console.WriteLine("设备MQTT参数设置成功");
                    }
                    else
                    {
                        Console.WriteLine($"失败：{retMsg3}");
                    }
            }
            else
            {
                Console.WriteLine($"失败：{retMsg2}");
            }

            // 开始订阅消息
            CreateMqttClient(ip, port, username, password);
            Thread.Sleep(5000);
            StartSubscribe(topics);

            // 发布消息
            StartPublish( "mqtt/sample/1", "xxxxData");
            StartPublish( "mqtt/xxx", "xxx");

            // 处理返回消息
            DealReceiveMessage("xxx");

            Console.ReadKey();
        }


        private static MqttServer mqttServer;
        /// <summary>
        /// 在当前电脑上创建MQTT Broker
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private static async void CreateMqttServer(string ip, int port, string username, string password)
        {
            var mqttFactory = new MqttFactory();
            var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().WithDefaultEndpointPort(port).Build();
            mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
            try {
                await mqttServer.StartAsync();
                Console.WriteLine("Server started.");
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 连接到指定的MQTT Broker
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private static void ConnectMqttServer(string ip, int port, string username, string password)
        {

        }
        /// <summary>
        /// 设置设备的MQTT参数
        /// </summary>
        /// <param name="deviceIP"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private static string SetDeviceMqttConfig(string deviceIP, string ip, int port, string username, string password)
        {
            var flag = 1;

            if (flag == 1)
                return "成功";
            else
                return "IP不在线或其他原因，请检查后重试";
        }
        /// <summary>
        /// 获得设备的MQTT参数
        /// </summary>
        /// <param name="deviceIP"></param>
        private static string GetDeviceMqttConfig(string deviceIP)
        {
            var flag = 1;

            if (flag == 1)
                return "成功";
            else
                return "赋值失败";
        }
        private static IMqttClient mqttClient;
        /// <summary>
        /// 创建一个MQTT客户端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async static void CreateMqttClient(string ip, int port, string username, string password)
        {
            var mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();
            try
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();

                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    Console.WriteLine("Received application message.");
                    var output = JsonSerializer.Serialize(e, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    Console.Write(e.GetType().Name+":\r\n"+output);

                    return Task.CompletedTask;
                };

                mqttClient.ApplicationMessageReceivedAsync += delegate (MqttApplicationMessageReceivedEventArgs args)
                {
                    // Do some work with the message...

                    // Now respond to the broker with a reason code other than success.
                    args.ReasonCode = MqttApplicationMessageReceivedReasonCode.ImplementationSpecificError;
                    args.ResponseReasonString = "That did not work!";

                    // Now the broker will resend the message again.
                    return Task.CompletedTask;
                };

                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                Console.WriteLine("The MQTT client is connected.");
            } catch(Exception e)
            {
                
            }
        }
        /// <summary>
        /// 开始订阅主题
        /// </summary>
        /// <param name="topics"></param>
        private static async void StartSubscribe(string[] topics)
        {
            var mqttFactory = new MqttFactory();
            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
               .WithTopicFilter(
                   f =>
                   {
                       //foreach(string topic in topics)
                       //{
                       f.WithTopic(topics[0]);
                       //}
                   })
               .Build();
            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            Console.WriteLine("MQTT client subscribed to topic.");
        }
        /// <summary>
        /// 发布一则消息
        /// </summary>
        /// <param name="mqttClient"></param>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        private async static void StartPublish(string topic, string payload)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            Console.WriteLine("MQTT application message is published.");
        }
        /// <summary>
        /// 处理接收到的信息
        /// </summary>
        /// <param name="xxx"></param>
        private static void DealReceiveMessage(string xxx)
        {
            Console.WriteLine($"处理消息：{xxx}");
        }






        /**
        public static TObject DumpToConsole<TObject>(this TObject @object)
        {
            var output = "NULL";
            if (@object != null)
            {
                output = JsonSerializer.Serialize(@object, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }

            Console.WriteLine($"[{@object?.GetType().Name}]:\r\n{output}");
            return @object;
        }
        **/
    }
}
