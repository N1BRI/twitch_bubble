#include <SoftwareSerial.h>

int mosfetPin = 2;
bool dataReceived = false; 

unsigned long previousMillis = 0;
unsigned long interval;

void setup() {
  Serial.begin(9600);
  pinMode(mosfetPin, OUTPUT);  
  digitalWrite(mosfetPin, LOW); 
}


void loop() {
  if (Serial.available() > 0) {
    String strInt = Serial.readStringUntil('\n');
    int converted_value_ms = strInt.toInt() * 1000; // Convert to milliseconds
    interval = converted_value_ms;

    digitalWrite(mosfetPin, HIGH);
    previousMillis = millis();
  }

  if (millis() - previousMillis >= interval) {
    digitalWrite(mosfetPin, LOW);
  }
}
