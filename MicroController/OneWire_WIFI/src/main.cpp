#include <Arduino.h>
#include <DallasTemperature.h>
#include <OneWire.h>
#include <WiFi.h>

// Onewire Bus
#define ONE_WIRE_BUS 15
#define SENSOR_RESOLUTION_BITS 10 // 9 - 12 Bits
OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature sensors(&oneWire);

// WIFI Connection
const char *ssid = "NAD";
const char *password = "30300275";
const char *host = "pi.potatosaucevfx.com";
const int port = 80;
const char *url = "/api/insert";

void postData(double tempF);    // Make function known

// Initialization
void setup()
{
    Serial.begin(115200);

    // Init Sensors
    sensors.begin();
    sensors.setResolution(SENSOR_RESOLUTION_BITS);
    Serial.println(String("Sensors Initialized...\nFound ") + sensors.getDeviceCount() + " device(s) on bus.");
    // End Init Sensors

    // Init WIFI
    Serial.println(String("Connecting to ") + ssid);
    WiFi.begin(ssid, password); // Connect to wifi
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(500);
        Serial.print(".");
    }
    // End Init WIFI
}

// System Loop
void loop()
{
    sensors.requestTemperatures();
    double tempF = sensors.getTempFByIndex(0);
    Serial.println(tempF + String(" °F"));
    postData(tempF);

    delay(600000);
}

void postData(double tempF)
{
    WiFiClient client;
    if (!client.connect(host, port))
    {
        Serial.println("connection failed");
        return;
    }

    // Post JSON object
    String PostData = "{\"tempF\": " + String(tempF) + "}";

    // Send request to server
    client.println(String("POST ") + url + " HTTP/1.1");
    client.println(String("Host: ") + host);
    client.println(String("Cache-Control: no-cache"));
    client.println(String("Content-Type: application/json; charset=utf-8"));
    client.print("Content-Length: ");
    client.println(PostData.length());
    client.println("Connection: close");
    client.println();
    client.println(PostData);

    // Timeout
    unsigned long timeout = millis();
    while (client.available() == 0)
    {
        if (millis() - timeout > 5000)
        {
            Serial.println(">>> Client Timeout !");
            client.stop();
            return;
        }
    }
    // Clear response
    client.flush();
}